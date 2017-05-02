using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace ExtendedTest.Managers
{
    public class NpcManager
    {
        public List<Sprite> _SpriteList;
        TilemapManager _TilemapManager;
        ContentManager _Content;
        Player thePlayer;

        public NpcManager(TilemapManager tMapManager, ContentManager content, Player player)
        {
            _SpriteList = new List<Sprite>();
            _TilemapManager = tMapManager;
            _Content = content;
            thePlayer = player;
        }

        public void CreateMonster(TmxObject thing, Vector2 pos)
        {
            if(thing.Type.Equals("Slime"))
            {
                Monster newSprite = new Monster(thing.X, thing.Width, thing.Height, thing.Y, this);
                newSprite._Position = pos;
                newSprite.LoadContent("Art/" + thing.Type, _Content);
                if(Convert.ToBoolean(thing.Properties["Agressive"]))
                {
                    newSprite.SetTarget(thePlayer);
                }
                newSprite._Tag = Sprite.SpriteType.kSlimeType;
                newSprite.Name = thing.Name.ToUpper();
                newSprite._CurrentState = Sprite.SpriteState.kStateActive;
                newSprite.parentList = _SpriteList;
                _SpriteList.Add(newSprite);
            }
        }

        public void UpdateNPCs(GameTime gameTime)
        {
            foreach(Sprite sprite in _SpriteList)
            {
                sprite.Update(gameTime, this._SpriteList);
            }
        }

        public void DrawNPCs(SpriteBatch spriteBatch)
        {
            foreach (Sprite sprite in _SpriteList)
            {
                sprite.Draw(spriteBatch);
            }

        }

        public Character findNPC(String sprite)
        {
            return (NPC)_SpriteList.Find(x => x.Name == sprite);
        }
    }
}
