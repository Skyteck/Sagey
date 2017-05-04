﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace ExtendedTest
{
    public class NpcManager
    {
        public List<Character> _SpriteListActive;
        public List<Character> _SpriteListDead;
        TilemapManager _TilemapManager;
        ContentManager _Content;
        CombatManager _CBmanager;
        Player thePlayer;

        public NpcManager(TilemapManager tMapManager, CombatManager cbManager ,ContentManager content, Player player)
        {
            _SpriteListActive = new List<Character>();
            _SpriteListDead = new List<Character>();
            _TilemapManager = tMapManager;
            _Content = content;
            _CBmanager = cbManager;
            thePlayer = player;
        }

        public void CreateMonster(TmxObject thing, Vector2 pos)
        {
            if(thing.Type.Equals("Slime"))
            {
                Monster newSprite = new Monster(this, _CBmanager);
                newSprite._Position = pos;
                newSprite.LoadContent("Art/" + thing.Type, _Content);
                newSprite.SetBoundaries(thing.X, thing.Width, thing.Height, thing.Y);
                if (Convert.ToBoolean(thing.Properties["Agressive"]))
                {
                    newSprite.AddTarget(thePlayer);
                }
                newSprite._Tag = Sprite.SpriteType.kSlimeType;
                newSprite.Name = thing.Name.ToUpper();
                newSprite._CurrentState = Sprite.SpriteState.kStateActive;
                newSprite.parentList = _SpriteListActive;
                _SpriteListActive.Add(newSprite);
            }
        }

        public void UpdateNPCs(GameTime gameTime)
        {
            foreach(Sprite sprite in _SpriteListActive)
            {
                sprite.UpdateActive(gameTime);
            }
            List<Character> combinedList = new List<Character>();
            combinedList.AddRange(_SpriteListDead);
            combinedList.AddRange(_SpriteListDead);
            _SpriteListActive = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateActive);
            _SpriteListDead = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateInActive);
        }

        public void DrawNPCs(SpriteBatch spriteBatch)
        {
            foreach (Sprite sprite in _SpriteListActive)
            {
                sprite.Draw(spriteBatch);
            }

        }

        public Character findNPCByName(String sprite)
        {
            return (NPC)_SpriteListActive.Find(x => x.Name == sprite);
        }

        public List<Character> findNPCsByTag(Sprite.SpriteType tag)
        {
            return _SpriteListActive.FindAll(x => x._Tag == tag); 
        }
    }
}
