using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.Managers
{
    public class UIManager
    {
        public List<UIPanel> UIPanels;
        public List<UIPanel> ActivePanels;
        public List<UIPanel> PanelsToRemove;
        public List<UIPanel> PanelsToAdd;

        public UIManager()
        {
            UIPanels = new List<UIPanel>();
            ActivePanels = new List<UIPanel>();
            PanelsToRemove = new List<UIPanel>();
            PanelsToAdd = new List<UIPanel>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (UIPanel panel in ActivePanels)
            {
                panel.Update(gameTime);
                panel._Position = panel._InitialPos;
            }

            foreach(UIPanel panel in PanelsToAdd)
            {
                ReallyShowPanel(panel);
            }

            foreach (UIPanel panel in PanelsToRemove)
            {
                ReallyHidePanel(panel);
            }
            PanelsToRemove.Clear();
            PanelsToAdd.Clear();
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
            if (element == null)
            {
                Console.WriteLine("UI Element: " + UIName + " not found.");
                return null;
            }
            return element;
        }

        public void HideAll()
        {
            foreach (UIPanel panel in ActivePanels)
            {
                PanelsToRemove.Add(panel);
            }
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
            PanelsToAdd.Add(panelToFind);
        }

        public void ShowPanel(UIPanel panel)
        {
            PanelsToAdd.Add(panel);
        }

        private void ReallyShowPanel(UIPanel panel)
        {
            if (!ActivePanels.Contains(panel))
            {
                ActivePanels.Add(panel);
                panel._Showing = true;
            }
        }

        public void HidePanel(String PanelName)
        {
            UIPanel panelToFind = UIPanels.Find(x => x.Name == PanelName);
            PanelsToRemove.Add(panelToFind);
        }

        public void HidePanel(UIPanel panel)
        {
            PanelsToRemove.Add(panel);
        }

        private void ReallyHidePanel(UIPanel panel)
        {
            if (ActivePanels.Contains(panel))
            {
                ActivePanels.Remove(panel);
                panel._Showing = false;
            }
        }

        internal UIPanel CheckPanelEdgesForResize(Vector2 mouseClickpos)
        {
            foreach(UIPanel panel in ActivePanels.Where(x=>x._Resizable == true))
            {
                if (panel.CheckForResize(mouseClickpos)) return panel;

            }
            return null;
        }

        internal UIPanel CheckPanelEdgesForMove(Vector2 mouseClickpos)
        {
            foreach (UIPanel panel in ActivePanels.Where(x => x._Resizable == true))
            {
                if (panel.CheckForMove(mouseClickpos)) return panel;

            }
            return null;
        }
    }
}
