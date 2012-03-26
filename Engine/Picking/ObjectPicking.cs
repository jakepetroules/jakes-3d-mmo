namespace MMO3D.Engine
{
    using System.Collections.ObjectModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Manages picking of 3D objects.
    /// </summary>
    public sealed class ObjectPicking
    {
        /// <summary>
        /// The objects currently being picked.
        /// </summary>
        private Collection<GameObjectBase> pickedObjects = new Collection<GameObjectBase>();

        /// <summary>
        /// The triangle that was picked.
        /// </summary>
        private VertexPositionColor[] pickedTriangle;

        /// <summary>
        /// Whether a triangle was picked as of the last operation.
        /// </summary>
        private bool picked;

        /// <summary>
        /// Initializes a new instance of the ObjectPicking class.
        /// </summary>
        public ObjectPicking()
        {
            this.DrawColor = Color.Magenta;
        }

        /// <summary>
        /// Gets or sets the color to use to draw picked triangles.
        /// </summary>
        /// <value>See summary.</value>
        public Color DrawColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use triangle precision.
        /// </summary>
        /// <value>See summary.</value>
        public bool Precision
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a list of objects currently being picked by the mouse.
        /// </summary>
        /// <param name="camera">The camera currently being used to view the 3D world.</param>
        /// <returns>See summary.</returns>
        public ReadOnlyCollection<GameObjectBase> GetPickedObjects(Camera camera, Point mousePosition)
        {
            this.pickedObjects.Clear();
            ReadOnlyCollection<GameObjectBase> modelsToCheck = EngineManager.Engine.AllGameObjects;
            Ray cursorRay = Picking.GetPickRay(camera, mousePosition);

            // Keep track of the closest object we have seen so far, so we can
            // choose the closest one if there are several models under the cursor.
            float closestIntersection = float.MaxValue;

            this.picked = false;

            // Loop over all our models.
            for (int i = 0; i < modelsToCheck.Count; i++)
            {
                bool insideBoundingSphere;
                Vector3 vertex1;
                Vector3 vertex2;
                Vector3 vertex3;

                // Objects with null models are not included in the search
                if (modelsToCheck[i].Model == null)
                {
                    continue;
                }

                // Perform the ray to model intersection test.
                float? intersection = Picking.RayIntersectsModel(cursorRay, modelsToCheck[i].Model, modelsToCheck[i].GetWorldTransform(), out insideBoundingSphere, out vertex1, out vertex2, out vertex3);

                // If this model passed the initial bounding sphere test, remember
                // that so we can display it at the top of the screen
                if (insideBoundingSphere)
                {
                    // We don't want to add models if we picked the bounding sphere
                    // unless we're NOT using precision picking
                    if (!this.Precision)
                    {
                        this.pickedObjects.Add(modelsToCheck[i]);
                    }
                }

                if (this.Precision)
                {
                    // Do we have a per-triangle intersection with this model?
                    if (intersection != null)
                    {
                        // If so, is it closer than any other model we might have
                        // previously intersected?
                        if (intersection < closestIntersection)
                        {
                            // Store information about this model
                            closestIntersection = intersection.Value;

                            // If the model's already in the collection of picked models, remove it temporarily
                            if (this.pickedObjects.Contains(modelsToCheck[i]))
                            {
                                this.pickedObjects.Remove(modelsToCheck[i]);
                            }

                            // Put this model back in, but at the top
                            this.pickedObjects.Insert(0, modelsToCheck[i]);

                            // Store vertex positions so we can display the picked triangle
                            this.pickedTriangle = new VertexPositionColor[]
                        {
                            new VertexPositionColor(vertex1, this.DrawColor),
                            new VertexPositionColor(vertex2, this.DrawColor),
                            new VertexPositionColor(vertex3, this.DrawColor)
                        };

                            this.picked = true;
                        }
                        else
                        {
                            this.pickedObjects.Add(modelsToCheck[i]);
                        }
                    }
                }
            }

            return new ReadOnlyCollection<GameObjectBase>(this.pickedObjects);
        }

        /// <summary>
        /// Helper for drawing the outline of the triangle currently under the cursor.
        /// </summary>
        /// <param name="cam">The camera currently being used to view the 3D world.</param>
        public void DrawPickedTriangle(Camera cam)
        {
            if (this.picked)
            {
                Picking.DrawPickedTriangle(cam, this.pickedTriangle);
            }
        }
    }
}
