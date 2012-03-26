namespace MMO3D.Engine
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content.Pipeline;
    using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
    using Microsoft.Xna.Framework.Content.Pipeline.Processors;

    /// <summary>
    /// Custom content pipeline processor attaches vertex position information to
    /// a model, which can be used at runtime to implement per-triangle picking.
    /// It derives from the built-in ModelProcessor, and overrides the Process
    /// method, using this to attach custom data to the model Tag property.
    /// </summary>
    [ContentProcessor(DisplayName = "Advanced Model Processor")]
    public class AdvancedModelProcessor : ModelProcessor
    {
        /// <summary>
        /// A list of all the points of the vertices in the model.
        /// </summary>
        private List<Vector3> vertices = new List<Vector3>();

        /// <summary>
        /// The main method in charge of processing the content.
        /// </summary>
        /// <param name="input">The root node content.</param>
        /// <param name="context">Context for the specified processor.</param>
        /// <returns>The model content.</returns>
        public override ModelContent Process(Microsoft.Xna.Framework.Content.Pipeline.Graphics.NodeContent input, ContentProcessorContext context)
        {
            // Look up the input vertex positions.
            this.FindVertices(input);

            // Chain to the base ModelProcessor class.
            ModelContent model = base.Process(input, context);

            model.Tag = this.vertices.ToArray();

            return model;
        }

        /// <summary>
        /// Helper for extracting a list of all the vertex positions in a model.
        /// </summary>
        /// <param name="node">The node to process.</param>
        private void FindVertices(Microsoft.Xna.Framework.Content.Pipeline.Graphics.NodeContent node)
        {
            // Is this node a mesh?
            MeshContent mesh = node as MeshContent;

            if (mesh != null)
            {
                // Look up the absolute transform of the mesh.
                Matrix absoluteTransform = mesh.AbsoluteTransform;

                // Loop over all the pieces of geometry in the mesh.
                foreach (GeometryContent geometry in mesh.Geometry)
                {
                    // Loop over all the indices in this piece of geometry.
                    // Every group of three indices represents one triangle.
                    foreach (int index in geometry.Indices)
                    {
                        // Look up the position of this vertex.
                        Vector3 vertex = geometry.Vertices.Positions[index];

                        // Transform from local into world space.
                        vertex = Vector3.Transform(vertex, absoluteTransform);

                        // Store this vertex.
                        this.vertices.Add(vertex);
                    }
                }
            }

            // Recursively scan over the children of this node.
            foreach (Microsoft.Xna.Framework.Content.Pipeline.Graphics.NodeContent child in node.Children)
            {
                this.FindVertices(child);
            }
        }
    }
}
