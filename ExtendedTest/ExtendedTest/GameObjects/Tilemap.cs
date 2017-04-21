using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ExtendedTest
{
    class TileMap
    {
        public TmxMap map;
        public Texture2D tileset;

        public int tileWidth;

        public int tileHeight;

        public int tilesetTilesWide;

        public int tilesetTilesHigh;

        public List<Tile> backgroundTiles;

        public TileMap(String path, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            backgroundTiles = new List<Tile>();
            map = new TmxMap(path);
            string tileSetPath = map.Tilesets[0].Name.ToString();
            tileSetPath = "Tilemaps/" + tileSetPath;
            tileset = content.Load<Texture2D>(tileSetPath);

            tileHeight = map.Tilesets[0].TileHeight;
            tileWidth = map.Tilesets[0].TileWidth;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;
            bool test = true;
            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                int gid = map.Layers[0].Tiles[i].Gid;

                // Empty tile, do nothing
                if (gid != 0)
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row;
                    if (tileFrame + 1 > tilesetTilesWide)
                    {
                        row = tileFrame - column * tilesetTilesWide;
                    }
                    else
                    {
                        row = 0;
                    }

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    //spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    Vector2 tilePos = new Vector2(x, y);
                    if (test == true)
                    {
                        test = false;
                    }
                    else
                    {
                        test = true;
                    }
                    Tile newTile = new Tile(tileset, tilePos, tileWidth, tileHeight, column, row, true);
                    backgroundTiles.Add(newTile);
                }
            }
        }

        public TmxList<TmxObject> findObjects()
        {
            return map.ObjectGroups["Object Layer 1"].Objects;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            //{
            //    int gid = map.Layers[0].Tiles[i].Gid;

            //    // Empty tile, do nothing
            //    if (gid != 0)
            //    {
            //        int tileFrame = gid - 1;
            //        int column = tileFrame % tilesetTilesWide;
            //        int row = (tileFrame + 1 > tilesetTilesWide) ? tileFrame - column * tilesetTilesWide : 0;

            //        float x = (i % map.Width) * map.TileWidth;
            //        float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

            //        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

            //        spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
            //    }
            //}
            List<Tile> drawTiles = new List<Tile>();
            drawTiles = backgroundTiles.FindAll(x => x.visible == true);
            foreach(Tile tile in drawTiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}