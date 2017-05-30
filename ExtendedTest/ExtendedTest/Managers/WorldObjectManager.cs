using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace ExtendedTest.Managers
{
    public class WorldObjectManager
    {
        List<WorldObject> objectList;
        List<WorldObject> objectListInactive;
        InventoryManager _InventoryManager;
        ContentManager Content;
        TilemapManager _TilemapManager;

        readonly Player thePlayer;

        internal List<WorldObject> ObjectList { get => objectList; set => objectList = value; }

        public WorldObjectManager(TilemapManager mapManager,  InventoryManager invenManager, ContentManager content, Player player)
        {
            ObjectList = new List<WorldObject>();
            objectListInactive = new List<WorldObject>();
            _InventoryManager = invenManager;
            Content = content;
            thePlayer = player;
            _TilemapManager = mapManager;
        }

        public void CreateObject(TmxObject thing, Vector2 pos)
        {
            if (thing.Type.Equals("tree"))
            {
                Tree anotherTree = new Tree(Tree.TreeType.kNormalTree);
                anotherTree.LoadContent("Art/tree", Content);
                anotherTree._Position = _TilemapManager.findTile(pos).tileCenter;
                anotherTree.parentList = ObjectList;
                anotherTree.Name = thing.Name;
                ObjectList.Add(anotherTree);

            }
            else if (thing.Type.Equals("rock"))
            {
                Rock anotherRock = new Rock(Rock.RockType.kNormalRock);
                anotherRock.LoadContent("Art/" + thing.Type, Content);
                anotherRock._Position = _TilemapManager.findTile(pos).tileCenter;
                anotherRock.parentList = ObjectList;
                anotherRock.Name = thing.Name;
                ObjectList.Add(anotherRock);
            }
            else if (thing.Type.Equals("FishingHole"))
            {
                FishingHole anotherFish = new FishingHole(FishingHole.FishingType.kNetType);
                anotherFish.LoadContent("Art/" + thing.Type, Content);
                anotherFish._Position = _TilemapManager.findTile(pos).tileCenter;
                anotherFish.parentList = ObjectList;
                anotherFish.Name = thing.Name;
                ObjectList.Add(anotherFish);
            }
        }

        public void Update(GameTime gameTime)
        {
            List<WorldObject> combinedList = new List<WorldObject>();
            combinedList.AddRange(objectList);
            combinedList.AddRange(objectListInactive);
            objectList = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateActive);
            objectListInactive = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateInActive);

            foreach (WorldObject sprite in ObjectList)
            {
                sprite.UpdateActive(gameTime);
            }

            foreach(WorldObject sprite in objectListInactive)
            {
                sprite.UpdateDead(gameTime);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Sprite sprite in ObjectList)
            {
                sprite.Draw(spriteBatch);
            }
        }

        internal void CreateObject(Sprite.SpriteType objectType, Vector2 position)
        {
            if (objectType == Sprite.SpriteType.kFireType)
            {
                Fire fire = new Fire();
                fire.LoadContent("Art/Fire", Content);
                fire._Position = _TilemapManager.findTile(position).tileCenter;
                fire.parentList = ObjectList;
                fire.Name = "Fire";
                ObjectList.Add(fire);

            }
        }
    }
}
