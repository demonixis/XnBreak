using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YNA.Components
{

    public class FpsComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private string fontName;
        private int frameRate;
        private int frameCounter;
        private TimeSpan elapsedTime;

        public FpsComponent (Game game, string fontName)
            : base (game)
        {
            this.fontName = fontName;
        }

        public override void Initialize ()
        {
            frameRate = 0;
            frameCounter = 0;
            elapsedTime = TimeSpan.Zero;

            spriteBatch = new SpriteBatch (Game.GraphicsDevice);
            spriteFont = Game.Content.Load<SpriteFont> (fontName);

            base.Initialize ();
        }

        public override void Update (GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds (1.0f))          // Si plus d'une seconde c'est passé
            {
                elapsedTime -= TimeSpan.FromSeconds (1.0f);         // On garde le temps supplémentaire
                frameRate = frameCounter;                           // frameRate contient le nombre de FPS
                frameCounter = 0;                                   // On remet le compteur à zéro
            }

            base.Update (gameTime);
        }

        public override void Draw (GameTime gameTime)
        {
            frameCounter++;

            spriteBatch.Begin ();
            spriteBatch.DrawString (spriteFont, String.Format ("{0} Fps", frameRate.ToString ()), new Vector2 (10, 10), Color.YellowGreen);
            spriteBatch.End ();

            base.Draw (gameTime);
        }


    }
}