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
        public Projectile(Character parent, CombatManager cbManager) : base(cbManager)
        {
            this.attack = parent.attack;
            this._CBmanager = cbManager;
            _Speed = 7f;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            if(_Target!= null)
            {
                setDestination(_Target._Position);
                if(_Target._BoundingBox.Intersects(this._BoundingBox))
                {
                    _CBmanager.PerformAttack(this, _Target);
                    _Target = null;
                }
            }
            else
            {
                _Draw = false;
                _Position = parent._Position;
            }

            base.UpdateActive(gameTime);
        }

        public void SetTarget(Character target)
        {
            _Target = target;
            _Draw = true;
        }

        public override void Activate()
        {
            base.Activate();
        }
    }
}
