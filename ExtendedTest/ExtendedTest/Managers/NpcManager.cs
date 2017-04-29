using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace ExtendedTest.Managers
{
    class NpcManager
    {
        List<Sprite> _SpriteList;
        TilemapManager _TilemapManager;
        ContentManager _Content;

        public NpcManager(TilemapManager tMapManager, ContentManager content)
        {
            _SpriteList = new List<Sprite>();
            _TilemapManager = tMapManager;
            _Content = content;
        }

        public void CreateMonster(enums.NPCenums.MonsterTypes monsterType, TmxObject thing, Vector2 pos, Character target)
        {
            if(monsterType == enums.NPCenums.MonsterTypes.kMonsterSlime)
            {
                Monster newSprite = new Monster(thing.X, thing.Width, thing.Height, thing.Y);
                newSprite._Position = pos;
                newSprite.LoadContent("Art/" + thing.Type, _Content);
                newSprite.SetTarget(target);
                newSprite._Tag = Sprite.SpriteType.kSlimeType;
                newSprite._CurrentState = Sprite.SpriteState.kStateActive;
                newSprite.parentList = _SpriteList;
                _SpriteList.Add(newSprite);
            }
        }
    }
}
