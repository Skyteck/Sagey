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
    class GameObjectManager
    {
        List<Sprite> objectList;
        InventoryManager _InventoryManager;
        ContentManager Content;
        TilemapManager tMapManager;

        Player thePlayter;

        public GameObjectManager(TilemapManager mapManager,  InventoryManager invenManager, ContentManager content, Player player)
        {
            objectList = new List<Sprite>();
            _InventoryManager = invenManager;
            Content = content;
            thePlayter = player;
            tMapManager = mapManager;
        }

        public void CreateObject(TmxObject thing, Vector2 pos)
        {
            if (thing.Type.Equals("tree"))
            {
                Tree anotherTree = new Tree(Tree.TreeType.kNormalTree);
                anotherTree.LoadContent("Art/tree", Content);
                anotherTree._Position = pos;
                anotherTree.parentList = objectList;
                objectList.Add(anotherTree);

            }
            else if (thing.Type.Equals("rock"))
            {
                Rock anotherRock = new Rock(Rock.RockType.kNormalRock);
                anotherRock.LoadContent("Art/" + thing.Type, Content);
                anotherRock._Position = pos;
                anotherRock.parentList = objectList;
                objectList.Add(anotherRock);
            }
        }

        public void Update(GameTime gameTime, List<Sprite> spriteList)
        {
            foreach(Sprite sprite in objectList)
            {
                sprite.Update(gameTime, spriteList);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Sprite sprite in objectList)
            {
                sprite.Draw(spriteBatch);
            }
        }
    }
}
