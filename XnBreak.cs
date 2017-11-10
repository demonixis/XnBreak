using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnBreak.Menu;
using XnBreak.Menu.Event;
using XnBreak.XnLevel;
using XnBreak.XnLevel.Event;

namespace XnBreak
{
    public class XnBreak : Microsoft.Xna.Framework.Game
    {
#if WINDOWS_PHONE
        const string xnVersion = "1.2 for Windows Phone 7";
#else
        const string xnVersion = "1.2 for Windows";
#endif

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        bool menuStatus = true;
        bool levelStatus = false;

        MenuMain _menu;
        Level _level;


        public XnBreak ()
        {
            graphics = new GraphicsDeviceManager (this);

#if WINDOWS_PHONE
            //graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
#else
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
#endif
            graphics.SynchronizeWithVerticalRetrace = true;
            Content.RootDirectory = "Content";
            this.Window.Title = "XnBreak ! " + xnVersion;
        }

        protected override void Initialize ()
        {
            _menu = new MenuMain (this, graphics, "menu/background");
            _menu.RunGame += this.GameLaunchReady;
            _menu.QuitGame += this.GameQuitRequest;
            _menu.Initialize ();
            _level = new Level (this, graphics);
            _level.levelIsFinish += this.LevelFinish;
            base.Initialize ();
        }

        protected override void LoadContent ()
        {
            spriteBatch = new SpriteBatch (GraphicsDevice);
            _menu.LoadContent ();
            _level.LoadContent ("jeu/Level_one");
        }


        protected override void UnloadContent ()
        {
            _level.UnloadContent ();
        }

        protected override void Update (GameTime gameTime)
        {
            if (Keyboard.GetState ().IsKeyDown (Keys.F5))
                graphics.ToggleFullScreen ();

            if (menuStatus)
                _menu.Update (gameTime);
            else if (levelStatus)
                _level.Update (gameTime);

            base.Update (gameTime);
        }

        protected override void Draw (GameTime gameTime)
        {
            GraphicsDevice.Clear (Color.CornflowerBlue);

            spriteBatch.Begin ();
            if (menuStatus)
                _menu.Draw (spriteBatch);
            if (levelStatus)
                _level.Draw (spriteBatch);
            spriteBatch.End ();

            base.Draw (gameTime);
        }

        #region Evénements traités 
        /// <summary>
        /// Appelée à lorsque l'item menu jeu est selectionné
        /// Initialise le niveau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLaunchReady (object sender, MenuPlaySelectedEventArgs e)
        {
            menuStatus = false;
            _level.Initialize ();
            levelStatus = true;
        }

        private void GameQuitRequest (object sender, MenuQuitSelectedEventArgs e)
        {
            this.Exit ();
        }

        /// <summary>
        /// Appelée lorsque le niveau est terminé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevelFinish (object sender, LevelFinishEventArgs e)
        {
            menuStatus = true;
            levelStatus = false;
        }
        #endregion
    }
}
