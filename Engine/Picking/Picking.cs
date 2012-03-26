namespace MMO3D.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Helper class containing methods to assist in picking.
    /// </summary>
    public static class Picking
    {
        /// <summary>
        /// Helper for drawing the outline of the triangle currently under the cursor.
        /// </summary>
        /// <param name="cam">The camera currently being used to view the 3D world.</param>
        /// <param name="pickedTriangle">The vertices of the triangle to draw.</param>
        public static void DrawPickedTriangle(Camera cam, VertexPositionColor[] pickedTriangle)
        {
            if (pickedTriangle != null && pickedTriangle.Length == 3)
            {
                BasicEffect effect = new BasicEffect(EngineManager.Engine.GraphicsDevice, null);
                effect.VertexColorEnabled = true;

                RenderState renderState = EngineManager.Engine.GraphicsDevice.RenderState;

                // Set line drawing renderstates. We disable backface culling
                // and turn off the depth buffer because we want to be able to
                // see the picked triangle outline regardless of which way it is
                // facing, and even if there is other geometry in front of it.
                renderState.FillMode = FillMode.WireFrame;
                renderState.CullMode = CullMode.None;
                renderState.DepthBufferEnable = false;

                // Activate the line drawing BasicEffect.
                effect.Projection = cam.ProjectionMatrix;
                effect.View = cam.ViewMatrix;

                effect.Begin();
                effect.CurrentTechnique.Passes[0].Begin();

                // Draw the triangle.
                EngineManager.Engine.GraphicsDevice.VertexDeclaration = new VertexDeclaration(EngineManager.Engine.GraphicsDevice, VertexPositionColor.VertexElements);

                EngineManager.Engine.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, pickedTriangle, 0, 1);

                effect.CurrentTechnique.Passes[0].End();
                effect.End();

                // Reset renderstates to their default values.
                renderState.FillMode = FillMode.Solid;
                renderState.CullMode = CullMode.CullCounterClockwiseFace;
                renderState.DepthBufferEnable = true;
            }
        }

        /// <summary>
        /// Checks whether a ray intersects a model. This method needs to access
        /// the model vertex data, so the model must have been built using the
        /// custom TrianglePickingProcessor provided as part of this sample.
        /// Returns the distance along the ray to the point of intersection, or null
        /// if there is no intersection.
        /// </summary>
        /// <param name="ray">The ray to use for intersection checking.</param>
        /// <param name="model">The model to check for intersection.</param>
        /// <param name="modelTransform">The model's world transformation matrix.</param>
        /// <param name="insideBoundingSphere">Whether the mouse is picking the object's bounding sphere.</param>
        /// <param name="vertex1">The location in world space, of the first point of the vertex being picked.</param>
        /// <param name="vertex2">The location in world space, of the second point of the vertex being picked.</param>
        /// <param name="vertex3">The location in world space, of the third point of the vertex being picked.</param>
        /// <returns>See summary.</returns>
        public static float? RayIntersectsModel(Ray ray, ExtendedModel model, Matrix modelTransform, out bool insideBoundingSphere, out Vector3 vertex1, out Vector3 vertex2, out Vector3 vertex3)
        {
            bool insideBoundingBox;
            return Picking.RayIntersectsAbstract(ray, null, model.GetVertices(), modelTransform, model.BoundingBox, model.BoundingSphere, out insideBoundingSphere, out insideBoundingBox, out vertex1, out vertex2, out vertex3);
        }

        /// <summary>
        /// Checks whether a ray intersects a terrain patch.
        /// Returns the distance along the ray to the point of intersection,
        /// or null if there is no intersection.
        /// </summary>
        /// <param name="ray">The ray to use for intersection checking.</param>
        /// <param name="patch">The model to check for intersection.</param>
        /// <param name="insideBoundingBox">Whether the mouse is picking the object's bounding box.</param>
        /// <param name="vertex1">The location in world space, of the first point of the vertex being picked.</param>
        /// <param name="vertex2">The location in world space, of the second point of the vertex being picked.</param>
        /// <param name="vertex3">The location in world space, of the third point of the vertex being picked.</param>
        /// <returns>See summary.</returns>
        public static float? RayIntersectsTerrainPatch(Ray ray, TerrainPatch patch, out bool insideBoundingBox, out Vector3 vertex1, out Vector3 vertex2, out Vector3 vertex3)
        {
            bool insideBoundingSphere;
            return Picking.RayIntersectsAbstract(ray, patch.GetIndexes(), patch.GetVertices(), patch.WorldMatrix, patch.BoundingBox, BoundingSphere.CreateFromPoints(patch.GetVertices()), out insideBoundingSphere, out insideBoundingBox, out vertex1, out vertex2, out vertex3);
        }

        /// <summary>
        /// Checks whether a ray intersects an object. Returns the distance along
        /// the ray to the point of intersection, or null if there is no intersection.
        /// </summary>
        /// <param name="ray">The ray to use for intersection checking.</param>
        /// <param name="indexes">The indices of the object. This can be null.</param>
        /// <param name="vertices">The vertices composing the object.</param>
        /// <param name="worldTransform">The world transformation matrix of the object.</param>
        /// <param name="boundingBox">The bounding box containing the object.</param>
        /// <param name="boundingSphere">The bounding sphere containing the object.</param>
        /// <param name="insideBoundingSphere">Whether the mouse is picking the object's bounding sphere.</param>
        /// <param name="insideBoundingBox">Whether the mouse is picking the object's bounding box.</param>
        /// <param name="vertex1">The location in world space, of the first point of the vertex being picked.</param>
        /// <param name="vertex2">The location in world space, of the second point of the vertex being picked.</param>
        /// <param name="vertex3">The location in world space, of the third point of the vertex being picked.</param>
        /// <returns>See summary.</returns>
        public static float? RayIntersectsAbstract(Ray ray, int[] indexes, Vector3[] vertices, Matrix worldTransform, BoundingBox boundingBox, BoundingSphere boundingSphere, out bool insideBoundingSphere, out bool insideBoundingBox, out Vector3 vertex1, out Vector3 vertex2, out Vector3 vertex3)
        {
            vertex1 = Vector3.Zero;
            vertex2 = Vector3.Zero;
            vertex3 = Vector3.Zero;

            // The input ray is in world space, but our model data is stored in object
            // space. We would normally have to transform all the model data by the
            // modelTransform matrix, moving it into world space before we test it
            // against the ray. That transform can be slow if there are a lot of
            // triangles in the model, however, so instead we do the opposite.
            // Transforming our ray by the inverse modelTransform moves it into object
            // space, where we can test it directly against our model data. Since there
            // is only one ray but typically many triangles, doing things this way
            // around can be much faster.
            Matrix inverseTransform = Matrix.Invert(worldTransform);

            ray.Position = Vector3.Transform(ray.Position, inverseTransform);
            ray.Direction = Vector3.TransformNormal(ray.Direction, inverseTransform);

            // Start off with a fast bounding sphere test.
            if (boundingSphere != null)
            {
                if (boundingSphere.Intersects(ray) == null)
                {
                    // If the ray does not intersect the bounding sphere, we cannot
                    // possibly have picked this model, so there is no need to even
                    // bother looking at the individual triangle data.
                    insideBoundingSphere = false;
                    insideBoundingBox = false;

                    return null;
                }
            }

            if (boundingBox != null)
            {
                if (boundingBox.Intersects(ray) == null)
                {
                    insideBoundingBox = false;
                    insideBoundingSphere = false;

                    return null;
                }
            }

            // The bounding sphere test passed, so we need to do a full
            // triangle picking test.
            insideBoundingSphere = true;
            insideBoundingBox = true;

            // Keep track of the closest triangle we found so far,
            // so we can always return the closest one.
            float? closestIntersection = null;

            if (indexes != null)
            {
                for (int i = 0; i < indexes.Length / 3; i++)
                {
                    // Perform a ray to triangle intersection test.
                    float? intersection;

                    Picking.RayIntersectsTriangle(ref ray, ref vertices[indexes[i]], ref vertices[indexes[i + 1]], ref vertices[indexes[i + 2]], out intersection);

                    // Does the ray intersect this triangle?
                    if (intersection != null)
                    {
                        // If so, is it closer than any other previous triangle?
                        if ((closestIntersection == null) || (intersection < closestIntersection))
                        {
                            // Store the distance to this triangle.
                            closestIntersection = intersection;

                            // Transform the three vertex positions into world space,
                            // and store them into the output vertex parameters.
                            Vector3.Transform(ref vertices[indexes[i]], ref worldTransform, out vertex1);
                            Vector3.Transform(ref vertices[indexes[i + 1]], ref worldTransform, out vertex2);
                            Vector3.Transform(ref vertices[indexes[i + 2]], ref worldTransform, out vertex3);
                        }
                    }
                }
            }
            else
            {
                // Loop over the vertex data, 3 at a time (3 vertices = 1 triangle).
                for (int i = 0; i < vertices.Length; i += 3)
                {
                    // Perform a ray to triangle intersection test.
                    float? intersection;

                    Picking.RayIntersectsTriangle(ref ray, ref vertices[i], ref vertices[i + 1], ref vertices[i + 2], out intersection);

                    // Does the ray intersect this triangle?
                    if (intersection != null)
                    {
                        // If so, is it closer than any other previous triangle?
                        if ((closestIntersection == null) || (intersection < closestIntersection))
                        {
                            // Store the distance to this triangle.
                            closestIntersection = intersection;

                            // Transform the three vertex positions into world space,
                            // and store them into the output vertex parameters.
                            Vector3.Transform(ref vertices[i], ref worldTransform, out vertex1);
                            Vector3.Transform(ref vertices[i + 1], ref worldTransform, out vertex2);
                            Vector3.Transform(ref vertices[i + 2], ref worldTransform, out vertex3);
                        }
                    }
                }
            }

            return closestIntersection;
        }

        /// <summary>
        /// Checks whether a ray intersects a triangle. This uses the algorithm
        /// developed by Tomas Moller and Ben Trumbore, which was published in the
        /// Journal of Graphics Tools, volume 2, "Fast, Minimum Storage Ray-Triangle
        /// Intersection".
        /// <para />
        /// This method is implemented using the pass-by-reference versions of the
        /// XNA math functions. Using these overloads is generally not recommended,
        /// because they make the code less readable than the normal pass-by-value
        /// versions. This method can be called very frequently in a tight inner loop,
        /// however, so in this particular case the performance benefits from passing
        /// everything by reference outweigh the loss of readability.
        /// </summary>
        /// <param name="ray">The ray to use for intersection checking.</param>
        /// <param name="vertex1">The location in world space, of the first point of the vertex being picked.</param>
        /// <param name="vertex2">The location in world space, of the second point of the vertex being picked.</param>
        /// <param name="vertex3">The location in world space, of the third point of the vertex being picked.</param>
        /// <param name="result">The distance along the ray to the point of intersection, or null if there is no intersection.</param>
        public static void RayIntersectsTriangle(ref Ray ray, ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, out float? result)
        {
            // Compute vectors along two edges of the triangle.
            Vector3 edge1, edge2;

            Vector3.Subtract(ref vertex2, ref vertex1, out edge1);
            Vector3.Subtract(ref vertex3, ref vertex1, out edge2);

            // Compute the determinant.
            Vector3 directionCrossEdge2;
            Vector3.Cross(ref ray.Direction, ref edge2, out directionCrossEdge2);

            float determinant;
            Vector3.Dot(ref edge1, ref directionCrossEdge2, out determinant);

            // If the ray is parallel to the triangle plane, there is no collision.
            if (determinant > -float.Epsilon && determinant < float.Epsilon)
            {
                result = null;
                return;
            }

            float inverseDeterminant = 1.0f / determinant;

            // Calculate the U parameter of the intersection point.
            Vector3 distanceVector;
            Vector3.Subtract(ref ray.Position, ref vertex1, out distanceVector);

            float triangleU;
            Vector3.Dot(ref distanceVector, ref directionCrossEdge2, out triangleU);
            triangleU *= inverseDeterminant;

            // Make sure it is inside the triangle.
            if (triangleU < 0 || triangleU > 1)
            {
                result = null;
                return;
            }

            // Calculate the V parameter of the intersection point.
            Vector3 distanceCrossEdge1;
            Vector3.Cross(ref distanceVector, ref edge1, out distanceCrossEdge1);

            float triangleV;
            Vector3.Dot(ref ray.Direction, ref distanceCrossEdge1, out triangleV);
            triangleV *= inverseDeterminant;

            // Make sure it is inside the triangle.
            if (triangleV < 0 || triangleU + triangleV > 1)
            {
                result = null;
                return;
            }

            // Compute the distance along the ray to the triangle.
            float rayDistance;
            Vector3.Dot(ref edge2, ref distanceCrossEdge1, out rayDistance);
            rayDistance *= inverseDeterminant;

            // Is the triangle behind the ray origin?
            if (rayDistance < 0)
            {
                result = null;
                return;
            }

            result = rayDistance;
        }

        /// <summary>
        /// Gets the position in world space from the cursor's position.
        /// </summary>
        /// <param name="cam">The camera currently being used to view the 3D world.</param>
        /// <returns>See summary.</returns>
        public static Vector3 GetPickedPosition(Camera cam, Point mousePosition)
        {
            Vector3 nearPoint = Picking.GetNearPoint(cam, mousePosition);
            Vector3 farPoint = Picking.GetFarPoint(cam, mousePosition);

            // Create a ray from the near clip plane to the far clip plane and normalize it...
            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            // And then create a new ray using nearPoint as the source.
            Ray r = new Ray(nearPoint, direction);

            // calculate the ray-plane intersection point
            Plane p = new Plane(Vector3.UnitZ, 0f);

            // calculate distance of intersection point from r.origin
            float denominator = Vector3.Dot(p.Normal, r.Direction);
            float numerator = Vector3.Dot(p.Normal, r.Position) + p.D;
            float t = -(numerator / denominator);

            // calculate the picked position on the z = 0 plane
            Vector3 pickedPosition = nearPoint + (direction * t);

            return pickedPosition;
        }

        /// <summary>
        /// Gets a ray starting at the camera's "eye" and pointing in
        /// the direction of the cursor to use in picking the object.
        /// </summary>
        /// <param name="cam">The camera currently being used to view the 3D world.</param>
        /// <returns>See summary.</returns>
        public static Ray GetPickRay(Camera cam, Point mousePosition)
        {
            Vector3 nearPoint = Picking.GetNearPoint(cam, mousePosition);
            Vector3 farPoint = Picking.GetFarPoint(cam, mousePosition);

            // Create a ray from the near clip plane to the far clip plane and normalize it...
            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            // And then create a new ray using nearPoint as the source.
            return new Ray(nearPoint, direction);
        }

        /// <summary>
        /// Gets a position in world space as close as possible to the camera.
        /// </summary>
        /// <param name="cam">The camera currently being used to view the 3D world.</param>
        /// <returns>See summary.</returns>
        private static Vector3 GetNearPoint(Camera cam, Point mousePosition)
        {
            // Create a position in screenspace using the cursor position. 0 is as
            // close as possible to the camera, 1 is as far away as possible.
            Vector3 nearSource = new Vector3(mousePosition.X, mousePosition.Y, 0);

            // Use Viewport.Unproject to tell what that screen space position
            // would be in world space. We'll need the projection matrix and view
            // matrix, which we can get from the camera object. We also need a world
            // matrix, which can just be identity.
            return EngineManager.Engine.GraphicsDevice.Viewport.Unproject(nearSource, cam.ProjectionMatrix, cam.ViewMatrix, Matrix.Identity);
        }

        /// <summary>
        /// Gets a position in world space as far away as possible from the camera.
        /// </summary>
        /// <param name="cam">The camera currently being used to view the 3D world.</param>
        /// <returns>See summary.</returns>
        private static Vector3 GetFarPoint(Camera cam, Point mousePosition)
        {
            // Create a position in screenspace using the cursor position. 0 is as
            // close as possible to the camera, 1 is as far away as possible.
            Vector3 farSource = new Vector3(mousePosition.X, mousePosition.Y, 1);

            // Use Viewport.Unproject to tell what that screen space position
            // would be in world space. We'll need the projection matrix and view
            // matrix, which we can get from the camera object. We also need a world
            // matrix, which can just be identity.
            return EngineManager.Engine.GraphicsDevice.Viewport.Unproject(farSource, cam.ProjectionMatrix, cam.ViewMatrix, Matrix.Identity);
        }
    }
}
