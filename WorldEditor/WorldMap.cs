namespace MMO3D.WorldEditor
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.IO;
    using Microsoft.Xna.Framework;
    using MMO3D.CommonCode;
    using MMO3D.Engine;
    using Xna = Microsoft.Xna.Framework;
    using System.Windows.Forms;

    /// <summary>
    /// Contains methods for generating 2D images of the terrain for world maps.
    /// </summary>
    public static class WorldMap
    {
        /// <summary>
        /// The progress (0-100) of the current DrawMap operation.
        /// </summary>
        private static int progress;

        /// <summary>
        /// Raised when the progress of map generation changes.
        /// </summary>
        public static event EventHandler ProgressChanged = delegate { };

        /// <summary>
        /// Gets the progress (0-100) of the current DrawMap operation.
        /// </summary>
        /// <value>See summary.</value>
        public static int Progress
        {
            get
            {
                return WorldMap.progress;
            }

            private set
            {
                WorldMap.progress = value;
                WorldMap.ProgressChanged(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to cancel
        /// generation of a world map as soon as possible.
        /// </summary>
        /// <value>See summary.</value>
        public static bool Cancel
        {
            get;
            set;
        }

        /// <summary>
        /// Draws the pack file to a bitmap and returns the result.
        /// </summary>
        /// <param name="packFileName">The file name of the pack file to read from.</param>
        /// <param name="textured">Whether the map should be drawn textured.</param>
        /// <param name="season">The season to draw the map in.</param>
        /// <param name="hideBase">
        /// Whether to draw undefined for non-base season
        /// instead of showing the base season terrain.
        /// </param>
        /// <returns>See summary.</returns>
        public static Bitmap DrawMap(string packFileName, bool textured, Season season, bool hideBase)
        {
            using (BinaryReader reader = new BinaryReader(new FileStream(packFileName, FileMode.Open)))
            {
                // Read pack file index
                TerrainPackFileReader index = new TerrainPackFileReader(reader);

                // Get the IDs of the patches into an array
                Point3D[] points = WorldMap.CopyPackFileIndexKeys(index.PackFileIndex);

                // Determine the lowest and highest patch IDs we'll need the coordinates of, to draw the map
                Point3D lowestPatchID = WorldMap.GetLowestTileID(index.PackFileIndex);
                Point3D highestPatchID = WorldMap.GetHighestTileID(index.PackFileIndex);

                // Determine the map size, in pixels
                Size bitmapSize = WorldMap.GetMapPixelSize(lowestPatchID, highestPatchID);

                // Generate the image objects
                Bitmap bmp = new Bitmap(bitmapSize.Width, bitmapSize.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(TextureManipulation.XnaColorToSystemColor(TerrainType.UndefinedTerrain.GetWorldMapColor()));

                WorldMap.Progress = 0;
                WorldMap.Cancel = false;

                // Draw all tiles onto the bitmap
                for (int i = 0; i < points.Length && !WorldMap.Cancel; i++)
                {
                    // Skip this tile if we're not at ground level
                    if (points[i].Z != 0)
                    {
                        continue;
                    }

                    WorldMap.DrawTerrainPatch(index.ReadTerrainPatch(points[i]), lowestPatchID, bitmapSize, g, textured, season, hideBase);

                    WorldMap.Progress = Convert.ToInt32(((i + 1f) / points.Length) * 100);
                }

                return bmp;
            }
        }

        /// <summary>
        /// Draws the collection of terrain patches to a bitmap and returns the result.
        /// </summary>
        /// <param name="terrainPatches">The filenames of the terrain patches to draw on the wold map.</param>
        /// <param name="index">The pack file terrain patch index.</param>
        /// <param name="textured">Whether the map should be drawn textured.</param>
        /// <param name="season">The season to draw the map in.</param>
        /// <param name="hideBase">
        /// Whether to draw undefined for non-base season
        /// instead of showing the base season terrain.
        /// </param>
        /// <returns>See summary.</returns>
        public static Bitmap DrawMap(StringCollection terrainPatches, PackFileIndex index, bool textured, Season season, bool hideBase)
        {
            // Determine the lowest and highest patch IDs we'll need the coordinates of, to draw the map
            Point3D lowestPatchID = WorldMap.GetLowestTileID(index);
            Point3D highestPatchID = WorldMap.GetHighestTileID(index);

            // Determine the map size, in pixels
            Size bitmapSize = WorldMap.GetMapPixelSize(lowestPatchID, highestPatchID);

            // Generate the image objects
            Bitmap bmp = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics g = Graphics.FromImage(bmp);

            WorldMap.Progress = 0;
            WorldMap.Cancel = false;

            // Draw all tiles onto the bitmap
            for (int i = 0; i < terrainPatches.Count && !WorldMap.Cancel; i++)
            {
                // Skip this tile if we're not at ground level
                TerrainPatch patch = TerrainPatch.FromByteArray(File.ReadAllBytes(terrainPatches[i]));
                if (patch.PatchId.Z != 0)
                {
                    continue;
                }

                WorldMap.DrawTerrainPatch(patch, lowestPatchID, bitmapSize, g, textured, season, hideBase);

                WorldMap.Progress = Convert.ToInt32((i + 1f) / terrainPatches.Count) * 100;
            }

            return bmp;
        }

        /// <summary>
        /// Draws the collection of terrain patches to a bitmap and returns the result.
        /// </summary>
        /// <param name="terrainPatches">The terrain patches to draw on the wold map.</param>
        /// <param name="textured">Whether the map should be drawn textured.</param>
        /// <param name="season">The season to draw the map in.</param>
        /// <param name="hideBase">
        /// Whether to draw undefined for non-base season
        /// instead of showing the base season terrain.
        /// </param>
        /// <returns>See summary.</returns>
        public static Bitmap DrawMap(Collection<TerrainPatch> terrainPatches, bool textured, Season season, bool hideBase)
        {
            // Determine the lowest and highest patch IDs we'll need the coordinates of, to draw the map
            Point3D lowestPatchID = WorldMap.GetLowestTileID(terrainPatches);
            Point3D highestPatchID = WorldMap.GetHighestTileID(terrainPatches);

            // Determine the map size, in pixels
            Size bitmapSize = WorldMap.GetMapPixelSize(lowestPatchID, highestPatchID);

            // Generate the image objects
            Bitmap bmp = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics g = Graphics.FromImage(bmp);

            WorldMap.Progress = 0;
            WorldMap.Cancel = false;

            // Draw all tiles onto the bitmap
            for (int i = 0; i < terrainPatches.Count && !WorldMap.Cancel; i++)
            {
                // Skip this tile if we're not at ground level
                if (terrainPatches[i].PatchId.Z != 0)
                {
                    continue;
                }

                WorldMap.DrawTerrainPatch(terrainPatches[i], lowestPatchID, bitmapSize, g, textured, season, hideBase);

                WorldMap.Progress = Convert.ToInt32((i + 1f) / terrainPatches.Count) * 100;
            }

            return bmp;
        }

        /// <summary>
        /// Draws the specified terrain patch using the specified graphics context.
        /// </summary>
        /// <param name="terrainPatch">The terrain patch to draw.</param>
        /// <param name="lowestPatchID">The lowest terrain patch ID that will be needed to draw the map.</param>
        /// <param name="bitmapSize">The height of the finished world map, in pixels.</param>
        /// <param name="g">The graphics context to draw in.</param>
        /// <param name="textured">Whether the map should be drawn textured.</param>
        /// <param name="season">The season to draw the map in.</param>
        /// <param name="hideBase">
        /// Whether to draw undefined for non-base season
        /// instead of showing the base season terrain.
        /// </param>
        private static void DrawTerrainPatch(TerrainPatch terrainPatch, Point3D lowestPatchID, Size bitmapSize, Graphics g, bool textured, Season season, bool hideBase)
        {
            for (int x = 0; x < TerrainPatch.PatchSize; x++)
            {
                for (int y = 0; y < TerrainPatch.PatchSize; y++)
                {
                    // Get the patch local point and screen point
                    Vector2 localPoint = new Vector2(x, y);
                    Xna.Point screen = WorldMap.ProjectScreenPoint(lowestPatchID, CoordinateConverter.LocalToWorld(terrainPatch.PatchId, new Xna.Point(x, y)), bitmapSize);

                    // Initially set the draw color to that of the terrain
                    Color drawColor = WorldMap.GetDrawColor(terrainPatch.GetTerrainTypeDataForSeason(localPoint, season, hideBase), x, y, textured);
                    
                    // Store the heighest terrain height so far
                    float highestHeight = terrainPatch.GetExactElevation(localPoint);

                    // Loop through all the fluids
                    for (int i = 0; i < terrainPatch.Fluids.Count; i++)
                    {
                        // If the fluid contains the local point, check its height
                        if (terrainPatch.Fluids[i].ContainsPoint(season, localPoint))
                        {
                            // If the fluid's height is higher than the highest height (initially, the terrain's)
                            if (terrainPatch.Fluids[i].GetExactHeight(season, localPoint) > highestHeight)
                            {
                                // Set the highest height to this fluid's, and set the draw color to this fluid's
                                highestHeight = terrainPatch.Fluids[i].GetExactHeight(season, localPoint);
                                drawColor = WorldMap.GetDrawColor(terrainPatch.Fluids[i].GetFluidType(season), x, y, textured);
                            }
                        }
                    }

                    g.FillRectangle(new SolidBrush(drawColor), screen.X, screen.Y, 1, 1);
                }
            }
        }

        /// <summary>
        /// Transforms a 3D space coordinate to a screen coordinate (the pixel location on the bitmap).
        /// </summary>
        /// <param name="lowestPatchID">The ID of the lowest terrain patch needed to draw the map.</param>
        /// <param name="worldPoint">The coordinates to transform, in tile space (relative to the tile).</param>
        /// <param name="bitmapSize">The size of the finished world map, in pixels.</param>
        /// <returns>See summary.</returns>
        private static Xna.Point ProjectScreenPoint(Point3D lowestPatchID, Xna.Point worldPoint, Size bitmapSize)
        {
            Xna.Point lowestWorldPoint = new Xna.Point(lowestPatchID.X * TerrainPatch.PatchSize, lowestPatchID.Y * TerrainPatch.PatchSize);

            return new Xna.Point(worldPoint.X - lowestWorldPoint.X, bitmapSize.Height - (worldPoint.Y - lowestWorldPoint.Y) - 1);
        }

        /// <summary>
        /// Gets the size of the world map in pixels.
        /// </summary>
        /// <param name="lowestPatchID">The lowest terrain patch ID that will be needed to draw the map.</param>
        /// <param name="highestPatchID">The highest terrain patch ID that will be needed to draw the map.</param>
        /// <returns>See summary.</returns>
        private static Size GetMapPixelSize(Point3D lowestPatchID, Point3D highestPatchID)
        {
            return new Size(((highestPatchID.X - lowestPatchID.X) + 1) * TerrainPatch.PatchSize, ((highestPatchID.Y - lowestPatchID.Y) + 1) * TerrainPatch.PatchSize);
        }

        /// <summary>
        /// Gets the lowest terrain patch ID that will be needed to draw the map.
        /// </summary>
        /// <param name="terrainPatches">The terrain patch collection to determine the IDs with.</param>
        /// <returns>See summary.</returns>
        private static Point3D GetLowestTileID(Collection<TerrainPatch> terrainPatches)
        {
            PackFileIndex dict = new PackFileIndex();

            for (int i = 0; i < terrainPatches.Count; i++)
            {
                dict.Entries.Add(terrainPatches[i].PatchId.ToString(), PackFileEntry.Null);
            }

            return WorldMap.GetLowestTileID(dict);
        }

        /// <summary>
        /// Gets the highest terrain patch ID that will be needed to draw the map.
        /// </summary>
        /// <param name="terrainPatches">The terrain patch collection to determine the IDs with.</param>
        /// <returns>See summary.</returns>
        private static Point3D GetHighestTileID(Collection<TerrainPatch> terrainPatches)
        {
            PackFileIndex dict = new PackFileIndex();

            for (int i = 0; i < terrainPatches.Count; i++)
            {
                dict.Entries.Add(terrainPatches[i].PatchId.ToString(), PackFileEntry.Null);
            }

            return WorldMap.GetHighestTileID(dict);
        }

        /// <summary>
        /// Gets the lowest terrain patch ID that will be needed to draw the map.
        /// </summary>
        /// <param name="index">The pack file patch index to determine the IDs with.</param>
        /// <returns>See summary.</returns>
        private static Point3D GetLowestTileID(PackFileIndex index)
        {
            Point3D lowestPatchID = Point3D.Zero;

            Point3D[] points = WorldMap.CopyPackFileIndexKeys(index);

            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].X < lowestPatchID.X)
                {
                    lowestPatchID.X = points[i].X;
                }

                if (points[i].Y < lowestPatchID.Y)
                {
                    lowestPatchID.Y = points[i].Y;
                }
            }

            return lowestPatchID;
        }

        /// <summary>
        /// Gets the highest terrain patch ID that will be needed to draw the map.
        /// </summary>
        /// <param name="index">The pack file patch index to determine the IDs with.</param>
        /// <returns>See summary.</returns>
        private static Point3D GetHighestTileID(PackFileIndex index)
        {
            Point3D highestPatchID = Point3D.Zero;

            Point3D[] points = WorldMap.CopyPackFileIndexKeys(index);

            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].X > highestPatchID.X)
                {
                    highestPatchID.X = points[i].X;
                }

                if (points[i].Y > highestPatchID.Y)
                {
                    highestPatchID.Y = points[i].Y;
                }
            }

            return highestPatchID;
        }

        /// <summary>
        /// Copies the keys of a pack file index into an array of Microsoft.Xna.Framework.Point structures.
        /// </summary>
        /// <param name="index">The pack file index.</param>
        /// <returns>See summary.</returns>
        private static Point3D[] CopyPackFileIndexKeys(PackFileIndex index)
        {
            string[] entries = new string[index.Entries.Count];
            index.Entries.Keys.CopyTo(entries, 0);

            Point3D[] points = new Point3D[entries.Length];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = MathExtensions.StringToPoint(entries[i]);
            }

            return points;
        }

        /// <summary>
        /// Gets the color a particular pixel should be drawn with.
        /// </summary>
        /// <param name="terrainType">The type of terrain.</param>
        /// <param name="localX">The patch local X coordinate.</param>
        /// <param name="localY">The patch local Y coordinate.</param>
        /// <param name="textured">Whether to return color for a textured render.</param>
        /// <returns>See summary.</returns>
        private static Color GetDrawColor(TerrainType terrainType, int localX, int localY, bool textured)
        {
            if (textured)
            {
                Bitmap texture = terrainType.GetImage();
                Xna.Point texCoords = WorldMap.GetPixels(new Vector2(localX / (float)TerrainPatch.PatchSize, localY / (float)TerrainPatch.PatchSize), texture);
                Color sysColor = texture.GetPixel(texCoords.X, texCoords.Y);
                Xna.Graphics.Color drawColor = new Microsoft.Xna.Framework.Graphics.Color(sysColor.R, sysColor.G, sysColor.B, sysColor.A);

                return TextureManipulation.XnaColorToSystemColor(drawColor);
            }
            else
            {
                return TextureManipulation.XnaColorToSystemColor(terrainType.GetWorldMapColor());
            }
        }

        /// <summary>
        /// Gets the color a particular pixel should be drawn with.
        /// </summary>
        /// <param name="fluidType">The type of terrain.</param>
        /// <param name="localX">The patch local X coordinate.</param>
        /// <param name="localY">The patch local Y coordinate.</param>
        /// <param name="textured">Whether to return color for a textured render.</param>
        /// <returns>See summary.</returns>
        private static Color GetDrawColor(FluidType fluidType, int localX, int localY, bool textured)
        {
            if (textured)
            {
                Bitmap texture = fluidType.GetImage();
                Xna.Point texCoords = WorldMap.GetPixels(new Vector2(localX / (float)TerrainPatch.PatchSize, localY / (float)TerrainPatch.PatchSize), texture);
                Color sysColor = texture.GetPixel(texCoords.X, texCoords.Y);
                Xna.Graphics.Color drawColor = new Microsoft.Xna.Framework.Graphics.Color(sysColor.R, sysColor.G, sysColor.B, sysColor.A);

                return TextureManipulation.XnaColorToSystemColor(drawColor);
            }
            else
            {
                return TextureManipulation.XnaColorToSystemColor(fluidType.GetWorldMapColor());
            }
        }

        /// <summary>
        /// Gets the pixel coordinates for a set of texture coordinates on a particular image.
        /// </summary>
        /// <param name="textureCoordinates">The texture coordinates.</param>
        /// <param name="image">The image to get the pixels of.</param>
        /// <returns>See summary.</returns>
        private static Xna.Point GetPixels(Vector2 textureCoordinates, Image image)
        {
            textureCoordinates = MathExtensions.Modulate(textureCoordinates, 1);

            int x = Convert.ToInt32(textureCoordinates.X * (image.Width - 1));
            int y = Convert.ToInt32(textureCoordinates.Y * (image.Height - 1));

            return new Xna.Point(x, y);
        }
    }
}
