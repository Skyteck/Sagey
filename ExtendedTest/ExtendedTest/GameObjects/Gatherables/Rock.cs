using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using Sagey.GameObjects.Items;

namespace Sagey.GameObjects.Gatherables
{
    public class Rock : Gatherable
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
                    _Difficulty = 5;
                    break;
                case RockType.kClayRock:
                    _Difficulty = 2;
                    break;
                case RockType.kIronRock:
                    _Difficulty = 10;
                    break;
                    
                default:
                    _Difficulty = 9001;
                    break;
            }
            
            this._Tag = Sprite.SpriteType.kRockType;
            this.MyWorldObjectTag = WorldObjectTag.kRockTag;
            this._CurrentState = Sprite.SpriteState.kStateActive;

            _TrueStartHP = 8;


            ItemBundle output = new ItemBundle();
            output.outputID = Enums.ItemID.kItemOre;
            output.amount = 1;
            output.odds = 100;
            CurrentDrop = output;
            InteractText = "Mine";
            Setup();
        }
        
    }
}
