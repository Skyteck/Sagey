using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class Fire : WorldObject
    {

        public Fire()
        {
            this._Tag = Sprite.SpriteType.kFireType;
            this._CurrentState = Sprite.SpriteState.kStateActive;
            this.MyWorldObjectTag = WorldObjectTag.kFireTag;
        }
    }
}
