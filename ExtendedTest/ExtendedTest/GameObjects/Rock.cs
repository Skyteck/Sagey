using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class Rock : Sprite
    {
        int difficulty = 300;
        Texture2D treeTop;
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
            this._HP = 1;
            this.startHP = 1;
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
        }

        public Item getChopped()
        {
            Random ran = new Random();
            int randomNumber = ran.Next(0, difficulty);
            if(randomNumber == 0)
            {
                this.ReceiveDamage(1);
                Ore ore = new Ore();
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
