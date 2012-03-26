namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Provides an extended implementation of the Model class.
    /// </summary>
    public class ExtendedModel
    {
        /// <summary>
        /// Cache of previously loaded ExtendedModels.
        /// </summary>
        private static readonly Dictionary<string, ExtendedModel> cache = new Dictionary<string, ExtendedModel>();

        /// <summary>
        /// The Microsoft.Xna.Framework.Graphics.Model that this object encapsulates.
        /// </summary>
        private readonly Model model;

        /// <summary>
        /// An array of all the points of the vertices in the model.
        /// </summary>
        private Vector3[] vertices;

        /// <summary>
        /// Initializes a new instance of the ExtendedModel class.
        /// </summary>
        /// <param name="content">The ContentManager to use to load the asset.</param>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <exception cref="InvalidOperationException">Vertex data missing or in invalid format.</exception>
        private ExtendedModel(PackFileContentManager content, string assetName)
        {
            this.model = content.Load<Model>(assetName);

            try
            {
                if (this.model.Tag != null && this.model.Tag is Vector3[])
                {
                    this.vertices = (Vector3[])this.model.Tag;
                }
                else
                {
                    throw new InvalidOperationException(Resources.VertexDataMissingInvalid);
                }

                this.BoundingSphere = BoundingSphere.CreateFromPoints(this.vertices);
                this.BoundingBox = BoundingBox.CreateFromPoints(this.vertices);
            }
            finally
            {
                this.model.Tag = null;
            }

            ExtendedModel.cache.Add(assetName, this);
        }

        /// <summary>
        /// Gets a collection of ModelBone objects which describe how each mesh in the
        /// Model.Meshes collection for this model relates to its parent mesh. Reference
        /// page contains links to related code samples.
        /// </summary>
        /// <value>A collection of ModelBone objects used by this model.</value>
        public ModelBoneCollection Bones
        {
            get { return this.model.Bones; }
        }

        /// <summary>
        /// Gets a collection of ModelMesh objects which compose the model. Each ModelMesh
        /// in a model may be moved independently and may be composed of multiple materials
        /// identified as ModelMeshPart objects. Reference page contains links to related
        /// code samples.
        /// </summary>
        /// <value>A collection of ModelMesh objects used by this model.</value>
        public ModelMeshCollection Meshes
        {
            get { return this.model.Meshes; }
        }

        /// <summary>
        /// Gets the root bone for this model.
        /// </summary>
        /// <value>The root bone for this model.</value>
        public ModelBone Root
        {
            get { return this.model.Root; }
        }

        /// <summary>
        /// Gets or sets an object identifying this model.
        /// </summary>
        /// <value>An object identifying this model.</value>
        public object Tag
        {
            get { return this.model.Tag; }
            set { this.model.Tag = value; }
        }

        /// <summary>
        /// Gets a bounding sphere containing the entire object.
        /// </summary>
        /// <value>See summary.</value>
        public BoundingSphere BoundingSphere
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a bounding box containing the entire object.
        /// </summary>
        /// <value>See summary.</value>
        public BoundingBox BoundingBox
        {
            get;
            private set;
        }

        /// <summary>
        /// Loads a model from an XNB asset.
        /// </summary>
        /// <param name="content">The ContentManager to use to load the asset.</param>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <returns>See summary.</returns>
        public static ExtendedModel Load(PackFileContentManager content, string assetName)
        {
            if (ExtendedModel.cache.ContainsKey(assetName))
            {
                return ExtendedModel.cache[assetName];
            }
            else
            {
                return new ExtendedModel(content, assetName);
            }
        }

        /// <summary>
        /// Gets an array of all the points of the vertices in the model.
        /// </summary>
        /// <returns>See summary.</returns>
        public Vector3[] GetVertices()
        {
            return this.vertices.Clone() as Vector3[];
        }

        /// <summary>
        /// Copies a transform of each bone in a model relative to all parent bones of
        /// the bone into a given array. Reference page contains links to related code
        /// samples.
        /// </summary>
        /// <param name="destinationBoneTransforms">The array to receive bone transforms.</param>
        public void CopyAbsoluteBoneTransformsTo(Matrix[] destinationBoneTransforms)
        {
            this.model.CopyAbsoluteBoneTransformsTo(destinationBoneTransforms);
        }

        /// <summary>
        /// Copies an array of transforms into each bone in the model.
        /// </summary>
        /// <param name="sourceBoneTransforms">An array containing new bone transforms.</param>
        public void CopyBoneTransformsFrom(Matrix[] sourceBoneTransforms)
        {
            this.model.CopyBoneTransformsFrom(sourceBoneTransforms);
        }

        /// <summary>
        /// Copies each bone transform relative only to the parent bone of the model
        /// to a given array. Reference page contains links to related code samples.
        /// </summary>
        /// <param name="destinationBoneTransforms">The array to receive bone transforms.</param>
        public void CopyBoneTransformsTo(Matrix[] destinationBoneTransforms)
        {
            this.model.CopyBoneTransformsTo(destinationBoneTransforms);
        }
    }
}
