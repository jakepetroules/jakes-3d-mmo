namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;
    using Petroules.Synteza;
    using MMO3D.CommonCode;

    /// <summary>
    /// Enumerates different types of sky.
    /// </summary>
    [Serializable]
    public enum SkyType
    {
        /// <summary>
        /// A blank, undefined sky - the clear color will be used.
        /// </summary>
        UndefinedSky = 0,

        /// <summary>
        /// A blue cloudy sky.
        /// </summary>
        Cloudy = 1
    }

    /// <summary>
    /// Extensions for the SkyType enumeration.
    /// </summary>
    public static class SkyTypeExtensions
    {
        /// <summary>
        /// Cache of images corresponding to sky types.
        /// </summary>
        private static Dictionary<SkyType, System.Drawing.Bitmap> imageCache = new Dictionary<SkyType, System.Drawing.Bitmap>();

        /// <summary>
        /// Cache of textures corresponding to sky types.
        /// </summary>
        private static Dictionary<SkyType, Texture2D> textureCache = new Dictionary<SkyType, Texture2D>();

        /// <summary>
        /// Gets a Microsoft.Xna.Framework.Graphics.Texture2D of the texture corresponding to the sky type.
        /// </summary>
        /// <param name="skyType">The sky type to get the texture of.</param>
        /// <returns>See summary.</returns>
        public static Texture2D GetTexture(this SkyType skyType)
        {
            if (SkyTypeExtensions.textureCache.ContainsKey(skyType))
            {
                return SkyTypeExtensions.textureCache[skyType];
            }
            else
            {
                System.Drawing.Bitmap image = skyType.GetImage();

                TextureCreationParameters parameters = new TextureCreationParameters();
                parameters.Filter = FilterOptions.Triangle;
                parameters.Format = SurfaceFormat.Color;
                parameters.MipFilter = FilterOptions.Triangle;
                parameters.MipLevels = TextureManipulation.GetMaximumMipLevels(image);

                Texture2D texture = TextureManipulation.Texture2DFromBitmap(image, EngineManager.Engine.GraphicsDevice, parameters);

                texture.LevelOfDetail = 1;
                texture.GenerateMipMaps(TextureFilter.Linear);

                SkyTypeExtensions.textureCache.Add(skyType, texture);
                return texture;
            }
        }

        /// <summary>
        /// Gets a System.Drawing.Bitmap of the texture corresponding to the sky type.
        /// </summary>
        /// <param name="skyType">The sky type to get the texture of.</param>
        /// <returns>See summary.</returns>
        public static System.Drawing.Bitmap GetImage(this SkyType skyType)
        {
            if (SkyTypeExtensions.imageCache.ContainsKey(skyType))
            {
                return SkyTypeExtensions.imageCache[skyType];
            }
            else
            {
                System.Drawing.Bitmap image = null;

                switch (skyType)
                {
                    case SkyType.Cloudy:
                        image = Resources.Cloudy;
                        break;
                    default:
                        image = Resources.Undefined;
                        break;
                }

                SkyTypeExtensions.imageCache.Add(skyType, image);
                return image;
            }
        }

        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static SkyType ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<SkyType>(enumConstant, SkyType.UndefinedSky);
        }

        /// <summary>
        /// Gets a sorted list of the enumeration constants as a
        /// collection of strings with the same names. The constant
        /// representing none, null, empty or default will be placed
        /// at the top of the list, the rest following alphabetically.
        /// </summary>
        /// <returns>See summary.</returns>
        public static string[] GetSortedList()
        {
            return EnumHelper.GetSortedList<SkyType>(SkyType.UndefinedSky);
        }
    }
}
