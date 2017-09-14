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

        public override void UpdateActive(GameTime gameTime)
        {
            base.UpdateActive(gameTime);

            if(MyPlant != null)
            {
                MyPlant.Update(gameTime);
            }
        }

        public void DoThing(GameObjects.Gatherables.Plant plant)
        {
            MyPlant = plant;
            MyPlant._Position = this._Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if(MyPlant != null)
            {
                MyPlant.Draw(spriteBatch);
            }
        }
    }
}
