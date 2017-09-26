﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using ExtendedTest.GameObjects.Gatherables;
using ExtendedTest.GameObjects.Gatherables.Plants;

namespace ExtendedTest.Managers
{
    public class GatherableManager
    {
        List<Gatherable> _GatherableListActive;
        List<Gatherable> _GatherableListInActive;
        InventoryManager _InventoryManager;
        ContentManager Content;
        TilemapManager _TilemapManager;
        List<Plant> PlantList;

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
            int randomNumber = ran.Next(0, thing._Difficulty);
            if (randomNumber == 0)
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
            foreach(Sprite sprite in _GatherableListActive)
            {
                sprite.Draw(spriteBatch);
            }
        }

        public Plant GetPlant(Plant.PlantType type)
        {
            GameObjects.Gatherables.Plants.StrawberryPlant p = (GameObjects.Gatherables.Plants.StrawberryPlant)FindPlant(Plant.PlantType.kStrawBerryType);
            return p;
        }
    }
}
