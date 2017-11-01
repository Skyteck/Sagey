using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using Sagey.GameObjects.Gatherables;
using Sagey.GameObjects.Gatherables.Plants;

namespace Sagey.Managers
{
    public class GatherableManager
    {
        List<Gatherable> _GatherableListActive;
        List<Gatherable> _GatherableListInActive;
        InventoryManager _InventoryManager;
        ContentManager Content;
        TilemapManager _TilemapManager;
        List<Plant> PlantList;
        Texture2D _HpTex;
        readonly Player thePlayer;
        
        public GatherableManager(TilemapManager mapManager,  InventoryManager invenManager, ContentManager content, Player player)
        {
            _GatherableListActive = new List<Gatherable>();
            _GatherableListInActive = new List<Gatherable>();
            PlantList = new List<Plant>();
            _InventoryManager = invenManager;
            Content = content;
            thePlayer = player;
            _TilemapManager = mapManager;
        }

        public void LoadContent(ContentManager content)
        {
            PlantList.Add(new GameObjects.Gatherables.Plants.StrawberryPlant());
            Content = content;
            _HpTex = content.Load<Texture2D>("Art/WhiteTexture");
            //PlantList.Add(new GameObjects.Gatherables.Plants.PotatoPlant());
            //PlantList.Add(new GameObjects.Gatherables.Plants.CornPlant());

            foreach(Plant plant in PlantList)
            {
                plant.LoadContent("Art/" + plant.Name, content);
            }
        }

        public void CreateGatherable(TmxObject thing, Vector2 pos)
        {
            if (thing.Type.Equals("tree"))
            {
                Tree anotherTree = new Tree(Tree.TreeType.kNormalTree);
                anotherTree.LoadContent("Art/tree", Content);
                anotherTree._Position = _TilemapManager.findTile(pos).tileCenter;
                anotherTree.Name = thing.Name;
                _GatherableListActive.Add(anotherTree);
                _GatherableListActive.Add(anotherTree);

            }
            else if (thing.Type.Equals("rock"))
            {
                Rock anotherRock = new Rock(Rock.RockType.kNormalRock);
                anotherRock.LoadContent("Art/" + thing.Type, Content);
                anotherRock._Position = _TilemapManager.findTile(pos).tileCenter;
                anotherRock.Name = thing.Name;
                _GatherableListActive.Add(anotherRock);
                _GatherableListActive.Add(anotherRock);
            }
            else if (thing.Type.Equals("FishingHole"))
            {
                FishingHole anotherFish = new FishingHole(FishingHole.FishingType.kNetType);
                anotherFish.LoadContent("Art/" + thing.Type, Content);
                anotherFish._Position = _TilemapManager.findTile(pos).tileCenter;
                anotherFish.Name = thing.Name;
                _GatherableListActive.Add(anotherFish);
                _GatherableListActive.Add(anotherFish);
            }
        }

        public void CreatePlant(Plant.PlantType type, Vector2 pos)
        {
            switch(type)
            {
                case Plant.PlantType.kStrawBerryType:
                    StrawberryPlant newPlant = new StrawberryPlant();
                    newPlant.LoadContent("Art/" + newPlant.Name, Content);
                    newPlant._Position = pos;
                    _GatherableListActive.Add(newPlant);
                    break;
                default:
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            List<Gatherable> gaList = new List<Gatherable>();
            gaList.AddRange(_GatherableListActive);
            gaList.AddRange(_GatherableListInActive);
            _GatherableListActive.Clear();
            _GatherableListInActive.Clear();
            _GatherableListActive = gaList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateActive);
            _GatherableListInActive = gaList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateInActive);

            foreach(WorldObject sprite in gaList)
            {
                sprite.Update(gameTime);
            }
        }

        public Gatherable CheckCollision(Vector2 pos)
        {
            foreach(Gatherable thing in _GatherableListActive)
            {
                if(thing._BoundingBox.Contains(pos))
                {
                    return thing;
                }
            }
            return null;
        }


        public Gatherable CheckCollision(Rectangle rect)
        {
            foreach (Gatherable thing in _GatherableListActive)
            {
                if (thing._BoundingBox.Intersects(rect))
                {
                    return thing;
                }
            }
            return null;
        }

        public Gatherable CheckWalkable(Rectangle rect)
        {

            List<Gatherable> checkList = _GatherableListActive.FindAll(x => x._Walkable == false);
            foreach (Gatherable thing in checkList)
            {
                if (thing._BoundingBox.Intersects(rect))
                {
                    return thing;
                }
            }
            return null;
        }

        public GameObjects.Items.ItemBundle GatherItem(Gatherable thing)
        {
            Random ran = new Random();
            thing.GetHit();
            if(thing._HP <= 0)
            {                
                return thing.GetGathered();
            }
            else
            {
                return new GameObjects.Items.ItemBundle();
            }
        }

        public Plant FindPlant(Plant.PlantType type)
        {
            return PlantList.Find(x => x.MyPlantType == type);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Gatherable sprite in _GatherableListActive)
            {
                sprite.Draw(spriteBatch);
                
                if (sprite._HP == sprite._StartHP) continue;
                drawHP(sprite, spriteBatch);
            }
        }

        public Plant GetPlant(Plant.PlantType type)
        {
            StrawberryPlant p = (StrawberryPlant)FindPlant(Plant.PlantType.kStrawBerryType);
            return p;
        }

        private void drawHP(Gatherable sprite, SpriteBatch spriteBatch)
        {
            float HPgone = (float)sprite._HP / sprite._StartHP;
            Rectangle rect = new Rectangle((int)sprite._TopLeft.X, (int)sprite._TopLeft.Y - 10, (int)(64 * HPgone), 6);
            int green = (int)(HPgone * 255);
            int red = (255 - (int)(HPgone * 255))+100;
            Color col = new Color(red, green, 0);
            spriteBatch.Draw(_HpTex, rect, col);
        }
    }
}
