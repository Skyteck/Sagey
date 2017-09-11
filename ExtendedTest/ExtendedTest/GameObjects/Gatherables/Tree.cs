using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.GameObjects.Gatherables
{
    class Tree : Gatherable
    {
        int ItemGiveCount = 3; 
        public enum TreeType
        {
            kNormalTree,
            kOakTree,
            kCedarTree
        }

        public TreeType treeType = TreeType.kNormalTree;
        public Tree(TreeType myType)
        {
            treeType = myType;
            switch(treeType)
            {
                case TreeType.kNormalTree:
                    difficulty = 10;
                    break;
                case TreeType.kOakTree:
                    difficulty = 12;
                    break;
                case TreeType.kCedarTree:
                    difficulty = 18;
                    break;                    
                default:
                    difficulty = 9001;
                    break;
            }
            Random ran = new Random();
            ItemGiveCount = ran.Next(1, 7);

            this._Tag = Sprite.SpriteType.kTreeType;
            this.MyWorldObjectTag = WorldObjectTag.kTreeTag;
            this._CurrentState = Sprite.SpriteState.kStateActive;

            Items.ItemBundle output = new Items.ItemBundle();
            output.output = Item.ItemType.kItemLog;
            output.amount = 1;
            output.odds = 95;
            for(int i = 0; i < output.odds; i++)
            {
                OutputItems.Add(output);
            }

            output = new Items.ItemBundle();
            output.output = Item.ItemType.kItemLog;
            output.amount = 2;
            output.odds = 1;
            for (int i = 0; i < output.odds; i++)
            {
                OutputItems.Add(output);
            }

            if (OutputItems.Count <100)
            {
                Items.ItemBundle noneBundle = new Items.ItemBundle();
                noneBundle.output = Item.ItemType.kItemNone;
                noneBundle.amount = 1;
                output.odds = 100 - OutputItems.Count;
                for(int i = 0; i < output.odds; i++)
                {
                    OutputItems.Add(noneBundle);
                }
            }
        }
    }
}
