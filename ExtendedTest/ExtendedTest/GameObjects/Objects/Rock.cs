using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace ExtendedTest
{
    class Rock : WorldObject
    {
        int difficulty = 300;
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
            this._CurrentState = Sprite.SpriteState.kStateActive;
        }

        public Item getChopped()
        {
            Random ran = new Random();
            int randomNumber = ran.Next(0, difficulty);
            if(randomNumber == 0)
            {
                Ore ore = new Ore();
                this._CurrentState = SpriteState.kStateInActive;
                this._Draw = false;
                return ore;
            }
            else
            {
                hits++;
                return null;
            }
        }
    }
}
