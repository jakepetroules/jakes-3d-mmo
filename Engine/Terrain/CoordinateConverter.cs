namespace MMO3D.Engine
{
    using System.Collections.ObjectModel;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;

    /// <summary>
    /// Provides methods to convert between different types of terrain coordinates - world, patch local, and vertex.
    /// </summary>
    /// <remarks>
    /// Definitions of each coordinate type are as follows:
    /// <para />
    /// <strong>World coordinates:</strong>
    /// These coordinates are the ones defining GLOBAL space, used to create world transformation matrices.
    /// <para />
    /// <strong>Local coordinates:</strong>
    /// These coordinates are local to a terrain patch, on either axis, they start at 0 and go up to the
    /// size of a terrain patch minus 1. They are calculated from the world coordinates by subtracting the
    /// greatest multiple of the terrain patch size that is less than the world coordinates, from the world
    /// coordinates. The remainder of this calculation results in the local coordinates. For example, the
    /// world coordinates 130, 129 would be the local coordinates 2, 1 of the terrain patch 1, 1.
    /// <strong>Vertex coordinates:</strong>
    /// These coordinates are like local coordinates, but are integers only and correspond to the VERTICES
    /// of the terrain patch, hence the name. Therefore, they can start at zero and go up to the size of
    /// the terrain patch, rather than the size of the terrain patch, minus 1. Up to four vertex coordinates
    /// will share a world coordinate.
    /// </remarks>
    public static class CoordinateConverter
    {
        /// <summary>
        /// Transforms global coordinates to patch local coordinates.
        /// </summary>
        /// <param name="position">The position to convert.</param>
        /// <returns>See summary.</returns>
        public static Vector2 WorldToLocal(Vector3 position)
        {
            return MathExtensions.Modulate(new Vector2(position.X, position.Y), TerrainPatch.PatchSize);
        }

        /// <summary>
        /// Transforms global coordinates to patch local coordinates.
        /// </summary>
        /// <param name="position">The position to convert.</param>
        /// <returns>See summary.</returns>
        public static Point WorldToLocal(Point position)
        {
            return MathExtensions.VectorToPoint(MathExtensions.Modulate(new Vector2(position.X, position.Y), TerrainPatch.PatchSize));
        }

        /// <summary>
        /// Gets a collection of patch vertex coordinates from a local position.
        /// Usually, there will only be one vertex coordinate returned, however if
        /// the local X coordinate OR local Y coordinate evaluate to zero, two vertex
        /// coordinates will be returned, and if both the local X AND Y coordinates
        /// evaluate to zero, four vertex coordinates will be returned.
        /// </summary>
        /// <param name="patchId">The patch ID that the local coordinates belong to.</param>
        /// <param name="local">The local coordinates to get the vertex coordinates of.</param>
        /// <returns>See summary.</returns>
        public static ReadOnlyCollection<TerrainPatchVertexCoordinates> LocalToVertex(Point3D patchId, Point local)
        {
            return CoordinateConverter.WorldToVertex(MathExtensions.PointToVector3(CoordinateConverter.LocalToWorld(patchId, local)));
        }

        /// <summary>
        /// Gets a collection of patch vertex coordinates from a global position.
        /// Usually, there will only be one vertex coordinate returned, however if
        /// the local X coordinate OR local Y coordinate evaluate to zero, two vertex
        /// coordinates will be returned, and if both the local X AND Y coordinates
        /// evaluate to zero, four vertex coordinates will be returned.
        /// </summary>
        /// <param name="position">The global position to get the patch local vertex coordinates of.</param>
        /// <returns>See summary.</returns>
        public static ReadOnlyCollection<TerrainPatchVertexCoordinates> WorldToVertex(Vector3 position)
        {
            return CoordinateConverter.WorldToVertex(position, false);
        }

        /// <summary>
        /// Gets a collection of patch vertex coordinates from a global position.
        /// Usually, there will only be one vertex coordinate returned, however if
        /// the local X coordinate OR local Y coordinate evaluate to zero, two vertex
        /// coordinates will be returned, and if both the local X AND Y coordinates
        /// evaluate to zero, four vertex coordinates will be returned.
        /// </summary>
        /// <param name="position">The global position to get the patch local vertex coordinates of.</param>
        /// <param name="includeOffMap">
        /// Whether to include points that are off the map.
        /// For example if the coordinates are 5,5 on one patch,
        /// coordinates from the southwest patch (133,133) will
        /// be returned although they technically do not exist.
        /// This is needed for the terrain cursor to be drawn properly.
        /// This parameter defaults to false.
        /// </param>
        /// <returns>See summary.</returns>
        public static ReadOnlyCollection<TerrainPatchVertexCoordinates> WorldToVertex(Vector3 position, bool includeOffMap)
        {
            Collection<TerrainPatchVertexCoordinates> coordCollection = new Collection<TerrainPatchVertexCoordinates>();

            // Get the patch ID and base local coordinates
            Point3D patchID = TerrainManager.GetTerrainPatchId(position, EngineManager.Engine.Terrain.CurrentHeightLevel);
            Point local = MathExtensions.VectorToPoint(CoordinateConverter.WorldToLocal(position));

            // Add the current patch's vertex coordinate (the base local coordinates)
            coordCollection.Add(new TerrainPatchVertexCoordinates(patchID, local));

            // Get position of the patch directly west
            if (local.X == 0 || includeOffMap)
            {
                coordCollection.Add(new TerrainPatchVertexCoordinates(new Point3D(patchID.X - 1, patchID.Y, patchID.Z), new Point(TerrainPatch.PatchSize + local.X, local.Y)));
            }

            // Get position of the patch directly south
            if (local.Y == 0 || includeOffMap)
            {
                coordCollection.Add(new TerrainPatchVertexCoordinates(new Point3D(patchID.X, patchID.Y - 1, patchID.Z), new Point(local.X, TerrainPatch.PatchSize + local.Y)));
            }

            // Get position of the patch directly south-west
            if ((local.X == 0 && local.Y == 0) || includeOffMap)
            {
                coordCollection.Add(new TerrainPatchVertexCoordinates(new Point3D(patchID.X - 1, patchID.Y - 1, patchID.Z), new Point(TerrainPatch.PatchSize + local.X, TerrainPatch.PatchSize + local.Y)));
            }

            return new ReadOnlyCollection<TerrainPatchVertexCoordinates>(coordCollection);
        }

        /// <summary>
        /// Translates patch local coordinates into global, world coordinates.
        /// </summary>
        /// <param name="patchId">The ID of the patch whose local coordinates we are passing.</param>
        /// <param name="localPosition">The local position to translate.</param>
        /// <returns>See summary.</returns>
        public static Point LocalToWorld(Point3D patchId, Point localPosition)
        {
            return new Point((patchId.X * TerrainPatch.PatchSize) + localPosition.X, (patchId.Y * TerrainPatch.PatchSize) + localPosition.Y);
        }
    }
}
