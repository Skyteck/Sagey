using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;

namespace ExtendedTest
{
    class TileMap
    {
        public TmxMap map;
        public Texture2D tileset;
        public Vector2 _Postion;
        public int tileWidth;

        public int tileHeight;

        public int tilesetTilesWide;

        public int tilesetTilesHigh;

        public List<Tile> backgroundTiles;

        public bool active = false;

        List<String> nearbyMaps;
        public Rectangle tileMapRect;

        public Vector2 mapTilePos;

        public TileMap(String mapName, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            backgroundTiles = new List<Tile>();
            nearbyMaps = new List<string>();
            map = new TmxMap("Content/Tilemaps/" + mapName + ".tmx");
            nearbyMaps = mapName.Split('-').ToList();

            string tileSetPath = map.Tilesets[0].Name.ToString();
            tileSetPath = "Tilemaps/" + tileSetPath;
            tileset = content.Load<Texture2D>(tileSetPath);

            tileHeight = map.Tilesets[0].TileHeight;
            tileWidth = map.Tilesets[0].TileWidth;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;
            int mapWidth = map.Width * tileWidth;
            int mapHeight = map.Height * tileHeight;
            this._Postion = new Vector2(Convert.ToInt32(nearbyMaps[0]) * mapWidth, Convert.ToInt32(nearbyMaps[1]) * mapHeight);
            mapTilePos = new Vector2(Convert.ToInt32(nearbyMaps[0]), Convert.ToInt32(nearbyMaps[1]));
            tileMapRect = new Rectangle((int)this._Postion.X, (int)this._Postion.Y, mapWidth, mapHeight);

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

                    int tileX = (int)(x / map.TileWidth);
                    int tileY = (int)(y / map.TileHeight);
                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                    
                    x += this._Postion.X;
                    y += this._Postion.Y;
                    Vector2 tilePos = new Vector2(x, y);
                    if (test == true)
                    {
                        test = false;
                    }
                    else
                    {
                        test = true;
                    }
                    Tile newTile = new Tile(tileset, tilePos, tileWidth, tileHeight, column, row, true, new Vector2(tileX, tileY));
                    backgroundTiles.Add(newTile);
                }
            }
        }

        public TmxList<TmxObject> findObjects()
        {
            if (map.ObjectGroups.Count >= 1)
            {
                return map.ObjectGroups["Object Layer 1"].Objects;

            }
            else return null;
        }

        public TmxList<TmxObject> findNPCs()
        {
            if (map.ObjectGroups.Count >= 1)
            {
                return map.ObjectGroups["NPC Layer"].Objects;

            }
            else return null;
        }

        public void Update(GameTime gameTime)
        {
            if(active)
            {
                List<Tile> activeTiles = backgroundTiles.FindAll(x => x.active == true);
                foreach(Tile tile in activeTiles)
                {
                    tile.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(active)
            {
                List<Tile> drawTiles = new List<Tile>();
                drawTiles = backgroundTiles.FindAll(x => x.visible == true);
                foreach(Tile tile in drawTiles)
                {
                    tile.Draw(spriteBatch);
                }
            }
        }

        public Tile findClickedTile(Vector2 pos)
        {
            foreach(Tile tile in backgroundTiles)
            {
                if(tile.localPos.X == pos.X && tile.localPos.Y == pos.Y)
                {
                    return tile;
                }
            }
            return null;
        }
    }
}