using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest.GameObjects.NPCs.Monsters
{
    public class Slime : Attackable
    {
        public Slime(Managers.NPCManager manager, Managers.CombatManager cbManager) : base(manager, cbManager)
        {
            attack = 6;
            defense = 5;
            attackRange = 256;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            //myShot = new Projectile(this, _CBManager);
            //myShot.LoadContent(path + "Shot", content);
            //AddChild(myShot);
        }
    }
}
