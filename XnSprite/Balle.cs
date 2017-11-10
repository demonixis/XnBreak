using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using YNA.Graphics2D;

using XnBreak.XnLevel;
using XnBreak.XnSprite.Event;

namespace XnBreak.XnSprite
{
    public class Balle : Sprite
    {
        public event EventHandler<BalleMissedEventArgs> balleMissed ;

        private float speed;
        private Vector2 direction;
        private bool moving;
        private Level _level;



        public bool Moving
        {
            get { return moving; }
            set { moving = value; }
        }

        public Balle (Level level)
            : base (level.Game, level.Graphics)
        {
            this.textureName = "mobile/balle";
            this._level = level;
            LoadContent ();
        }

        public override void Initialize ()
        {
            this.moving = false;
            this.speed = 4.0f;
            this.direction = new Vector2 (1.0f, -1.0f);
#if WINDOWS_PHONE
            this.position = new Vector2(
                _level.PlayableSurface.Width / 2 - ScaledTextureWidth / 2,
                (_level.PlayableSurface.Height - 40));
#else
            this.position = new Vector2 (
                _level.PlayableSurface.Width / 2 - ScaledTextureWidth / 2,
                _level.PlayableSurface.Height - 50 * scale.Y);
#endif
        }

        public void Update (GameTime gameTime, List<Sprite> sprites)
        {
            Update (gameTime);

            if (moving)
            {
                for (int i = 0; i < sprites.Count; i++)
                {
                    if (sprites[i] is Palet)
                    {
                        if (CollisionDetector.RectangleDetector (this, sprites[i]))
                            direction.Y *= -1.0f;
                    }
                    else if (sprites[i] is Brick)
                    {
                        Brick b = (Brick)sprites[i];
                        if (CollisionDetector.RectangleDetector (this, sprites[i]))
                        {
                            direction *= -1.0f;
                            b.Show = false;
                            b.DestinationRectangle = Rectangle.Empty;
                            b.OnBrickDestroyed (new BrickDestroyedEventArgs (b, i));
                        }
                    }
                }

                // Collision avec les murs droite et gauche
                if (position.X < _level.PlayableSurface.X || position.X + ScaledTextureWidth >= _level.PlayableSurface.Width + _level.PlayableSurface.X)
                {
                    direction.X *= -1;
                }
                // Collision avec le mur du haut
                else if (position.Y <= _level.PlayableSurface.Y)
                {
                    direction.Y *= -1;
                }
                // Collision avec le mur du bas : Perte d'une vie
                else if (position.Y + ScaledTextureHeight >= _level.PlayableSurface.Height + _level.PlayableSurface.Y)
                {
                    OnBalleMissed (new BalleMissedEventArgs (this));
                }
                    

                position += direction * speed;
            }
            else
            {
                position.X = (sprites[0].Position.X + sprites[0].ScaledTextureWidth / 2) - (ScaledTextureWidth / 2);
            }
        }

        private void OnBalleMissed (BalleMissedEventArgs e)
        {
            balleMissed (this, e);
        }
    }
}
