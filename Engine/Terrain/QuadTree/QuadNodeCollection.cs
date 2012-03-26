namespace MMO3D.Engine
{
    using System;

    /// <summary>
    /// Represents a collection of <see cref="QuadNode"/>s.
    /// </summary>
    public class QuadNodeCollection : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the QuadNodeCollection class.
        /// </summary>
        public QuadNodeCollection()
        {
            this.Children = new QuadNode[this.ChildCount];
        }

        /// <summary>
        /// Gets the number of children contained in the collection.
        /// </summary>
        /// <value>See summary.</value>
        public virtual int ChildCount
        {
            get { return 4; }
        }

        /// <summary>
        /// Gets the <see cref="QuadNode"/>s contained in the collection.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode[] Children
        {
            get;
            private set;
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
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (int i = 0; i < QuadTree.NodeChildrenCount; i++)
                {
                    if (this.Children[i] != null)
                    {
                        this.Children[i].Dispose(disposing);
                    }
                }
            }
        }
    }
}
