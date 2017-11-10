﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YNA.Interface
{
    public interface IXnMinimalStructure
    {
        void Initialize ();

        void LoadContent ();

        void UnloadContent ();

        void Update (GameTime gameTime);

        void Draw (SpriteBatch spriteBatch);
    }
}
