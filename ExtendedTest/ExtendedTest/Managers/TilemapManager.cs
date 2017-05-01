using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class TilemapManager
    {
        public List<TileMap> mapList;

        public TilemapManager()
        {
            mapList = new List<TileMap>();
        }

        public void AddMap(TileMap newMap)
        {
            mapList.Add(newMap);
        }

        public TileMap findMap(Vector2 pos)
        {
            foreach(TileMap map in mapList)
            {
                if(map.mapTilePos.X == pos.X && map.mapTilePos.Y == pos.Y)
                {
                    return map;
                }
            }
            return null;
        }

        public Tile findTile(Vector2 pos)
        {
            Vector2 posToTileMapPos = PosToWorldTilePos(pos);
            Vector2 localTileMapPos = new Vector2(pos.X - (posToTileMapPos.X * 2048), pos.Y - (posToTileMapPos.Y * 2048));
            TileMap mapClicked = findMap(PosToWorldTilePos(pos));
            if(mapClicked!=null)
            {
                Tile clickedTile = mapClicked.findClickedTile(PosToMapPos(localTileMapPos));

                return clickedTile;
            }
            return null;
        }

        private Vector2 PosToWorldTilePos(Vector2 pos)
        {

            int clickMapX = (int)pos.X / 2048;
            int clickMapY = (int)pos.Y / 2048;
            return new Vector2(clickMapX, clickMapY);
        }

        private Vector2 PosToMapPos(Vector2 pos)
        {
            //need to change this to the coordinates within the tilemap itself...
            int clickMapX = (int)pos.X / 64;
            int clickMapY = (int)pos.Y / 64;
            return new Vector2(clickMapX, clickMapY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(TileMap map in mapList)
            {
                map.Draw(spriteBatch);
            }
        }
    }
}
