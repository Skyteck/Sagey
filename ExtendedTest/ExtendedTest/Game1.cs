﻿using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TiledSharp;

namespace ExtendedTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Player player;
        //List<Sprite> gameObjectList;
        MouseState previousMouseState;
        KeyboardState previousKBState;
        public Camera  camera;

        SpriteFont font;
        bool typingMode = false;
        public KbHandler kbHandler;

        //Managers
        Managers.InventoryManager _InvenManager;
        Managers.TilemapManager _MapManager;
        public Managers.NPCManager _NPCManager;
        Managers.WorldObjectManager _GameObjectManager;
        Managers.CombatManager _CBManager;
        Managers.UIManager _UIManager;
        Managers.ChemistryManager _ChemistryManager;
        Managers.ItemManager _ItemManager;
        //UI
        
        Vector2 mouseClickPos;
        Sprite mouseCursor;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _ItemManager = new Managers.ItemManager(Content);
            _InvenManager = new Managers.InventoryManager(Content, _ItemManager);
            _CBManager = new Managers.CombatManager();
            player = new Player(_InvenManager, _CBManager);
            _MapManager = new Managers.TilemapManager(_NPCManager, _GameObjectManager);
            _NPCManager = new Managers.NPCManager(_MapManager, _CBManager,  Content, player);
            _GameObjectManager = new Managers.WorldObjectManager(_MapManager, _InvenManager, Content, player);
            _UIManager = new Managers.UIManager(_InvenManager);
            _ChemistryManager = new Managers.ChemistryManager(_InvenManager, _GameObjectManager, _NPCManager, Content, _ItemManager);
            camera = new Camera(GraphicsDevice);
            kbHandler = new KbHandler();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadPlayerContent();
            LoadGUI();

            _MapManager.LoadMap("0-0", Content);
            LoadMapNPCs(_MapManager.findMapByName("0-0"));
            LoadMapObjects(_MapManager.findMapByName("0-0"));
            _MapManager.LoadMap("0-1", Content);
            font = Content.Load<SpriteFont>("Fonts/Fipps");
            _InvenManager.loadContent();
            _ItemManager.LoadItems("Content/JSON/Items.json");
            _ChemistryManager.LoadIcons();


            for (int i = 0; i < 5; i++)
            {
                _InvenManager.AddItem(Item.ItemType.kItemLog);
            }
            _InvenManager.AddItem(Item.ItemType.kItemMatches);

            //XDocument xmlTest = XDocument.Load("Content/Items.xml");
            //IEnumerable<XElement> itemList = xmlTest.Elements("Items");
            //IEnumerable<XElement> LogList = itemList.Elements("Logs");
            //foreach(XElement item in LogList.Elements("Item"))
            //{
            //    String test = item.Element("Name").Value;
            //    String test2 = item.Element("SaleValue").Value;
            //    String test3 = item.Element("Weight").Value;
            //}
            //IEnumerable<XElement> oreList = itemList.Elements("Ores");
            //foreach (XElement item in oreList.Elements("Item"))
            //{
            //    String test = item.Element("Name").Value;
            //    String test2 = item.Element("SaleValue").Value;
            //    String test3 = item.Element("Weight").Value;
            //}

            //List<Sprite> test = new List<Sprite>();
            //test = gameObjectList.FindAll(x => x._Tag == Sprite.SpriteType.kTreeType);

            // TODO: use this.Content to load your game content here
        }

        private void LoadPlayerContent()
        {
            player.LoadContent("Art/Player", Content);
            player._Position = new Vector2(400, 400);
        }

        private void LoadGUI()
        {
            GameObjects.UIObjects.InventoryPanel inventoryBG = new GameObjects.UIObjects.InventoryPanel(_InvenManager);
            inventoryBG.LoadContent("Art/inventoryBG", Content);
            inventoryBG._Position = new Vector2(250, 450);
            inventoryBG.Name = "Inventory";
            inventoryBG._Opacity = 0.6f;
            _UIManager.UIPanels.Add(inventoryBG);

            GameObjects.UIObjects.CraftingPanel craftingPanel = new GameObjects.UIObjects.CraftingPanel(_ChemistryManager);
            craftingPanel.LoadContent("Art/inventoryBG", Content);
            craftingPanel._Position = new Vector2(450, 450);
            craftingPanel.Name = "Crafting";
            craftingPanel._Opacity = 0.9f;
            _UIManager.UIPanels.Add(craftingPanel);



            mouseCursor = new Sprite();
            mouseCursor.LoadContent("Art/log", Content);
            mouseCursor.Name = "Cursor";
        }

        public void LoadMapNPCs(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.findNPCs();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    int adjustThingX = (int)(thing.X + (testMap._Postion.X + (thing.Width / 2)));
                    int adjustThingY = (int)(thing.Y + (testMap._Postion.Y + (thing.Height / 2)));
                    _NPCManager.CreateMonster(thing, new Vector2(adjustThingX, adjustThingY));
                }
            }
        }

        public void LoadMapObjects(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.findObjects();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    Vector2 newPos = new Vector2((int)thing.X + testMap._Postion.X, (int)thing.Y + testMap._Postion.Y);
                    _GameObjectManager.CreateObject(thing, newPos);
                }
            }
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(this.IsActive)
            {
                // TODO: Add your update logic here
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();


                ProcessMouse(gameTime);
                ProcessKeyboard(gameTime);

                kbHandler.Update();
                //if (typingMode && !kbHandler.typingMode) //ugly, but should show that input mode ended...?
                //{
                //    processor.Parsetext(kbHandler.Input);
                //    if (processor.currentError != string.Empty) kbHandler.Input = processor.currentError;
                //    //kbHandler.Input = string.Empty;
                //}
                player.UpdateActive(gameTime);
                _NPCManager.UpdateNPCs(gameTime);
                _GameObjectManager.Update(gameTime);

                if(!kbHandler.typingMode)
                {
                    //processor.currentError = string.Empty;
                    ProcessCamera(gameTime);

                }

                _UIManager.getUIElement("Inventory")._Position = camera.ToWorld(400, 400);
                _UIManager.getUIElement("Crafting")._Position = camera.ToWorld(600, 400);

                foreach (UIPanel element in _UIManager.UIPanels)
                {
                    element.UpdateActive(gameTime);
                }

                base.Update(gameTime);
                //Show FPS
                if((1 / gameTime.ElapsedGameTime.TotalSeconds) <= 59)
                {
                    Console.WriteLine("BAD FPS!!!!!!!!!!");
                }
                typingMode = kbHandler.typingMode;
            }
        }

        private void ProcessKeyboard(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();
            //get player tile.

            Tile playerTile = _MapManager.findTile(player._Position);
            Vector2 newPos = player._Position;
            if (kbState.IsKeyDown(Keys.A) || kbState.IsKeyDown(Keys.Left))
            {
                //player._Position.X -= player._Speed;
                newPos.X -= 64;
            }
            else if (kbState.IsKeyDown(Keys.D) || kbState.IsKeyDown(Keys.Right))
            {
                //player._Position.X += player._Speed;
                newPos.X += 64;
            }
            if (kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.Up))
            {
                //player._Position.Y -= player._Speed;
                newPos.Y -= 64;
            }
            else if (kbState.IsKeyDown(Keys.S) || kbState.IsKeyDown(Keys.Down))
            {
                //player._Position.Y += player._Speed;
                newPos.Y += 64;
            }
            Tile newDest = _MapManager.findWalkableTile(newPos);
            if(newDest!= null)
            {
                if(newDest != playerTile)
                {
                    player.setDestination(newDest.tileCenter);

                }
            }

            if(IsKeyPressed(Keys.Space))
            {
                WorldObject gotHit = player.CheckObjectHit(_GameObjectManager.ObjectList);
                if(gotHit == null)
                {
                    player.checkCharacterHit(_NPCManager._SpriteListActive);
                }
            }

            if(IsKeyPressed(Keys.E))
            {
                _ChemistryManager.CheckRecipes();
                _UIManager.TogglePanel("Crafting");
            }
            if (IsKeyPressed(Keys.I))
            {
                _UIManager.TogglePanel("Inventory");
            }

            previousKBState = kbState;
        }

        private bool IsKeyPressed(Keys key)
        {
            KeyboardState kbState = Keyboard.GetState();
            if(kbState.IsKeyDown(key) && previousKBState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsKeyReleased(Keys key)
        {
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyUp(key) && previousKBState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsKeyHeld(Keys key)
        {
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(key) && previousKBState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ProcessMouse(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            String command = string.Empty;
            if(mouseState.LeftButton==ButtonState.Pressed && previousMouseState.LeftButton==ButtonState.Released)
            {
                Vector2 mouseClickpos = camera.ToWorld(new Vector2(mouseState.Position.X, mouseState.Position.Y));

                //check if a click was in the crafting panel
                foreach(UIPanel panel in _UIManager.UIPanels)
                {
                    panel.ProcessClick(mouseClickpos);
                }

            }

            if (mouseState.ScrollWheelValue > previousMouseState.ScrollWheelValue)
            {
                camera.Scale += 0.2f;
                if (camera.Scale > 2f)
                {
                    camera.Scale = 2f;
                }
            }
            else if (mouseState.ScrollWheelValue < previousMouseState.ScrollWheelValue)
            {
                camera.Scale -= 0.25f;
                if (camera.Scale < 0.6f)
                {
                    camera.Scale = 0.6f;
                }
            }

            previousMouseState = mouseState;

        }

        private void ProcessCamera(GameTime gameTime)
        {
            //KeyboardState state = Keyboard.GetState();
            //if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            //{
            //    this.camera.Position = new Vector2(this.camera.Position.X - 5, this.camera.Position.Y);
            //}
            //else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            //{
            //    this.camera.Position = new Vector2(this.camera.Position.X + 5, this.camera.Position.Y);
            //}
            //if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            //{
            //    this.camera.Position = new Vector2(this.camera.Position.X, this.camera.Position.Y - 5);
            //}
            //else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            //{
            //    this.camera.Position = new Vector2(this.camera.Position.X, this.camera.Position.Y + 5);
            //}

            this.camera.Position = player._Position;

            camera.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(camera);

            _MapManager.Draw(spriteBatch);

            player.Draw(spriteBatch);

            _NPCManager.DrawNPCs(spriteBatch);

            _GameObjectManager.Draw(spriteBatch);

            _UIManager.Draw(spriteBatch);

            //Vector2 invenBgpos = _UIManager.getUIElement("Inventory")._TopLeft;
            //_InvenManager.Draw(spriteBatch, invenBgpos);

            mouseCursor.Draw(spriteBatch);

            spriteBatch.DrawString(font, kbHandler.Input, camera.ToWorld(new Vector2(100, 100)), Color.Black);
            spriteBatch.DrawString(font, player._HP.ToString(), camera.ToWorld(new Vector2(200, 200)), Color.White);

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
