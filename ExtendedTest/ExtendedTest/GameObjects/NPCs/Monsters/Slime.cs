using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Sagey.GameObjects.NPCs.Monsters
{
    public class Slime : NPC
    {
        public Slime(Managers.NPCManager manager) : base(manager)
        {
            attack = 6;
            defense = 5;
            attackRange = 256;
            _HP = 4;
            startHP = 4;
            _CanFight = true;
            _StartSpeed = 2f;
            _Speed = _StartSpeed;
            Tame();
            ItemDrops.Add(Enums.ItemID.kItemSlimeGoo);
            this._AttackStyle = AttackStyle.kMeleeStyle;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            //myShot = new Projectile(this, _CBManager);
            //myShot.LoadContent(path + "Shot", content);
            //AddChild(myShot);
            //SelectSprite();
        }
    }
}
