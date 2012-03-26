namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a chunk of map, referred to as a patch.
    /// </summary>
    public sealed class TerrainPatch : ICloneable, IDisposable
    {
        /// <summary>
        /// Index buffer for the terrain patch.
        /// </summary>
        private DynamicIndexBuffer indexBuffer;

        /// <summary>
        /// Vertex buffer for the terrain patch.
        /// </summary>
        private DynamicVertexBuffer vertexBuffer;

        /// <summary>
        /// The indices of the terrain patch.
        /// </summary>
        private int[] indexes;

        /// <summary>
        /// The vertices of the terrain patch.
        /// </summary>
        private VertexPositionNormalMultipleTexture[] vertices;

        /// <summary>
        /// The vertices of the terrain patch as an array of Vector3 objects.
        /// </summary>
        private Vector3[] verticesPositions = new Vector3[TerrainPatch.PatchSizeP1 * TerrainPatch.PatchSizeP1];

        /// <summary>
        /// The heights of the vertices.
        /// </summary>
        private float[] vertexHeightsArray = new float[TerrainPatch.PatchSizeP1 * TerrainPatch.PatchSizeP1];

        /// <summary>
        /// The textures of the vertices. The first index specifies the season, and the second index specifies the terrain type.
        /// </summary>
        private TerrainType[][] vertexTexturesArray = new TerrainType[][]
        {
            new TerrainType[TerrainPatch.PatchSizeP1 * TerrainPatch.PatchSizeP1],
            new TerrainType[TerrainPatch.PatchSizeP1 * TerrainPatch.PatchSizeP1],
            new TerrainType[TerrainPatch.PatchSizeP1 * TerrainPatch.PatchSizeP1]
        };

        /// <summary>
        /// Whether the vertex buffer needs to be updated on the next call to draw.
        /// </summary>
        private bool vertexBufferDirty;

        /// <summary>
        /// Initializes a new instance of the TerrainPatch class.
        /// </summary>
        /// <param name="patchId">The ID of the terrain patch.</param>
        public TerrainPatch(Point3D patchId)
        {
            this.PatchId = patchId;
            this.Fluids = new Collection<Fluid>();
            this.WorldMatrix = Matrix.CreateTranslation(this.PatchId.X * TerrainPatch.PatchSize, this.PatchId.Y * TerrainPatch.PatchSize, this.PatchId.Z * TerrainManager.HeightLevelDifference);
        }

        /// <summary>
        /// Initializes a new instance of the TerrainPatch class.
        /// </summary>
        /// <param name="patchId">The ID of the terrain patch.</param>
        /// <param name="defaultHeight">The default height of the patch.</param>
        /// <param name="defaultTerrain">The default terrain type of the patch.</param>
        public TerrainPatch(Point3D patchId, float defaultHeight, TerrainType defaultTerrain)
            : this(patchId)
        {
            for (int x = 0; x <= TerrainPatch.PatchSize; x++)
            {
                for (int y = 0; y <= TerrainPatch.PatchSize; y++)
                {
                    Point point = new Point(x, y);
                    this.SetElevationDataInternal(point, defaultHeight);
                    this.SetTerrainTypeDataInternal(point, defaultTerrain);
                }
            }

            this.BoundingBox = BoundingBox.CreateFromPoints(this.verticesPositions);
        }

        /// <summary>
        /// Gets the width (x) or depth (y) of a terrain patch.
        /// </summary>
        /// <value>See summary.</value>
        public static int PatchSize
        {
            get { return 128; }
        }

        /// <summary>
        /// Gets the season this terrain patch was loaded with.
        /// </summary>
        /// <value>See summary.</value>
        public Season LoadedSeason
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether to render base season terrain as undefined.
        /// This is the value that the terrain patch was loaded with and cannot be
        /// changed; see <see cref="TerrainManager.HideBaseSeasonTerrain"/>.
        /// </summary>
        /// <value>See summary.</value>
        public bool LoadedHideBaseSeasonTerrain
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ID of the terrain patch.
        /// </summary>
        /// <value>See summary.</value>
        public Point3D PatchId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the world transformation matrix of the terrain patch.
        /// </summary>
        /// <value>See summary.</value>
        public Matrix WorldMatrix
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the terrain patch's bounding box.
        /// </summary>
        /// <value>See summary.</value>
        public BoundingBox BoundingBox
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the 3D cursor used to manipulate the terrain.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainCursor Cursor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the fluids to be rendered along with this terrain patch.
        /// </summary>
        /// <value>See summary.</value>
        public Collection<Fluid> Fluids
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the width (x) or depth (y) of a terrain patch, plus 1.
        /// Used for internal purposes.
        /// </summary>
        /// <value>See summary.</value>
        private static int PatchSizeP1
        {
            get { return TerrainPatch.PatchSize + 1; }
        }

        /// <summary>
        /// Creates a new instance of the TerrainPatch class using all the properties
        /// of an existing TerrainPatch but with a different patch ID.
        /// </summary>
        /// <param name="patchId">The ID of the terrain patch.</param>
        /// <param name="terrainPatch">The terrain patch whose properties to use.</param>
        /// <returns>See summary.</returns>
        public static TerrainPatch FromExisting(Point3D patchId, TerrainPatch terrainPatch)
        {
            TerrainPatch pat = terrainPatch.Clone() as TerrainPatch;
            pat.PatchId = patchId;

            return pat;
        }

        /// <summary>
        /// Generates a terrain patch from an array of bytes; returns null if the data is invalid.
        /// </summary>
        /// <param name="data">The array of bytes to generate the patch from.</param>
        /// <returns>See summary.</returns>
        public static TerrainPatch FromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            else
            {
                ReadOnlyCollection<Season> seasons = SeasonExtensions.GetSeasons();

                BinaryReader reader = new BinaryReader(new MemoryStream(data));

                try
                {
                    // Read the patch ID
                    int x = reader.ReadInt32();
                    int y = reader.ReadInt32();
                    int z = reader.ReadInt32();

                    // Create a patch object
                    TerrainPatch patch = new TerrainPatch(new Point3D(x, y, z));

                    // Read the array of heights
                    for (int i = 0; i < patch.vertexHeightsArray.Length; i++)
                    {
                        patch.vertexHeightsArray[i] = reader.ReadSingle();
                    }

                    // Read terrain types for all seasons
                    for (int j = 0; j < seasons.Count; j++)
                    {
                        // Read the array of textures
                        for (int i = 0; i < patch.vertexTexturesArray[j].Length; i++)
                        {
                            patch.vertexTexturesArray[(int)seasons[j]][i] = (TerrainType)reader.ReadInt16();
                        }
                    }

                    // Read the number of fluids
                    short countFluids = reader.ReadInt16();

                    // Read all the fluids
                    for (int i = 0; i < countFluids; i++)
                    {
                        // Create a new fluid object to store on the patch
                        Fluid fluid = new Fluid(patch.PatchId);

                        // Read fluid properties for all seasons
                        for (int j = 0; j < seasons.Count; j++)
                        {
                            // Read the vertices
                            Vector3 southWest = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                            Vector3 southEast = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                            Vector3 northWest = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                            Vector3 northEast = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                            // Read the fluid type
                            short fluidType = reader.ReadInt16();

                            // Read the fluid flow direction
                            short flowDirection = reader.ReadInt16();

                            // Read the fluid flow speed
                            float flowSpeed = reader.ReadSingle();

                            // Copy the read variables into the properties of the fluid object
                            fluid.SetPoint(seasons[j], FluidVertex.Southwest, southWest);
                            fluid.SetPoint(seasons[j], FluidVertex.Southeast, southEast);
                            fluid.SetPoint(seasons[j], FluidVertex.Northwest, northWest);
                            fluid.SetPoint(seasons[j], FluidVertex.Northeast, northEast);
                            fluid.SetFluidType(seasons[j], (FluidType)fluidType);
                            fluid.SetFlowDirection(seasons[j], (FluidFlowDirection)flowDirection);
                            fluid.SetFlowSpeed(seasons[j], flowSpeed);
                        }

                        // Add the fluid to the patch
                        patch.Fluids.Add(fluid);
                    }

                    return patch;
                }
                catch (EndOfStreamException)
                {
                    return null;
                }
                catch (IOException)
                {
                    return null;
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Creates a copy of the TerrainPatch, not including dynamic
        /// objects like buffers and vertex and index arrays.
        /// </summary>
        /// <returns>See summary.</returns>
        public object Clone()
        {
            return TerrainPatch.FromByteArray(this.ToByteArray());
        }

        /// <summary>
        /// Gets the indices of the terrain patch.
        /// </summary>
        /// <returns>See summary.</returns>
        public int[] GetIndexes()
        {
            return this.indexes.Clone() as int[];
        }

        /// <summary>
        /// Gets the vertices of the terrain patch.
        /// </summary>
        /// <returns>See summary.</returns>
        public Vector3[] GetVertices()
        {
            return this.verticesPositions.Clone() as Vector3[];
        }

        /// <summary>
        /// Initializes the geometry by loading the vertex and index buffers.
        /// </summary>
        public void InitializeGeometry()
        {
            this.LoadedSeason = EngineManager.Engine.Terrain.CurrentSeason;
            this.LoadedHideBaseSeasonTerrain = EngineManager.Engine.Terrain.HideBaseSeasonTerrain;

            this.LoadVertices();
            this.BoundingBox = BoundingBox.CreateFromPoints(this.verticesPositions);
            this.LoadIndices();
            this.CalculateNormals();
            this.CreateBuffers();
        }

        /// <summary>
        /// Writes the terrain patch to a byte array.
        /// </summary>
        /// <returns>See summary.</returns>
        public byte[] ToByteArray()
        {
            ReadOnlyCollection<Season> seasons = SeasonExtensions.GetSeasons();

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            // Write the terrain patch ID
            writer.Write(this.PatchId.X);
            writer.Write(this.PatchId.Y);
            writer.Write(this.PatchId.Z);

            // Write the array of heights
            for (int i = 0; i < this.vertexHeightsArray.Length; i++)
            {
                writer.Write(this.vertexHeightsArray[i]);
            }

            // Write terrain type for all seasons
            for (int j = 0; j < seasons.Count; j++)
            {
                // Write the array of textures
                for (int i = 0; i < this.vertexTexturesArray[j].Length; i++)
                {
                    writer.Write((short)this.vertexTexturesArray[(int)seasons[j]][i]);
                }
            }

            // Write the number of fluids
            writer.Write((short)this.Fluids.Count);

            // Write fluid properties for all seasons
            for (int j = 0; j < seasons.Count; j++)
            {
                // Write each fluid's properties
                for (int i = 0; i < this.Fluids.Count; i++)
                {
                    // Vertex 0 position
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Southwest).X);
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Southwest).Y);
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Southwest).Z);

                    // Vertex 1 position
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Southeast).X);
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Southeast).Y);
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Southeast).Z);

                    // Vertex 2 position
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Northwest).X);
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Northwest).Y);
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Northwest).Z);

                    // Vertex 3 position
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Northeast).X);
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Northeast).Y);
                    writer.Write(this.Fluids[i].GetPoint(seasons[j], FluidVertex.Northeast).Z);

                    // Type of fluid
                    writer.Write((short)this.Fluids[i].GetFluidType(seasons[j]));

                    // Direction of fluid flow
                    writer.Write((short)this.Fluids[i].GetFlowDirection(seasons[j]));

                    // Speed of fluid flow
                    writer.Write(this.Fluids[i].GetFlowSpeed(seasons[j]));
                }
            }

            writer.BaseStream.Position = 0;
            byte[] bytes = new byte[writer.BaseStream.Length];
            writer.BaseStream.Read(bytes, 0, bytes.Length);

            return bytes;
        }

        /// <summary>
        /// Synchronizes the terrain patch by making the properties of the edges of adjacent
        /// patches equal to the properties of this one.
        /// Returns the collection of terrain patches affected. This allows them to be saved to the desired location.
        /// Only affected, non-null patches are included.
        /// </summary>
        /// <returns>See summary.</returns>
        public ReadOnlyCollection<TerrainPatch> GlueEdges()
        {
            TerrainPatch patch = this;
            Collection<TerrainPatch> affectedPatches = new Collection<TerrainPatch>();

            for (int x = 0; x < TerrainPatch.PatchSize; x++)
            {
                Point loc = new Point(x, 0);

                ReadOnlyCollection<TerrainPatch> pat1 = patch.SetElevationData(loc, patch.GetElevationData(loc.X, loc.Y));
                ReadOnlyCollection<TerrainPatch> pat2 = patch.SetTerrainTypeData(loc, patch.GetTerrainTypeData(loc.X, loc.Y));

                for (int i = 0; i < pat1.Count; i++)
                {
                    if (!affectedPatches.Contains(pat1[i]))
                    {
                        affectedPatches.Add(pat1[i]);
                    }
                }

                for (int i = 0; i < pat2.Count; i++)
                {
                    if (!affectedPatches.Contains(pat2[i]))
                    {
                        affectedPatches.Add(pat2[i]);
                    }
                }
            }

            for (int y = 0; y < TerrainPatch.PatchSize; y++)
            {
                Point loc = new Point(0, y);

                ReadOnlyCollection<TerrainPatch> pat1 = patch.SetElevationData(loc, patch.GetElevationData(loc.X, loc.Y));
                ReadOnlyCollection<TerrainPatch> pat2 = patch.SetTerrainTypeData(loc, patch.GetTerrainTypeData(loc.X, loc.Y));

                for (int i = 0; i < pat1.Count; i++)
                {
                    if (!affectedPatches.Contains(pat1[i]))
                    {
                        affectedPatches.Add(pat1[i]);
                    }
                }

                for (int i = 0; i < pat2.Count; i++)
                {
                    if (!affectedPatches.Contains(pat2[i]))
                    {
                        affectedPatches.Add(pat2[i]);
                    }
                }
            }

            // We don't need to save this patch because nothing was changed, but return the affected ones
            return new ReadOnlyCollection<TerrainPatch>(affectedPatches);
        }

        /// <summary>
        /// Gets the exact elevation of any point on the terrain patch from a set of local coordinates.
        /// </summary>
        /// <param name="localX">The local X coordinate of the point to find the elevation of.</param>
        /// <param name="localY">The local Y coordinate of the point to find the elevation of.</param>
        /// <returns>See summary.</returns>
        public float GetExactElevation(int localX, int localY)
        {
            return this.GetExactElevation(new Vector2(localX, localY));
        }

        /// <summary>
        /// Gets the exact elevation of any point on the terrain patch from a set of local coordinates.
        /// </summary>
        /// <param name="location">The local location of the point to find the elevation of.</param>
        /// <returns>See summary.</returns>
        public float GetExactElevation(Vector2 location)
        {
            return MathExtensions.GetExactHeight(location, this.vertexHeightsArray);
        }

        /// <summary>
        /// Gets the elevation at any vertex/point on the terrain patch from a set of local coordinates.
        /// </summary>
        /// <param name="localX">The local X coordinate of the location to get the elevation of.</param>
        /// <param name="localY">The local Y coordinate of the location to get the elevation of.</param>
        /// <returns>See summary.</returns>
        public float GetElevationData(int localX, int localY)
        {
            return this.GetElevationData(new Vector2(localX, localY));
        }

        /// <summary>
        /// Gets the elevation at any vertex/point on the terrain patch from a set of local coordinates.
        /// </summary>
        /// <param name="location">The local location to get the elevation of.</param>
        /// <returns>See summary.</returns>
        public float GetElevationData(Vector2 location)
        {
            return this.vertexHeightsArray[TerrainPatch.GetIndex(location)];
        }

        /// <summary>
        /// Sets the elevation at any vertex/point on the terrain patch with a set of local coordinates.
        /// Returns a collection of the affected terrain patches, not including this one.
        /// </summary>
        /// <param name="location">The local location to set the elevation of.</param>
        /// <param name="height">The elevation value to set.</param>
        /// <returns>See summary.</returns>
        public ReadOnlyCollection<TerrainPatch> SetElevationData(Vector2 location, float height)
        {
            return this.SetElevationData(new Point((int)location.X, (int)location.Y), height);
        }

        /// <summary>
        /// Sets the elevation at any vertex/point on the terrain patch with a set of local coordinates.
        /// Returns a collection of the affected terrain patches.
        /// </summary>
        /// <param name="location">The local location to set the elevation of.</param>
        /// <param name="height">The elevation value to set.</param>
        /// <returns>See summary.</returns>
        public ReadOnlyCollection<TerrainPatch> SetElevationData(Point location, float height)
        {
            return TerrainPatch.SynchronizeCoordinates(CoordinateConverter.LocalToVertex(this.PatchId, location), height, null);
        }

        /// <summary>
        /// Gets the actual terrain type at any vertex/point on the terrain patch from a set of local coordinates.
        /// </summary>
        /// <param name="localX">The local X coordinate of the location to get the terrain type of.</param>
        /// <param name="localY">The local Y coordinate of the location to get the terrain type of.</param>
        /// <returns>See summary.</returns>
        public TerrainType GetTerrainTypeData(int localX, int localY)
        {
            return this.GetTerrainTypeData(new Vector2(localX, localY));
        }

        /// <summary>
        /// Gets the actual terrain type at any vertex/point on the terrain patch from a set of local coordinates.
        /// </summary>
        /// <param name="location">The local location to get the terrain type of.</param>
        /// <returns>See summary.</returns>
        public TerrainType GetTerrainTypeData(Vector2 location)
        {
            return this.GetTerrainTypeDataForSeason(location, EngineManager.Engine.Terrain.CurrentSeason);
        }

        /// <summary>
        /// Gets the terrain type at any vertex/point on the terrain patch from a set of local coordinates, in the particular season.
        /// </summary>
        /// <param name="location">The local location to get the terrain type of.</param>
        /// <param name="season">The season to get the terrain type in.</param>
        /// <param name="hideBase">
        /// Whether to get undefined for non-base season
        /// instead of showing the base season terrain.
        /// The default overload is the engine's current state.
        /// </param>
        /// <returns>See summary.</returns>
        public TerrainType GetTerrainTypeDataForSeason(Vector2 location, Season season, bool hideBase)
        {
            // The terrain type of the season; this can be anything
            TerrainType seasonTerrainType = (TerrainType)this.vertexTexturesArray[(int)season][TerrainPatch.GetIndex(location)];

            // If we're hiding the base layer (e.g. we want undefined to show AS undefined),
            // return the season's actual terrain type
            if (hideBase)
            {
                return seasonTerrainType;
            }
            else
            {
                // Otherwise return the midseason type if this season's type is undefined,
                // or the season itself's type if it is not undefined
                if (season != Season.Midseason && seasonTerrainType == TerrainType.UndefinedTerrain)
                {
                    return (TerrainType)this.vertexTexturesArray[(int)Season.Midseason][TerrainPatch.GetIndex(location)];
                }
                else
                {
                    return seasonTerrainType;
                }
            }
        }

        /// <summary>
        /// Gets the terrain type at any vertex/point on the terrain patch from a set of local coordinates, in the particular season.
        /// </summary>
        /// <param name="location">The local location to get the terrain type of.</param>
        /// <param name="season">The season to get the terrain type in.</param>
        /// <returns>See summary.</returns>
        public TerrainType GetTerrainTypeDataForSeason(Vector2 location, Season season)
        {
            return this.GetTerrainTypeDataForSeason(location, season, EngineManager.Engine.Terrain.HideBaseSeasonTerrain);
        }

        /// <summary>
        /// Gets the terrain type at any vertex/point on the terrain patch from a set of local coordinates, in the particular season.
        /// </summary>
        /// <param name="localX">The local X coordinate of the location to get the terrain type of.</param>
        /// <param name="localY">The local Y coordinate of the location to get the terrain type of.</param>
        /// <param name="season">The season to get the terrain type in.</param>
        /// <returns>See summary.</returns>
        public TerrainType GetTerrainTypeDataForSeason(int localX, int localY, Season season)
        {
            return (TerrainType)this.vertexTexturesArray[(int)season][TerrainPatch.GetIndex(new Vector2(localX, localY))];
        }

        /// <summary>
        /// Sets the terrain type at any vertex/point on the terrain patch with a set of local coordinates.
        /// Returns a collection of the affected terrain patches, not including this one.
        /// </summary>
        /// <param name="location">The local location to set the terrain type of.</param>
        /// <param name="terrainType">The terrain type value to set.</param>
        /// <returns>See summary.</returns>
        public ReadOnlyCollection<TerrainPatch> SetTerrainTypeData(Vector2 location, TerrainType terrainType)
        {
            return this.SetTerrainTypeData(new Point((int)location.X, (int)location.Y), terrainType);
        }

        /// <summary>
        /// Sets the terrain type at any vertex/point on the terrain patch with a set of local coordinates.
        /// Returns a collection of the affected terrain patches, not including this one.
        /// </summary>
        /// <param name="location">The local location to set the terrain type of.</param>
        /// <param name="terrainType">The terrain type value to set.</param>
        /// <returns>See summary.</returns>
        public ReadOnlyCollection<TerrainPatch> SetTerrainTypeData(Point location, TerrainType terrainType)
        {
            return TerrainPatch.SynchronizeCoordinates(CoordinateConverter.LocalToVertex(this.PatchId, location), null, terrainType);
        }

        /// <summary>
        /// Draws the terrain patch at its proper location and returns the number of draw calls used.
        /// </summary>
        /// <param name="terrainEffect">The effect used to draw the ground in this terrain patch.</param>
        /// <returns>See summary.</returns>
        public int Draw(MultipleTextureTerrainEffect terrainEffect)
        {
            // Ensure that the vertices, indices and buffers have been created
            if (this.vertices == null || this.indexes == null ||
                this.vertexBuffer == null || this.indexBuffer == null)
            {
                this.InitializeGeometry();
            }

            if (this.vertexBufferDirty && this.vertexBuffer != null)
            {
                this.vertexBuffer.SetData<VertexPositionNormalMultipleTexture>(this.vertices);
            }

            if (this.Cursor != null)
            {
                // Just so it's not null...
                terrainEffect.ShowCursor = this.Cursor.Show;
                terrainEffect.CursorPosition = MathExtensions.TruncateVector(this.Cursor.Position);
                terrainEffect.CursorSize = this.Cursor.Size;
            }

            EngineManager.Engine.SetDefaultRenderState();

            terrainEffect.CommitChanges();

            EngineManager.Engine.GraphicsDevice.Vertices[0].SetSource(this.vertexBuffer, 0, VertexPositionNormalMultipleTexture.SizeInBytes);
            EngineManager.Engine.GraphicsDevice.Indices = this.indexBuffer;
            EngineManager.Engine.GraphicsDevice.VertexDeclaration = new VertexDeclaration(EngineManager.Engine.GraphicsDevice, VertexPositionNormalMultipleTexture.GetVertexElements());
            
            // The number of times draw was called
            int drawCalls = 0;

            // The total number of triangles we will draw
            int totalTriangles = this.indexes.Length / 3;

            // The total triangles we have drawn - this is for ending the loop
            int totalTrianglesDrawn = 0;

            // The current triangle we're at
            int currentTriangle = 0;

            // Loop until we have drawn all the triangles
            while (totalTrianglesDrawn < totalTriangles)
            {
                // Number of triangles to draw in this batch
                int trianglesToDraw = 0;

                // Triangle to start drawing at
                int startTriangle = currentTriangle;

                // Loop from the current triangle to the total number of triangles
                for (; currentTriangle < totalTriangles; currentTriangle++)
                {
                    trianglesToDraw++;
                    totalTrianglesDrawn++;

                    if (totalTrianglesDrawn >= totalTriangles)
                    {
                        break;
                    }
                    
                    if (this.vertices[this.indexes[currentTriangle * 3]].TerrainType != this.vertices[this.indexes[(currentTriangle + 1) * 3]].TerrainType)
                    {
                        currentTriangle++;
                        break;
                    }
                }

                terrainEffect.Texture = this.vertices[this.indexes[startTriangle * 3]].TerrainType.GetTexture();
                terrainEffect.CommitChanges();

                EngineManager.Engine.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.vertices.Length, startTriangle * 3, trianglesToDraw);

                drawCalls++;
            }

            return drawCalls;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the index in a one-dimensional array, from an X and X coordinate.
        /// </summary>
        /// <param name="location">The Vector2 containing the X and Y coordinates.</param>
        /// <returns>See summary.</returns>
        private static int GetIndex(Vector2 location)
        {
            return TerrainPatch.GetIndex((int)location.X, (int)location.Y);
        }

        /// <summary>
        /// Gets the index in a one-dimensional array, from an X and X coordinate.
        /// </summary>
        /// <param name="location">The Point containing the X and Y coordinates.</param>
        /// <returns>See summary.</returns>
        private static int GetIndex(Point location)
        {
            return TerrainPatch.GetIndex(location.X, location.Y);
        }

        /// <summary>
        /// Gets the index in a one-dimensional array, from an X and Y coordinate.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>See summary.</returns>
        private static int GetIndex(int x, int y)
        {
            return MathExtensions.GetIndex(x, y, TerrainPatch.PatchSizeP1);
        }

        /// <summary>
        /// Synchronizes the height and terrain type of the vertices in the specified collection.
        /// Returns the collection of terrain patches affected.
        /// This allows them to be saved to the desired location.
        /// Only affected, non-null patches are included.
        /// </summary>
        /// <param name="vertexCoordinates">The vertex coordinates to be set.</param>
        /// <param name="height">The height, or null if it should not be set.</param>
        /// <param name="type">The terrain type, or null if it should not be set.</param>
        /// <returns>See summary.</returns>
        private static ReadOnlyCollection<TerrainPatch> SynchronizeCoordinates(ReadOnlyCollection<TerrainPatchVertexCoordinates> vertexCoordinates, float? height, TerrainType? type)
        {
            Collection<TerrainPatch> returnedPatches = new Collection<TerrainPatch>();

            // DO NOT start at zero (this is the patch we are starting from),
            // or there will be an infinite loop - this method is only called
            // from set height data both if the patch is loaded AND there was
            // more than one coordinate in the collection
            for (int i = 0; i < vertexCoordinates.Count; i++)
            {
                Point3D patchID = vertexCoordinates[i].PatchId;
                Point vertexCoord = vertexCoordinates[i].VertexCoordinate;

                TerrainPatch loadedPatch = EngineManager.Engine.Terrain.GetTerrainPatch(patchID);
                if (loadedPatch != null)
                {
                    if (height.HasValue)
                    {
                        loadedPatch.SetElevationDataInternal(vertexCoord, height.Value);
                    }

                    if (type.HasValue)
                    {
                        loadedPatch.SetTerrainTypeDataInternal(vertexCoord, type.Value);
                    }

                    returnedPatches.Add(loadedPatch);
                }
            }

            return new ReadOnlyCollection<TerrainPatch>(returnedPatches);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.vertexBuffer.Dispose();
                this.indexBuffer.Dispose();
            }

            this.indexes = null;
            this.vertices = null;
            this.verticesPositions = null;
            this.vertexHeightsArray = null;
            this.vertexTexturesArray = null;
        }

        /// <summary>
        /// Loads the vertices of the terrain patch.
        /// </summary>
        private void LoadVertices()
        {
            this.vertices = new VertexPositionNormalMultipleTexture[TerrainPatch.PatchSizeP1 * TerrainPatch.PatchSizeP1];

            for (int x = 0; x < TerrainPatch.PatchSizeP1; x++)
            {
                for (int y = 0; y < TerrainPatch.PatchSizeP1; y++)
                {
                    int index = x + (y * TerrainPatch.PatchSizeP1);

                    Vector3 pos = new Vector3(x, y, this.vertexHeightsArray[TerrainPatch.GetIndex(x, y)]);

                    this.verticesPositions[index] = pos;

                    this.vertices[index].Position = pos;
                    this.vertices[index].Normal = Vector3.Zero;
                    this.vertices[index].TextureCoordinate = new Vector2(x / TerrainManager.TextureFrequency, y / TerrainManager.TextureFrequency);
                    this.vertices[index].TerrainType = this.LoadedHideBaseSeasonTerrain ? this.GetTerrainTypeDataForSeason(x, y, this.LoadedSeason) : this.GetTerrainTypeData(x, y);
                }
            }
        }

        /// <summary>
        /// Loads the indices of the terrain patch.
        /// </summary>
        private void LoadIndices()
        {
            this.indexes = new int[TerrainPatch.PatchSize * TerrainPatch.PatchSize * 6];

            for (int x = 0; x < TerrainPatch.PatchSize; x++)
            {
                for (int y = 0; y < TerrainPatch.PatchSize; y++)
                {
                    int baseIndex = (x + (y * TerrainPatch.PatchSize)) * 6;

                    int lowerLeft = x + (y * TerrainPatch.PatchSizeP1);
                    int lowerRight = (x + 1) + (y * TerrainPatch.PatchSizeP1);
                    int topLeft = x + ((y + 1) * TerrainPatch.PatchSizeP1);
                    int topRight = (x + 1) + ((y + 1) * TerrainPatch.PatchSizeP1);

                    this.indexes[baseIndex + 0] = topLeft; // used to be topRight
                    this.indexes[baseIndex + 1] = lowerRight;
                    this.indexes[baseIndex + 2] = lowerLeft;
                    this.indexes[baseIndex + 3] = topLeft; // used to be topRight
                    this.indexes[baseIndex + 4] = topRight; // used to be lowerLeft
                    this.indexes[baseIndex + 5] = lowerRight;
                }
            }
        }

        /// <summary>
        /// Calculates the normals of the terrain patch.
        /// </summary>
        private void CalculateNormals()
        {
            for (int i = 0; i < this.indexes.Length / 3; i++)
            {
                int index1 = this.indexes[(i * 3) + 0];
                int index2 = this.indexes[(i * 3) + 1];
                int index3 = this.indexes[(i * 3) + 2];

                Vector3 normal = MathExtensions.GetNormal(this.vertices[index1].Position, this.vertices[index2].Position, this.vertices[index3].Position);

                this.vertices[index1].Normal = normal;
                this.vertices[index2].Normal = normal;
                this.vertices[index3].Normal = normal;
            }
        }

        /// <summary>
        /// Copies the terrain patch vertices and indices into buffers.
        /// </summary>
        private void CreateBuffers()
        {
            this.vertexBuffer = new DynamicVertexBuffer(EngineManager.Engine.GraphicsDevice, this.vertices.Length * VertexPositionNormalMultipleTexture.SizeInBytes, BufferUsage.WriteOnly);
            this.vertexBuffer.ContentLost += new EventHandler(this.TerrainVertexBuffer_ContentLost);
            this.vertexBuffer.SetData<VertexPositionNormalMultipleTexture>(this.vertices);

            this.indexBuffer = new DynamicIndexBuffer(EngineManager.Engine.GraphicsDevice, typeof(int), this.indexes.Length, BufferUsage.WriteOnly);
            this.indexBuffer.ContentLost += new EventHandler(this.TerrainIndexBuffer_ContentLost);
            this.indexBuffer.SetData<int>(this.indexes);
        }

        /// <summary>
        /// Restores the data of the vertex buffer when it is lost.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TerrainVertexBuffer_ContentLost(object sender, EventArgs e)
        {
            this.vertexBuffer.SetData<VertexPositionNormalMultipleTexture>(this.vertices);
        }

        /// <summary>
        /// Restores the data of the index buffer when it is lost.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TerrainIndexBuffer_ContentLost(object sender, EventArgs e)
        {
            this.indexBuffer.SetData<int>(this.indexes);
        }

        /// <summary>
        /// Internal method for setting terrain elevation data of this patch only.
        /// </summary>
        /// <param name="location">The local location to set the elevation of.</param>
        /// <param name="height">The elevation value to set.</param>
        private void SetElevationDataInternal(Point location, float height)
        {
            this.vertexHeightsArray[TerrainPatch.GetIndex(location)] = height;
            this.verticesPositions[TerrainPatch.GetIndex(location)].Z = height;

            if (this.vertices != null)
            {
                Vector3 v = this.vertices[TerrainPatch.GetIndex(location)].Position;
                v.Z = height;
                this.vertices[TerrainPatch.GetIndex(location)].Position = v;
            }

            this.vertexBufferDirty = true;
        }

        /// <summary>
        /// Internal method for setting terrain type data of this patch only.
        /// </summary>
        /// <param name="location">The local location to set the height of.</param>
        /// <param name="terrainType">The terrain type value to set.</param>
        private void SetTerrainTypeDataInternal(Point location, TerrainType terrainType)
        {
            this.vertexTexturesArray[(int)EngineManager.Engine.Terrain.CurrentSeason][TerrainPatch.GetIndex(location)] = terrainType;

            if (this.vertices != null)
            {
                this.vertices[TerrainPatch.GetIndex(location)].TerrainType = this.LoadedHideBaseSeasonTerrain ? terrainType : this.GetTerrainTypeData(location.X, location.Y);
            }

            this.vertexBufferDirty = true;
        }
    }
}
