using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ExtendedTest.GameObjects.UIObjects
{
    public class DialogPanel : UIPanel
    {
        Managers.DialogManager _DialogManager;
        List<string> DialogList;
        List<String> DialogOptions;
        List<Rectangle> optionsRects;
        int CurrentIndex = 0;
        Vector2 offset = new Vector2(10, 10);
        int currentStringLength = 0;

        bool showOptions = false;

        private Vector2 _characterSize
        {
            get
            {
                return count.MeasureString(new StringBuilder("W", 1));
            }
        } 

        public DialogPanel(Managers.DialogManager dm)
        {
            DialogList = new List<string>();
            DialogOptions = new List<string>();
            optionsRects = new List<Rectangle>();
            _Resizable = false;
            adjustedHeight = 100;
            adJustedWidth = 450;
            _DialogManager = dm;
            _DialogManager.DialogPlayed += HandleDialogPlayed;
        }

        protected override void UpdateActive(GameTime gt)
        {
            base.UpdateActive(gt);
            
            if(_BoundingBox.Contains(InputHelper.MouseScreenPos))
            {
                if(InputHelper.LeftButtonClicked)
                {
                    CurrentIndex++;
                    if(CurrentIndex >= DialogList.Count)
                    {
                        _UIManager.HidePanel(this);
                        CurrentIndex = 0;
                    }
                }
                else if(InputHelper.RightButtonClicked)
                {
                    CurrentIndex--;
                    if(CurrentIndex < 0)
                    {
                        CurrentIndex = 0;
                    }
                }
                else if(InputHelper.MiddleButtonClicked)
                {
                    Console.WriteLine(adJustedWidth + " " + adjustedHeight);
                }

            }

            if (CurrentIndex == DialogList.Count - 1) //last one?
            {
                showOptions = true;
            }
            else
            {
                showOptions = false;
            }

            if (showOptions)
            {
                int optionPicked = 0;
                foreach (Rectangle rect in optionsRects)
                {
                    if(rect.Contains(InputHelper.MouseScreenPos) && InputHelper.LeftButtonClicked)
                    {
                        showOptions = false;
                        CurrentIndex = 0;
                        //_UIManager.HidePanel(this);
                        _DialogManager.PlayMessage(_DialogManager.CurrentDialog.options[optionPicked]);
                        break;
                    }
                    optionPicked++;
                }
            }
        }

        public void HandleDialogPlayed(object sender, EventArgs args)
        {
            DialogList.Clear();
            optionsRects.Clear();
            DialogOptions.Clear();
            CurrentIndex = 0;
            if(_DialogManager.CurrentDialog == null)
            {
                _UIManager.HidePanel(this);
                return;
            }
            foreach(string msg in _DialogManager.CurrentDialog.textList)
            {
                DialogList.Add(msg);
            }
            if(_DialogManager.CurrentDialog.options.Count > 0)
            {
                int rectsMade = 0;
                foreach(Managers.DialogOption option in _DialogManager.CurrentDialog.options)
                {
                    DialogOptions.Add(option.optiontext);
                    Rectangle optionrect = new Rectangle(this._BoundingBox.Left, this._BoundingBox.Bottom + ((count.LineSpacing + 5) * rectsMade), this.adJustedWidth, 20);
                    optionsRects.Add(optionrect);
                    rectsMade++;
                }
            }
            _UIManager.ShowPanel(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DialogList.Count <= 0) return;
            // https://github.com/Andrux51/MonoGame-Tutorial-DialogBox/blob/master/MonoGame-Tutorial-DialogBox/DialogBox.cs
            base.Draw(spriteBatch);
            Vector2 drawPos = new Vector2(_Position.X + offset.X, _Position.Y + offset.Y);
            spriteBatch.DrawString(count, DialogList[CurrentIndex], drawPos, Color.White);

            if(showOptions)
            {
                int optionCount = 0;
                foreach(Rectangle rect in optionsRects)
                {
                    spriteBatch.Draw(_Texture, rect, Color.White);
                    spriteBatch.DrawString(count, DialogOptions[optionCount], new Vector2(rect.Left + 3, rect.Top + 3), Color.White);
                    optionCount++;
                }
            }
        }


    }

    class WordInfo
    {
        int wordLength = 0;
        StringBuilder word;
        public WordInfo(String w)
        {
            word = new StringBuilder(w);
        }
    }
}
