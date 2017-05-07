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
        NpcManager _NpcManager;
        WorldObjectManager _WorldObjectManager;
        public TilemapManager(NpcManager npcManager, WorldObjectManager _GameObjectmanager)
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

        public Tile findClosestTile(Vector2 targetPos, Vector2 playerPos)
        {
            Tile targetTile = findTile(targetPos);
            Tile playerTile = findTile(playerPos);
            Tile ClosestTile = targetTile;
            //get distance for tiles surrounding targettile
            float distance = Vector2.Distance(targetTile.tileCenter, playerTile.tileCenter);

            //get top left tile
            Tile topleft = findTile(new Vector2(targetTile.tileCenter.X - 64, targetTile.tileCenter.Y - 64));
            float newDistance = Vector2.Distance(topleft.tileCenter, playerTile.tileCenter);
            if(newDistance < distance)
            {
                distance = newDistance;
                ClosestTile = topleft;
            }

            //get top center tile
            Tile topCenter = findTile(new Vector2(targetTile.tileCenter.X, targetTile.tileCenter.Y - 64));
            newDistance = Vector2.Distance(topCenter.tileCenter, playerTile.tileCenter);
            if (newDistance < distance)
            {
                distance = newDistance;
                ClosestTile = topCenter;
            }

            //get top right tile
            Tile topRight = findTile(new Vector2(targetTile.tileCenter.X + 64, targetTile.tileCenter.Y - 64));
            newDistance = Vector2.Distance(topRight.tileCenter, playerTile.tileCenter);
            if (newDistance < distance)
            {
                distance = newDistance;
                ClosestTile = topRight;
            }

            //get left tile
            Tile LeftTile = findTile(new Vector2(targetTile.tileCenter.X - 64, targetTile.tileCenter.Y));
            newDistance = Vector2.Distance(LeftTile.tileCenter, playerTile.tileCenter);
            if (newDistance < distance)
            {
                distance = newDistance;
                ClosestTile = LeftTile;
            }

            //get right tile
            Tile rightTIle = findTile(new Vector2(targetTile.tileCenter.X + 64, targetTile.tileCenter.Y ));
            newDistance = Vector2.Distance(rightTIle.tileCenter, playerTile.tileCenter);
            if (newDistance < distance)
            {
                distance = newDistance;
                ClosestTile = rightTIle;
            }

            //get bottom left tile
            Tile bottomLeft = findTile(new Vector2(targetTile.tileCenter.X - 64, targetTile.tileCenter.Y + 64));
            newDistance = Vector2.Distance(bottomLeft.tileCenter, playerTile.tileCenter);
            if (newDistance < distance)
            {
                distance = newDistance;
                ClosestTile = bottomLeft;
            }

            //get bottom center tile
            Tile bottomCenter = findTile(new Vector2(targetTile.tileCenter.X , targetTile.tileCenter.Y + 64));
            newDistance = Vector2.Distance(bottomCenter.tileCenter, playerTile.tileCenter);
            if (newDistance < distance)
            {
                distance = newDistance;
                ClosestTile = bottomCenter;
            }

            //get top right tile
            Tile bottomRight = findTile(new Vector2(targetTile.tileCenter.X + 64, targetTile.tileCenter.Y + 64));
            newDistance = Vector2.Distance(bottomRight.tileCenter, playerTile.tileCenter);
            if (newDistance < distance)
            {
                distance = newDistance;
                ClosestTile = bottomRight;
            }

            return ClosestTile;
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

        public  void LoadMap(String mapname, ContentManager Content)
        {
            TileMap testMap = new TileMap(mapname, Content);
            mapList.Add(testMap);
            testMap.active = true;

        }



    }
}
