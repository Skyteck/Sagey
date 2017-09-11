using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using ExtendedTest.GameObjects.Items;

namespace ExtendedTest.GameObjects.Gatherables
{
    class Rock : Gatherable
    {
        public enum RockType
        {
            kNormalRock,
            kIronRock,
            kClayRock
        }

        public RockType rockType = RockType.kNormalRock;
        public Rock(RockType myType)
        {
            rockType = myType;
            switch(rockType)
            {
                case RockType.kNormalRock:
                    difficulty = 5;
                    break;
                case RockType.kClayRock:
                    difficulty = 2;
                    break;
                case RockType.kIronRock:
                    difficulty = 10;
                    break;
                    
                default:
                    difficulty = 9001;
                    break;
            }
            
            this._Tag = Sprite.SpriteType.kRockType;
            this.MyWorldObjectTag = WorldObjectTag.kRockTag;
            this._CurrentState = Sprite.SpriteState.kStateActive;


            ItemBundle output = new ItemBundle();
            output.output = Item.ItemType.kItemOre;
            output.amount = 1;
            output.odds = 100;


            for (int i = 0; i < output.odds; i++)
            {
                OutputItems.Add(output);
            }

            if (OutputItems.Count < 100)
            {
                Items.ItemBundle noneBundle = new Items.ItemBundle();
                noneBundle.output = Item.ItemType.kItemNone;
                noneBundle.amount = 1;
                output.odds = 100 - OutputItems.Count;
                for (int i = 0; i < output.odds; i++)
                {
                    OutputItems.Add(noneBundle);
                }
            }
        }        
    }
}
