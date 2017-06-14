using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.GameObjects.Objects.Gatherables
{
    class Tree : Gatherable
    {
        int hits = 0;
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
                    difficulty = 600;
                    break;
                case TreeType.kOakTree:
                    difficulty = 1200;
                    break;
                case TreeType.kCedarTree:
                    difficulty = 1800;
                    break;                    
                default:
                    difficulty = 9001;
                    break;
            }
            Random ran = new Random();
            ItemGiveCount = ran.Next(1, 7);

            this._Tag = Sprite.SpriteType.kTreeType;
            this.myWorldObjectTag = WorldObjectTag.kTreeTag;
            this._CurrentState = Sprite.SpriteState.kStateActive;

            OutputItem output = new OutputItem();
            output.output = Item.ItemType.kItemLog;
            output.amount = 1;
            output.odds = 100;
            for(int i = 0; i < output.odds; i++)
            {
                OutputItems.Add(output);
            }
        }

        public override void Revive()
        {
            base.Revive();

            Random ran = new Random();
            ItemGiveCount = ran.Next(1, 7);
        }
    }
}
