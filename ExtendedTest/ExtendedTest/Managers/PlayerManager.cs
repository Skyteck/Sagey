using ExtendedTest.GameObjects.Objects.Gatherables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    public class PlayerManager
    {
        Player _Player;
        WorldObject _CurrentWOTarget;
        //Managers
        InventoryManager _InventoryManager;
        KeyboardState _PrevKBState;
        WorldObjectManager _WorldObjectManager;
        NPCManager _NPCManager;
        TilemapManager _MapManager;
        public Vector2 _PlayerPos;

        private bool bankerGo = false;
        public bool _BankerGo { get => bankerGo; set => bankerGo = value; }
        public enum PlayerState
        {
            kStateIdle = 0,
            kStateWalk,
            kStateWC,
            kStateRun,
            kStateFish,
            kStateMine,
            kStateAttack,
            kStateDefend
        }

        private PlayerState playerState = PlayerState.kStateIdle;
        public PlayerState _PlayerState { get => playerState; set => playerState = value; }

        public float _PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
        private float playerSpeed = 128.0f;

        private Rectangle CheckRect
        {
            get
            {
                if (_Player._Direction == Sprite.Direction.kDirectionDown)
                {
                    return new Rectangle((int)_Player._Position.X - 32, (int)_Player._Position.Y + 32, 64, 32);
                }
                else if (_Player._Direction == Sprite.Direction.kDirectionUp)
                {
                    return new Rectangle((int)_Player._Position.X - 32, (int)_Player._Position.Y - 64, 64, 32);
                }
                else if (_Player._Direction == Sprite.Direction.kDirectionLeft)
                {
                    return new Rectangle((int)_Player._Position.X - 64, (int)_Player._Position.Y - 32, 32, 642);
                }
                else if (_Player._Direction == Sprite.Direction.kDirectionRight)
                {
                    return new Rectangle((int)_Player._Position.X + 32, (int)_Player._Position.Y - 32, 32, 64);
                }
                else
                {
                    Console.WriteLine("Error creating player check rectangle");
                    return new Rectangle();
                }
            }
        }

        public PlayerManager(Player p, InventoryManager IM, WorldObjectManager WOM, NPCManager NPCM, TilemapManager tm)
        {
            _Player = p;
            _InventoryManager = IM;
            _WorldObjectManager = WOM;
            _NPCManager = NPCM;
            _MapManager = tm;
        }

        public void Update(GameTime gt)
        {
            _PlayerPos = _Player._Position;
            Vector2 currentPos = _Player._Position;
            ProcessKeyboard(gt);
            if(CheckCollision())
            {
                _Player._Position = currentPos;
            }
            _Player.UpdateActive(gt);

            if(_CurrentWOTarget != null)
            {
                if(_CurrentWOTarget._CurrentState != Sprite.SpriteState.kStateInActive)
                {
                    ProcessWorldObject(_CurrentWOTarget);
                }
                else
                {
                    ClearTargets();
                    ChangePlayerState(PlayerState.kStateIdle);
                }
            }

            if(_Player._PlayerAttacking)
            {
                CheckSwordCollision();
            }

            CheckArrowCollision();
            _PlayerPos = _Player._Position;
        }

        private void CheckSwordCollision()
        {
            NPC npcHit = _NPCManager.CheckAttacks(_Player.swordTip._BoundingBox);
            if(npcHit != null)
            {
                Console.WriteLine(npcHit.Name);
                npcHit.ReceiveDamage(1);

            }
        }

        private void CheckArrowCollision()
        {
            foreach(Projectile arrow in _Player._ActiveArrows)
            {
                NPC npcHit = _NPCManager.CheckAttacks(arrow._ProjectileTip);
                if (npcHit != null)
                {
                    arrow.Deactivate();
                    npcHit.ReceiveDamage(1);

                }
            }
        }

        private bool CheckCollision()
        {
            //check collision with map
            //will check every rectangle on the current active map... not good but optimize later
            TileMap currentMap = _MapManager.findMap(_MapManager.PosToWorldTilePos(_PlayerPos));
            List<Rectangle> mapRects = currentMap.WallList;
            List<Vector2> test = HelperFunctions.RotatedRectList(_Player._WorldBoundingBox, _Player._Rotation);
            foreach (Rectangle rect in mapRects)
            {
                List<Vector2> dddd = HelperFunctions.RectToList(rect);
                bool collided = CollisionDetection2D.BoundingRectangle(test, dddd);
                if (collided)
                {
                    return true;
                }
            }
            

            //check collision with other sprites
            Sprite hit = _NPCManager.CheckCollisions(_Player._BoundingBox);
            if (hit == null)
            {
                hit = _WorldObjectManager.checkCollision(_Player._BoundingBox);
            }
            if(hit != null)
            {
                return true;
            }
            return false;
        }

        public void ProcessKeyboard(GameTime gt)
        {
            KeyboardState kbState = Keyboard.GetState();
            bool moved = false;
            Vector2 newPos = _Player._Position;
            if (kbState.IsKeyDown(Keys.A) || kbState.IsKeyDown(Keys.Left))
            {
                //player._Position.X -= player._Speed;
                newPos.X -= (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                _Player._Direction = Sprite.Direction.kDirectionLeft;
                moved = true;
            }
            else if (kbState.IsKeyDown(Keys.D) || kbState.IsKeyDown(Keys.Right))
            {
                //player._Position.X += player._Speed;
                newPos.X += (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                _Player._Direction = Sprite.Direction.kDirectionRight;
                moved = true;
            }
            if (kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.Up))
            {
                //player._Position.Y -= player._Speed;
                newPos.Y -= (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                _Player._Direction = Sprite.Direction.kDirectionUp;
                moved = true;
            }
            else if (kbState.IsKeyDown(Keys.S) || kbState.IsKeyDown(Keys.Down))
            {
                //player._Position.Y += player._Speed;
                newPos.Y += (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                _Player._Direction = Sprite.Direction.kDirectionDown;
                moved = true;
            }

            if(kbState.IsKeyDown(Keys.R) )
            {
                _Player._Rotation += 0.05f;
            }
            if(kbState.IsKeyDown(Keys.F))
            {
                _Player._Rotation = 0f;
            }

            _Player._Position = newPos;
            if ( kbState.IsKeyDown(Keys.Space) && _PrevKBState.IsKeyUp(Keys.Space))
            {
                //This is for interacting with NPCs or other objects.
                //first check if we talked to an NPC;

                NPC npcHit = _NPCManager.CheckCollisions(CheckRect);
                if(npcHit != null)
                {
                    ProcessNPC(npcHit);
                }
                else
                {
                    _CurrentWOTarget = _WorldObjectManager.checkCollision(CheckRect);
                }
            }

            if (kbState.IsKeyDown(Keys.V) && _PrevKBState.IsKeyUp(Keys.V))
            {
                _Player.Attack();
            }

            if (kbState.IsKeyDown(Keys.B) && _PrevKBState.IsKeyUp(Keys.B))
            {
                _Player.RangedAttack();
            }

            //if (kbState.IsKeyDown(Keys.J) && _PrevKBState.IsKeyUp(Keys.J))
            //{
            //    _Player.ToggleCorners();
            //}

            if (moved)
            {
                ClearTargets();
            }
            _PrevKBState = kbState;
        }

        private void ProcessWorldObject(WorldObject woHit)
        {
            if(woHit.IsGatherable)
            {
                Gather(woHit as Gatherable);
                ChangePlayerState(PlayerState.kStateWC);
            }
        }

        private void Gather(Gatherable gatherable)
        {
            Item.ItemType itemGet = gatherable.GetGathered();
            if(itemGet != Item.ItemType.kItemNone)
            {
                _InventoryManager.AddItem(itemGet);
            }
        }
        private void ProcessNPC(NPC npcHit)
        {
            if(npcHit._Tag == Sprite.SpriteType.kNPCType)
            {
                _BankerGo = true;
            }
        }

        public void Draw(SpriteBatch SB)
        {
            _Player.Draw(SB);
        }

        public void ChangePlayerState(PlayerState state)
        {
            _PlayerState = state;
            _Player.ChangeState(state);
        }

        public void SetPosition(float x, float y)
        {
            _Player._Position.X = x;
            _Player._Position.Y = y;
            _PlayerPos = new Vector2(x, y);
        }

        public void ClearTargets()
        {
            _CurrentWOTarget = null;
            _BankerGo = false;
        }
    }
}
