using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.GameObjects.Gatherables
{
    class FishingHole : Gatherable
    {
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
                    _Difficulty = 2;
                    break;
                case FishingType.kBaitType:
                    _Difficulty = 6;
                    break;
                case FishingType.kFlyFishType:
                    _Difficulty = 9;
                    break;                    
                default:
                    _Difficulty = 90000001;
                    break;
            }
            Random ran = new Random();
            fishCount = ran.Next(1, 7);

            this._Tag = Sprite.SpriteType.kFishingType;
            this.MyWorldObjectTag = WorldObjectTag.kFishingHoleTag;
            this._CurrentState = Sprite.SpriteState.kStateActive;


            Items.ItemBundle output = new Items.ItemBundle();
            output.outputID = Enums.ItemID.kItemFish;
            output.amount = 1;
            output.odds = 100;
            CurrentDrop = output;
            InteractText = "Fish";
            _TrueStartHP = 3;

            Setup();
        }
    }
}
