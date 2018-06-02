using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using YNA.Graphics2D;
using YNA.Interface;

namespace XnBreak.Menu
{
    public class MenuItem : IXnMinimalStructure
    {
        private Menu _parentMenu;
        private string itemName;
        private bool selected;
        private Texture2D[] iTextures;
        private Vector2 position;

        public string Name
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle Rectangle
        {
            get 
            { 
                return new Rectangle((int)this.Position.X, 
                    (int)this.position.Y, 
                    iTextures != null ? this.iTextures[0].Width : 50, 
                    iTextures != null ? this.iTextures[0].Height : 50); 
            }
        }

        public MenuItem (Menu parentMenu)
        {
            this._parentMenu = parentMenu;
            this.selected = false;
            Initialize ();
        }

        public void Initialize ()
        {
            if (iTextures == null)
                iTextures = new Texture2D[2];
        }

        public void LoadContent (string []textureName)
        {
            if (textureName.Length == iTextures.Length)
            {
                for (int i = 0; i < iTextures.Length; i++)
                    iTextures[i] = _parentMenu.Game.Content.Load<Texture2D> (textureName[i]);
            }
            else
                throw new Exception ("[MenuItem] : Le nombre de textures fournies en paramètres doit être identiques au nombre de texture à afficher");
        }

        public void LoadContent () { }

        public void UnloadContent ()
        {
            for (int i = 0; i < iTextures.Length; i++)
                iTextures[i].Dispose ();
        }

        public void Update (GameTime gameTime)
        {

        }

        public void Draw (SpriteBatch spriteBatch)
        {
            if (selected)
                spriteBatch.Draw (iTextures[1], position, null, Color.White, 0.0f, Vector2.Zero, _parentMenu.Scale, SpriteEffects.None, 0.0f); 
            else
                spriteBatch.Draw (iTextures[0], position, null, Color.White, 0.0f, Vector2.Zero, _parentMenu.Scale, SpriteEffects.None, 0.0f); 
        }

    }
}
