using Microsoft.Xna.Framework;
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
    public class NPCManager
    {
        public List<Character> _SpriteListActive;
        public List<Character> _SpriteListDead;
        public TilemapManager _TilemapManager;
        ContentManager _Content;
        CombatManager _CBmanager;
        Player thePlayer;
        List<Projectile> _ProjectileList;

        public NPCManager(TilemapManager tMapManager, CombatManager cbManager ,ContentManager content, Player player)
        {
            _SpriteListActive = new List<Character>();
            _SpriteListDead = new List<Character>();
            _ProjectileList = new List<Projectile>();
            _TilemapManager = tMapManager;
            _Content = content;
            _CBmanager = cbManager;
            thePlayer = player;
        }

        public void CreateMonster(TmxObject thing, Vector2 pos)
        {
            if(thing.Type.Equals("Slime"))
            {
                GameObjects.NPCs.Monsters.Slime newSprite = new GameObjects.NPCs.Monsters.Slime(this, _CBmanager);
                newSprite._Position = _TilemapManager.findTile(pos).tileCenter;
                newSprite.LoadContent("Art/" + thing.Type, _Content);
                newSprite.SetBoundaries(thing.X, thing.Width, thing.Height, thing.Y);
                if (Convert.ToBoolean(thing.Properties["Agressive"]))
                {
                    newSprite.AddTarget(thePlayer);
                }
                newSprite._Tag = Sprite.SpriteType.kMonsterType;
                newSprite.Name = thing.Name.ToUpper();
                newSprite._CurrentState = Sprite.SpriteState.kStateActive;
                newSprite.parentList = _SpriteListActive;
                _SpriteListActive.Add(newSprite);
            }
        }

        public void UpdateNPCs(GameTime gameTime)
        {

            List<Character> combinedList = new List<Character>();
            combinedList.AddRange(_SpriteListActive);
            combinedList.AddRange(_SpriteListDead);
            _SpriteListActive = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateActive);
            _SpriteListDead = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateInActive);

            foreach (NPC sprite in _SpriteListActive)
            {
                sprite.UpdateActive(gameTime);
            }

            foreach (NPC sprite in _SpriteListDead)
            {
                sprite.UpdateDead(gameTime);
            }
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
