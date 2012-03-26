namespace MMO3D.Engine
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// TODO: Experimental QuadTerrain class.
    /// </summary>
    public sealed class QuadTerrain : IDisposable
    {
        /// <summary>
        /// Thread to update the terrain's QuadTree asynchronously.
        /// </summary>
        private BackgroundWorker terrainUpdater;

        /// <summary>
        /// Initializes a new instance of the QuadTerrain class.
        /// </summary>
        public QuadTerrain()
        {
            throw new NotImplementedException("This class is only experimental and is not yet ready for use.");
        }

        /// <summary>
        /// Gets the list of QuadTrees that make up the terrain.
        /// </summary>
        /// <value>See summary.</value>
        public Vector<QuadTree> QuadTrees
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the quadtree terrain.
        /// </summary>
        public void InitializeQuadTree()
        {
            (EngineManager.Engine.CurrentCamera as ChaseCamera).HeightDifference = 200;
            (EngineManager.Engine.CurrentCamera as ChaseCamera).CameraDistance = 200;
            EngineManager.Engine.CurrentCamera.Update();

            this.QuadTrees = new Vector<QuadTree>(10);

            int landSize = 16;
            QuadTree quadTree = new QuadTree(8, (landSize * landSize) / 2, Vector2.Zero);
            quadTree.HeightData = new float[landSize, landSize];

            this.QuadTrees.Add(quadTree);

            for (int i = 0; i < this.QuadTrees.Count; i++)
            {
                this.QuadTrees[i].Initialize();
            }

            for (int i = 0; i < this.QuadTrees.Count; i++)
            {
                this.QuadTrees[i].Load(EngineManager.Engine.GraphicsDevice, new MultipleTextureTerrainEffect(EngineManager.Engine.GraphicsDevice) { Texture = TerrainType.Grass.GetTexture() });
            }

            this.terrainUpdater = new BackgroundWorker();
            this.terrainUpdater.WorkerSupportsCancellation = true;
            this.terrainUpdater.DoWork += new DoWorkEventHandler(this.TerrainUpdater_DoWork);
            this.terrainUpdater.RunWorkerAsync();
        }

        /// <summary>
        /// Renders the terrain.
        /// </summary>
        public void Render()
        {
            for (int i = 0; i < this.QuadTrees.Count; i++)
            {
                this.QuadTrees[i].Render();
            }
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
                this.terrainUpdater.CancelAsync();

                while (this.terrainUpdater.IsBusy)
                {
                    Thread.Sleep(1);
                }

                this.terrainUpdater.Dispose();
            }
        }

        /// <summary>
        /// Updates the terrain.
        /// </summary>
        private void Update()
        {
            for (int i = 0; i < this.QuadTrees.Count; i++)
            {
                this.QuadTrees[i].Update();
            }
        }

        /// <summary>
        /// Updates the QuadTree of the terrain asynchronously.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TerrainUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!this.terrainUpdater.CancellationPending)
            {
                this.Update();
            }
        }
    }
}
