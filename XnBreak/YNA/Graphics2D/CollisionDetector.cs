using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YNA.Graphics2D
{
    public class CollisionDetector
    {
        public static bool RectangleDetector (Sprite a, Sprite b)
        {
            return a.DestinationRectangle.Intersects (b.DestinationRectangle);
        }

        public static bool PixelsDetector (Sprite a, Sprite b)
        {
            int top = Math.Max (a.DestinationRectangle.Top, b.DestinationRectangle.Top);
            int bottom = Math.Min (a.DestinationRectangle.Bottom, b.DestinationRectangle.Bottom);
            int left = Math.Max (a.DestinationRectangle.Left, b.DestinationRectangle.Left);
            int right = Math.Min (a.DestinationRectangle.Right, b.DestinationRectangle.Right);

            for (int y = top; y < bottom; y++)  // De haut en bas
            {
                for (int x = left; x < right; x++)  // de gauche à droite
                {
                    int index_A = (x - a.DestinationRectangle.Left) + (y - a.DestinationRectangle.Top) * a.DestinationRectangle.Width;
                    int index_B = (x - b.DestinationRectangle.Left) + (y - b.DestinationRectangle.Top) * b.DestinationRectangle.Width;
                    Color colorSpriteA = a.TextureData[index_A];
                    Color colorSpriteB = b.TextureData[index_B];

                    if (colorSpriteA.A != 0 && colorSpriteB.A != 0)
                        return true;
                }
            }
            return false;
        }

        public static bool Detect (Sprite a, Sprite b)
        {
            return RectangleDetector (a, b) && PixelsDetector (a, b);
        }

    }
}
