using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace YNA.Interface
{
    public interface IXnStructure
    {
        void Initialize ();

        void LoadContent ();

        void LoadContent (string textureName);

        void UnloadContent ();

        void Update ();

        void Update (GameTime gameTime);

        void Draw (SpriteBatch spriteBatch);

    }
}
