using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.GameObjects.Objects.Gatherables
{
    class FishingHole : WorldObject
    {
        int hits = 0;
        int fishCount = 3; 
        public enum FishingType
        {
            kFlyFishType,
            kBaitType,
            kNetType
        }

        public FishingType fishingType = FishingType.kNetType;
        public FishingHole(FishingType myType)
        {
            fishingType = myType;
            switch(fishingType)
            {
                case FishingType.kNetType:
                    difficulty = 200;
                    break;
                case FishingType.kBaitType:
                    difficulty = 600;
                    break;
                case FishingType.kFlyFishType:
                    difficulty = 900;
                    break;                    
                default:
                    difficulty = 90000001;
                    break;
            }
            Random ran = new Random();
            fishCount = ran.Next(1, 7);

            this._Tag = Sprite.SpriteType.kFishingType;
            this.myWorldObjectTag = WorldObjectTag.kFishingHoleTag;
            this._CurrentState = Sprite.SpriteState.kStateActive;
        }

        public Item.ItemType getFished()
        {
            Random ran = new Random();
            int randomNumber = ran.Next(0, difficulty);
            if(randomNumber == 0)
            {
                fishCount--;
                if(fishCount <= 0)
                {
                    this._CurrentState = SpriteState.kStateInActive;
                    this._Draw = false;
                }
                return Item.ItemType.kItemFish;
            }
            else
            {
                hits++;
                return Item.ItemType.kItemNone;
            }
        }

        public override void Revive()
        {
            base.Revive();

            Random ran = new Random();
            fishCount = ran.Next(1, 7);
        }
    }
}
