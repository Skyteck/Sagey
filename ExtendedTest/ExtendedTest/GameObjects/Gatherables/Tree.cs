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
                    _Difficulty = 10;
                    break;
                case TreeType.kOakTree:
                    _Difficulty = 12;
                    break;
                case TreeType.kCedarTree:
                    _Difficulty = 18;
                    break;                    
                default:
                    _Difficulty = 9001;
                    break;
            }

            _StartHP = 10;

            Random ran = new Random();
            ItemGiveCount = ran.Next(1, 7);

            this._Tag = Sprite.SpriteType.kTreeType;
            this.MyWorldObjectTag = WorldObjectTag.kTreeTag;
            this._CurrentState = Sprite.SpriteState.kStateActive;

            Items.ItemBundle output = new Items.ItemBundle();
            output.output = Item.ItemType.kItemLog;
            output.amount = 1;
            output.odds = 95;
            
            output = new Items.ItemBundle();
            output.output = Item.ItemType.kItemLog;
            output.amount = 2;
            output.odds = 1;
            CurrentDrop = output;
            Setup();
        }
    }
}
