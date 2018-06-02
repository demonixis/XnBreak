using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using YNA.Graphics2D;
using YNA.Input;

using XnBreak.XnLevel;

namespace XnBreak.XnSprite
{
    public class Palet : Sprite
    {
        private InputManager input;
        private Level _level;

        private int touchId;
        private TouchCollection touchPoints;

        private float speed;

        public Palet (Level level)
            : base (level.Game, level.Graphics)
        {
            this.textureName = "mobile/joueur";
            this._level = level;
            input = InputManager.GetInstance ();
            LoadContent ();
        }

        public override void Initialize ()
        {
            position = new Vector2 (
                (_level.PlayableSurface.X + _level.PlayableSurface.Width) / 2 - ScaledTextureWidth / 2,
                (_level.PlayableSurface.Y + _level.PlayableSurface.Height) - ScaledTextureHeight - 10);
            speed = 5.0f;
        }

        public override void Update (GameTime gameTime)
        {
            base.Update (gameTime);

            

#if WINDOWS_PHONE
            touchPoints = TouchPanel.GetState();

            foreach (var points in touchPoints)
            {
                

                switch (points.State)
                {
                    case TouchLocationState.Pressed:
                        touchId = points.Id;
                        break;
                    case TouchLocationState.Moved:
                        if (points.Position.X < position.X + ScaledTextureWidth / 2 && position.X > _level.PlayableSurface.X && touchId == points.Id)
                        {
                            position = new Vector2(position.X - speed, position.Y);
                        }
                        else if (points.Position.X > position.X + ScaledTextureWidth / 2 && position.X + ScaledTextureWidth < _level.PlayableSurface.Width + _level.PlayableSurface.X && touchId == points.Id)
                        {
                            position = new Vector2(position.X + speed, position.Y);
                        }

                        touchId = points.Id;
                        break;
                    case TouchLocationState.Released:
                        touchId = 0;
                        break;
                    case TouchLocationState.Invalid:
                        break;
                    default:
                        break;
                }

                
            }
#else
            input.Update (Keyboard.GetState());

            if (input.GetPressedKey (Keys.Left) && position.X > _level.PlayableSurface.X)
            {
                position = new Vector2 (position.X - speed, position.Y);
            }

            if (input.GetPressedKey (Keys.Right) && position.X + ScaledTextureWidth < _level.PlayableSurface.Width + _level.PlayableSurface.X)
            {
                position = new Vector2 (position.X + speed, position.Y);
            }
#endif

        }
    }
}
