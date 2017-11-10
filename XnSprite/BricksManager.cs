using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using XnBreak.XnLevel;

namespace XnBreak.XnSprite.Manager
{
    public class BricksManager
    {
        private Game game;
        private GraphicsDeviceManager graphics;

        private float padding = 1.2f;   // Espacement entre les briques

        private List<Brick> allBrick;   // Collection de briques

        private Level _level;           // Niveau parent

        /// <summary>
        /// Retourne la collection de briques
        /// </summary>
        public List<Brick> ListBricks
        {
            get { return allBrick; }
        }

        public BricksManager (Level level)
        {
            this._level = level;
            this.game = _level.Game;
            this.graphics = _level.Graphics;
            this.allBrick = new List<Brick> ();
            this.padding = 1.2f * _level.SpriteScale.X; // Prise en compte de l'échelle du niveau
        }

        /// <summary>
        /// Création d'une ligne de briques. Les coordonnées sont des données relatives
        /// </summary>
        /// <param name="type">type de la brique</param>
        /// <param name="x">position de départ sur X du mur /!\ relative /!\</param>
        /// <param name="y">position de depart sur Y du mur /!\ relatif /!\</param>
        /// <param name="length">taille du mur, maximum 8</param>
        private void CreateLine (BrickType type, float x, float y, int length)
        {
            int size = length;
            if (size > 8)
                size = 8;

            Brick[] bricks = new Brick[size];
          
            for (int i = 0; i < size; i++)
            {
                bricks[i] = new Brick (_level, type);
                
                bricks[i].Position = new Vector2 (
                    (1 + _level.PlayableSurface.X * x + (i * bricks[i].ScaledTextureWidth * padding)),
                    (1 + _level.PlayableSurface.Y + (y * bricks[i].ScaledTextureHeight * padding)));

                allBrick.Add (bricks[i]);
            }
        }

        /// <summary>
        /// Création des lignes de briques
        /// Coordonnées relative
        /// </summary>
        public void Initialize ()
        {
            CreateLine (BrickType.Purple, 2.5f, 0.5f, 7);
            CreateLine (BrickType.Blue, 2.5f, 1.5f, 7);
            CreateLine (BrickType.Green, 2.5f, 2.5f, 7);
            CreateLine (BrickType.Orange, 2.5f, 3.5f, 7);
            CreateLine (BrickType.Yellow, 2.5f, 4.5f, 7);
            CreateLine (BrickType.Red, 2.5f, 5.5f, 7);
            CreateLine (BrickType.Pink, 2.5f, 6.5f, 7);
            CreateLine (BrickType.Cyan, 2.5f, 7.5f, 7);
        }

        public void Update (GameTime gameTime)
        {
            foreach (Brick b in allBrick)
                b.Update (gameTime);
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (Brick b in allBrick)
                b.Draw (spriteBatch);
        }
    }
}
