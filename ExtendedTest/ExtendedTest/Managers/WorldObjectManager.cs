using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using Sagey.GameObjects.Gatherables;
using Sagey.Enums;
using Sagey.GameObjects.Objects;

namespace Sagey.Managers
{
    public class WorldObjectManager
    {
        public event Delegates.GameEvent ItemPickedUpEvent;

        List<WorldObject> objectList;
        List<WorldObject> objectListInactive;
        List<WorldObject> detectorList;
        List<WorldItem> WorldItems;

        InventoryManager _InventoryManager;
        ContentManager Content;
        TilemapManager _TilemapManager;
        GatherableManager _GatherableManager;
        ItemManager _ItemManager;

        readonly Player thePlayer;

        internal List<WorldObject> ObjectList { get => objectList; set => objectList = value; }

        public WorldObjectManager(TilemapManager mapManager,  InventoryManager invenManager, ContentManager content, Player player, ItemManager im)
        {
            ObjectList = new List<WorldObject>();
            objectListInactive = new List<WorldObject>();
            detectorList = new List<WorldObject>();
            WorldItems = new List<WorldItem>();
            _InventoryManager = invenManager;
            Content = content;
            thePlayer = player;
            _TilemapManager = mapManager;
            _ItemManager = im;
        }

        public void SetGatherManager(GatherableManager gm)
        {
            _GatherableManager = gm;
        }

        public void AttachEvents(EventManager em)
        {
            ItemPickedUpEvent += em.HandleEvent;
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

            foreach (WorldItem item in WorldItems.FindAll(x=>x._CurrentState == Sprite.SpriteState.kStateActive))
            {
                item.Update(gameTime);


                if (Vector2.Distance(thePlayer._Position, item._Position) <= 120)
                {
                    item.SetTarget(thePlayer);
                }

                if(item._BoundingBox.Intersects(thePlayer._BoundingBox))
                {
                    _InventoryManager.AddItem(item.MyItem._ID);
                    OnItemPickedUp(item.MyItem._Name);
                    item.Deactivate();
                }
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

            foreach(Sprite item in WorldItems.FindAll(x=>x._CurrentState == Sprite.SpriteState.kStateActive))
            {
                item.Draw(spriteBatch);
            }
        }

        internal void CreateItem(ItemID itemID, Vector2 position)
        {
            WorldItem wi = new WorldItem(_ItemManager.GetItem(itemID), position);
            WorldItems.Add(wi);
            
        }

        private void OnItemPickedUp(string itemName)
        {
            ItemPickedUpEvent?.Invoke(EventTypes.kEventItemPickedUp, itemName);
        }

        //public List<WorldItem> GetGroundItems(Vector2 pos, int range)
        //{
        //    List<WorldItem> itemsInRange = new List<WorldItem>();

        //    foreach(WorldItem item in WorldItems)
        //    {
        //        if(Vector2.Distance(pos, item._Position) <= range)
        //        {
        //            itemsInRange.Add(item);
        //        }
        //    }

        //    return itemsInRange;
        //}
    }
}
