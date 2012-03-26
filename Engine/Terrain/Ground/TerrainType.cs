namespace MMO3D.Engine
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;
    using Petroules.Synteza;
    using MMO3D.CommonCode;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Enumerates different types of terrain.
    /// </summary>
    [Serializable]
    public enum TerrainType
    {
        /// <summary>
        /// Specifies undefined or unknown terrain.
        /// </summary>
        UndefinedTerrain = 0,

        /// <summary>
        /// Specifies brown dirt terrain.
        /// </summary>
        Dirt = 1,

        /// <summary>
        /// Specifies green grass terrain.
        /// </summary>
        Grass = 2,

        /// <summary>
        /// Specifies pebble terrain.
        /// </summary>
        Pebbles = 3,

        /// <summary>
        /// Specifies solid, rocky terrain.
        /// </summary>
        Rock = 4,

        /// <summary>
        /// Defines sandy terrain.
        /// </summary>
        Sand = 5,

        /// <summary>
        /// Defines snowy terrain.
        /// </summary>
        Snow = 6,

        /// <summary>
        /// Defines desert sand terrain.
        /// </summary>
        DesertSand = 7
    }

    /// <summary>
    /// Extensions for the TerrainType enumeration.
    /// </summary>
    public static class TerrainTypeExtensions
    {
        /// <summary>
        /// Cache of images corresponding to terrain types.
        /// </summary>
        private static Dictionary<TerrainType, System.Drawing.Bitmap> imageCache = new Dictionary<TerrainType, System.Drawing.Bitmap>();

        /// <summary>
        /// Cache of textures corresponding to terrain types.
        /// </summary>
        private static Dictionary<TerrainType, Texture2D> textureCache = new Dictionary<TerrainType, Texture2D>();

        /// <summary>
        /// Gets a Microsoft.Xna.Framework.Graphics.Texture2D of the texture corresponding to the terrain type.
        /// </summary>
        /// <param name="terrainType">The terrain type to get the texture of.</param>
        /// <returns>See summary.</returns>
        public static Texture2D GetTexture(this TerrainType terrainType)
        {
            if (TerrainTypeExtensions.textureCache.ContainsKey(terrainType))
            {
                return TerrainTypeExtensions.textureCache[terrainType];
            }
            else
            {
                System.Drawing.Bitmap image = terrainType.GetImage();

                /*TextureCreationParameters parameters = new TextureCreationParameters();
                parameters.Filter = FilterOptions.Triangle;
                parameters.Format = SurfaceFormat.Color;
                parameters.MipFilter = FilterOptions.Triangle;
                parameters.MipLevels = TextureManipulation.GetMaximumMipLevels(image);*/

                Texture2D texture = TextureManipulation.Texture2DFromBitmap(image, EngineManager.Engine.GraphicsDevice);//, parameters);

                //texture.LevelOfDetail = 1;
                //
                texture.GenerateMipMaps(TextureFilter.Linear);

                TerrainTypeExtensions.textureCache.Add(terrainType, texture);
                return texture;
            }
        }

        /// <summary>
        /// Gets a System.Drawing.Bitmap of the texture corresponding to the terrain type.
        /// </summary>
        /// <param name="terrainType">The terrain type to get the texture of.</param>
        /// <returns>See summary.</returns>
        public static System.Drawing.Bitmap GetImage(this TerrainType terrainType)
        {
            if (TerrainTypeExtensions.imageCache.ContainsKey(terrainType))
            {
                return TerrainTypeExtensions.imageCache[terrainType];
            }
            else
            {
                System.Drawing.Bitmap image = null;

                switch (terrainType)
                {
                    case TerrainType.Dirt:
                        image = Resources.Dirt;
                        break;
                    case TerrainType.Grass:
                        image = Resources.Grass;
                        break;
                    case TerrainType.Pebbles:
                        image = Resources.Pebbles;
                        break;
                    case TerrainType.Rock:
                        image = Resources.Rock;
                        break;
                    case TerrainType.Sand:
                        image = Resources.Sand;
                        break;
                    case TerrainType.Snow:
                        image = Resources.Snow;
                        break;
                    case TerrainType.DesertSand:
                        image = Resources.DesertSand;
                        break;
                    default:
                        image = Resources.Undefined;
                        break;
                }

                TerrainTypeExtensions.imageCache.Add(terrainType, image);
                return image;
            }
        }

        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found,
        /// the constant representing none, null, empty or default will be returned.
        /// </summary>
        /// <param name="enumConstant">The string to parse.</param>
        /// <returns>See summary.</returns>
        public static TerrainType ParseFromString(string enumConstant)
        {
            return EnumHelper.ParseFromString<TerrainType>(enumConstant, TerrainType.UndefinedTerrain);
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
            return EnumHelper.GetSortedList<TerrainType>(TerrainType.UndefinedTerrain);
        }

        /// <summary>
        /// Gets a value indicating whether the terrain type is traversable.
        /// </summary>
        /// <param name="terrainType">The terrain type to determine the traversability of.</param>
        /// <returns>See summary.</returns>
        public static bool IsTraversable(this TerrainType terrainType)
        {
            // Most terrain types should be traversable,
            // so simply add a case for each un-traversable
            // terrain type and let the default case handle
            // all the others
            switch (terrainType)
            {
                case TerrainType.UndefinedTerrain:
                    return false;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Gets the color that this terrain type should be drawn with on the world map.
        /// </summary>
        /// <param name="terrainType">The terrain type to determine the world map color-code of.</param>
        /// <returns>See summary.</returns>
        public static Color GetWorldMapColor(this TerrainType terrainType)
        {
            switch (terrainType)
            {
                case TerrainType.Dirt:
                    return Color.Brown;
                case TerrainType.Grass:
                    return Color.Green;
                case TerrainType.Pebbles:
                    return Color.SlateBlue;
                case TerrainType.Rock:
                    return Color.SlateGray;
                case TerrainType.Sand:
                    return Color.Tan;
                case TerrainType.Snow:
                    return Color.Snow;
                case TerrainType.DesertSand:
                    return Color.Yellow;
                default:
                    return Color.Black;
            }
        }
    }
}
