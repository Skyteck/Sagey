using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExtendedTest.GameObjects.Objects
{
    class DirtPatch : WorldObject
    {
        public GameObjects.Gatherables.Plant MyPlant { get; set; }

        public DirtPatch()
        {
            MyWorldObjectTag = WorldObjectTag.kDirtTag;
            _Detector = true;
        }
        
    }
}
