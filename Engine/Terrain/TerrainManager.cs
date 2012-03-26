namespace MMO3D.Engine
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines a region-based terrain class.
    /// </summary>
    public sealed class TerrainManager : IDisposable
    {
        /// <summary>
        /// This value sets the frequency of texture repitition.
        /// It should be set to a power of 2, or seams will appear.
        /// </summary>
        public const float TextureFrequency = 16;

        /// <summary>
        /// The difference in base height between each height level.
        /// </summary>
        public const int HeightLevelDifference = 4;

        /// <summary>
        /// An object used to read the terrain pack file.
        /// </summary>
        private static TerrainPackFileReader packFileReader;

        /// <summary>
        /// The Sky object for displaying the sky.
        /// </summary>
        private Sky sky;

        /// <summary>
        /// The effect used to render terrain patches (the ground).
        /// </summary>
        private MultipleTextureTerrainEffect terrainEffect;

        /// <summary>
        /// The effect used to render fluids (water, lava, etc.).
        /// </summary>
        private TexturedFluidEffect fluidEffect;

        /// <summary>
        /// Initializes static members of the TerrainManager class.
        /// </summary>
        static TerrainManager()
        {
            Point[] points = new Point[]
            {
                new Point(-1, -1),
                new Point(0, -1),
                new Point(1, -1),
                new Point(-1, 0),
                new Point(0, 0),
                new Point(1, 0),
                new Point(-1, 1),
                new Point(0, 1),
                new Point(1, 1)
            };

            TerrainManager.Offsets = new ReadOnlyCollection<Point>(points);
        }

        /// <summary>
        /// Initializes a new instance of the TerrainManager class.
        /// </summary>
        public TerrainManager()
        {
            this.Cursor = new TerrainCursor();

            this.sky = new Sky();

            this.terrainEffect = new MultipleTextureTerrainEffect(EngineManager.Engine.GraphicsDevice);

            this.terrainEffect.LightingEnabled = true;
            this.terrainEffect.LightDirection = new Vector3(-0.5f, -0.5f, -1);
            this.terrainEffect.Ambient = 0.4f;

            this.terrainEffect.PatchSize = TerrainPatch.PatchSize;

            this.fluidEffect = new TexturedFluidEffect(EngineManager.Engine.GraphicsDevice);

            this.fluidEffect.LightingEnabled = true;
            this.fluidEffect.LightDirection = new Vector3(-0.5f, -0.5f, -1);
            this.fluidEffect.Ambient = 1.4f;

            // Create terrain patch collection and populate it with 9 null entries
            this.TerrainPatches = new Collection<TerrainPatch>();
            for (int i = 0; i < TerrainManager.Offsets.Count; i++)
            {
                this.TerrainPatches.Add(null);
            }

            // Load the terrain with the terrain patch the camera is pointing at, and surrounding ones
            this.Reload(TerrainManager.GetTerrainPatchId(EngineManager.Engine.CurrentCamera.Target, this.CurrentHeightLevel), true);
        }

        /// <summary>
        /// Gets the positions of other patches in the terrain,
        /// relative to the center patch (used when loading patches).
        /// Indexes correspond to the following directions when viewed
        /// from a bird's eye view looking down the positive Y axis:<para />
        /// <list type="bullet">
        /// <item><description>0 = BottomLeft</description></item>
        /// <item><description>1 = Bottom</description></item>
        /// <item><description>2 = BottomRight</description></item>
        /// <item><description>3 = Left</description></item>
        /// <item><description>4 = Center</description></item>
        /// <item><description>5 = Right</description></item>
        /// <item><description>6 = TopLeft</description></item>
        /// <item><description>7 = Top</description></item>
        /// <item><description>8 = TopRight</description></item>
        /// </list>
        /// These patches can also be accessed by properties of the same name.
        /// </summary>
        /// <value>See summary.</value>
        public static ReadOnlyCollection<Point> Offsets
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the bottom-left patch (from a top-down view); index 0.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch BottomLeft
        {
            get { return this.TerrainPatches[0]; }
        }

        /// <summary>
        /// Gets the bottom patch (from a top-down view); index 1.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch Bottom
        {
            get { return this.TerrainPatches[1]; }
        }

        /// <summary>
        /// Gets the bottom-right patch (from a top-down view); index 2.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch BottomRight
        {
            get { return this.TerrainPatches[2]; }
        }

        /// <summary>
        /// Gets the left patch (from a top-down view); index 3.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch Left
        {
            get { return this.TerrainPatches[3]; }
        }

        /// <summary>
        /// Gets the center patch (from a top-down view); index 4.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch Center
        {
            get { return this.TerrainPatches[4]; }
        }

        /// <summary>
        /// Gets the right patch (from a top-down view); index 5.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch Right
        {
            get { return this.TerrainPatches[5]; }
        }

        /// <summary>
        /// Gets the top-left patch (from a top-down view); index 6.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch TopLeft
        {
            get { return this.TerrainPatches[6]; }
        }

        /// <summary>
        /// Gets the top patch (from a top-down view); index 7.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch Top
        {
            get { return this.TerrainPatches[7]; }
        }

        /// <summary>
        /// Gets the top-right patch (from a top-down view); index 8.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainPatch TopRight
        {
            get { return this.TerrainPatches[8]; }
        }

        /// <summary>
        /// Gets the array of terrain patches that compose the currently loaded region.
        /// </summary>
        /// <value>See summary.</value>
        public ReadOnlyCollection<TerrainPatch> TerrainPatchList
        {
            get { return new ReadOnlyCollection<TerrainPatch>(this.TerrainPatches); }
        }

        /// <summary>
        /// Gets or sets the current height level we have loaded.
        /// </summary>
        /// <value>See summary.</value>
        public int CurrentHeightLevel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current season we have loaded.
        /// </summary>
        /// <value>See summary.</value>
        public Season CurrentSeason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to render base season terrain as undefined.
        /// </summary>
        /// <value>See summary.</value>
        /// <remarks>
        /// For example: if a particular vertex, in midseason, is
        /// grass, and in winter it is undefined, when rendering
        /// winter, the vertex will render with undefined, NOT grass
        /// when this value is set to true. This is an option for the
        /// editor for ease of use, and should always be false when
        /// in the game client.
        /// </remarks>
        public bool HideBaseSeasonTerrain
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the 3D cursor used to manipulate the terrain.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainCursor Cursor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the array of terrain patches that compose the currently loaded terrain.
        /// </summary>
        /// <value>See summary.</value>
        private Collection<TerrainPatch> TerrainPatches
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the ID of the terrain patch for the specified global position.
        /// </summary>
        /// <param name="position">The global position.</param>
        /// <param name="heightLevel">The height level we are getting a terrain patch for.</param>
        /// <returns>See summary.</returns>
        public static Point3D GetTerrainPatchId(Point position, int heightLevel)
        {
            return new Point3D((int)Math.Floor(position.X / (float)TerrainPatch.PatchSize), (int)Math.Floor(position.Y / (float)TerrainPatch.PatchSize), heightLevel);
        }

        /// <summary>
        /// Gets the ID of the terrain patch for the specified global position.
        /// </summary>
        /// <param name="position">The global position.</param>
        /// <param name="heightLevel">The height level we are getting a terrain patch for.</param>
        /// <returns>See summary.</returns>
        public static Point3D GetTerrainPatchId(Vector3 position, int heightLevel)
        {
            return new Point3D((int)Math.Floor(position.X / (float)TerrainPatch.PatchSize), (int)Math.Floor(position.Y / (float)TerrainPatch.PatchSize), heightLevel);
        }

        /// <summary>
        /// Resets the flows of all fluids currently loaded into memory.
        /// </summary>
        public void ResetFlows()
        {
            for (int i = 0; i < this.TerrainPatches.Count; i++)
            {
                if (this.TerrainPatches[i] != null)
                {
                    for (int j = 0; j < this.TerrainPatches[i].Fluids.Count; j++)
                    {
                        this.TerrainPatches[i].Fluids[j].ResetFlow();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the loaded terrain patch instance for the requested patch ID, or null if it is not found.
        /// </summary>
        /// <param name="patchId">The patch ID of the terrain patch instance to find.</param>
        /// <returns>See summary.</returns>
        public TerrainPatch GetLoadedTerrainPatch(Point3D patchId)
        {
            for (int i = 0; i < this.TerrainPatches.Count; i++)
            {
                if (this.TerrainPatches[i] != null && patchId.Equals(this.TerrainPatches[i].PatchId))
                {
                    return this.TerrainPatches[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the loaded terrain patch instance for the requested global position, or null if it is not found.
        /// </summary>
        /// <param name="position">The position to find the terrain patch instance of.</param>
        /// <returns>See summary.</returns>
        public TerrainPatch GetLoadedTerrainPatch(Vector3 position)
        {
            return this.GetLoadedTerrainPatch(TerrainManager.GetTerrainPatchId(position, this.CurrentHeightLevel));
        }

        /// <summary>
        /// Gets the terrain patch from memory cache or disk, or null if it is not found.
        /// </summary>
        /// <param name="patchX">The terrain patch ID X coordinate.</param>
        /// <param name="patchY">The terrain patch ID Y coordinate.</param>
        /// <param name="patchZ">The terrain patch ID Z coordinate.</param>
        /// <returns>See summary.</returns>
        public TerrainPatch GetTerrainPatch(int patchX, int patchY, int patchZ)
        {
            return this.GetTerrainPatch(new Point3D(patchX, patchY, patchZ));
        }

        /// <summary>
        /// Gets the terrain patch from memory cache or disk, or null if it is not found.
        /// </summary>
        /// <param name="patchId">The terrain patch ID.</param>
        /// <returns>See summary.</returns>
        public TerrainPatch GetTerrainPatch(Point3D patchId)
        {
            // If the requested terrain patch is already loaded, return it
            TerrainPatch patch = this.GetLoadedTerrainPatch(patchId);
            if (patch != null)
            {
                return patch;
            }
            else
            {
                if (EngineManager.Engine.InEditorMode)
                {
                    try
                    {
                        return TerrainPatch.FromByteArray(File.ReadAllBytes(string.Format(
                            CultureInfo.InvariantCulture,
                            "{0}/{1}/{2},{3},{4}.pat",
                            System.Windows.Forms.Application.StartupPath,
                            "GameData/Terrain",
                            patchId.X,
                            patchId.Y,
                            patchId.Z)));
                    }
                    catch (IOException)
                    {
                        return null;
                    }
                }
                else
                {
                    const string TerrainFile = "GameData/terrain.tpc";

                    if (TerrainManager.packFileReader == null)
                    {
                        try
                        {
                            TerrainManager.packFileReader = new TerrainPackFileReader(TerrainFile);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("Failed to read terrain file: " + e);
                        }
                    }

                    return TerrainManager.packFileReader.ReadTerrainPatch(patchId);
                }
            }
        }

        /// <summary>
        /// Forces the reloading of the terrain patch with the specified ID.
        /// </summary>
        /// <param name="patchId">The ID of the patch to force reloading of.</param>
        public void ForcePatchReload(Point3D patchId)
        {
            for (int i = 0; i < this.TerrainPatches.Count; i++)
            {
                if (this.TerrainPatches[i] != null && patchId.Equals(this.TerrainPatches[i].PatchId))
                {
                    this.TerrainPatches[i] = null;
                    this.TerrainPatches[i] = this.GetTerrainPatch(patchId);
                    this.ResetFlows();
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the elevation of the terrain at the given position, if available.
        /// </summary>
        /// <param name="position">The world position on the map to get the elevation of.</param>
        /// <returns>The elevation of the terrain, or zero if the position requested is not currently loaded.</returns>
        public float GetTerrainElevation(Vector3 position)
        {
            try
            {
                return this.GetLoadedTerrainPatch(position).GetExactElevation(CoordinateConverter.WorldToLocal(position));
            }
            catch (IndexOutOfRangeException)
            {
                return 0;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the type of the terrain at the given world position, if available.
        /// </summary>
        /// <param name="position">The position on the map to get the terrain type of.</param>
        /// <returns>See summary.</returns>
        public TerrainType GetTerrainType(Vector3 position)
        {
            try
            {
                return this.GetLoadedTerrainPatch(position).GetTerrainTypeData(CoordinateConverter.WorldToLocal(position));
            }
            catch (IndexOutOfRangeException)
            {
                return TerrainType.UndefinedTerrain;
            }
            catch (NullReferenceException)
            {
                return TerrainType.UndefinedTerrain;
            }
        }

        /// <summary>
        /// Gets the type of the fluid at the given world position, if available.
        /// </summary>
        /// <param name="position">The world position on the map to get the fluid type of.</param>
        /// <returns>See summary.</returns>
        public FluidType GetFluidType(Vector3 position)
        {
            return this.GetFluidTypeForSeason(position, this.CurrentSeason);
        }

        /// <summary>
        /// Gets the type of the fluid at the given world position, if available, in the particular season.
        /// </summary>
        /// <param name="position">The world position on the map to get the fluid type of.</param>
        /// <param name="season">The season to get the fluid type in.</param>
        /// <returns>See summary.</returns>
        public FluidType GetFluidTypeForSeason(Vector3 position, Season season)
        {
            TerrainPatch pat = this.GetLoadedTerrainPatch(position);
            Vector2 local = CoordinateConverter.WorldToLocal(position);

            FluidType returnValue = FluidType.UndefinedFluid;
            float maxHeight = pat.GetExactElevation(local);

            for (int i = 0; i < pat.Fluids.Count; i++)
            {
                if (pat.Fluids[i].ContainsPoint(season, local))
                {
                    if (pat.Fluids[i].GetExactHeight(season, local) > maxHeight)
                    {
                        maxHeight = pat.Fluids[i].GetExactHeight(season, local);
                        returnValue = pat.Fluids[i].GetFluidType(season);
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Determines if the terrain at the given world position is traversable.
        /// </summary>
        /// <param name="position">The position on the world map to determine the traversability of.</param>
        /// <returns>See summary.</returns>
        public bool IsTraversable(Vector3 position)
        {
            return this.GetTerrainType(position).IsTraversable() && this.GetFluidType(position).IsTraversable();
        }

        /// <summary>
        /// This is called when the terrain should draw itself.
        /// </summary>
        public void Draw()
        {
            // Make sure the map's correctly loaded at the proper position.
            this.Reload(EngineManager.Engine.CurrentCamera.Target);

            // Draw the sky
            this.sky.Draw(EngineManager.Engine.CurrentCamera);

            this.terrainEffect.View = EngineManager.Engine.CurrentCamera.ViewMatrix;
            this.terrainEffect.Projection = EngineManager.Engine.CurrentCamera.ProjectionMatrix;

            // Draw terrain
            this.terrainEffect.Begin();
            foreach (EffectPass pass in this.terrainEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                // Draw ALL the terrain patches themselves FIRST...
                for (int i = 0; i < this.TerrainPatches.Count; i++)
                {
                    if (this.TerrainPatches[i] != null)
                    {
                        this.terrainEffect.World = this.TerrainPatches[i].WorldMatrix;
                        this.terrainEffect.PatchId = this.TerrainPatches[i].PatchId;

                        this.TerrainPatches[i].Cursor = this.Cursor;
                        this.TerrainPatches[i].Draw(this.terrainEffect);
                    }
                }

                pass.End();
            }

            this.terrainEffect.End();

            this.fluidEffect.View = EngineManager.Engine.CurrentCamera.ViewMatrix;
            this.fluidEffect.Projection = EngineManager.Engine.CurrentCamera.ProjectionMatrix;

            // Draw fluids
            this.fluidEffect.Begin();
            foreach (EffectPass pass in this.fluidEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                // ...THEN draw ALL the fluids, otherwise we get ugly seams
                for (int i = 0; i < this.TerrainPatches.Count; i++)
                {
                    if (this.TerrainPatches[i] != null)
                    {
                        for (int j = 0; j < this.TerrainPatches[i].Fluids.Count; j++)
                        {
                            this.fluidEffect.World = this.TerrainPatches[i].Fluids[j].WorldMatrix;
                            this.fluidEffect.Transparency = this.TerrainPatches[i].Fluids[j].GetFluidType(this.CurrentSeason).Transparency();
                            this.fluidEffect.Texture = this.TerrainPatches[i].Fluids[j].GetFluidType(this.CurrentSeason).GetTexture();

                            this.TerrainPatches[i].Fluids[j].Draw(this.fluidEffect, this.CurrentSeason);
                        }
                    }
                }

                pass.End();
            }

            this.fluidEffect.End();
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
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.terrainEffect.Dispose();
                this.fluidEffect.Dispose();

                for (int i = 0; i < this.TerrainPatches.Count; i++)
                {
                    this.TerrainPatches[i].Dispose();
                }
            }

            this.TerrainPatches.Clear();
        }

        /// <summary>
        /// Reloads the terrain data for the given position. Does not force reloading.
        /// </summary>
        /// <param name="position">The position to reload for.</param>
        private void Reload(Vector3 position)
        {
            this.Reload(TerrainManager.GetTerrainPatchId(position, this.CurrentHeightLevel), false);
        }

        /// <summary>
        /// Reloads the terrain data with the desired center terrain patch.
        /// </summary>
        /// <param name="patchID">The ID of the center terrain patch to load.</param>
        /// <param name="forceReload">Whether to force reloading even if the center terrain patch being is the same as the one currently loaded.</param>
        private void Reload(Point3D patchID, bool forceReload)
        {
            for (int i = 0; i < this.TerrainPatches.Count; i++)
            {
                Point3D pt = new Point3D(patchID.X + TerrainManager.Offsets[i].X, patchID.Y + TerrainManager.Offsets[i].Y, patchID.Z);

                if (forceReload || this.TerrainPatches[i] == null || !this.TerrainPatches[i].PatchId.Equals(pt) || this.TerrainPatches[i].LoadedSeason != this.CurrentSeason || this.TerrainPatches[i].LoadedHideBaseSeasonTerrain != this.HideBaseSeasonTerrain)
                {
                    // If the season or hide base season terrain was changed, we need to FORCE reload
                    if (this.TerrainPatches[i] != null && (this.TerrainPatches[i].LoadedSeason != this.CurrentSeason || this.TerrainPatches[i].LoadedHideBaseSeasonTerrain != this.HideBaseSeasonTerrain))
                    {
                        this.ForcePatchReload(pt);
                    }
                    else
                    {
                        this.TerrainPatches[i] = this.GetTerrainPatch(pt);
                    }
                }
            }
        }
    }
}
