using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ExtendedTest
{
    public class Projectile : Character
    {
        public int _Attack;
        public Character _Target;
        public CombatManager _CBmanager;
        public Projectile(Character parent)
        {
            this._Attack = parent.attack;
            this._CBmanager = parent._CBManager;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            setDestination(_Target._Position);
            base.UpdateActive(gameTime);
        }

        public void SetTarget(Character target)
        {
            _Target = target;
        }
    }
}
