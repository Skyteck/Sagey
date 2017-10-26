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
    public class NPCManager
    {
        public List<NPC> _SpriteListActive;
        public List<NPC> _SpriteListDead;
        public TilemapManager _TilemapManager;
        DialogManager _DialogManager;
        ContentManager _Content;
        Player thePlayer;
        List<Projectile> _ProjectileList;

        public NPCManager(TilemapManager tMapManager, ContentManager content, Player player, DialogManager dm)
        {
            _SpriteListActive = new List<NPC>();
            _SpriteListDead = new List<NPC>();
            _ProjectileList = new List<Projectile>();
            _TilemapManager = tMapManager;
            _DialogManager = dm;
            _Content = content;
            thePlayer = player;
        }

        public void CreateNPC(TmxObject thing, Vector2 pos)
        {
            if(thing.Type.Equals("Slime"))
            {
                GameObjects.NPCs.Monsters.Slime newSprite = new GameObjects.NPCs.Monsters.Slime(this);
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
            else if(thing.Type.Equals("Banker"))
            {
                GameObjects.NPCs.Banker newSprite = new GameObjects.NPCs.Banker(this);
                newSprite._Position = _TilemapManager.findTile(pos).tileCenter;
                newSprite.LoadContent("Art/" + thing.Type, _Content);
                newSprite.SetBoundaries(thing.X, thing.Width, thing.Height, thing.Y);
                //if (Convert.ToBoolean(thing.Properties["Agressive"]))
                //{
                //    //newSprite.AddTarget(thePlayer);
                //}
                newSprite._Tag = Sprite.SpriteType.kNPCType;
                newSprite.Name = thing.Name.ToUpper();
                newSprite._CurrentState = Sprite.SpriteState.kStateActive;
                newSprite.parentList = _SpriteListActive;
                _SpriteListActive.Add(newSprite);
            }
        }

        public void UpdateNPCs(GameTime gameTime)
        {

            List<NPC> combinedList = new List<NPC>();
            combinedList.AddRange(_SpriteListActive);
            combinedList.AddRange(_SpriteListDead);
            _SpriteListActive = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateActive);
            _SpriteListDead = combinedList.FindAll(x => x._CurrentState == Sprite.SpriteState.kStateInActive);

            foreach(NPC sprite in combinedList)
            {
                sprite.Update(gameTime);
            }
        }

        public void DrawNPCs(SpriteBatch spriteBatch)
        {
            foreach (Sprite sprite in _SpriteListActive)
            {
                sprite.Draw(spriteBatch);
            }

        }

        public NPC FindNPCByName(String sprite)
        {
            return _SpriteListActive.Find(x => x.Name == sprite);
        }

        public List<NPC> FindNPCsByTag(Sprite.SpriteType tag)
        {
            return _SpriteListActive.FindAll(x => x._Tag == tag); 
        }

        public NPC CheckCollisions(Rectangle checkRect)
        {
            foreach(NPC npc in _SpriteListActive)
            {
                if(npc._BoundingBox.Intersects(checkRect))
                {
                    return npc;
                }
            }
            return null;
        }

        public NPC CheckAttacks(Rectangle rect)
        {
            List<NPC> attackables = _SpriteListActive.FindAll(x => x._CanFight == true);
            foreach(NPC thing in attackables)
            {
                if(thing._BoundingBox.Intersects(rect))
                {
                    return thing;
                }
            }
            return null;
        }

        public bool AttemptInteract(Rectangle checkRect)
        {
            foreach(NPC thing in _SpriteListActive.FindAll(x=>x._Interactable == true))
            {
                thing.Interact();
                return true;
            }
            return false;
        }

        public void PlayDialogue(string msgID)
        {
            _DialogManager.PlayMessage(msgID);
        }
    }
}
