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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Sagey.Managers;

namespace Sagey
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Player player;

        //public Camera  camera;
        TestCamera _TestCamera;

        SpriteFont font;
        bool typingMode = false;
        public KbHandler kbHandler;

        //Managers
        Managers.InventoryManager _InvenManager;
        Managers.TilemapManager _MapManager;
        public Managers.NPCManager _NPCManager;
        Managers.WorldObjectManager _WorldObjectManager;
        Managers.UIManager _UIManager;
        Managers.ChemistryManager _ChemistryManager;
        Managers.ItemManager _ItemManager;
        Managers.PlayerManager _PlayerManager;
        Managers.BankManager _BankManager;
        Managers.GatherableManager _GatherableManager;
        Managers.DialogManager _DialogManager;
        QuestManager _QuestManager;
        EventManager _EventManager;
        
        //UI        
        Vector2 mouseClickPos;
        Sprite mouseCursor;
        Texture2D _SelectTex;
        Sprite _SelectedSprite = null;
        bool BankMode = false;


        const int TargetWidth = 480;
        const int TargetHeight = 270;
        Matrix Scale;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            //this.IsFixedTimeStep = false;
            //graphics.SynchronizeWithVerticalRetrace = false;

            graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            graphics.ApplyChanges();

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
            player = new Player();
            _UIManager = new Managers.UIManager();
            _QuestManager = new QuestManager();
            _MapManager = new Managers.TilemapManager();
            _DialogManager = new Managers.DialogManager(_QuestManager);
            _EventManager = new EventManager(_QuestManager);
            _ItemManager = new Managers.ItemManager(Content);
            _InvenManager = new Managers.InventoryManager(_ItemManager);
            _BankManager = new Managers.BankManager(_ItemManager);
            _WorldObjectManager = new Managers.WorldObjectManager(_MapManager, _InvenManager, Content, player, _ItemManager);
            _NPCManager = new Managers.NPCManager(_MapManager, Content, player, _DialogManager, _InvenManager, _WorldObjectManager);
            _GatherableManager = new Managers.GatherableManager(_MapManager, _InvenManager, Content, player);
            _ChemistryManager = new Managers.ChemistryManager(_InvenManager, _WorldObjectManager, _NPCManager, Content, _ItemManager);

            _PlayerManager = new Managers.PlayerManager(player, _InvenManager, _WorldObjectManager, _NPCManager, _MapManager, _GatherableManager);
            _WorldObjectManager.SetGatherManager(_GatherableManager);
            kbHandler = new KbHandler();

            _SelectedSprite = new Sprite();

            InputHelper.Init();

            _TestCamera = new TestCamera(GraphicsDevice);

            base.Initialize();

            //EVENTS
            _DialogManager.BankOpened += HandleBankOpened;
            _PlayerManager.BankOpened += HandleBankOpened;
            _PlayerManager.PlayerMoved += HandlePlayerMoved;

            _BankManager.AttachEvents(_EventManager);
            _NPCManager.AttachEvents(_EventManager);
            _ChemistryManager.AttachEvents(_EventManager);
            _WorldObjectManager.AttachEvents(_EventManager);
            _GatherableManager.AttachEvents(_EventManager);
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            //camera = new Camera(GraphicsDevice);
            
            float scaleX = graphics.PreferredBackBufferWidth / TargetWidth;
            float scaleY = graphics.PreferredBackBufferHeight / TargetHeight;
            Scale = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));

            LoadPlayerContent();
            LoadGUI();
            _MapManager.LoadMap("0-0", Content);
            LoadMapNPCs(_MapManager.findMapByName("0-0"));
            LoadMapObjects(_MapManager.findMapByName("0-0"));
            _MapManager.LoadMap("0-1", Content);
            font = Content.Load<SpriteFont>("Fonts/Fipps");
            _ItemManager.LoadItems("Content/JSON/ItemList.json");
            _ChemistryManager.LoadRecipes("Content/JSON/RecipeList.json");
            _ChemistryManager.LoadIcons();
            _GatherableManager.LoadContent(Content);

            _SelectTex = Content.Load<Texture2D>("Art/WhiteTexture");
            //check if save exists

            //else start a new save
            bool saveExist = false;
            string path = Content.RootDirectory + @"\Save\save.txt";
            if (System.IO.File.Exists(path))
            {
                saveExist = true;
            }
            else 
            {
                System.IO.Directory.CreateDirectory(Content.RootDirectory + @"\Save\");
            }
            if (saveExist)
            {
                //load
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                string line = file.ReadLine();
                if (line != null)
                {
                    string[] playerPos = line.Split(' ');
                    float.TryParse(playerPos[0], out var x);
                    float.TryParse(playerPos[1], out var y);
                    _PlayerManager.SetPosition(x, y);
                }
                bool bankMode = false;
                bool inventoryMode = false;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Equals("B"))
                    {
                        bankMode = true;
                        continue;
                    }
                    else if (line.Equals("BEnd"))
                    {
                        bankMode = false;
                        continue;
                    }
                    if (line.Equals("I"))
                    {
                        inventoryMode = true;
                        continue;
                    }
                    else if (line.Equals("IEnd"))
                    {
                        inventoryMode = false;
                        continue;
                    }
                    if (bankMode || inventoryMode)
                    {
                        string[] items = line.Split(' ');
                        Int32.TryParse(items[0], out int itemType);
                        Enums.ItemID type = (Enums.ItemID)itemType;
                        Int32.TryParse(items[1], out int amt);
                        if (bankMode)
                        {
                            _BankManager.AddItem(type, amt);
                        }
                        else if (inventoryMode)
                        {
                            _InvenManager.AddItem(type, amt);
                        }
                    }
                    
                }

                file.Close();
            }
            else
            {
                player._Position = new Vector2(32, 320);

                _InvenManager.AddItem(Enums.ItemID.kItemLog, 5);
                _InvenManager.AddItem(Enums.ItemID.kItemMatches);
                _InvenManager.AddItem(Enums.ItemID.kItemFish, 2);
                _InvenManager.AddItem(Enums.ItemID.kItemMilk, 1);
                _InvenManager.AddItem(Enums.ItemID.kItemBucket, 3);

                _BankManager.AddItem(Enums.ItemID.kItemLog, 10);
                _BankManager.AddItem(Enums.ItemID.kItemFish, 3);
            }

            _QuestManager.GenerateQuest();
            //List<Dialog> dList = new List<Dialog>();

            //Dialog d1 = new Dialog();
            //d1.ID = "NPC1";
            //d1.textList.Add("I'm a talking slime!");
            //d1.textList.Add("y = mx + b");
            //d1.textList.Add("Do you know the muffin pan?");
            //DialogOption option = new DialogOption();
            //option.NextMsgID = "muffinYes";
            //option.optiontext = "I think it's muffin pan though.";
            //d1.options.Add(option);
            //option = new DialogOption();
            //option.NextMsgID = "muffinNo";
            //option.optiontext = "That's not how the story goes...";
            //d1.options.Add(option);
            //dList.Add(d1);

            //Dialog d2 = new Dialog();
            //d2.ID = "MightyDucks";
            //d2.textList.Add("I love Mike!");
            //dList.Add(d2);
            //List <Dialog> list2 = new List<Dialog>();

            //List<Recipe> rList = new List<Recipe>();
            //Recipe matches = new Recipes.MatchesRecipe();
            //Recipe DoubleLog = new Recipes.DoubleLogRecipe();
            //Recipe fishStick = new Recipes.FishStickRecipe();

            //rList.Add(matches);
            //rList.Add(DoubleLog);
            //rList.Add(fishStick);

            

            string text = JsonConvert.SerializeObject(_QuestManager.GetActiveQuests(), Newtonsoft.Json.Formatting.Indented);

            path = Content.RootDirectory + @"\JSON\Dialog_EN_US.json";
            _DialogManager.LoadDialog(path);


            //_DialogManager.PlayMessage("NPC1");
            //var dialog = System.IO.File.ReadAllText(path);
            //list2 = JsonConvert.DeserializeObject<List<Dialog>>(dialog);
            
            

            // TODO: use this.Content to load your game content here
        }

        private void LoadPlayerContent()
        {
            player.LoadContent("Art/Player", Content);
        }

        private void LoadGUI()
        {
            GameObjects.UIObjects.InventoryPanel inventoryBG = new GameObjects.UIObjects.InventoryPanel(_InvenManager);
            inventoryBG.LoadContent("Art/BlackTexture", Content);
            inventoryBG._InitialPos = new Vector2(200, 200);
            inventoryBG.Name = "Inventory";
            inventoryBG._Opacity = 0.6f;
            inventoryBG._UIManager = _UIManager;
            _UIManager.UIPanels.Add(inventoryBG);

            //_UIManager.TogglePanel("Inventory");

            GameObjects.UIObjects.CraftingPanel craftingPanel = new GameObjects.UIObjects.CraftingPanel(_ChemistryManager);
            craftingPanel.LoadContent("Art/BlackTexture", Content);
            craftingPanel._InitialPos = new Vector2(600, 400);
            craftingPanel.Name = "Crafting";
            craftingPanel._Opacity = 0.9f;
            craftingPanel._UIManager = _UIManager;
            _UIManager.UIPanels.Add(craftingPanel);

            
            GameObjects.UIObjects.BankPanel bankPanel = new GameObjects.UIObjects.BankPanel(_BankManager);
            bankPanel.LoadContent("Art/BlackTexture", Content);
            bankPanel._InitialPos = new Vector2(400, 200);
            bankPanel.Name = "Bank";
            bankPanel._Opacity = 1f;
            bankPanel._UIManager = _UIManager;
            _UIManager.UIPanels.Add(bankPanel);

            GameObjects.UIObjects.DialogPanel dPanel = new GameObjects.UIObjects.DialogPanel(_DialogManager);
            dPanel.LoadContent("Art/BlackTexture", Content);
            dPanel._InitialPos = new Vector2(0, 0);
            dPanel.Name = "Dialog";
            dPanel._Opacity = 1;
            dPanel._UIManager = _UIManager;
            _UIManager.UIPanels.Add(dPanel);

            mouseCursor = new Sprite();
            mouseCursor.Name = "Cursor";
        }

        public void LoadMapNPCs(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.FindNPCs();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    int adjustThingX = (int)(thing.X + (testMap._Postion.X + (thing.Width / 2)));
                    int adjustThingY = (int)(thing.Y + (testMap._Postion.Y + (thing.Height / 2)));
                    _NPCManager.CreateNPC(thing, new Vector2(adjustThingX, adjustThingY));
                }
            }
        }

        public void LoadMapObjects(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.FindObjects();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    Vector2 newPos = new Vector2((int)thing.X + testMap._Postion.X, (int)thing.Y + testMap._Postion.Y);
                    //_WorldObjectManager.CreateObject(thing, newPos);
                    if(thing.Type == "Dirt")
                    {
                        _WorldObjectManager.CreateObject(thing, newPos);
                    }
                    else
                    {
                        _GatherableManager.CreateGatherable(thing, newPos);

                    }
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
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    SaveGame();
                    Exit();
                }

                InputHelper.Update();

                _EventManager.ProcessEvents();

                ProcessMouse(gameTime);
                ProcessKeyboard(gameTime);
                

                if(InputHelper.IsKeyPressed(Keys.J))
                {
                    _BankManager.AddItem(Enums.ItemID.kItemBucket);
                }

                //if (typingMode && !kbHandler.typingMode) //ugly, but should show that input mode ended...?
                //{
                //    processor.Parsetext(kbHandler.Input);
                //    if (processor.currentError != string.Empty) kbHandler.Input = processor.currentError;
                //    //kbHandler.Input = string.Empty;
                //}
                _PlayerManager.Update(gameTime);
                _SelectedSprite = null;
                if(_PlayerManager._FrontSprite != null)
                {
                    _SelectedSprite = _PlayerManager._FrontSprite;
                }

                _NPCManager.UpdateNPCs(gameTime);
                _WorldObjectManager.Update(gameTime);
                _GatherableManager.Update(gameTime);
                

                //ProcessCamera(gameTime);
                _TestCamera._Position = player._Position;
                _UIManager.Update(gameTime);
                
                base.Update(gameTime);

                //Console.WriteLine(frameRate);



            }
        }

        private void ProcessKeyboard(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            if(InputHelper.IsKeyPressed(Keys.E))
            {
                _UIManager.TogglePanel("Crafting");
            }
            if (InputHelper.IsKeyPressed(Keys.I))
            {
                _UIManager.TogglePanel("Inventory");
            }
            if(InputHelper.IsKeyPressed(Keys.Escape))
            {
                _UIManager.HideAll();
            }
            if(InputHelper.IsKeyPressed(Keys.P))
            {
                SaveGame();
            }
        }

        private void ProcessMouse(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            String command = string.Empty;
            //check if there was a click
            if(InputHelper.LeftButtonClicked)
            {
                if(_ChemistryManager.selectedItem != null)
                {
                    //we have a left click, and the inventory manager has an item selected. FIgure out what we clicked on.
                    //clicked on the inventory panel?
                    UIPanel panel = _UIManager.GetPanelClicked(InputHelper.MouseScreenPos);
                    if(panel != null && panel.Name == "Inventory")
                    {
                        panel.ProcessClick(InputHelper.MouseScreenPos);
                    }
                    //check other inventory items

                    //check world objects
                    
                    //check NPCs
                    



                }
                else
                {
                    
                //first check if the click was on any of the panels edge for resizing
                UIPanel panelHit = _UIManager.CheckPanelEdgesForResize(InputHelper.MouseScreenPos);

                //check if a click was in the crafting panel
                foreach(UIPanel panel in _UIManager.ActivePanels)
                {
                    //if(BankMode)
                    //{
                    //    if (panel.Name.Equals("Bank"))
                    //    {
                    //        Enums.ItemID item = (panel as GameObjects.UIObjects.BankPanel).ProcessClick(InputHelper.MouseScreenPos);
                    //        if (item != Enums.ItemID.kItemNone)
                    //        {
                    //            _BankManager.RemoveItem(item);
                    //            _InvenManager.AddItem(item);
                    //        }
                    //    }
                    //    if (panel.Name.Equals("Inventory"))
                    //    {
                    //        Enums.ItemID item = (panel as GameObjects.UIObjects.InventoryPanel).ProcessClick(InputHelper.MouseScreenPos);
                    //        if (item != Enums.ItemID.kItemNone)
                    //        {
                    //            _InvenManager.RemoveItem(item);
                    //            _BankManager.AddItem(item);
                    //        }
                    //    }
                    //}
                    panel.ProcessClick(InputHelper.MouseScreenPos);
                }
                }

            }
            else if(InputHelper.RightButtonClicked)
            {               

                //first check if the click was on any of the panels edge for resizing
                UIPanel panelHit = _UIManager.CheckPanelEdgesForMove(InputHelper.MouseScreenPos);
                
            }

            //if (InputHelper.RightButtonPressed)
            //{
            //    //_GatherableManager.CreatePlant(GameObjects.Gatherables.Plant.PlantType.kStrawBerryType, new Vector2(0,0));
            //}

            //if (InputHelper.MouseScrolledUp)
            //{
            //    camera.Scale += 0.1f;
            //    if (camera.Scale > 2f)
            //    {
            //        camera.Scale = 2f;
            //    }
            //}
            //else if (InputHelper.MouseScrolledDown)
            //{
            //    camera.Scale -= 0.1f;
            //    if (camera.Scale < 0.6f)
            //    {
            //        camera.Scale = 0.6f;
            //    }
            //}
            

        }

        //private void ProcessCamera(GameTime gameTime)
        //{
        //    this.camera.Position = _PlayerManager._PlayerPos;

        //    camera.Update(gameTime);
        //}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Black);

            //http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
            //spriteBatch.Begin(camera);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        _TestCamera.GetTransform());

            _MapManager.Draw(spriteBatch);

            _PlayerManager.Draw(spriteBatch);

            _NPCManager.DrawNPCs(spriteBatch);

            _WorldObjectManager.Draw(spriteBatch);
            _GatherableManager.Draw(spriteBatch);

            //Vector2 invenBgpos = _UIManager.getUIElement("Inventory")._TopLeft;
            //_InvenManager.Draw(spriteBatch, invenBgpos);

            //mouseCursor.Draw(spriteBatch);
            DrawSelectRect(spriteBatch);
            //spriteBatch.DrawString(font, kbHandler.Input, camera.ToWorld(new Vector2(100, 100)), Color.Black);
            //spriteBatch.DrawString(font, player._HP.ToString(), camera.ToWorld(new Vector2(200, 200)), Color.White);
            
            base.Draw(gameTime);
            spriteBatch.End();
            spriteBatch.Begin();
            _UIManager.Draw(spriteBatch);
            spriteBatch.DrawString(font, _PlayerManager.currentInteracttext, new Vector2(100, 100), Color.White);


            var frameRate = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            spriteBatch.DrawString(font, frameRate.ToString(), new Vector2(100, 400), Color.White);
            spriteBatch.End();

        }

        private void DrawSelectRect(SpriteBatch sb)
        {
            int border = 3;
            Rectangle rect;
            if (_SelectedSprite != null)
            {
                rect = _SelectedSprite._BoundingBox;
                sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y, border, rect.Height + border), Color.White);
                sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y, rect.Width + border, border), Color.White);
                sb.Draw(_SelectTex, new Rectangle(rect.X + rect.Width, rect.Y, border, rect.Height + border), Color.White);
                sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width + border, border), Color.White);
            }

            rect = _PlayerManager.CheckRect;
            sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y, border, rect.Height + border), Color.White);
            sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y, rect.Width + border, border), Color.White);
            sb.Draw(_SelectTex, new Rectangle(rect.X + rect.Width, rect.Y, border, rect.Height + border), Color.White);
            sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width + border, border), Color.White);
        }

        private void CheckPlayerHit()
        {

        }

        private void SaveGame()
        {
            string playerPos = string.Format("{0} {1}", _PlayerManager._PlayerPos.X, _PlayerManager._PlayerPos.Y);

            List<string> bankStuff = _BankManager.getList();
            List<string> inventoryStuff = _InvenManager.getList();
            string test = Content.RootDirectory + @"\Save\save.txt";


            bool saveExist = false;
            if (System.IO.File.Exists(test))
            {
                saveExist = true;
            }
            else
            {
                System.IO.Directory.CreateDirectory(Content.RootDirectory + @"\Save\");
            }

            if(saveExist)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(test))
                {
                    file.WriteLine(playerPos);
                    file.WriteLine("B");
                    foreach (string line in bankStuff)
                    {
                        file.WriteLine(line);
                    }
                    file.WriteLine("BEnd");
                    file.WriteLine("I");
                    foreach (string line in inventoryStuff)
                    {
                        file.WriteLine(line);
                    }
                    file.WriteLine("IEnd");
                }
            }
            

            //string TestInventorySave = JsonConvert.SerializeObject(_InvenManager.itemSlots);
        }

        #region Events

        public void HandleBankOpened(object sender, EventArgs args)
        {
            BankMode = true;
            _UIManager.ShowPanel("Bank");
        }
        private void HandlePlayerMoved(object sender, EventArgs args)
        {
            BankMode = false;
            _UIManager.HidePanel("Bank");
        }

        #endregion
    }

}
