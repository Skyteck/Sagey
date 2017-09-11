using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest.GameObjects.NPCs.Monsters
{
    public class Slime : NPC
    {
        public Slime(Managers.NPCManager manager) : base(manager)
        {
            attack = 6;
            defense = 5;
            attackRange = 256;
            _HP = 1;
            startHP = 1;
            _CanFight = true;
            _Speed = 2f;
            this._AttackStyle = AttackStyle.kMeleeStyle;
            SetStatus(Enums.EffectTypes.kEffectBurn);
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
