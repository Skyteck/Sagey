using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Sagey.Managers
{
    public class TilemapManager
    {
        List<TileMap> mapList;
        public TileMap ActiveMap;
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

        public TileMap findMapByName(String name)
        {
            foreach(TileMap map in mapList)
            {
                if(map.name.Equals(name))
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

        public List<Tile> FindAdjacentTiles(Vector2 pos, bool allowDiagonal = true)
        {
            Tile targetTile = findTile(pos);
            List<Tile> adjacentTiles = new List<Tile>();

            //get top center tile
            Tile topCenter = findTile(new Vector2(targetTile.tileCenter.X, targetTile.tileCenter.Y - 64));
            if (topCenter.walkable)
            {
                adjacentTiles.Add(topCenter);
            }
            Tile LeftTile = findTile(new Vector2(targetTile.tileCenter.X - 64, targetTile.tileCenter.Y));
            if (LeftTile.walkable)
            {
                adjacentTiles.Add(LeftTile);
            }
            Tile rightTIle = findTile(new Vector2(targetTile.tileCenter.X + 64, targetTile.tileCenter.Y));
            if (rightTIle.walkable)
            {
                adjacentTiles.Add(rightTIle);
            }
            Tile bottomCenter = findTile(new Vector2(targetTile.tileCenter.X, targetTile.tileCenter.Y + 64));
            if (bottomCenter.walkable)
            {
                adjacentTiles.Add(bottomCenter);
            }

            if (allowDiagonal)
            {
                Tile topleft = findTile(new Vector2(targetTile.tileCenter.X - 64, targetTile.tileCenter.Y - 64));
                if (topleft.walkable)
                {
                    adjacentTiles.Add(topleft);
                }
                Tile topRight = findTile(new Vector2(targetTile.tileCenter.X + 64, targetTile.tileCenter.Y - 64));
                if (topRight.walkable)
                {
                    adjacentTiles.Add(topRight);
                }
                Tile bottomLeft = findTile(new Vector2(targetTile.tileCenter.X - 64, targetTile.tileCenter.Y + 64));
                if (bottomLeft.walkable)
                {
                    adjacentTiles.Add(bottomLeft);
                }
                Tile bottomRight = findTile(new Vector2(targetTile.tileCenter.X + 64, targetTile.tileCenter.Y + 64));
                if (bottomRight.walkable)
                {
                    adjacentTiles.Add(bottomRight);
                }
            }

            return adjacentTiles;
        }
        

        public Tile FindClosestTile(List<Tile> list, Tile target)
        {
            float distance = 99999999;
            Tile closest = list.First();
            float newDistance = Vector2.Distance(closest.tileCenter, target.tileCenter);

            foreach(Tile tile in list)
            {
                newDistance = Vector2.Distance(tile.tileCenter, target.tileCenter);
                if(newDistance < distance)
                {
                    distance = newDistance;
                    closest = tile;
                }
            }

            return closest;
        }
        

        public Vector2 PosToWorldTilePos(Vector2 pos)
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

        internal Tile findWalkableTile(Vector2 newPos)
        {
            Tile newTile = findTile(newPos);
            if(newTile.walkable)
            {
                return newTile;
            }
            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(TileMap map in mapList)
            {
                map.Draw(spriteBatch);
            }
        }

        public  void LoadMap(String mapname, ContentManager Content)
        {
            TileMap testMap = new TileMap(mapname, Content);
            mapList.Add(testMap);
            testMap.active = true;

        }



    }
}
