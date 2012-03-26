namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MMO3D.CommonCode;

    /// <summary>
    /// Defines fluids to be rendered along with terrain.
    /// </summary>
    public sealed class Fluid : IDisposable
    {
        /// <summary>
        /// Number of vertices in a fluid.
        /// </summary>
        private const int Vertices = 4;

        /// <summary>
        /// Vertex buffer for the fluid.
        /// </summary>
        private DynamicVertexBuffer vertexBuffer;

        /// <summary>
        /// The vertices of the fluid.
        /// </summary>
        private VertexPositionNormalTexture[] vertices;

        /// <summary>
        /// The fluid type of this fluid in each season.
        /// </summary>
        private FluidType[] fluidTypes = new FluidType[SeasonExtensions.Count];

        /// <summary>
        /// The points that compose the rectangular area of the fluid, in patch coordinates.
        /// The first index specifies the season, and the second index specifies the vertex.
        /// </summary>
        private Vector3[][] points = new Vector3[][] { new Vector3[Fluid.Vertices], new Vector3[Fluid.Vertices], new Vector3[Fluid.Vertices] };

        /// <summary>
        /// The direction in which the fluid should flow in each season.
        /// </summary>
        private FluidFlowDirection[] flowDirections = new FluidFlowDirection[SeasonExtensions.Count];

        /// <summary>
        /// The speed at which the fluids should flow in each season.
        /// </summary>
        private float[] flowSpeeds = new float[SeasonExtensions.Count];

        /// <summary>
        /// The current position of the fluid flow.
        /// </summary>
        private float currentFlowPosition = 0;

        /// <summary>
        /// Initializes a new instance of the Fluid class.
        /// </summary>
        /// <param name="patchId">The ID of the terrain patch this fluid is on.</param>
        public Fluid(Point3D patchId)
        {
            this.PatchId = patchId;
            this.WorldMatrix = Matrix.CreateTranslation(this.PatchId.X * TerrainPatch.PatchSize, this.PatchId.Y * TerrainPatch.PatchSize, this.PatchId.Z * TerrainManager.HeightLevelDifference);
        }

        /// <summary>
        /// Gets the ID of the terrain patch this fluid is on.
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
        /// Gets the fluid type of this fluid in the particular season.
        /// </summary>
        /// <param name="season">The season to get the fluid type for.</param>
        /// <returns>See summary.</returns>
        public FluidType GetFluidType(Season season)
        {
            return this.fluidTypes[(int)season];
        }

        /// <summary>
        /// Sets the fluid type of this fluid in the particular season.
        /// </summary>
        /// <param name="season">The season to set the fluid type for.</param>
        /// <param name="fluidType">The fluid type to set.</param>
        public void SetFluidType(Season season, FluidType fluidType)
        {
            this.fluidTypes[(int)season] = fluidType;
        }

        /// <summary>
        /// Gets the particular vertex in the particular season.
        /// </summary>
        /// <param name="season">The season to get the vertex in.</param>
        /// <param name="vertex">The vertex to get.</param>
        /// <returns>See summary.</returns>
        public Vector3 GetPoint(Season season, FluidVertex vertex)
        {
            return this.points[(int)season][(int)vertex];
        }

        /// <summary>
        /// Sets the particular vertex in the particular season.
        /// </summary>
        /// <param name="season">The season to get the vertex in.</param>
        /// <param name="vertex">The vertex to set.</param>
        /// <param name="point">The value to set the vertex to.</param>
        public void SetPoint(Season season, FluidVertex vertex, Vector3 point)
        {
            this.points[(int)season][(int)vertex] = point;

            switch (vertex)
            {
                case FluidVertex.Southwest:
                    this.points[(int)season][(int)FluidVertex.Northwest].X = this.points[(int)season][(int)FluidVertex.Southwest].X;
                    this.points[(int)season][(int)FluidVertex.Southeast].Y = this.points[(int)season][(int)FluidVertex.Southwest].Y;
                    break;
                case FluidVertex.Southeast:
                    this.points[(int)season][(int)FluidVertex.Northeast].X = this.points[(int)season][(int)FluidVertex.Southeast].X;
                    this.points[(int)season][(int)FluidVertex.Southwest].Y = this.points[(int)season][(int)FluidVertex.Southeast].Y;
                    break;
                case FluidVertex.Northwest:
                    this.points[(int)season][(int)FluidVertex.Southwest].X = this.points[(int)season][(int)FluidVertex.Northwest].X;
                    this.points[(int)season][(int)FluidVertex.Northeast].Y = this.points[(int)season][(int)FluidVertex.Northwest].Y;
                    break;
                case FluidVertex.Northeast:
                    this.points[(int)season][(int)FluidVertex.Southeast].X = this.points[(int)season][(int)FluidVertex.Northeast].X;
                    this.points[(int)season][(int)FluidVertex.Northwest].Y = this.points[(int)season][(int)FluidVertex.Northeast].Y;
                    break;
            }
        }

        /// <summary>
        /// Gets the direction in which the fluid should flow in the particular season.
        /// </summary>
        /// <param name="season">The season to get the flow direction for.</param>
        /// <returns>See summary.</returns>
        public FluidFlowDirection GetFlowDirection(Season season)
        {
            return this.flowDirections[(int)season];
        }

        /// <summary>
        /// Sets the direction in which the fluid should flow in the particular season.
        /// </summary>
        /// <param name="season">The season to set the flow direction for.</param>
        /// <param name="flowDirection">The flow direction to set.</param>
        public void SetFlowDirection(Season season, FluidFlowDirection flowDirection)
        {
            this.flowDirections[(int)season] = flowDirection;
        }

        /// <summary>
        /// Gets the speed at which the fluid should flow in the particular season.
        /// </summary>
        /// <param name="season">The season to get the flow speed in.</param>
        /// <returns>See summary.</returns>
        public float GetFlowSpeed(Season season)
        {
            return this.flowSpeeds[(int)season];
        }

        /// <summary>
        /// Sets the speed at which the fluid should flow in the particular season.
        /// The minimum value is 0 and the maximum value is 0.5.
        /// </summary>
        /// <param name="season">The season to set the flow speed in.</param>
        /// <param name="flowSpeed">The flow speed to set.</param>
        public void SetFlowSpeed(Season season, float flowSpeed)
        {
            this.flowSpeeds[(int)season] = MathHelper.Clamp(flowSpeed, 0, 0.5f);
        }

        /// <summary>
        /// Gets the width (X axis size) of the fluid in the particular season.
        /// </summary>
        /// <param name="season">The season to get the width of the fluid in.</param>
        /// <returns>See summary.</returns>
        public float GetWidth(Season season)
        {
            return this.GetPoint(season, FluidVertex.Northeast).X - this.GetPoint(season, FluidVertex.Southwest).X;
        }

        /// <summary>
        /// Gets the depth (Y axis size) of the fluid in the particular season.
        /// </summary>
        /// <param name="season">The season to get the depth of the fluid in.</param>
        /// <returns>See summary.</returns>
        public float GetDepth(Season season)
        {
            return this.GetPoint(season, FluidVertex.Northeast).Y - this.GetPoint(season, FluidVertex.Southwest).Y;
        }

        /// <summary>
        /// Gets the exact height of any point on the fluid from a set of local (patch) coordinates.
        /// </summary>
        /// <param name="season">The season to use when retrieving the exact height.</param>
        /// <param name="location">The local location of the point to find the height of.</param>
        /// <returns>See summary.</returns>
        public float GetExactHeight(Season season, Vector2 location)
        {
            location.X = (location.X - this.GetPoint(season, FluidVertex.Southwest).X) / this.GetWidth(season);
            location.Y = (location.Y - this.GetPoint(season, FluidVertex.Southwest).Y) / this.GetDepth(season);

            return MathExtensions.GetExactHeight(location, new float[] { this.GetPoint(season, FluidVertex.Southwest).Z, this.GetPoint(season, FluidVertex.Northwest).Z, this.GetPoint(season, FluidVertex.Southeast).Z, this.GetPoint(season, FluidVertex.Northeast).Z });
        }

        /// <summary>
        /// Checks if the rectangle of this fluid contains a local terrain patch point.
        /// </summary>
        /// <param name="season">The season to use when checking if a point is contained.</param>
        /// <param name="point">The local point to check.</param>
        /// <returns>See summary.</returns>
        public bool ContainsPoint(Season season, Vector2 point)
        {
            System.Drawing.RectangleF rectangle = new System.Drawing.RectangleF(this.GetPoint(season, FluidVertex.Southwest).X, this.GetPoint(season, FluidVertex.Southwest).Y, this.GetWidth(season), this.GetDepth(season));

            return rectangle.Contains(point.X, point.Y);
        }

        /// <summary>
        /// Resets the flow of this fluid.
        /// </summary>
        public void ResetFlow()
        {
            this.currentFlowPosition = 0;
        }

        /// <summary>
        /// Draws the fluid.
        /// </summary>
        /// <param name="fluidEffect">The effect used to draw the fluid.</param>
        /// <param name="season">The season to draw.</param>
        public void Draw(Effect fluidEffect, Season season)
        {
            this.Update(season);

            if (this.vertices == null || this.vertexBuffer == null)
            {
                this.CreateBuffers();
            }

            EngineManager.Engine.SetDefaultRenderState();

            fluidEffect.CommitChanges();

            EngineManager.Engine.GraphicsDevice.Vertices[0].SetSource(this.vertexBuffer, 0, VertexPositionNormalTexture.SizeInBytes);
            EngineManager.Engine.GraphicsDevice.VertexDeclaration = new VertexDeclaration(EngineManager.Engine.GraphicsDevice, VertexPositionNormalTexture.VertexElements);

            EngineManager.Engine.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, this.vertices.Length / 3);
        }

        /// <summary>
        /// Returns a System.String representing the MMO3D.Engine.Fluid.
        /// </summary>
        /// <returns>See summary.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "[{0}] {1}->{2}->{3}", this.PatchId, this.GetFluidType(Season.Midseason), this.GetFluidType(Season.Summer), this.GetFluidType(Season.Winter));
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
                this.vertexBuffer.Dispose();
            }
        }

        /// <summary>
        /// Recreates the vertices and sets the vertex buffer.
        /// </summary>
        /// <param name="season">The season to recreate the vertices for.</param>
        private void Update(Season season)
        {
            // Reload the vertices for the current flow position plus the flow speed, and store it
            this.LoadVertices(season, this.GetFlowDirection(season), this.currentFlowPosition += this.GetFlowSpeed(season));

            if (this.vertexBuffer != null)
            {
                try
                {
                    this.vertexBuffer.SetData(this.vertices);
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        /// <summary>
        /// Loads the vertices of the fluid.
        /// </summary>
        /// <param name="season">The season to load the vertices for.</param>
        /// <param name="angle">The angle the fluid should flow in.</param>
        /// <param name="speed">The amount by which to increase the flow of the fluid.</param>
        private void LoadVertices(Season season, FluidFlowDirection angle, float speed)
        {
            Vector2[] coords = this.GetQuadTextureCoordinates(season, angle, speed);

            VertexPositionNormalTexture lowerLeft = new VertexPositionNormalTexture(this.GetPoint(season, FluidVertex.Southwest), Vector3.UnitZ, coords[0]);
            VertexPositionNormalTexture lowerRight = new VertexPositionNormalTexture(this.GetPoint(season, FluidVertex.Southeast), Vector3.UnitZ, coords[1]);
            VertexPositionNormalTexture topLeft = new VertexPositionNormalTexture(this.GetPoint(season, FluidVertex.Northwest), Vector3.UnitZ, coords[2]);
            VertexPositionNormalTexture topRight = new VertexPositionNormalTexture(this.GetPoint(season, FluidVertex.Northeast), Vector3.UnitZ, coords[3]);

            this.vertices = new VertexPositionNormalTexture[6];
            this.vertices[0] = topLeft;
            this.vertices[1] = lowerRight;
            this.vertices[2] = lowerLeft;
            this.vertices[3] = topLeft;
            this.vertices[4] = topRight;
            this.vertices[5] = lowerRight;
        }

        /// <summary>
        /// Creates the fluid buffers.
        /// </summary>
        private void CreateBuffers()
        {
            this.vertexBuffer = new DynamicVertexBuffer(EngineManager.Engine.GraphicsDevice, this.vertices.Length * VertexPositionNormalTexture.SizeInBytes, BufferUsage.WriteOnly);
            this.vertexBuffer.ContentLost += new EventHandler(this.VertexBuffer_ContentLost);
            this.vertexBuffer.SetData(this.vertices);
        }

        /// <summary>
        /// Restores the data of the vertex buffer when it is lost.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void VertexBuffer_ContentLost(object sender, EventArgs e)
        {
            this.vertexBuffer.SetData(this.vertices);
        }

        /// <summary>
        /// Gets the texture coordinates of the fluid, from the angle and speed.
        /// </summary>
        /// <param name="season">The season to get the texture coordinates of.</param>
        /// <param name="angle">The angle the fluid should flow in.</param>
        /// <param name="speed">The amount by which to increase the flow of the fluid.</param>
        /// <returns>See summary.</returns>
        private Vector2[] GetQuadTextureCoordinates(Season season, FluidFlowDirection angle, float speed)
        {
            // The texture coordinates for each vertex, to return
            Vector2[] coords = new Vector2[4];

            // The amount to advance by
            // Defaults to nothing
            Vector2 advance = Vector2.Zero;

            switch (angle)
            {
                case FluidFlowDirection.East:
                    advance = new Vector2(speed, 0);
                    break;
                case FluidFlowDirection.Northeast:
                    advance = new Vector2(speed, speed);
                    break;
                case FluidFlowDirection.North:
                    advance = new Vector2(0, speed);
                    break;
                case FluidFlowDirection.Northwest:
                    advance = new Vector2(speed, speed);
                    break;
                case FluidFlowDirection.West:
                    advance = new Vector2(speed, 0);
                    break;
                case FluidFlowDirection.Southwest:
                    advance = new Vector2(speed, speed);
                    break;
                case FluidFlowDirection.South:
                    advance = new Vector2(0, speed);
                    break;
                case FluidFlowDirection.Southeast:
                    advance = new Vector2(speed, speed);
                    break;
            }

            // Multiply the amount to advance the flow,
            // by 1 or -1 depending on the angle
            advance *= (angle >= 0 && (int)angle <= 179) ? -1 : 1;

            // Generate all the coordinates
            if (angle == FluidFlowDirection.Northwest || angle == FluidFlowDirection.Southeast)
            {
                coords[0] = (MathExtensions.TruncateVector(this.GetPoint(season, FluidVertex.Northwest)) / TerrainManager.TextureFrequency) - advance;
                coords[1] = (MathExtensions.TruncateVector(this.GetPoint(season, FluidVertex.Northeast)) / TerrainManager.TextureFrequency) - advance;
                coords[2] = (MathExtensions.TruncateVector(this.GetPoint(season, FluidVertex.Southwest)) / TerrainManager.TextureFrequency) - advance;
                coords[3] = (MathExtensions.TruncateVector(this.GetPoint(season, FluidVertex.Southeast)) / TerrainManager.TextureFrequency) - advance;
            }
            else
            {
                coords[0] = (MathExtensions.TruncateVector(this.GetPoint(season, FluidVertex.Southwest)) / TerrainManager.TextureFrequency) + advance;
                coords[1] = (MathExtensions.TruncateVector(this.GetPoint(season, FluidVertex.Southeast)) / TerrainManager.TextureFrequency) + advance;
                coords[2] = (MathExtensions.TruncateVector(this.GetPoint(season, FluidVertex.Northwest)) / TerrainManager.TextureFrequency) + advance;
                coords[3] = (MathExtensions.TruncateVector(this.GetPoint(season, FluidVertex.Northeast)) / TerrainManager.TextureFrequency) + advance;
            }

            return coords;
        }
    }
}
