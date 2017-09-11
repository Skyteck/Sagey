using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ExtendedTest.GameObjects.Objects
{
    class DirtPatch : WorldObject
    {
        public Plant MyPlant { get; set; }

        public DirtPatch()
        {
            MyWorldObjectTag = WorldObjectTag.kDirtTag;
            _Detector = true;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            base.UpdateActive(gameTime);

            if(MyPlant != null)
            {
                MyPlant.Update(gameTime);
            }
        }

        public void DoThing(Plant plant)
        {
            MyPlant = plant;
            MyPlant._Position = this._Position;
        }
    }
}
