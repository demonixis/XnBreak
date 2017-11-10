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
    public enum BrickType
    {
        Blue, Cyan, Green, Orange, Pink, Purple, Red, Yellow
    }

    public class Brick : Sprite
    {
        public event EventHandler<BrickDestroyedEventArgs> brickDestroyed;

        private Level _level;
        private int score;

        public int Score
        {
            get { return score; }
            protected set { score = value; }
        }

        public Brick (Level level, BrickType type)
            : base (level.Game, level.Graphics)
        {
            this._level = level;
            switch (type)
            {
                case BrickType.Blue: 
                    this.textureName = "briques/blue";
                    score = 10;
                    break;

                case BrickType.Cyan:
                    this.textureName = "briques/cyan";
                    score = 14;
                    break;

                case BrickType.Green: 
                    this.textureName = "briques/green";
                    score = 8;
                    break;

                case BrickType.Orange:
                    this.textureName = "briques/orange";
                    score = 12;
                    break;

                case BrickType.Pink:
                    this.textureName = "briques/pink";
                    score = 5;
                    break;

                case BrickType.Purple:
                    this.textureName = "briques/purple";
                    score = 6;
                    break;

                case BrickType.Red:
                    this.textureName = "briques/red";
                    score = 14;
                    break;

                case BrickType.Yellow: 
                    this.textureName = "briques/yellow";
                    score = 3;
                    break;

                default: break;
            }
            LoadContent ();
        }

        public void HideBrick ()
        {
            position = new Vector2 (-5, -5);
            destinationRectangle = Rectangle.Empty;
        }

        public override void Update (GameTime gameTime)
        {
            if (!show)
                HideBrick ();
            else
                base.Update (gameTime);
        }

        public void OnBrickDestroyed (BrickDestroyedEventArgs e)
        {
            brickDestroyed (this, e);
        }
    }
}
