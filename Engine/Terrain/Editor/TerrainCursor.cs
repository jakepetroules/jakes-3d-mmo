namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;

    /// <summary>
    /// Represents a 3D cursor used to manipulate terrain.
    /// </summary>
    public sealed class TerrainCursor
    {
        /// <summary>
        /// An empty read-only collection of VertexProperties structs.
        /// </summary>
        private static readonly ReadOnlyCollection<VertexProperties> EmptyReadOnlyCollection = new ReadOnlyCollection<VertexProperties>(new Collection<VertexProperties>());

        /// <summary>
        /// The diameter (if even) of the cursor in units,
        /// or (if odd), the maximum number of vertices it selects
        /// along either axis.
        /// </summary>
        private int size;

        /// <summary>
        /// Initializes a new instance of the TerrainCursor class.
        /// </summary>
        public TerrainCursor()
        {
            this.Size = 1;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the cursor should be shown.
        /// </summary>
        /// <value>See summary.</value>
        public bool Show
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the position of the cursor in world space.
        /// </summary>
        /// <value>See summary.</value>
        public Vector3 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the diameter (if even) of the cursor in units,
        /// or (if odd), the maximum number of vertices it selects
        /// along either axis. If the value is less than zero, it will
        /// be adjusted to zero.
        /// </summary>
        /// <value>See summary.</value>
        public int Size
        {
            get
            {
                return this.size;
            }

            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.size = value;
            }
        }

        /// <summary>
        /// Gets a collection of vertices along with the new heights and textures to set on them.
        /// </summary>
        /// <param name="terrain">A reference to the terrain manager; needed to find existing data in the terrain (for Flatten and Smooth).</param>
        /// <param name="cursorSize">The diameter of the cursor in units.</param>
        /// <param name="cursorLocation">The location of the cursor in world space.</param>
        /// <param name="tool">The tool being used to manipulate the terrain.</param>
        /// <param name="intensity">
        /// The intensity of the current operation. For raising or lowering height,
        /// this is the <em>change</em> in height of the <em>center</em> point.
        /// The height of other points is based off of this. For smoothing,
        /// this is the amount of smoothing to apply; the value will be clamped
        /// into the range zero to one. For Flatten and Texture, this is ignored.
        /// </param>
        /// <param name="terrainType">The type of terrain to set all points to, <em>if</em> the tool is texture.</param>
        /// <returns>See summary.</returns>
        public static ReadOnlyCollection<VertexProperties> GetVertexProperties(TerrainManager terrain, int cursorSize, Point cursorLocation, TerrainControlTools tool, float intensity, TerrainType terrainType)
        {
            // If it's the select tool - which we shouldn't use with this anyways,
            // just throw back an empty collection
            if (tool.IsSet(TerrainControlTools.Select) && tool.IsNotSet(TerrainControlTools.TerrainType))
            {
                return TerrainCursor.EmptyReadOnlyCollection;
            }
            else
            {
                // This is the mutable collection to store our returned data;
                // we'll encapsulate in a read-only one later
                Collection<VertexProperties> newVertexProperties = new Collection<VertexProperties>();

                // Gets all the points selected by this cursor (by its position and size)
                ReadOnlyCollection<PointDistance> points = TerrainCursor.GetPointsInCircle(cursorLocation, cursorSize);

                // If the tool is Flatten or Smooth
                float[] heights = null;
                float heightsMode = float.NaN;
                float heightsAverage = float.NaN;
                float smoothingFactor = float.NaN;
                if (tool.IsSet(TerrainControlTools.Flatten) || tool.IsSet(TerrainControlTools.Smooth))
                {
                    heights = new float[points.Count];

                    for (int i = 0; i < points.Count; i++)
                    {
                        heights[i] = terrain.GetTerrainElevation(MathExtensions.PointToVector3(points[i].GlobalCoordinate));
                    }

                    if (tool.IsSet(TerrainControlTools.Flatten))
                    {
                        heightsMode = TerrainCursor.CalculateAverageMode(heights);
                    }
                    else if (tool.IsSet(TerrainControlTools.Smooth))
                    {
                        float sum = 0;
                        for (int i = 0; i < heights.Length; i++)
                        {
                            sum += heights[i];
                        }

                        heightsAverage = sum / heights.Length;

                        // We need to reverse the smoothing range - externally,
                        // 1 is smoothest (flat), 0 is roughest (no change),
                        // internally it's the opposite
                        smoothingFactor = Math.Abs(MathHelper.Clamp(intensity, 0, 1) - 1);
                    }
                }

                // Loop through all the points the cursor is selecting
                for (int i = 0; i < points.Count; i++)
                {
                    // Variables to store the height and terrain type of each vertex in the point
                    float? vertexHeight = null;
                    TerrainType? vertexTerrainType = tool.IsSet(TerrainControlTools.TerrainType) ? terrainType as Nullable<TerrainType> : null;

                    // If the tool is raising or lowering the terrain, we can do a linear
                    // height determination based on the distance of each point from the center point
                    if (tool.IsSet(TerrainControlTools.Raise) || tool.IsSet(TerrainControlTools.Lower))
                    {
                        // Set the height for the point based on its distance from the center point
                        int radius = cursorSize / 2;

                        // Prevent division by zero...
                        if (radius < 1)
                        {
                            radius = 1;
                        }

                        float distanceReversed = Math.Abs(points[i].Distance - radius);

                        // If the tool is Raise
                        vertexHeight = (distanceReversed / radius) * intensity;

                        // Switch the sign if the tool is Lower
                        if (tool.IsSet(TerrainControlTools.Lower))
                        {
                            vertexHeight = -vertexHeight;
                        }
                    }
                    else if (tool.IsSet(TerrainControlTools.Flatten))
                    {
                        // Simply set the vertex height to the average mode
                        vertexHeight = heightsMode - heights[i];
                    }
                    else if (tool.IsSet(TerrainControlTools.Smooth))
                    {
                        // If the user specified an intensity of zero, nothing changes
                        if (intensity <= 0)
                        {
                            return TerrainCursor.EmptyReadOnlyCollection;
                        }

                        // Calculate the point's height using our formula...
                        vertexHeight = heightsAverage + ((heights[i] - heightsAverage) * smoothingFactor) - heights[i];
                    }

                    newVertexProperties.Add(new VertexProperties(points[i].GlobalCoordinate, terrain.CurrentHeightLevel, vertexHeight, vertexTerrainType));
                }

                return new ReadOnlyCollection<VertexProperties>(newVertexProperties);
            }
        }

        /// <summary>
        /// Gets the collection of global points that make up a circle of the given diameter
        /// along with their distances from the center point.
        /// </summary>
        /// <param name="center">The global center point of the circle.</param>
        /// <param name="diameter">The diameter of the circle, in units.</param>
        /// <returns>See summary.</returns>
        /// <remarks>
        /// Diameter must be integer greater than or equal to 0.
        /// Though both odd and even numbers can be supplied, every
        /// odd number will return the same result as itself, minus 1.
        /// This can be looked at from the perspective that if you supply
        /// an odd number, you are specifying the maximum number of vertices
        /// along either axis that will be selected, and if you supply an even
        /// number, you are specifying the maximum distance in units along
        /// either axis, that will be selected. If an integer less than zero
        /// is supplied, it will be transformed into zero.
        /// </remarks>
        private static ReadOnlyCollection<PointDistance> GetPointsInCircle(Point center, int diameter)
        {
            // If the diameter is less than zero, just make it zero.
            if (diameter < 0)
            {
                diameter = 0;
            }

            // Integer dvision is used for a reason; see remarks
            int radius = diameter / 2;

            // The list of points composing the circle; we return this collection
            Collection<PointDistance> points = new Collection<PointDistance>();

            // Make a rectangle encapsulating the circle - the minimum number of points we need to check
            Rectangle rect = new Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            for (int i = rect.X; i <= rect.X + rect.Width; i++)
            {
                for (int j = rect.Y; j <= rect.Y + rect.Height; j++)
                {
                    Vector2 pt = new Vector2(i, j);

                    // Get the distance of this point, from the center point; this can be zero
                    // since one of the points we check will be the center point itself
                    float distance = Vector2.Distance(pt, MathExtensions.PointToVector2(center));

                    // If the distance is less than or equal to the radius,
                    // the point is part of (inside) the circle, so add it
                    // to the list of points composing the circle
                    if (distance <= radius)
                    {
                        points.Add(new PointDistance(MathExtensions.VectorToPoint(pt), distance));
                    }
                }
            }

            return new ReadOnlyCollection<PointDistance>(points);
        }

        /// <summary>
        /// Calculates the average mode of an array.
        /// </summary>
        /// <param name="array">The array to determine the average mode of.</param>
        /// <returns>See summary.</returns>
        private static float CalculateAverageMode(float[] array)
        {
            Dictionary<float, int> table = new Dictionary<float, int>();
            int count = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (!table.ContainsKey(array[i]))
                {
                    table.Add(array[i], 0);
                }

                table[array[i]]++;

                if (table[array[i]] > count)
                {
                    count = table[array[i]];
                }
            }

            Collection<float> modeValues = new Collection<float>();
            foreach (KeyValuePair<float, int> entry in table)
            {
                if (entry.Value >= count)
                {
                    modeValues.Add(entry.Key);
                }
            }

            float sum = 0;
            for (int i = 0; i < modeValues.Count; i++)
            {
                sum += modeValues[i];
            }

            return sum / modeValues.Count;
        }

        /// <summary>
        /// Encapsulates a world point, and its distance from the center point of a circle.
        /// </summary>
        /// <remarks>
        /// Note that the center point is not stored in the struct and must be remembered
        /// by the user of the struct. Since this is private, it should not be an issue.
        /// </remarks>
        private struct PointDistance
        {
            /// <summary>
            /// Initializes a new instance of the PointDistance struct.
            /// </summary>
            /// <param name="globalCoordinate">The global coordinate we are measuring the distance from the center, of.</param>
            /// <param name="distance">The distance of this world point, from the circle's center point.</param>
            public PointDistance(Point globalCoordinate, float distance)
                : this()
            {
                this.GlobalCoordinate = globalCoordinate;
                this.Distance = distance;
            }

            /// <summary>
            /// Gets or sets the global coordinate we are measuring the distance from the center, of.
            /// </summary>
            /// <value>See summary.</value>
            public Point GlobalCoordinate
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets or sets the distance of this world point, from the circle's center point.
            /// </summary>
            /// <value>See summary.</value>
            public float Distance
            {
                get;
                private set;
            }
        }
    }
}
