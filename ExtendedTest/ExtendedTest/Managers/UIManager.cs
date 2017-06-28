using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{ 
    public class UIManager
    {
        public List<UIPanel> UIPanels;
        public List<UIPanel> ActivePanels;
        InventoryManager _invenManager;
        public UIManager(InventoryManager invenManager)
        {
            UIPanels = new List<UIPanel>();
            ActivePanels = new List<UIPanel>();
            _invenManager = invenManager;
        }

        public void Update(GameTime gameTime)
        {
            foreach(UIPanel element in UIPanels)
            {
                element.UpdateActive(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (UIPanel panel in ActivePanels)
            {
                panel.Draw(spriteBatch);
            }
        }

        public UIPanel getUIElement(String UIName)
        {
            UIPanel element = UIPanels.Find(x => x.Name == UIName);
            if(element == null)
            {
                Console.WriteLine("UI Element: " + UIName + " not found.");
                return null;
            }
            return element;
        }

        public void TogglePanel(String PanelName)
        {
            //check if the panel is already in the active list
            //if it's not add it. if it is remove it.
            UIPanel panelToFind = UIPanels.Find(x => x.Name == PanelName);
            if (ActivePanels.Contains(panelToFind))
            {
                ActivePanels.Remove(panelToFind);
                panelToFind._Showing = false;
            }
            else
            {
                ActivePanels.Add(panelToFind);
                panelToFind._Showing = true;
            }
        }

        public void ShowPanel(String PanelName)
        {
            UIPanel panelToFind = UIPanels.Find(x => x.Name == PanelName);
            if (!ActivePanels.Contains(panelToFind))
            {
                ActivePanels.Add(panelToFind);
                panelToFind._Showing = true;
            }
        }

        public void HidePanel(String PanelName)
        {
            UIPanel panelToFind = UIPanels.Find(x => x.Name == PanelName);
            if (ActivePanels.Contains(panelToFind))
            {
                ActivePanels.Remove(panelToFind);
                panelToFind._Showing = false;
            }
        }
    }
}
