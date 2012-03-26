namespace MMO3D.Engine
{
    using System;
    using System.Threading;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents a QuadTree structure.
    /// </summary>
    public sealed class QuadTree : QuadNodeCollection, IDisposable
    {
        /// <summary>
        /// The max number of children that a node can have.
        /// </summary>
        public const int NodeChildrenCount = 4;

        /// <summary>
        /// The max number of children that a node can have.
        /// </summary>
        internal const int ProcessIterationMaxValue = 256;

        /// <summary>
        /// The number of times to update the quad vertices when loading the tree.
        /// </summary>
        internal const int LoadUpdateOccurrences = 10;

        /// <summary>
        /// The vertex declaration of the terrain vertices.
        /// </summary>
        private VertexDeclaration vertexDeclaration;

        /// <summary>
        /// The current index and vertex buffers.
        /// </summary>
        private BuffersData currentBufferData;

        /// <summary>
        /// List of buffer data to dispose.
        /// </summary>
        private Vector<BuffersData> disposeData;

        /// <summary>
        /// List of the buffer data that was last loaded.
        /// </summary>
        private Vector<BuffersData> lastLoadedData;

        /// <summary>
        /// Initializes a new instance of the QuadTree class.
        /// </summary>
        /// <param name="depth">The depth of the tree.</param>
        /// <param name="size">The size of the terrain part represented by the root quad tree node.</param>
        /// <param name="location">The location of the QuadTree.</param>
        public QuadTree(byte depth, int size, Vector2 location)
        {
            this.NodeRelevance = 0.1f;
            this.QuadTreeDetailAtFar = 200000;
            this.QuadTreeDetailAtFront = 5000;
            this.VertexDetail = 17;

            this.Location = location;
            this.Depth = depth;
            this.Size = size;

            this.HeightFieldSpace = (float)(size / Math.Pow(2, depth - 1));

            this.disposeData = new Vector<BuffersData>();
            this.lastLoadedData = new Vector<BuffersData>();

            this.Vertices = new Vector<VertexPositionNormalMultipleTexture>(1000);
            this.Indexes = new Vector<int>(1000);
        }

        /// <summary>
        /// Gets the vertices of the quad tree.
        /// </summary>
        /// <value>See summary.</value>
        public Vector<VertexPositionNormalMultipleTexture> Vertices
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the indexes of the quad tree.
        /// </summary>
        /// <value>See summary.</value>
        public Vector<int> Indexes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the heights array.
        /// </summary>
        /// <value>See summary.</value>
        public float[,] HeightData
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the detail threshold relevance for nodes.
        /// </summary>
        /// <value>See summary.</value>
        public float NodeRelevance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the detail threshold for the vertices.
        /// </summary>
        /// <value>See summary.</value>
        public float VertexDetail
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the near detail threshold for the QuadTree.
        /// </summary>
        /// <value>See summary.</value>
        public float QuadTreeDetailAtFront
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the far detail threshold for the QuadTree.
        /// </summary>
        /// <value>See summary.</value>
        public float QuadTreeDetailAtFar
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the effect used to render.
        /// </summary>
        /// <value>See summary.</value>
        public MultipleTextureTerrainEffect Effect
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the GraphicsDevice used to render.
        /// </summary>
        /// <value>See summary.</value>
        public GraphicsDevice Device
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the location of the current tree.
        /// </summary>
        /// <value>See summary.</value>
        public Vector2 Location
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the depth of the current tree.
        /// </summary>
        /// <value>See summary.</value>
        public int Depth
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the minimal distance between to vertex for the leaf node.
        /// </summary>
        /// <value>See summary.</value>
        public float HeightFieldSpace
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the size of the tree.
        /// </summary>
        /// <value>See summary.</value>
        /// <remarks>A tree is a square.</remarks>
        public int Size
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of children that this node has.
        /// </summary>
        /// <value>See summary.</value>
        public override int ChildCount
        {
            get { return 1; }
        }

        /// <summary>
        /// Gets the current minimal depth upond with node childs are not checked and automatically validated.
        /// </summary>
        /// <value>See summary.</value>
        internal int MinimalDepth
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the internal value used to identify the update phase and optimize vertex array searching.
        /// </summary>
        /// <value>See summary.</value>
        internal int ProcessIterationId
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the children of the quad tree.
        /// </summary>
        public void Initialize()
        {
            this.Children[0] = new QuadNode(null, NodeChild.Northeast);
            this.Children[0].Location = this.Location;
            this.Children[0].ParentTree = this;
            this.Children[0].Initialize();
        }

        /// <summary>
        /// Loads the data of the quad tree.
        /// </summary>
        /// <param name="device">The graphics device to use for rendering.</param>
        /// <param name="effect">The effect to user for rendering.</param>
        public void Load(GraphicsDevice device, MultipleTextureTerrainEffect effect)
        {
            this.Device = device;
            this.Effect = effect;

            device.DeviceReset += new EventHandler(this.Device_DeviceReset);

            this.Effect.World = Matrix.CreateTranslation(Vector3.Zero);
            this.Effect.Projection = EngineManager.Engine.CurrentCamera.ProjectionMatrix;

            this.MinimalDepth = 4;

            for (int i = 0; i < QuadTree.LoadUpdateOccurrences; i++)
            {
                this.UpdateQuadVertices();
            }

            this.MinimalDepth = 1;

            this.BuildQuadVerticesList();

            this.currentBufferData = this.lastLoadedData[0];

            this.vertexDeclaration = new VertexDeclaration(this.Device, VertexPositionNormalMultipleTexture.GetVertexElements());
            this.Device.VertexDeclaration = this.vertexDeclaration;
            this.Device.Vertices[0].SetSource(this.currentBufferData.VertexBuffer, 0, VertexPositionNormalMultipleTexture.SizeInBytes);
            this.Device.Indices = this.currentBufferData.IndexBuffer;
        }

        /// <summary>
        /// Updates the quad tree.
        /// </summary>
        public void Update()
        {
            Thread.Sleep(1000);

            this.ProcessIterationId += 1;
            if (this.ProcessIterationId > QuadTree.ProcessIterationMaxValue)
            {
                this.ProcessIterationId = 0;
            }

            this.UpdateQuadVertices();
            this.BuildQuadVerticesList();
            this.ClearLastLoaded();
        }

        /// <summary>
        /// Renders the quad tree.
        /// </summary>
        public void Render()
        {
            if (this.lastLoadedData.Count > 0)
            {
                this.disposeData.Add(this.currentBufferData);
                this.currentBufferData = this.lastLoadedData[0];
                this.Device.Vertices[0].SetSource(this.currentBufferData.VertexBuffer, 0, VertexPositionNormalMultipleTexture.SizeInBytes);
                this.Device.Indices = this.currentBufferData.IndexBuffer;
                this.lastLoadedData.RemoveAt(0);
            }

            this.Effect.View = EngineManager.Engine.CurrentCamera.ViewMatrix;

            this.Effect.Begin();

            foreach (EffectPass pass in this.Effect.CurrentTechnique.Passes)
            {
                pass.Begin();

                this.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.currentBufferData.NumberOfVertices, 0, this.currentBufferData.NumberOfIndexes);

                pass.End();
            }

            this.Effect.End();
        }

        /// <summary>
        /// Gets the height of the ground at the specified position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <returns>See summary.</returns>
        public float GetHeight(float x, float y)
        {
            float x2 = Math.Abs((x / 128) % 256);
            float y2 = Math.Abs((y / 128) % 256);

            return this.HeightData[(int)x2, (int)y2];
        }

        /// <summary>
        /// Gets the sub node size at the specified level depth.
        /// </summary>
        /// <param name="depth">The depth of the specified level.</param>
        /// <returns>See summary.</returns>
        public float GetNodeSizeAtLevel(int depth)
        {
            int diff = (int)((this.Depth - 1) - depth);
            double result = Math.Pow(2, diff);
            return this.HeightFieldSpace * (float)result;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                for (int i = 0; i < this.disposeData.Count; i++)
                {
                    BuffersData data = this.disposeData[i];
                    data.VertexBuffer.Dispose();
                    data.VertexBuffer = null;
                    data.IndexBuffer.Dispose();
                    data.IndexBuffer = null;
                }

                this.disposeData.Clear();
                this.disposeData = null;

                for (int i = 0; i < this.lastLoadedData.Count; i++)
                {
                    BuffersData data = this.lastLoadedData[i];
                    data.VertexBuffer.Dispose();
                    data.VertexBuffer = null;
                    data.IndexBuffer.Dispose();
                    data.IndexBuffer = null;
                    this.lastLoadedData.RemoveAt(0);
                }

                this.vertexDeclaration.Dispose();
            }

            this.lastLoadedData.Clear();
            this.lastLoadedData = null;
        }

        /// <summary>
        /// Handles the graphics device being reset.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Device_DeviceReset(object sender, EventArgs e)
        {
            this.Device.VertexDeclaration = this.vertexDeclaration;
            this.Device.Vertices[0].SetSource(this.currentBufferData.VertexBuffer, 0, VertexPositionNormalMultipleTexture.SizeInBytes);
            this.Device.Indices = this.currentBufferData.IndexBuffer;
        }

        /// <summary>
        /// Clears the data that was last loaded into the quad tree.
        /// </summary>
        private void ClearLastLoaded()
        {
            while (this.disposeData.Count > 0)
            {
                BuffersData data = this.disposeData[0];
                this.disposeData.RemoveAt(0);
                data.VertexBuffer.Dispose();
                data.VertexBuffer = null;
                data.IndexBuffer.Dispose();
                data.IndexBuffer = null;
            }
        }

        /// <summary>
        /// First step in quad tree update.
        /// This method updates the quads of each vertex.
        /// </summary>
        private void UpdateQuadVertices()
        {
            for (int i = 0; i < this.Children.Length; i++)
            {
                this.Children[i].Update();
            }
        }

        /// <summary>
        /// Second step in quad tree update.
        /// This method getd all enabled vertices for all sub quad nodes and builds two lists of vertices and and indices.
        /// </summary>
        private void BuildQuadVerticesList()
        {
            this.Vertices.Clear(1000);
            this.Indexes.Clear(1000);

            for (int i = 0; i < this.Children.Length; i++)
            {
                this.Children[i].GetEnabledVertices();
            }

            if (!this.Device.IsDisposed)
            {
                IndexBuffer indexBuffer;
                VertexBuffer vertexBuffer;

                vertexBuffer = new VertexBuffer(this.Device, typeof(VertexPositionNormalMultipleTexture), this.Vertices.Count, BufferUsage.WriteOnly);
                vertexBuffer.SetData<VertexPositionNormalMultipleTexture>(this.Vertices.ToArray());

                indexBuffer = new IndexBuffer(this.Device, typeof(int), this.Indexes.Count, BufferUsage.WriteOnly);
                indexBuffer.SetData<int>(this.Indexes.ToArray());

                BuffersData data = new BuffersData();
                data.IndexBuffer = indexBuffer;
                data.VertexBuffer = vertexBuffer;
                data.NumberOfIndexes = this.Indexes.Count / 3;
                data.NumberOfVertices = this.Vertices.Count;

                this.lastLoadedData.Add(data);
            }
        }
    }
}
