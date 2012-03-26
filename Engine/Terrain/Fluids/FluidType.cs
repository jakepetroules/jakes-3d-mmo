namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;
    using Petroules.Synteza;
    using MMO3D.CommonCode;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Enumerates different types of fluids.
    /// </summary>
    [Serializable]
    public enum FluidType
    {
        /// <summary>
        /// Specifies an undefined or unknown fluid.
        /// </summary>
        UndefinedFluid = 0,

        /// <summary>
        /// Specifies water.
        /// </summary>
        Water = 1,

        /// <summary>
        /// Specifies lava.
        /// </summary>
        Lava = 2
    }

    /// <summary>
    /// Extensions for the FluidType enumeration.
    /// </summary>
    public static class FluidTypeExtensions
    {
        /// <summary>
        /// Cache of images corresponding to fluid types.
        /// </summary>
        private static Dictionary<FluidType, System.Drawing.Bitmap> imageCache = new Dictionary<FluidType, System.Drawing.Bitmap>();

        /// <summary>
        /// Cache of textures corresponding to fluid types.
        /// </summary>
        private static Dictionary<FluidType, Texture2D> textureCache = new Dictionary<FluidType, Texture2D>();

        /// <summary>
        /// Gets a Microsoft.Xna.Framework.Graphics.Texture2D of the texture corresponding to the fluid type.
        /// </summary>
        /// <param name="fluidType">The fluid type to get the texture of.</param>
        /// <returns>See summary.</returns>
        public static Texture2D GetTexture(this FluidType fluidType)
        {
            if (FluidTypeExtensions.textureCache.ContainsKey(fluidType))
            {
                return FluidTypeExtensions.textureCache[fluidType];
            }
            else
            {
                System.Drawing.Bitmap image = fluidType.GetImage();

                TextureCreationParameters parameters = new TextureCreationParameters();
                parameters.Filter = FilterOptions.Triangle;
                parameters.Format = SurfaceFormat.Color;
                parameters.MipFilter = FilterOptions.Triangle;
                parameters.MipLevels = TextureManipulation.GetMaximumMipLevels(image);

                Texture2D texture = TextureManipulation.Texture2DFromBitmap(image, EngineManager.Engine.GraphicsDevice, parameters);

                texture.LevelOfDetail = 1;
                texture.GenerateMipMaps(TextureFilter.Linear);

                FluidTypeExtensions.textureCache.Add(fluidType, texture);
                return texture;
            }
        }

        /// <summary>
        /// Gets a System.Drawing.Bitmap of the texture corresponding to the fluid type.
        /// </summary>
        /// <param name="fluidType">The fluid type to get the texture of.</param>
        /// <returns>See summary.</returns>
        public static System.Drawing.Bitmap GetImage(this FluidType fluidType)
        {
            if (FluidTypeExtensions.imageCache.ContainsKey(fluidType))
            {
                return FluidTypeExtensions.imageCache[fluidType];
            }
            else
            {
                System.Drawing.Bitmap image = null;

                switch (fluidType)
                {
                    case FluidType.Water:
                        image = Resources.Water;
                        break;
                    case FluidType.Lava:
                        image = Resources.Lava;
                        break;
                    default:
                        image = Resources.Undefined;
                        break;
                }

                FluidTypeExtensions.imageCache.Add(fluidType, image);
                return image;
            }
        }

        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static FluidType ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<FluidType>(enumConstant, FluidType.UndefinedFluid);
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
            return EnumHelper.GetSortedList<FluidType>(FluidType.UndefinedFluid);
        }

        /// <summary>
        /// Gets a value indicating whether the fluid type is traversable.
        /// </summary>
        /// <param name="fluidType">The fluid type to determine the traversability of.</param>
        /// <returns>See summary.</returns>
        public static bool IsTraversable(this FluidType fluidType)
        {
            // Most fluid types should NOT be traversable,
            // so simply add a case for each traversable
            // terrain type and let the default case handle
            // all the others
            switch (fluidType)
            {
                case FluidType.UndefinedFluid:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the transparency of this fluid type as number from 0 to 1.
        /// </summary>
        /// <param name="fluidType">The fluid type to determine the transparency of.</param>
        /// <returns>See summary.</returns>
        public static float Transparency(this FluidType fluidType)
        {
            switch (fluidType)
            {
                case FluidType.Water:
                    return 0.6f;
                case FluidType.Lava:
                    return 1;
                default:
                    return 1;
            }
        }

        /// <summary>
        /// Gets the color that this fluid type should be drawn with on the world map.
        /// </summary>
        /// <param name="fluidType">The fluid type to determine the world map color-code of.</param>
        /// <returns>See summary.</returns>
        public static Color GetWorldMapColor(this FluidType fluidType)
        {
            switch (fluidType)
            {
                case FluidType.Water:
                    return Color.Blue;
                case FluidType.Lava:
                    return Color.Orange;
                default:
                    return Color.Black;
            }
        }
    }
}
