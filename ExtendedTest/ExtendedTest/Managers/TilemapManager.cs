using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace ExtendedTest
{
    public class TilemapManager
    {
        List<TileMap> mapList;
        NPCManager _NpcManager;
        WorldObjectManager _WorldObjectManager;
        public TilemapManager(NPCManager npcManager, WorldObjectManager _GameObjectmanager)
        {
            mapList = new List<TileMap>();
            _NpcManager = npcManager;
            _WorldObjectManager = _GameObjectmanager;
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

        public Tile findClosestTile(Vector2 targetPos, Vector2 playerPos, bool allowDiagonal = true)
        {
            Tile targetTile = findTile(targetPos);
            Tile playerTile = findTile(playerPos);
            Tile ClosestTile = null;
            List<Tile> walkableNearby = FindAdjacentTiles(targetTile.tileCenter, true);
            //get distance for tiles surrounding targettile
            float distance = Vector2.Distance(targetTile.tileCenter, playerTile.tileCenter);
            float newDistance;




            if(walkableNearby.Count > 0)
            {
                distance = Vector2.Distance(walkableNearby[0].tileCenter, playerTile.tileCenter);
                foreach(Tile tile in walkableNearby)
                {
                    newDistance = Vector2.Distance(tile.tileCenter, playerTile.tileCenter);
                    if (newDistance <= distance)
                    {
                        ClosestTile = tile;
                        distance = newDistance;
                    }
                }
            }

            if(ClosestTile!= null)
            {
                ClosestTile.myColor = Color.Black;
            }
            return ClosestTile;
        }

        public List<Tile> wtfDoIDo(Vector2 pos1, Vector2 pos2, bool toAdjacent = false, bool allowDiagonal = true)
        {
            Tile targetTile = findTile(pos1);
            Tile currentTile = findTile(pos2);
            Console.WriteLine(currentTile.localPos);

            List<Tile> closedSet = new List<Tile>();
            List<Tile> openSet = FindAdjacentTiles(currentTile.tileCenter);
            List<Tile> path = new List<Tile>();

            bool pathFound = false;
            path.Add(targetTile);
            Tile lastTile = targetTile;

            float distance = 99999999999;
            Tile closestTile = currentTile;
            while(!pathFound)
            {
                //get closest node to target in openSet
                Tile tile = FindClosestTile(openSet, currentTile);
                tile.myColor = Color.Black;
                Console.WriteLine(tile.localPos);
                if(tile == targetTile)
                {
                    pathFound = true;
                    break;
                }
                List<Tile> adjacentNotInClosed = FindAdjacentTiles(tile.tileCenter);
                List<Tile> newList = adjacentNotInClosed.Except(closedSet).ToList();
                List<Tile> newList2 = newList.Except(openSet).ToList();
                openSet.AddRange(newList2);
                openSet.Remove(tile);
                closedSet.Add(tile);

            }






            return path;

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

        public List<Tile> CalculatePath(Vector2 pos1, Vector2 pos2, bool toAdjacent = false, bool allowDiagonal = true)
        {
            Tile targetTile = findTile(pos1);
            Tile currentTile = findTile(pos2);
            bool pathFound = false;
            int stepCount = 15;
            List<Tile> path = wtfDoIDo(pos1, pos2);
            return path;
            path.Add(targetTile);
            Tile lastTile = targetTile;
            List<Tile> lookedAtTiles = new List<Tile>();
            while(!pathFound)
            {
                Tile newTile = findClosestTile(lastTile.tileCenter, currentTile.tileCenter);
                if(newTile== null)
                {
                    return null;
                }
                if(newTile == currentTile)
                {
                    pathFound = true;
                }
                else
                {
                    //if a tile is already in the path then go back until we find a tile not in the path?
                    if(path.Contains(newTile)) //tile found in path already
                    {

                    }
                    else
                    {
                        path.Add(newTile);

                    }
                    lookedAtTiles.Add(newTile);
                    Console.WriteLine(newTile.localPos);
                    lastTile = newTile;
                }

            }
            path.Reverse();
            Console.WriteLine("Done");
            return path;
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
