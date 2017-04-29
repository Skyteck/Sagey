using Microsoft.Xna.Framework;
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
            Vector2 blah = PosToWorldTilePos(pos);
            foreach(TileMap map in mapList)
            {
                if(map.mapTilePos.X == blah.X && map.mapTilePos.Y == blah.Y)
                {
                    return map;
                }
            }
            return null;
        }

        public Tile findTile(Vector2 pos)
        {
            TileMap mapClicked = findMap(PosToWorldTilePos(pos));
            Tile clickedTile = mapClicked.findClickedTile(PosToMapPos(pos));
            return clickedTile;
        }

        private Vector2 PosToWorldTilePos(Vector2 pos)
        {

            int clickMapX = (int)pos.X / 2048;
            int clickMapY = (int)pos.Y / 2048;
            return new Vector2(clickMapX, clickMapY);
        }

        private Vector2 PosToMapPos(Vector2 pos)
        {

            int clickMapX = (int)pos.X / 64;
            int clickMapY = (int)pos.Y / 64;
            return new Vector2(clickMapX, clickMapY);
        }
    }
}
