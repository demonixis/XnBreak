using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using YNA.Management;

namespace YNA.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScoreComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteFont scoreFont;
        private SpriteBatch spriteScore;
        private string fontName;
        
        private PlayerInformation playerInformation;

        /// <summary>
        /// Crée un objet d'affichage des score. Attention un spriteFont nommé scoreFont doit être présent
        /// dans le content manager
        /// </summary>
        /// <param name="game"></param>
        public ScoreComponent (Game game)
            : base (game)
        {

        }

        public ScoreComponent (Game game, string fontName, PlayerInformation playerInformation)
            : base (game)
        {
            this.playerInformation = playerInformation;
            this.fontName = fontName;
        }

        
        /// <summary>
        /// Initialisation du composant
        /// </summary>
        public override void Initialize ()
        {
            spriteScore = new SpriteBatch (Game.GraphicsDevice);
            base.Initialize ();
        }

        /// <summary>
        /// Chargement de la texture
        /// </summary>
        protected override void LoadContent ()
        {
            base.LoadContent ();
            try
            {
                scoreFont = Game.Content.Load<SpriteFont> (fontName);
            }
            catch (ContentLoadException e)
            {
                Console.Error.WriteLine (e.Message);
                Console.Error.WriteLine ("\n[ScoringComponent] ERREUR : Impossible de charger le spriteFont");
            }
        }


        public override void Update (GameTime gameTime) { base.Update (gameTime); }

        public override void Draw (GameTime gameTime)
        {
            spriteScore.Begin ();
            spriteScore.DrawString (scoreFont, playerInformation.CurrentScore + " Points", new Vector2 (Game.GraphicsDevice.Viewport.Width - 10, 10), Color.Yellow);
            spriteScore.End ();
            base.Draw (gameTime);
        }

        public void Draw (GameTime gameTime, Vector2 scorePosition, Color scoreColor)
        {
            spriteScore.Begin ();
            spriteScore.DrawString (scoreFont, playerInformation.CurrentScore + " Points", scorePosition, scoreColor);
            spriteScore.End ();
            base.Draw (gameTime);
        }
    }
}