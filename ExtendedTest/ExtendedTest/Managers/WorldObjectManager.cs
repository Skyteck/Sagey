using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using ExtendedTest.GameObjects.Gatherables;

namespace ExtendedTest.Managers
{
    public class WorldObjectManager
    {
        List<WorldObject> objectList;
        List<WorldObject> objectListInactive;
        List<WorldObject> detectorList;
        InventoryManager _InventoryManager;
        ContentManager Content;
        TilemapManager _TilemapManager;
        GatherableManager _GatherableManager;

        readonly Player thePlayer;

        internal List<WorldObject> ObjectList { get => objectList; set => objectList = value; }

        public WorldObjectManager(TilemapManager mapManager,  InventoryManager invenManager, ContentManager content, Player player)
        {
            ObjectList = new List<WorldObject>();
            objectListInactive = new List<WorldObject>();
            detectorList = new List<WorldObject>();
            _InventoryManager = invenManager;
            Content = content;
            thePlayer = player;
            _TilemapManager = mapManager;
        }

        public void SetGatherManager(GatherableManager gm)
        {
            _GatherableManager = gm;
        }

        public void CreateObject(TmxObject thing, Vector2 pos)
        {
            if (thing.Type.Equals("Dirt"))
            {
                GameObjects.Objects.DirtPatch anotherFish = new GameObjects.Objects.DirtPatch();
                anotherFish.LoadContent("Art/ClearBox", Content);
                anotherFish._Position = _TilemapManager.findTile(pos).tileCenter;
                anotherFish.ParentList = ObjectList;
                anotherFish.Name = thing.Name;
                ObjectList.Add(anotherFish);
            }
        }

        public void CreateObject(Sprite.SpriteType objectType, Vector2 position)
        {
            if (objectType == Sprite.SpriteType.kFireType)
            {
                Fire fire = new Fire();
                fire.LoadContent("Art/Fire", Content);
                fire._Position = _TilemapManager.findTile(position).tileCenter;
                fire.ParentList = ObjectList;
                fire.Name = "Fire";
                ObjectList.Add(fire);

            }
        }

        public void Update(GameTime gameTime)
        {
            List<WorldObject> combinedList = new List<WorldObject>();
            combinedList.AddRange(objectList);
            combinedList.AddRange(objectListInactive);
            objectList.Clear();
            objectListInactive.Clear();
            detectorList.Clear();
            objectList = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateActive);
            objectListInactive = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateInActive);

            detectorList = objectList.FindAll(x => x._Detector == true);


            foreach(WorldObject sprite in combinedList)
            {
                sprite.Update(gameTime);
            }
        }

        public WorldObject CheckClicks(Vector2 pos)
        {
            foreach(WorldObject thing in objectList)
            {
                if(thing._BoundingBox.Contains(pos))
                {
                    return thing;
                }
            }
            return null;
        }

        public WorldObject CheckWalkable(Rectangle rect)
        {
            List<WorldObject> checkList = objectList.FindAll(x => x._Walkable == false);
            foreach (WorldObject thing in checkList)
            {
                if (thing._BoundingBox.Intersects(rect))
                {
                    return thing;
                }
            }
            return null;
        }

        public WorldObject CheckCollision(Rectangle rect)
        {
            List<WorldObject> checkList = objectList.FindAll(x => x._Detector == false);
            foreach(WorldObject thing in checkList)
            {
                if(thing._BoundingBox.Intersects(rect))
                {
                    return thing;
                }
            }
            return null;
        }

        public WorldObject CheckDetectors(Rectangle rect)
        {
            foreach (WorldObject thing in detectorList)
            {
                if (thing._BoundingBox.Intersects(rect))
                {
                    return thing;
                }
            }
            return null;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Sprite sprite in ObjectList)
            {
                sprite.Draw(spriteBatch);
            }
        }
    }
}
