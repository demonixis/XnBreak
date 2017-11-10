using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using YNA.Interface;
using YNA.Management;

using XnBreak.XnLevel;

namespace XnBreak.XnLevel
{
    public class LevelHUD : IXnMinimalStructure
    {
        private Game game;
        private GraphicsDeviceManager graphics;
        private PlayerInformation informations;
        private Level _level;

        private SpriteFont spriteFont;

        private string score = "";
        private string time = "";
        private TimeSpan timeSpan;
        private string live = "";

        private Vector2 scorePosition;
        private Vector2 timePosition;
        private Vector2 livePosition;


        public LevelHUD (Level level, PlayerInformation informations)
        {
            this._level = level;
            this.game = _level.Game;
            this.graphics = _level.Graphics;
            this.informations = informations;
            LoadContent ();
        }

        public void Initialize () 
        {
            timeSpan = TimeSpan.Zero;
            scorePosition = new Vector2 (_level.ScreenWidth - 170 * _level.SpriteScale.X, 100 * _level.SpriteScale.Y);
            timePosition = new Vector2 (_level.ScreenWidth - 200 * _level.SpriteScale.X, 220 * _level.SpriteScale.Y);
            livePosition = new Vector2 (_level.ScreenWidth - 170 * _level.SpriteScale.X, 360 * _level.SpriteScale.Y);
        }

        public void LoadContent ()
        {
            spriteFont = game.Content.Load<SpriteFont> ("font/score");
        }

        public void UnloadContent ()
        {
            spriteFont = null;
        }

        public void Update (GameTime gameTime)
        {
            timeSpan += gameTime.ElapsedGameTime;

            score = informations.CurrentScore.ToString ();
            time = string.Format ("{0}m{1}s",
                timeSpan.Minutes.ToString(), timeSpan.Seconds.ToString());
            live = informations.Live.ToString ();
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString (spriteFont, score, scorePosition, Color.Yellow, 0.0f, Vector2.Zero, _level.SpriteScale, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString (spriteFont, time, timePosition, Color.Yellow, 0.0f, Vector2.Zero, _level.SpriteScale, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString (spriteFont, live, livePosition, Color.Yellow, 0.0f, Vector2.Zero, _level.SpriteScale, SpriteEffects.None, 0.0f);
        }
    }
}
