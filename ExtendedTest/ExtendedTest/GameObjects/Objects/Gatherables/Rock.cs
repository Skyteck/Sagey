using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace ExtendedTest.GameObjects.Objects.Gatherables
{
    class Rock : Gatherable
    {
        int hits = 0;

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
                    difficulty = 300;
                    break;
                case RockType.kClayRock:
                    difficulty = 600;
                    break;
                case RockType.kIronRock:
                    difficulty = 900;
                    break;
                    
                default:
                    difficulty = 9001;
                    break;
            }
            
            this._Tag = Sprite.SpriteType.kRockType;
            this.myWorldObjectTag = WorldObjectTag.kRockTag;
            this._CurrentState = Sprite.SpriteState.kStateActive;


            OutputItem output = new OutputItem();
            output.output = Item.ItemType.kItemOre;
            output.amount = 1;
            output.odds = 100;
            OutputItems.Add(output);
        }        
    }
}
