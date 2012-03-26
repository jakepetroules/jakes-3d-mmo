namespace MMO3D.Engine
{
    using System;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines a QuadNode, which is a leaf or branch inside the <see cref="QuadTree"/>.
    /// It shares vertices with its <see cref="QuadNode"/> neighbor.
    /// </summary>
    [DebuggerDisplay("Depth: {Depth} at {Location}, {Position} of parent and {EnabledVertices} enabled.")]
    public sealed class QuadNode : QuadNodeCollection, IDisposable
    {
        /// <summary>
        /// The number of sides for a <see cref="QuadNode"/>.
        /// </summary>
        /// <remarks>Sides are Center, West, North, East, South.</remarks>
        public const int SidesNumber = 5;

        /// <summary>
        /// The maximum number of vertices for a <see cref="QuadNode"/>.
        /// </summary>
        /// <remarks>Vertices are located on Center, West, North West, North, North East, East, South East, South, South West.</remarks>
        public const int VerticesNumber = 9;

        /// <summary>
        /// The current index we are using in the <see cref="FillWithVertex"/> method.
        /// </summary>
        private static int currentIndice = 0;

        /// <summary>
        /// The terrain primitive we are using in the <see cref="FillWithVertex"/> method.
        /// </summary>
        private TerrainPrimitive terrainPrimitive;

        /// <summary>
        /// The bounding box of the northwest child node.
        /// </summary>
        private BoundingBox boundingBoxNorthWestChild;

        /// <summary>
        /// The bounding box of the northeast child node.
        /// </summary>
        private BoundingBox boundingBoxNorthEastChild;

        /// <summary>
        /// The bounding box of the southwest child node.
        /// </summary>
        private BoundingBox boundingBoxSouthWestChild;

        /// <summary>
        /// The bounding box of the southeast child node.
        /// </summary>
        private BoundingBox boundingBoxSouthEastChild;

        /// <summary>
        /// The dot product of the northwest child node.
        /// </summary>
        private float dotNorthWestChild;

        /// <summary>
        /// The dot product of the southwest child node.
        /// </summary>
        private float dotSouthWestChild;

        /// <summary>
        /// The dot product of the southeast child node.
        /// </summary>
        private float dotSouthEastChild;

        /// <summary>
        /// The dot product of the northeast child node.
        /// </summary>
        private float dotNorthEastChild;

        /// <summary>
        /// The interpolated vertex heights.
        /// </summary>
        private float[] realToInterpolatedVertexHeight;

        /// <summary>
        /// Initializes a new instance of the QuadNode class with the specified parent and position.
        /// </summary>
        /// <param name="parent">The parent node.</param>
        /// <param name="position">The node's position.</param>
        public QuadNode(QuadNode parent, NodeChild position)
        {
            // Array for neighbors at each side
            this.Neighbor = new QuadNode[4];

            // Array for all the nine vertices of the current node
            this.Vertices = new TerrainVertex[QuadNode.VerticesNumber];

            // The interpolated position difference with the real position
            this.realToInterpolatedVertexHeight = new float[QuadNode.SidesNumber];

            this.Parent = parent;
            this.Position = position;

            if (this.Parent == null)
            {
                this.Depth = 0;
            }
            else
            {
                this.Depth = Convert.ToByte(this.Parent.Depth + 1);
                this.ParentTree = this.Parent.ParentTree;
            }
        }

        /// <summary>
        /// Gets the enabled vertices.
        /// </summary>
        /// <value>See summary.</value>
        public NodeContents EnabledContent
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="QuadNode"/> is fully relevant and does not need to be split.
        /// </summary>
        /// <value>See summary.</value>
        public bool IsFullRelevant
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets a named position for the current <see cref="QuadNode"/>.
        /// </summary>
        /// <value>See summary.</value>
        public NodeChild Position
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets all of the current <see cref="QuadNode"/>'s neighbors.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode[] Neighbor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets all the vertices for the current <see cref="QuadNode"/>.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex[] Vertices
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the depth of the current <see cref="QuadNode"/> inside the <see cref="QuadTree"/>.
        /// </summary>
        /// <value>See summary.</value>
        /// <remarks>0 indicates the root, and this.ParentTree.Depth the value for the leaf.</remarks>
        public byte Depth
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the location of the <see cref="QuadNode"/>.
        /// </summary>
        /// <value>See summary.</value>
        /// <remarks>Vector2 for X and Y coordinates components. The Z value is computed with a call to the GetHeight method of the associated <see cref="QuadTree"/>.</remarks>
        public Vector2 Location
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="QuadNode"/> parent of the current node <see cref="QuadNode"/>.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode Parent
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the owner <see cref="QuadTree"/>.
        /// </summary>
        /// <value>See summary.</value>
        public QuadTree ParentTree
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the east neighbor.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode EastNeighbor
        {
            get
            {
                return this.Neighbor[(int)NodeSideVertex.East];
            }

            set
            {
                this.Neighbor[(int)NodeSideVertex.East] = value;
            }
        }

        /// <summary>
        /// Gets or sets the west neighbor.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode WestNeighbor
        {
            get
            {
                return this.Neighbor[(int)NodeSideVertex.West];
            }

            set
            {
                this.Neighbor[(int)NodeSideVertex.West] = value;
            }
        }

        /// <summary>
        /// Gets or sets the north neighbor.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode NorthNeighbor
        {
            get
            {
                return this.Neighbor[(int)NodeSideVertex.North];
            }

            set
            {
                this.Neighbor[(int)NodeSideVertex.North] = value;
            }
        }

        /// <summary>
        /// Gets or sets the south neighbor.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode SouthNeighbor
        {
            get
            {
                return this.Neighbor[(int)NodeSideVertex.South];
            }

            set
            {
                this.Neighbor[(int)NodeSideVertex.South] = value;
            }
        }

        /// <summary>
        /// Gets or sets the north west vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex NorthWestVertex
        {
            get
            {
                return this.Vertices[(int)NodeVertex.NorthWest];
            }

            set
            {
                this.Vertices[(int)NodeVertex.NorthWest] = value;
            }
        }

        /// <summary>
        /// Gets or sets the north east vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex NorthEastVertex
        {
            get
            {
                return this.Vertices[(int)NodeVertex.NorthEast];
            }

            set
            {
                this.Vertices[(int)NodeVertex.NorthEast] = value;
            }
        }

        /// <summary>
        /// Gets or sets the south west vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex SouthWestVertex
        {
            get
            {
                return this.Vertices[(int)NodeVertex.SouthWest];
            }

            set
            {
                this.Vertices[(int)NodeVertex.SouthWest] = value;
            }
        }

        /// <summary>
        /// Gets or sets the south east vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex SouthEastVertex
        {
            get
            {
                return this.Vertices[(int)NodeVertex.SouthEast];
            }

            set
            {
                this.Vertices[(int)NodeVertex.SouthEast] = value;
            }
        }

        /// <summary>
        /// Gets or sets the east vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex EastVertex
        {
            get
            {
                return this.Vertices[(int)NodeVertex.East];
            }

            set
            {
                this.Vertices[(int)NodeVertex.East] = value;
            }
        }

        /// <summary>
        /// Gets or sets the west vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex WestVertex
        {
            get
            {
                return this.Vertices[(int)NodeVertex.West];
            }

            set
            {
                this.Vertices[(int)NodeVertex.West] = value;
            }
        }

        /// <summary>
        /// Gets or sets the north vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex NorthVertex
        {
            get
            {
                return this.Vertices[(int)NodeVertex.North];
            }

            set
            {
                this.Vertices[(int)NodeVertex.North] = value;
            }
        }

        /// <summary>
        /// Gets or sets the south vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex SouthVertex
        {
            get
            {
                return this.Vertices[(int)NodeVertex.South];
            }

            set
            {
                this.Vertices[(int)NodeVertex.South] = value;
            }
        }

        /// <summary>
        /// Gets or sets the center vertex.
        /// </summary>
        /// <value>See summary.</value>
        public TerrainVertex CenterVertex
        {
            get
            {
                return this.Vertices[(int)NodeVertex.Center];
            }

            set
            {
                this.Vertices[(int)NodeVertex.Center] = value;
            }
        }

        /// <summary>
        /// Gets or sets the north west child.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode NorthWestChild
        {
            get
            {
                return this.Children[(int)NodeChild.Northwest];
            }

            set
            {
                this.Children[(int)NodeChild.Northwest] = value;
            }
        }

        /// <summary>
        /// Gets or sets the north east child.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode NorthEastChild
        {
            get
            {
                return this.Children[(int)NodeChild.Northeast];
            }

            set
            {
                this.Children[(int)NodeChild.Northeast] = value;
            }
        }

        /// <summary>
        /// Gets or sets the south west child.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode SouthWestChild
        {
            get
            {
                return this.Children[(int)NodeChild.Southwest];
            }

            set
            {
                this.Children[(int)NodeChild.Southwest] = value;
            }
        }

        /// <summary>
        /// Gets or sets the south east child.
        /// </summary>
        /// <value>See summary.</value>
        public QuadNode SouthEastChild
        {
            get
            {
                return this.Children[(int)NodeChild.Southeast];
            }

            set
            {
                this.Children[(int)NodeChild.Southeast] = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="QuadNode"/>'s size.
        /// </summary>
        /// <value>See summary.</value>
        public float NodeSize
        {
            get { return this.ParentTree.GetNodeSizeAtLevel(this.Depth); }
        }

        /// <summary>
        /// Gets a point in a bounding box closest to the specified point.
        /// </summary>
        /// <param name="box">The bounding box.</param>
        /// <param name="point">The point to check.</param>
        /// <returns>See summary.</returns>
        public static Vector3 GetBoundingBoxClosestPointToPoint(BoundingBox box, Vector3 point)
        {
            float x;
            float y;
            float z;

            if (point.X > box.Max.X)
            {
                x = box.Max.X;
            }
            else if (point.X < box.Min.X)
            {
                x = box.Min.X;
            }
            else
            {
                x = point.X;
            }

            if (point.Y > box.Max.Y)
            {
                y = box.Max.Y;
            }
            else if (point.Y < box.Min.Y)
            {
                y = box.Min.Y;
            }
            else
            {
                y = point.Y;
            }

            if (point.Z > box.Max.Z)
            {
                z = box.Max.Z;
            }
            else if (point.Z < box.Min.Z)
            {
                z = box.Min.Z;
            }
            else
            {
                z = point.Z;
            }

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Initializes the <see cref="QuadNode"/>.
        /// </summary>
        public void Initialize()
        {
            float size = this.NodeSize;

            float x = this.Location.X;
            float y = this.Location.Y;

            float wholeX = x + size;
            float wholeY = y + size;

            float halfX = x + (size / 2);
            float halfY = y + (size / 2);

            float heightXYWhole = this.ParentTree.GetHeight(x, wholeY);
            float heightXWholeYWhole = this.ParentTree.GetHeight(wholeX, wholeY);
            float heightXWholeY = this.ParentTree.GetHeight(wholeX, y);
            float heightXY = this.ParentTree.GetHeight(x, y);

            // Compute the normal of the current quad
            Vector3 plane1Normal = new Plane(new Vector3(x, wholeY, heightXYWhole), new Vector3(wholeX, y, heightXWholeY), new Vector3(x, y, heightXY)).Normal;
            Vector3 plane2Normal = new Plane(new Vector3(x, wholeY, heightXYWhole), new Vector3(wholeX, wholeY, heightXWholeYWhole), new Vector3(wholeX, y, heightXWholeY)).Normal;
            Vector3 normal = plane1Normal * plane2Normal;
            normal.Normalize();

            // First compute the 4 edges of the current square; here we know all positions and heights
            {
                if (this.Parent != null)
                {
                    // If we have a parent maybe we can use its vertices
                    switch (this.Position)
                    {
                        case NodeChild.Northwest:
                            this.NorthWestVertex = this.Parent.NorthWestVertex;
                            this.NorthEastVertex = this.Parent.NorthVertex;
                            this.SouthEastVertex = this.Parent.CenterVertex;
                            this.SouthWestVertex = this.Parent.WestVertex;
                            break;
                        case NodeChild.Northeast:
                            this.NorthWestVertex = this.Parent.NorthVertex;
                            this.NorthEastVertex = this.Parent.NorthEastVertex;
                            this.SouthWestVertex = this.Parent.CenterVertex;
                            this.SouthEastVertex = this.Parent.EastVertex;
                            break;
                        case NodeChild.Southwest:
                            this.NorthWestVertex = this.Parent.WestVertex;
                            this.NorthEastVertex = this.Parent.CenterVertex;
                            this.SouthWestVertex = this.Parent.SouthWestVertex;
                            this.SouthEastVertex = this.Parent.SouthVertex;
                            break;
                        case NodeChild.Southeast:
                            this.NorthWestVertex = this.Parent.CenterVertex;
                            this.NorthEastVertex = this.Parent.EastVertex;
                            this.SouthWestVertex = this.Parent.SouthVertex;
                            this.SouthEastVertex = this.Parent.SouthEastVertex;
                            break;
                        default:
                            break;
                    }

                    // Only the sides can be new
                    this.CenterVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(new Vector3(halfX, halfY, this.ParentTree.GetHeight(halfX, halfY)), normal, this.GetTextureCoordinates(halfX, halfY)));
                    this.WestVertex = this.WestNeighbor != null ? this.WestNeighbor.EastVertex : new TerrainVertex(new VertexPositionNormalMultipleTexture(new Vector3(x, halfY, this.ParentTree.GetHeight(x, halfY)), normal, this.GetTextureCoordinates(x, halfY)));
                    this.NorthVertex = this.NorthNeighbor != null ? this.NorthNeighbor.SouthVertex : new TerrainVertex(new VertexPositionNormalMultipleTexture(new Vector3(halfX, wholeY, this.ParentTree.GetHeight(halfX, wholeY)), normal, this.GetTextureCoordinates(halfX, wholeY)));
                    this.EastVertex = this.EastNeighbor != null ? this.EastNeighbor.WestVertex : new TerrainVertex(new VertexPositionNormalMultipleTexture(new Vector3(wholeX, halfY, this.ParentTree.GetHeight(wholeX, halfY)), normal, this.GetTextureCoordinates(wholeX, halfY)));
                    this.SouthVertex = this.SouthNeighbor != null ? this.SouthNeighbor.NorthVertex : new TerrainVertex(new VertexPositionNormalMultipleTexture(new Vector3(halfX, y, this.ParentTree.GetHeight(halfX, y)), normal, this.GetTextureCoordinates(halfX, y)));
                }
                else
                {
                    // This occurs only on depth = 0
                    Vector3 northWest = new Vector3(x, wholeY, heightXYWhole);
                    Vector3 northEast = new Vector3(wholeX, wholeY, heightXWholeYWhole);
                    Vector3 southEast = new Vector3(wholeX, y, heightXWholeY);
                    Vector3 southWest = new Vector3(x, y, heightXY);
                    Vector3 west = new Vector3(x, halfY, this.ParentTree.GetHeight(x, halfY));
                    Vector3 north = new Vector3(halfX, wholeY, this.ParentTree.GetHeight(halfX, wholeY));
                    Vector3 east = new Vector3(wholeX, halfY, this.ParentTree.GetHeight(wholeX, halfY));
                    Vector3 south = new Vector3(halfX, y, this.ParentTree.GetHeight(halfX, y));

                    this.NorthWestVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(northWest, normal, this.GetTextureCoordinates(x, wholeY)));
                    this.NorthEastVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(northEast, normal, this.GetTextureCoordinates(wholeX, wholeY)));
                    this.SouthEastVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(southEast, normal, this.GetTextureCoordinates(wholeX, y)));
                    this.SouthWestVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(southWest, normal, this.GetTextureCoordinates(x, y)));
                    this.CenterVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(new Vector3(halfX, this.ParentTree.GetHeight(halfX, halfY), halfY), normal, this.GetTextureCoordinates(halfX, halfY)));
                    this.WestVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(west, normal, this.GetTextureCoordinates(x, halfY)));
                    this.NorthVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(north, normal, this.GetTextureCoordinates(halfX, wholeY)));
                    this.EastVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(east, normal, this.GetTextureCoordinates(wholeX, halfY)));
                    this.SouthVertex = new TerrainVertex(new VertexPositionNormalMultipleTexture(south, normal, this.GetTextureCoordinates(halfX, y)));
                }
            }

            // At the beginning the four edges are enabled
            {
                this.EnableVertex(NodeContents.NorthwestVertex, NodeVertex.NorthWest);
                this.EnableVertex(NodeContents.NortheastVertex, NodeVertex.NorthEast);
                this.EnableVertex(NodeContents.SoutheastVertex, NodeVertex.SouthEast);
                this.EnableVertex(NodeContents.SouthwestVertex, NodeVertex.SouthWest);
            }

            // Then interpolate the 4 sides and the center.
            // When can easily deduce the position x and z because we know the size of the current square and its location.
            // For the height we have to interpolate from the two neighbor egdes.
            {
                float centerHeight = (float)(0.25 * (this.NorthEastVertex.Value.Position.Y + this.NorthWestVertex.Value.Position.Y + this.SouthWestVertex.Value.Position.Y + this.SouthEastVertex.Value.Position.Y));
                float eastSideHeight = (float)(0.5 * (this.SouthEastVertex.Value.Position.Y + this.NorthEastVertex.Value.Position.Y));
                float northSideHeight = (float)(0.5 * (this.NorthEastVertex.Value.Position.Y + this.NorthWestVertex.Value.Position.Y));
                float westSideHeight = (float)(0.5 * (this.NorthWestVertex.Value.Position.Y + this.SouthWestVertex.Value.Position.Y));
                float southSideHeight = (float)(0.5 * (this.SouthWestVertex.Value.Position.Y + this.SouthEastVertex.Value.Position.Y));

                this.realToInterpolatedVertexHeight[(int)NodeSideVertex.Center] = Math.Abs(centerHeight - this.CenterVertex.Value.Position.Y);
                this.realToInterpolatedVertexHeight[(int)NodeSideVertex.East] = Math.Abs(eastSideHeight - this.EastVertex.Value.Position.Y);
                this.realToInterpolatedVertexHeight[(int)NodeSideVertex.North] = Math.Abs(northSideHeight - this.NorthVertex.Value.Position.Y);
                this.realToInterpolatedVertexHeight[(int)NodeSideVertex.West] = Math.Abs(westSideHeight - this.WestVertex.Value.Position.Y);
                this.realToInterpolatedVertexHeight[(int)NodeSideVertex.South] = Math.Abs(southSideHeight - this.SouthVertex.Value.Position.Y);
            }

            this.boundingBoxNorthWestChild = BoundingBox.CreateFromPoints(new Vector3[] { this.CenterVertex.Value.Position, this.NorthWestVertex.Value.Position, this.WestVertex.Value.Position, this.NorthVertex.Value.Position });
            this.boundingBoxNorthEastChild = BoundingBox.CreateFromPoints(new Vector3[] { this.CenterVertex.Value.Position, this.NorthEastVertex.Value.Position, this.EastVertex.Value.Position, this.NorthVertex.Value.Position });
            this.boundingBoxSouthWestChild = BoundingBox.CreateFromPoints(new Vector3[] { this.CenterVertex.Value.Position, this.SouthWestVertex.Value.Position, this.WestVertex.Value.Position, this.SouthVertex.Value.Position });
            this.boundingBoxSouthEastChild = BoundingBox.CreateFromPoints(new Vector3[] { this.CenterVertex.Value.Position, this.SouthEastVertex.Value.Position, this.EastVertex.Value.Position, this.SouthVertex.Value.Position });

            Vector3 childnormal = Vector3.Multiply(
                new Plane(this.NorthVertex.Value.Position, this.NorthEastVertex.Value.Position, this.EastVertex.Value.Position).Normal,
                new Plane(this.NorthVertex.Value.Position, this.EastVertex.Value.Position, this.CenterVertex.Value.Position).Normal);
            normal.Normalize();
            this.dotNorthEastChild = 1 - Vector3.Dot(childnormal, normal);

            childnormal = Vector3.Multiply(
                new Plane(this.NorthWestVertex.Value.Position, this.NorthVertex.Value.Position, this.CenterVertex.Value.Position).Normal,
                new Plane(this.NorthWestVertex.Value.Position, this.CenterVertex.Value.Position, this.WestVertex.Value.Position).Normal);
            childnormal.Normalize();
            this.dotNorthWestChild = 1 - Vector3.Dot(childnormal, normal);

            childnormal = Vector3.Multiply(
                new Plane(this.CenterVertex.Value.Position, this.EastVertex.Value.Position, this.SouthEastVertex.Value.Position).Normal,
                new Plane(this.CenterVertex.Value.Position, this.SouthEastVertex.Value.Position, this.SouthVertex.Value.Position).Normal);
            childnormal.Normalize();
            this.dotSouthEastChild = 1 - Vector3.Dot(childnormal, normal);

            childnormal = Vector3.Multiply(
                new Plane(this.WestVertex.Value.Position, this.CenterVertex.Value.Position, this.SouthVertex.Value.Position).Normal,
                new Plane(this.WestVertex.Value.Position, this.SouthVertex.Value.Position, this.SouthWestVertex.Value.Position).Normal);
            childnormal.Normalize();
            this.dotSouthWestChild = 1 - Vector3.Dot(childnormal, normal);
        }

        /// <summary>
        /// Gets the texture coordinates for the specified position.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>See summary.</returns>
        public Vector2 GetTextureCoordinates(float x, float y)
        {
            x = x - this.ParentTree.Location.X;
            y = y - this.ParentTree.Location.Y;

            return new Vector2(x / ((float)this.ParentTree.Size / TerrainManager.TextureFrequency), y / ((float)this.ParentTree.Size / TerrainManager.TextureFrequency));
        }

        /// <summary>
        /// Updates the <see cref="QuadNode"/>.
        /// </summary>
        public void Update()
        {
            if (this.Depth < this.ParentTree.Depth)
            {
                // Check vertices
                {
                    // The vertices are shared so we don't have to check all the sides vertex only two not opposite are enough
                    // It is possible to stop all vertices checks and use only childs checks
                    // You will have the same result with a few less details.
                    // CheckVertexAt(VertexPosition.West, EnabledVertex.West, Sides.West);
                    // CheckVertexAt(VertexPosition.North, EnabledVertex.North, Sides.North);
                    this.CheckVertexAt(NodeVertex.East, NodeContents.EastVertex, NodeSideVertex.East);
                    this.CheckVertexAt(NodeVertex.South, NodeContents.SouthVertex, NodeSideVertex.South);
                }

                // Check children if we are not on a leaf
                {
                    this.CheckChildAt(NodeChild.Northwest, NodeContents.NorthwestChild, this.dotNorthWestChild, this.boundingBoxNorthWestChild);
                    this.CheckChildAt(NodeChild.Northeast, NodeContents.NortheastChild, this.dotNorthEastChild, this.boundingBoxNorthEastChild);
                    this.CheckChildAt(NodeChild.Southeast, NodeContents.SoutheastChild, this.dotSouthEastChild, this.boundingBoxSouthEastChild);
                    this.CheckChildAt(NodeChild.Southwest, NodeContents.SouthwestChild, this.dotSouthWestChild, this.boundingBoxSouthWestChild);
                }

                for (int i = 0; i < QuadTree.NodeChildrenCount; i++)
                {
                    if ((this.Children[i] != null) && (!this.Children[i].IsFullRelevant))
                    {
                        this.Children[i].Update();
                    }
                }
            }
        }

        /// <summary>
        /// Enables the specified flag and the specified vertex.
        /// </summary>
        /// <param name="value">The flag to enable.</param>
        /// <param name="vertex">The vertex to enable.</param>
        public void EnableVertex(NodeContents value, NodeVertex vertex)
        {
            this.EnableVertex(value);
            this.Vertices[(int)vertex].AddReferenceTo(this);
        }

        /// <summary>
        /// Enables the specified flag.
        /// </summary>
        /// <param name="value">The flag to enable.</param>
        public void EnableVertex(NodeContents value)
        {
            this.EnabledContent |= value;

            if (((int)this.EnabledContent >> 5) != 0)
            {
                this.EnabledContent |= NodeContents.CenterVertex;
                this.CenterVertex.AddReferenceTo(this);
            }

            // If an edge is enabled, we must enable its neighbor to share the edge
            if (((int)value >> 5) != 0 && (this.Parent != null))
            {
                switch (value)
                {
                    case NodeContents.WestVertex:

                        if (this.WestNeighbor == null)
                        {
                            if (this.Position == NodeChild.Northwest)
                            {
                                if (this.Parent.WestNeighbor != null)
                                {
                                    if (this.Parent.WestNeighbor.NorthEastChild == null)
                                    {
                                        this.Parent.WestNeighbor.AddChild(NodeChild.Northeast, NodeContents.NortheastChild);
                                    }
                                }
                            }
                            else if (this.Position == NodeChild.Southwest)
                            {
                                if (this.Parent.WestNeighbor != null)
                                {
                                    if (this.Parent.WestNeighbor.SouthEastChild == null)
                                    {
                                        this.Parent.WestNeighbor.AddChild(NodeChild.Southeast, NodeContents.SoutheastChild);
                                    }
                                }
                            }
                            else if (this.Position == NodeChild.Southeast)
                            {
                                if (this.Parent.SouthWestChild == null)
                                {
                                    this.Parent.AddChild(NodeChild.Southwest, NodeContents.SouthwestChild);
                                }
                            }
                            else if (this.Position == NodeChild.Northeast)
                            {
                                if (this.Parent.NorthWestChild == null)
                                {
                                    this.Parent.AddChild(NodeChild.Northwest, NodeContents.NorthwestChild);
                                }
                            }
                        }

                        break;

                    case NodeContents.NorthVertex:

                        if (this.NorthNeighbor == null)
                        {
                            if (this.Position == NodeChild.Northwest)
                            {
                                if (this.Parent.NorthNeighbor != null)
                                {
                                    if (this.Parent.NorthNeighbor.SouthWestChild == null)
                                    {
                                        this.Parent.NorthNeighbor.AddChild(NodeChild.Southwest, NodeContents.SouthwestChild);
                                    }
                                }
                            }
                            else if (this.Position == NodeChild.Northeast)
                            {
                                if (this.Parent.NorthNeighbor != null)
                                {
                                    if (this.Parent.NorthNeighbor.SouthEastChild == null)
                                    {
                                        this.Parent.NorthNeighbor.AddChild(NodeChild.Southeast, NodeContents.SoutheastChild);
                                    }
                                }
                            }
                            else if (this.Position == NodeChild.Southeast)
                            {
                                if (this.Parent.NorthEastChild == null)
                                {
                                    this.Parent.AddChild(NodeChild.Northeast, NodeContents.NortheastChild);
                                }
                            }
                            else if (this.Position == NodeChild.Southwest)
                            {
                                if (this.Parent.NorthWestChild == null)
                                {
                                    this.Parent.AddChild(NodeChild.Northwest, NodeContents.NorthwestChild);
                                }
                            }
                        }

                        break;

                    case NodeContents.EastVertex:

                        if (this.EastNeighbor == null)
                        {
                            if (this.Position == NodeChild.Northeast)
                            {
                                if (this.Parent.EastNeighbor != null)
                                {
                                    if (this.Parent.EastNeighbor.NorthWestChild == null)
                                    {
                                        this.Parent.EastNeighbor.AddChild(NodeChild.Northwest, NodeContents.NorthwestChild);
                                    }
                                }
                            }
                            else if (this.Position == NodeChild.Southeast)
                            {
                                if (this.Parent.EastNeighbor != null)
                                {
                                    if (this.Parent.EastNeighbor.SouthWestChild == null)
                                    {
                                        this.Parent.EastNeighbor.AddChild(NodeChild.Southwest, NodeContents.SouthwestChild);
                                    }
                                }
                            }
                            else if (this.Position == NodeChild.Northwest)
                            {
                                if (this.Parent.NorthEastChild == null)
                                {
                                    this.Parent.AddChild(NodeChild.Northeast, NodeContents.NortheastChild);
                                }
                            }
                            else if (this.Position == NodeChild.Southwest)
                            {
                                if (this.Parent.SouthEastChild == null)
                                {
                                    this.Parent.AddChild(NodeChild.Southeast, NodeContents.SoutheastChild);
                                }
                            }
                        }

                        break;

                    case NodeContents.SouthVertex:

                        if (this.SouthNeighbor == null)
                        {
                            if (this.Position == NodeChild.Southeast)
                            {
                                if (this.Parent.SouthNeighbor != null)
                                {
                                    if (this.Parent.SouthNeighbor.NorthEastChild == null)
                                    {
                                        this.Parent.SouthNeighbor.AddChild(NodeChild.Northeast, NodeContents.NortheastChild);
                                    }
                                }
                            }
                            else if (this.Position == NodeChild.Southwest)
                            {
                                if (this.Parent.SouthNeighbor != null)
                                {
                                    if (this.Parent.SouthNeighbor.NorthWestChild == null)
                                    {
                                        this.Parent.SouthNeighbor.AddChild(NodeChild.Northwest, NodeContents.NorthwestChild);
                                    }
                                }
                            }
                            else if (this.Position == NodeChild.Northeast)
                            {
                                if (this.Parent.SouthEastChild == null)
                                {
                                    this.Parent.AddChild(NodeChild.Southeast, NodeContents.SoutheastChild);
                                }
                            }
                            else if (this.Position == NodeChild.Northwest)
                            {
                                if (this.Parent.SouthWestChild == null)
                                {
                                    this.Parent.AddChild(NodeChild.Southwest, NodeContents.SouthwestChild);
                                }
                            }
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Disables the specified flag and the specified vertex.
        /// </summary>
        /// <param name="value">The flag to disable.</param>
        /// <param name="vertex">The vertex to disable.</param>
        public void DisableVertex(NodeContents value, NodeVertex vertex)
        {
            this.DisableVertex(value);
            this.Vertices[(int)vertex].RemoveReferenceFrom(this);
        }

        /// <summary>
        /// Disables the specified flag.
        /// </summary>
        /// <param name="value">The flag to disable.</param>
        public void DisableVertex(NodeContents value)
        {
            this.EnabledContent &= ~value;

            if (((int)this.EnabledContent >> 5) == 0)
            {
                this.EnabledContent &= ~NodeContents.CenterVertex;
                this.CenterVertex.RemoveReferenceFrom(this);
            }
        }

        /// <summary>
        /// Builds the triangle view of the current node.
        /// </summary>
        public void GetEnabledVertices()
        {
            if (!this.HaveNoEdge())
            {
                if (this.NorthWestChild == null)
                {
                    if (this.SouthWestChild != null)
                    {
                        this.FillWithVertex(NodeVertex.Center);
                        this.FillWithVertex(NodeVertex.West);
                        this.FillWithVertex(NodeVertex.NorthWest);
                    }
                    else if (this.WestVertex.Enabled)
                    {
                        this.FillWithVertex(NodeVertex.Center);
                        this.FillWithVertex(NodeVertex.West);
                        this.FillWithVertex(NodeVertex.NorthWest);
                    }

                    this.FillWithVertex(NodeVertex.Center);
                    this.FillWithVertex(NodeVertex.NorthWest);

                    if (this.NorthEastChild != null)
                    {
                        this.FillWithVertex(NodeVertex.North);
                    }
                }

                // NorthEast
                if (this.NorthEastChild == null)
                {
                    if (this.NorthWestChild != null)
                    {
                        this.FillWithVertex(NodeVertex.Center);
                        this.FillWithVertex(NodeVertex.North);
                    }
                    else if (this.NorthVertex.Enabled)
                    {
                        this.FillWithVertex(NodeVertex.North);
                        this.FillWithVertex(NodeVertex.Center);
                        this.FillWithVertex(NodeVertex.North);
                    }

                    this.FillWithVertex(NodeVertex.NorthEast);
                    this.FillWithVertex(NodeVertex.Center);
                    this.FillWithVertex(NodeVertex.NorthEast);

                    if (this.SouthEastChild != null)
                    {
                        this.FillWithVertex(NodeVertex.East);
                    }
                }

                // SouthEast
                if (this.SouthEastChild == null)
                {
                    if (this.NorthEastChild != null)
                    {
                        this.FillWithVertex(NodeVertex.Center);
                        this.FillWithVertex(NodeVertex.East);
                    }
                    else if (this.EastVertex.Enabled)
                    {
                        this.FillWithVertex(NodeVertex.East);
                        this.FillWithVertex(NodeVertex.Center);
                        this.FillWithVertex(NodeVertex.East);
                    }

                    this.FillWithVertex(NodeVertex.SouthEast);
                    this.FillWithVertex(NodeVertex.Center);
                    this.FillWithVertex(NodeVertex.SouthEast);

                    if (this.SouthWestChild != null)
                    {
                        this.FillWithVertex(NodeVertex.South);
                    }
                }

                if (this.SouthWestChild == null)
                {
                    if (this.SouthEastChild != null)
                    {
                        this.FillWithVertex(NodeVertex.Center);
                        this.FillWithVertex(NodeVertex.South);
                    }
                    else if (this.SouthVertex.Enabled)
                    {
                        this.FillWithVertex(NodeVertex.South);
                        this.FillWithVertex(NodeVertex.Center);
                        this.FillWithVertex(NodeVertex.South);
                    }

                    this.FillWithVertex(NodeVertex.SouthWest);
                    this.FillWithVertex(NodeVertex.Center);
                    this.FillWithVertex(NodeVertex.SouthWest);

                    if (this.NorthWestChild != null)
                    {
                        this.FillWithVertex(NodeVertex.West);
                    }
                    else
                    {
                        if (this.WestVertex.Enabled)
                        {
                            this.FillWithVertex(NodeVertex.West);
                        }
                        else
                        {
                            this.FillWithVertex(NodeVertex.NorthWest);
                        }
                    }
                }
            }
            else
            {
                this.FillWithVertex(NodeVertex.NorthWest);
                this.FillWithVertex(NodeVertex.NorthEast);
                this.FillWithVertex(NodeVertex.SouthEast);
                this.FillWithVertex(NodeVertex.NorthWest);
                this.FillWithVertex(NodeVertex.SouthEast);
                this.FillWithVertex(NodeVertex.SouthWest);
            }

            for (int i = 0; i < QuadTree.NodeChildrenCount; i++)
            {
                if (this.Children[i] != null)
                {
                    this.Children[i].GetEnabledVertices();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="QuadNode"/> is a leaf.
        /// </summary>
        /// <returns>See summary.</returns>
        public bool IsLeaf()
        {
            return ((int)this.EnabledContent >> 9) == 0;
        }

        /// <summary>
        /// Returns true if the current <see cref="QuadNode"/> has no side vertices enabled.
        /// </summary>
        /// <returns>See summary.</returns>
        public bool HaveNoEdge()
        {
            return !this.NorthVertex.Enabled && !this.SouthVertex.Enabled && !this.EastVertex.Enabled && !this.WestVertex.Enabled;
        }

        /// <summary>
        /// Checks the relevance of a vertex.
        /// </summary>
        /// <param name="vertexPosition">The position of the vertex.</param>
        /// <param name="side">The side of the vertex.</param>
        /// <param name="cameraPosition">The camera's position.</param>
        /// <returns>See summary.</returns>
        public bool VertexTest(Vector3 vertexPosition, NodeSideVertex side, Vector3 cameraPosition)
        {
            // Get the distance between interpolated height position and real height position
            float lengthToTest = this.realToInterpolatedVertexHeight[(int)side];

            // Get the distance from the camera position to the vertex position
            float distanceCameraToPoint = Vector3.Distance(vertexPosition, cameraPosition);

            // Check with the threshold
            return lengthToTest * this.ParentTree.VertexDetail > distanceCameraToPoint;
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
                for (int i = 0; i < this.Vertices.Length; i++)
                {
                    this.Vertices[i].RemoveReferenceFrom(this);
                }

                for (int i = 0; i < QuadTree.NodeChildrenCount; i++)
                {
                    if (this.Children[i] != null)
                    {
                        this.Children[i].Dispose();
                        this.Children[i] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Links the current node to its neighbors.
        /// </summary>
        private void InitializeNeighbors()
        {
            if (this.Parent == null)
            {
                return;
            }

            switch (this.Position)
            {
                case NodeChild.Northwest:

                    // East/West
                    {
                        this.EastNeighbor = this.Parent.NorthEastChild;
                        if (this.Parent.NorthEastChild != null)
                        {
                            this.Parent.NorthEastChild.WestNeighbor = this;
                        }
                    }

                    // South/North
                    {
                        this.SouthNeighbor = this.Parent.SouthWestChild;
                        if (this.Parent.SouthWestChild != null)
                        {
                            this.Parent.SouthWestChild.NorthNeighbor = this;
                        }
                    }

                    // West/East
                    if (this.Parent.WestNeighbor != null)
                    {
                        this.WestNeighbor = this.Parent.WestNeighbor.NorthEastChild;
                        if (this.Parent.WestNeighbor.NorthEastChild != null)
                        {
                            this.Parent.WestNeighbor.NorthEastChild.EastNeighbor = this;
                        }
                    }

                    // North/South
                    if (this.Parent.NorthNeighbor != null)
                    {
                        this.NorthNeighbor = this.Parent.NorthNeighbor.SouthWestChild;
                        if (this.Parent.NorthNeighbor.SouthWestChild != null)
                        {
                            this.Parent.NorthNeighbor.SouthWestChild.SouthNeighbor = this;
                        }
                    }

                    break;

                case NodeChild.Northeast:

                    // West/East
                    this.WestNeighbor = this.Parent.NorthWestChild;
                    if (this.Parent.NorthWestChild != null)
                    {
                        this.Parent.NorthWestChild.EastNeighbor = this;
                    }

                    // South/North
                    this.SouthNeighbor = this.Parent.SouthEastChild;
                    if (this.Parent.SouthEastChild != null)
                    {
                        this.Parent.SouthEastChild.NorthNeighbor = this;
                    }

                    // East/West
                    if (this.Parent.EastNeighbor != null)
                    {
                        this.EastNeighbor = this.Parent.EastNeighbor.NorthWestChild;
                        if (this.Parent.EastNeighbor.NorthWestChild != null)
                        {
                            this.Parent.EastNeighbor.NorthWestChild.WestNeighbor = this;
                        }
                    }

                    // North/South
                    if (this.Parent.NorthNeighbor != null)
                    {
                        this.NorthNeighbor = this.Parent.NorthNeighbor.SouthEastChild;
                        if (this.Parent.NorthNeighbor.SouthEastChild != null)
                        {
                            this.Parent.NorthNeighbor.SouthEastChild.SouthNeighbor = this;
                        }
                    }

                    break;

                case NodeChild.Southwest:

                    // East/West
                    this.EastNeighbor = this.Parent.SouthEastChild;
                    if (this.Parent.SouthEastChild != null)
                    {
                        this.Parent.SouthEastChild.WestNeighbor = this;
                    }

                    // North/South
                    this.NorthNeighbor = this.Parent.NorthWestChild;
                    if (this.Parent.NorthWestChild != null)
                    {
                        this.Parent.NorthWestChild.SouthNeighbor = this;
                    }

                    // West/East
                    if (this.Parent.WestNeighbor != null)
                    {
                        this.WestNeighbor = this.Parent.WestNeighbor.SouthEastChild;
                        if (this.Parent.WestNeighbor.SouthEastChild != null)
                        {
                            this.Parent.WestNeighbor.SouthEastChild.EastNeighbor = this;
                        }
                    }

                    // South/North
                    if (this.Parent.SouthNeighbor != null)
                    {
                        this.SouthNeighbor = this.Parent.SouthNeighbor.NorthWestChild;
                        if (this.Parent.SouthNeighbor.NorthWestChild != null)
                        {
                            this.Parent.SouthNeighbor.NorthWestChild.NorthNeighbor = this;
                        }
                    }

                    break;

                case NodeChild.Southeast:

                    // West/East
                    this.WestNeighbor = this.Parent.SouthWestChild;
                    if (this.Parent.SouthWestChild != null)
                    {
                        this.Parent.SouthWestChild.EastNeighbor = this;
                    }

                    // North/South
                    this.NorthNeighbor = this.Parent.NorthEastChild;
                    if (this.Parent.NorthEastChild != null)
                    {
                        this.Parent.NorthEastChild.SouthNeighbor = this;
                    }

                    // East/West
                    if (this.Parent.EastNeighbor != null)
                    {
                        this.EastNeighbor = this.Parent.EastNeighbor.SouthWestChild;
                        if (this.Parent.EastNeighbor.SouthWestChild != null)
                        {
                            this.Parent.EastNeighbor.SouthWestChild.WestNeighbor = this;
                        }
                    }

                    // South/North
                    if (this.Parent.SouthNeighbor != null)
                    {
                        this.SouthNeighbor = this.Parent.SouthNeighbor.NorthEastChild;
                        if (this.Parent.SouthNeighbor.NorthEastChild != null)
                        {
                            this.Parent.SouthNeighbor.NorthEastChild.NorthNeighbor = this;
                        }
                    }

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the specified vertex is being used by a child.
        /// </summary>
        /// <param name="position">The vertex to check for use.</param>
        /// <returns>See summary.</returns>
        private bool EdgeUsed(NodeVertex position)
        {
            return this.Vertices[(int)position].Enabled;
        }

        /// <summary>
        /// Returns true if the specified <see cref="NodeContents"/> is enabled.
        /// </summary>
        /// <param name="enabledVertex">Flag to analyze.</param>
        /// <returns>See summary.</returns>
        private bool IsEnabled(NodeContents enabledVertex)
        {
            return (this.EnabledContent & enabledVertex) != NodeContents.None;
        }

        /// <summary>
        /// Returns true if the specified <see cref="NodeContents"/> is disabled.
        /// </summary>
        /// <param name="disabledVertex">Flag to analyze.</param>
        /// <returns>See summary.</returns>
        private bool IsDisabled(NodeContents disabledVertex)
        {
            return (this.EnabledContent & disabledVertex) == NodeContents.None;
        }

        /// <summary>
        /// Fills the <see cref="NodeVertex"/> with data.
        /// </summary>
        /// <param name="position">The <see cref="NodeVertex"/> to fill.</param>
        private void FillWithVertex(NodeVertex position)
        {
            VertexPositionNormalMultipleTexture vertexValue = this.Vertices[(int)position].Value;
            TerrainVertex vertex = this.Vertices[(int)position];

            if (this.ParentTree.ProcessIterationId != vertex.LastUsedIteration)
            {
                vertex.BufferIndex = this.ParentTree.Vertices.Count;
                this.ParentTree.Vertices.Add(vertexValue);
                vertex.LastUsedIteration = this.ParentTree.ProcessIterationId;
            }

            this.ParentTree.Indexes.Add(vertex.BufferIndex);

            if (QuadNode.currentIndice == 0)
            {
                this.terrainPrimitive = new TerrainPrimitive(0, 0, 0);
                this.terrainPrimitive.Index1 = vertex.BufferIndex;
                QuadNode.currentIndice++;
            }
            else if (currentIndice == 1)
            {
                this.terrainPrimitive.Index2 = vertex.BufferIndex;
                QuadNode.currentIndice++;
            }
            else if (currentIndice == 2)
            {
                this.terrainPrimitive.Index3 = vertex.BufferIndex;
                QuadNode.currentIndice = 0;
                this.terrainPrimitive.SetNormal(this.ParentTree.Vertices);
            }
        }

        /// <summary>
        /// Checks the specified vertex of the current <see cref="QuadNode"/>.
        /// </summary>
        /// <param name="position">Position of the vertex.</param>
        /// <param name="flag">Flag to enable/disable.</param>
        /// <param name="side">The side vertex.</param>
        private void CheckVertexAt(NodeVertex position, NodeContents flag, NodeSideVertex side)
        {
            // If the flag is not enabled and the vertex can be enabled...
            if (this.IsDisabled(flag) && this.VertexTest(this.Vertices[(int)position].Value.Position, side, EngineManager.Engine.CurrentCamera.Position))
            {
                this.EnableVertex(flag, position);
            }
            else if (this.IsEnabled(flag) && !this.VertexTest(this.Vertices[(int)position].Value.Position, side, EngineManager.Engine.CurrentCamera.Position))
            {
                // If the flag is enabled and the vertex has to be disabled...
                this.DisableVertex(flag, position);
            }
        }

        /// <summary>
        /// Checks the specified child of the current <see cref="QuadNode"/>.
        /// </summary>
        /// <param name="position">Position of the child.</param>
        /// <param name="flag">Flag to enable/disable.</param>
        /// <param name="dotprod">Dot product.</param>
        /// <param name="childBox">Associated child's bounding box.</param>
        private void CheckChildAt(NodeChild position, NodeContents flag, float dotprod, BoundingBox childBox)
        {
            // If the flag is not enabled and the child's bounding box shows that the child has to be enabled
            if (this.IsDisabled(flag) && this.ChildTest(dotprod, childBox, EngineManager.Engine.CurrentCamera.Position))
            {
                this.AddChild(position, flag);
            }
            else if (this.IsEnabled(flag) && this.Children[(int)position].IsLeaf() && this.Children[(int)position].HaveNoEdge() && !this.ChildTest(dotprod, childBox, EngineManager.Engine.CurrentCamera.Position))
            {
                // If the flag is enabled and the child has no children or side edges and the child's bounding box shows that the child has to be disabled
                this.RemoveChild(position, flag);
            }
        }

        /// <summary>
        /// Removes a child from the specified position and disables its flag.
        /// </summary>
        /// <param name="position">The child to remove.</param>
        /// <param name="flag">The flag to disable.</param>
        private void RemoveChild(NodeChild position, NodeContents flag)
        {
            this.DisableVertex(flag);
            QuadNode node = this.Children[(int)position];
            switch (position)
            {
                case NodeChild.Northwest:
                    this.DisableVertex(NodeContents.NorthVertex, NodeVertex.North);
                    this.DisableVertex(NodeContents.WestVertex, NodeVertex.West);
                    break;
                case NodeChild.Northeast:
                    this.DisableVertex(NodeContents.NorthVertex, NodeVertex.North);
                    this.DisableVertex(NodeContents.EastVertex, NodeVertex.East);
                    break;
                case NodeChild.Southwest:
                    this.DisableVertex(NodeContents.SouthVertex, NodeVertex.South);
                    this.DisableVertex(NodeContents.WestVertex, NodeVertex.West);
                    break;
                default:
                    this.DisableVertex(NodeContents.SouthVertex, NodeVertex.South);
                    this.DisableVertex(NodeContents.EastVertex, NodeVertex.East);
                    break;
            }

            this.Children[(int)position] = null;
            this.InitializeNeighbors();
            node.InitializeNeighbors();
            node.Dispose();
        }

        /// <summary>
        /// Adds a child at the specified position and enables its flag.
        /// </summary>
        /// <param name="position">The child to add.</param>
        /// <param name="flag">The flag to enable.</param>
        private void AddChild(NodeChild position, NodeContents flag)
        {
            this.EnableVertex(flag);
            QuadNode node = new QuadNode(this, position);
            this.Children[(int)position] = node;
            float size = node.NodeSize;

            switch (position)
            {
                case NodeChild.Northwest:
                    node.Location = this.Location + new Vector2(0, size);
                    break;
                case NodeChild.Northeast:
                    node.Location = this.Location + new Vector2(size, size);
                    break;
                case NodeChild.Southwest:
                    node.Location = this.Location + new Vector2(0, 0);
                    break;
                default:
                    node.Location = this.Location + new Vector2(size, 0);
                    break;
            }

            node.InitializeNeighbors();
            this.InitializeNeighbors();
            node.Initialize();

            switch (position)
            {
                case NodeChild.Northwest:
                    this.EnableVertex(NodeContents.NorthVertex, NodeVertex.North);
                    this.EnableVertex(NodeContents.WestVertex, NodeVertex.West);
                    break;
                case NodeChild.Northeast:
                    this.EnableVertex(NodeContents.NorthVertex, NodeVertex.North);
                    this.EnableVertex(NodeContents.EastVertex, NodeVertex.East);
                    break;
                case NodeChild.Southwest:
                    this.EnableVertex(NodeContents.SouthVertex, NodeVertex.South);
                    this.EnableVertex(NodeContents.WestVertex, NodeVertex.West);
                    break;
                default:
                    this.EnableVertex(NodeContents.SouthVertex, NodeVertex.South);
                    this.EnableVertex(NodeContents.EastVertex, NodeVertex.East);
                    break;
            }
        }

        /// <summary>
        /// Checks whether a child node is relevant.
        /// </summary>
        /// <param name="dotprod">Dot product.</param>
        /// <param name="childBoundingBox">The bounding box of the child node.</param>
        /// <param name="cameraPosition">The camera's position.</param>
        /// <returns>See summary.</returns>
        private bool ChildTest(float dotprod, BoundingBox childBoundingBox, Vector3 cameraPosition)
        {
            // By default, the four children of the root node are visible
            if (this.Depth < this.ParentTree.MinimalDepth)
            {
                return true;
            }

            // Get the closest point to the camera and check the distance
            float distanceCameraToPoint = Vector3.Distance(GetBoundingBoxClosestPointToPoint(childBoundingBox, cameraPosition), cameraPosition);

            // Check with the threshold
            return ((distanceCameraToPoint - this.ParentTree.QuadTreeDetailAtFront) / this.ParentTree.QuadTreeDetailAtFar) < dotprod;
        }
    }
}
