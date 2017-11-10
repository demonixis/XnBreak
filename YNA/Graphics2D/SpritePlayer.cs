using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace YNA.Graphics2D
{
    public class SpritePlayer : Sprite
    {
        private float currentFrame;
        private int maxFrame;
        private bool isMoving;

        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }

        // TODO créer une structure avec tout ce bordel ;-D ca sera moins merdique à passer en paramètre
        public SpritePlayer (SpriteConfiguration spriteConfiguration, int maxFrame)
            : base (ref spriteConfiguration)
        {
            this.currentFrame = 0;
            this.maxFrame = maxFrame;
            this.isMoving = false;
        }

        public override void Update (GameTime gameTime)
        {
            if (maxFrame != 0)
            {
                currentFrame += gameTime.ElapsedGameTime.Milliseconds * 0.01f;

                if (currentFrame > maxFrame)
                    currentFrame = 0;

                sourceRectangle = new Rectangle (
                    (int)currentFrame * sourceRectangle.Value.Width, 
                    sourceRectangle.Value.Y, 
                    sourceRectangle.Value.Width, 
                    sourceRectangle.Value.Height);
            }
            
        }
    }
}
