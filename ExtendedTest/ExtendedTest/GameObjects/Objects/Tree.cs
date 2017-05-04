using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class Tree : WorldObject
    {
        int difficulty = 300;
        int hits = 0;

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


            this._Tag = Sprite.SpriteType.kTreeType;
            this._CurrentState = Sprite.SpriteState.kStateActive;
        }

        public Item getChopped()
        {
            Random ran = new Random();
            int randomNumber = ran.Next(0, difficulty);
            if(randomNumber == 0)
            {
                Log log = new Log();
                return log;
            }
            else
            {
                hits++;
                return null;
            }
        }
    }
}
