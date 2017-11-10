using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using YNA.Interface;
using YNA.Management;
using YNA.Input;

namespace XnBreak.Menu
{
    public class Menu : IXnMinimalStructure
    {
        protected Game game;
        protected GraphicsDeviceManager graphics;

        protected int screenWidth;
        protected int screenHeight;

        protected Texture2D background;
        protected string textureName;

        protected Vector2 scale;

        protected InputManager input;

        private KeyboardState currentState;
        private KeyboardState lastState;

        public Game Game { get { return game; } }
        public GraphicsDeviceManager Graphics { get { return graphics; } }

        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Menu (Game game, GraphicsDeviceManager graphics, string textureName)
        {
            this.game = game;
            this.graphics = graphics;
            this.textureName = textureName;
            this.screenWidth = graphics.GraphicsDevice.Viewport.Width;
            this.screenHeight = graphics.GraphicsDevice.Viewport.Height;
            this.scale = new Vector2 (
                (float)screenWidth / 1280.0f,
                (float)screenHeight / 800.0f);
        }

        public virtual void Initialize ()
        {
            input = InputManager.GetInstance ();
        }

        public virtual void LoadContent ()
        {
            if (textureName != string.Empty)
                background = game.Content.Load<Texture2D> (textureName);
            else
                throw new Exception ("[Menu] La texture n'existe pas");
        }


        public virtual void UnloadContent ()
        {
            background.Dispose ();
        }

        public virtual void Update (GameTime gameTime)
        {
            lastState = currentState;
            currentState = Keyboard.GetState ();
            //input.Update (currentState, lastState);
            input.Update (currentState);
        }

        public virtual void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw (background, new Rectangle (0, 0, screenWidth, screenHeight), Color.White);
        }
    }
}
