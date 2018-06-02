using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using YNA.Interface;
using YNA.Management;
using YNA.Input;

using XnBreak.Menu.Event;

namespace XnBreak.Menu
{
    public class MenuMain : Menu
    {
        public event EventHandler<MenuPlaySelectedEventArgs> RunGame;
        public event EventHandler<MenuQuitSelectedEventArgs> QuitGame;

        private Dictionary<string, MenuItem> items;
        private List<MenuItem> menuItems;
        private int histoiry;

        private int touchId;
        private TouchCollection touchPoints;

        public MenuMain (Game game, GraphicsDeviceManager graphics, string textureName) 
            : base (game, graphics, textureName) 
        {
            items = new Dictionary<string, MenuItem> (2);
            histoiry = 0;
        }

        public override void Initialize ()
        {
            base.Initialize ();
            items = new Dictionary<string, MenuItem> (2);
            items["play"] = new MenuItem (this);
            items["play"].Name = "play";
            items["play"].Position = new Vector2 (315 * scale.X, 400 * scale.Y);
            items["play"].Selected = true;
            items["quit"] = new MenuItem (this);
            items["quit"].Name = "quit";
            items["quit"].Position = new Vector2 (315 * scale.X, 500 * scale.Y);

            menuItems = new List<MenuItem>();
            for (int i = 0; i < 2; i++)
                menuItems.Add(new MenuItem(this));

            histoiry = 0; // Historique et état des items en cours
            
        }

        public override void LoadContent ()
        {
            base.LoadContent ();
            items["play"].LoadContent (new string[] { "menu/play_normal", "menu/play_shine" });
            items["quit"].LoadContent (new string[] { "menu/quit_normal", "menu/quit_shine" });
        }

        public override void UnloadContent ()
        {
            base.UnloadContent ();

            foreach (MenuItem i in items.Values)
                i.UnloadContent ();
            items.Clear ();
            items = null;
        }

        public override void Update (GameTime gameTime)
        {
            base.Update (gameTime);
            

#if WINDOWS_PHONE

            touchPoints = TouchPanel.GetState();

            foreach (var touchPoint in touchPoints)
            {
                switch (touchPoint.State)
                {
                    case TouchLocationState.Pressed:
                        foreach (MenuItem item in items.Values)
                        {
                            if (item.Rectangle.Contains((int)touchPoint.Position.X, (int)touchPoint.Position.Y))
                            {
                                item.Selected = true;
                                if (item.Name == "play")
                                    OnRunGame (new MenuPlaySelectedEventArgs ());
                                else if (item.Name == "quit")
                                    OnQuitGame(new MenuQuitSelectedEventArgs());
                            }
                        }
                        break;
                    case TouchLocationState.Moved:
                        foreach (MenuItem item in menuItems)
                        {
                            if (item.Rectangle.Contains((int)touchPoint.Position.X, (int)touchPoint.Position.Y))
                            {
                                items["play"].Selected = !items["play"].Selected;
                                items["quit"].Selected = !items["quit"].Selected;
                                   
                            }
                        }
                        break;
                    case TouchLocationState.Released:
                        break;
                    case TouchLocationState.Invalid:
                        break;
                    default:
                        break;
                }
            }
#endif

            if (input.GetOnePressedKey (Keys.Up) || input.GetOnePressedKey (Keys.Down))
            {
                items["play"].Selected = !items["play"].Selected;
                items["quit"].Selected = !items["quit"].Selected;

            }

            if (input.GetPressedKey (Keys.Enter) && items["play"].Selected)
            {
                OnRunGame (new MenuPlaySelectedEventArgs ());
            }
            else if (input.GetPressedKey (Keys.Enter) && items["quit"].Selected)
            {
                OnQuitGame (new MenuQuitSelectedEventArgs ());
            }


        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            base.Draw (spriteBatch);
            items["play"].Draw (spriteBatch);
            items["quit"].Draw (spriteBatch);
        }

        private void OnRunGame(MenuPlaySelectedEventArgs e)
        {
            RunGame(this, e);
        }

        private void OnQuitGame (MenuQuitSelectedEventArgs e)
        {
            QuitGame (this, e);
        }
    }
}
