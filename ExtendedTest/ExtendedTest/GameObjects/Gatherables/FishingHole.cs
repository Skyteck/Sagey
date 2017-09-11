using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.GameObjects.Gatherables
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
                    difficulty = 2;
                    break;
                case FishingType.kBaitType:
                    difficulty = 6;
                    break;
                case FishingType.kFlyFishType:
                    difficulty = 9;
                    break;                    
                default:
                    difficulty = 90000001;
                    break;
            }
            Random ran = new Random();
            fishCount = ran.Next(1, 7);

            this._Tag = Sprite.SpriteType.kFishingType;
            this.MyWorldObjectTag = WorldObjectTag.kFishingHoleTag;
            this._CurrentState = Sprite.SpriteState.kStateActive;


            Items.ItemBundle output = new Items.ItemBundle();
            output.output = Item.ItemType.kItemFish;
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
