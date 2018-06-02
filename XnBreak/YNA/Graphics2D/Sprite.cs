using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace YNA.Graphics2D
{
    public struct SpriteConfiguration
    {
        public Game game;
        public GraphicsDeviceManager graphics;
        public ContentManager content;
        public string name;
        public string textureName;
        public Vector2 position;
        public Rectangle? sourceRectangle;
        public Color color;
        public float rotation;
        public Vector2 origin;
        public Vector2 scale;
        public SpriteEffects effect;
        public float layerDepth;
        public bool show;
    }

    public class Sprite
    {
        #region Definiation des variables/objets de travail
        // Compte le nombre de sprites 
        protected static int spriteCount = 0;
           
        // Membres classe game
        protected Game game;
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;

        // Membres propres aux sprites
        protected string name;                          // Nom du sprite
        protected string textureName;                   // Nom de la texture du sprite
        protected Texture2D texture;                    // Texture du sprite
        protected Vector2 position;                     // Position du sprite
        protected Rectangle destinationRectangle;       // Rectangle du sprite
        protected Rectangle? sourceRectangle;           // Rectangle de séléction
        protected Color color;                          // Couleur appliquée sur le sprite
        protected float rotation;                       // Rotation du sprite
        protected Vector2 origin;                       // Point d'origine du sprite
        protected Vector2 scale;                        // Taille (echelle) du sprite
        protected SpriteEffects effect;                 // Effet appliqué au sprite
        protected float layerDepth;                     // Prondeur d'affichage du sprite (entre 0.0f et 1.0f)

        protected float width;                          // Largeur d'une tuile de sprite
        protected float height;                         // Hauteur d'une tuile de sprite
        protected bool show;                            // Indique si le sprite est visible ou pas

        protected int screenWidth;                      // Largeur du viewport en cours
        protected int screenHeight;                     // Hauteur du viewport en cours
        #endregion

        #region Setter/Getter
        /// <summary>
        /// Retourne le nombre de sprite crées
        /// </summary>
        public static int SpriteCount
        {
            get { return spriteCount; }
        }

        /// <summary>
        /// Retourne ou change le nom du sprite
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Retourne ou change le nom de la texture
        /// </summary>
        public string TextureName
        {
            get { return textureName; }
            set { textureName = value; }
        }

        /// <summary>
        /// Retourne l'objet Texture2D qui permet de récupérer les informations sur 
        /// - la taille
        /// - La carte de couleur
        /// - Etc..
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Retourne la largeur à l'écran de la texture
        /// Utilisé dans les cas de redimension de texture avec la propriété scale
        /// le resultat renvoyé est : texture.Witdh * scale.X
        /// </summary>
        public float ScaledTextureWidth
        {
            get { return texture.Width * scale.X; }
        }

        /// <summary>
        /// Retourne la hauteur à l'écran de la texture
        /// Utilisé dans les cas de redimension de texture avec la propriété scale
        /// le resultat renvoyé est : texture.Height * scale.Y
        /// </summary>
        public float ScaledTextureHeight
        {
            get { return texture.Height * scale.Y; }
        }

        /// <summary>
        /// Retourne ou change la position courante du sprite
        /// Par defaut : Vector.Zero
        /// Ou : Défini lors de la construction de l'objet
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Retourne ou change la valeur du rectangle englobant le sprite
        /// Initialisé lors du chargement de la texture via la méthode LoadContent()
        /// Par defaut : taille du sprite
        /// </summary>
        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }

        public Rectangle ScaledDestinationRectangle
        {
            get
            {
                return new Rectangle (
                    (int)position.X, (int)position.Y,
                    (int)ScaledTextureWidth, (int)ScaledTextureHeight);
            }
        }

        /// <summary>
        /// Retourne ou change la valeur du rectangle source qui permet de selectionner une partie du sprite à afficher
        /// Par defaut : null
        /// </summary>
        public Rectangle? SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }

        /// <summary>
        /// Retourne ou change la valeur de la couleur appliqué au sprite lors du rendu
        /// Par defaut : Color.White
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Retourne ou change la valeur de rotation du sprite
        /// Attention : Valeur en radian
        /// Valeurs : MathHelper.Pi/PiOver2/PiOver4 et valeur négatives
        /// Par defaut : 0.0f (pas de rotation)
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// Retourne ou change le point d'origine du sprite
        /// Par defaut : Vector.Zero (coin haut gauche)
        /// </summary>
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// Retourne ou change le degré de rotation du sprite
        /// Par defaut : Vector.One (taille normale)
        /// </summary>
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Retourne ou change l'effet utilisé par le sprite en cours
        /// Par defaut : SpriteEffects.None
        /// </summary>
        public SpriteEffects Effect
        {
            get { return effect; }
            set { effect = value; }
        }

        /// <summary>
        /// Retourne la valeur de profondeur du sprite
        /// Une valeur de 0.0f affichera le sprite au premier plan
        /// Une valeur de 1.0f affichera le sprite au dernier plan
        /// Cette méthode est protégée contre les dépassements
        /// Les valeurs autorisées sont 0.0f à 1.0f
        /// </summary>
        public float LayerDepht
        {
            get { return layerDepth; }
            set
            {
                if (value < 0.0f) 
                    layerDepth = 0.0f;
                else if (value > 1.0f) 
                    layerDepth = 1.0f;
                else 
                    layerDepth = value;
            }
        }

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public bool Show
        {
            get { return show; }
            set { show = value; }
        }

        /// <summary>
        /// Retourne un tableau Color contenant des informations sur les couleurs de chaque pixel de la texture.
        /// </summary>
        public Color[] TextureData
        {
            get
            {
                Color[] textureData = new Color[texture.Width * texture.Height];
                texture.GetData (textureData);
                return textureData;
            }
        }

        #endregion

        /// <summary>
        /// Construit un objet de type Sprite avec tous ses attributs par defaut
        /// Attention : Une texture defaut.{png|jpeg|bmp} doit être présente dans le content manager
        /// </summary>
        /// <param name="game">L'objet Game</param>
        /// <param name="graphics">Le gestionnaire d'affichage</param>
        /// <param name="content">Le content manager</param>
        public Sprite (Game game, GraphicsDeviceManager graphics)  
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            // --- Initialisation par defaut --- //
            this.position = Vector2.Zero;
            this.name = "Sprite " + spriteCount;
            this.textureName = "defaut";
            this.sourceRectangle = null;
            this.Color = Color.White;
            this.rotation = 0.0f;
            this.origin = Vector2.Zero;
            this.scale = Vector2.One;
            this.effect = SpriteEffects.None;
            this.layerDepth = 0.0f;
            this.show = true;
            this.screenWidth = graphics.GraphicsDevice.Viewport.Width;
            this.screenHeight = graphics.GraphicsDevice.Viewport.Height;
            spriteCount++;   
        }

        public Sprite (Game game, GraphicsDeviceManager graphics, Vector2 position, string textureName)
            : this(game, graphics)
        {
            this.position = position;
            this.textureName = textureName;
        }

        public Sprite (Game game, GraphicsDeviceManager graphics, Vector2 position, string name, string textureName, Rectangle? sourceRectangle,
            Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, float layerDepth)
        {
            this.game = game;
            this.graphics = graphics;
            this.position = position;
            this.name = name;
            this.textureName = textureName;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.rotation = rotation;
            this.origin = origin;
            this.scale = scale;
            this.effect = effect;
            this.layerDepth = layerDepth;
            this.show = true;
            this.screenWidth = graphics.GraphicsDevice.Viewport.Width;
            this.screenHeight = graphics.GraphicsDevice.Viewport.Height;
            spriteCount++;
        }

        /// <summary>
        /// Construit un objet de type Sprite personnalisé.
        /// </summary>
        /// <param name="spriteConfiguration">Structure de configuration du sprite, par référence</param>
        public Sprite (ref SpriteConfiguration spriteConfiguration)
        {
            this.game = spriteConfiguration.game;
            this.graphics = spriteConfiguration.graphics;
            this.content = spriteConfiguration.content;
            this.name = spriteConfiguration.name;
            this.textureName = spriteConfiguration.textureName;
            this.position = spriteConfiguration.position;
            this.sourceRectangle = spriteConfiguration.sourceRectangle;
            this.color = spriteConfiguration.color;
            this.rotation = spriteConfiguration.rotation;
            this.origin = spriteConfiguration.origin;
            this.scale = spriteConfiguration.scale;
            this.effect = spriteConfiguration.effect;
            this.layerDepth = spriteConfiguration.layerDepth;
            this.show = true;
            this.screenWidth = graphics.GraphicsDevice.Viewport.Width;
            this.screenHeight = graphics.GraphicsDevice.Viewport.Height;
            spriteCount++;
        }


        #region Méthode de travail de la classe Sprite
        /// <summary>
        /// Met le point d'origine du sprite au millieu
        /// Cela est particuliérement utile en cas de rotation
        /// </summary>
        public void SetOriginToCenter ()
        {
            if (texture != null)
                origin = new Vector2 (texture.Width / 2, texture.Height / 2);
            else
                throw new Exception ("[Sprite] The texture is not loaded");
        }

        /// <summary>
        /// Met le point d'origine du sprite aux coordonnées par defaut
        /// c'est à dire 0, 0
        /// </summary>
        public void SetOriginToDefault ()
        {
            if (texture != null)
                origin = new Vector2 (0, 0);
            else
                throw new Exception ("[Sprite] The texture is not loaded");
        }

        /// <summary>
        /// Centre le sprite sur l'axe X par rapport à la résolution actuelle de l'écran
        /// Lance une exception si la texture du sprite n'a pas été chargée.
        /// Cette méthode doit être apellée après LoadContent()
        /// </summary>
        public virtual void SetSpriteCenterOnX ()
        {
            if (texture != null)
                position = new Vector2 ((graphics.GraphicsDevice.Viewport.Width / 2) - (texture.Width / 2), position.Y);
            else
                throw new Exception ("[Sprite] The texture is not loaded");
        }

        /// <summary>
        /// Centre le sprite sur l'axe Y par rapport à la résolution actuelle de l'écran
        /// Lance une exception si la texture du sprite n'a pas été chargée.
        /// Cette méthode doit être apellée après LoadContent()
        /// </summary>
        public virtual void SetSpriteCenterOnY ()
        {
            if (texture != null)
                position = new Vector2 (position.X, (graphics.GraphicsDevice.Viewport.Height / 2) - (texture.Height / 2));
            else
                throw new Exception ("[Sprite] The texture is not loaded");
        }

        /// <summary>
        /// Centre le sprite au millieu de l'écran par rapport à la résolution actuelle de l'écran
        /// Lance une exception si la texture du sprite n'a pas été chargée.
        /// Cette méthode doit être apellée après LoadContent()
        /// </summary>
        public virtual void SetSpriteCenterOnScreen ()
        {
            if (texture != null)
            {
                position = new Vector2 (
                    (graphics.GraphicsDevice.Viewport.Width / 2) - (texture.Width / 2),
                    (graphics.GraphicsDevice.Viewport.Height / 2) - (texture.Height / 2));
            }
            else
                throw new Exception ("[Sprite] The texture is not loaded");
        }
        #endregion
      

        public virtual void Initialize ()
        {
            // A implémenter dans les classes dérivées
            // Dans le cas d'un décors ca ne sert à rien
        }
        
        /// <summary>
        /// Chargement de la texture dans le ContentManager
        /// </summary>
        public virtual void LoadContent ()
        {
            if (textureName == null || textureName == String.Empty)
                throw new Exception ("[Sprite] The texture name or its path are incorrect");
            else
                texture = content.Load<Texture2D> (textureName);
        }

        public virtual void LoadContent (string textureName)
        {
            this.textureName = textureName;
            LoadContent ();
        }
        
        /// <summary>
        /// Mise à jour de l'affichage
        /// Logique du jeu
        /// Evénements
        /// Etc...
        /// </summary>
        /// <param name="gameTime">Temps de jeu</param>
        public virtual void Update (GameTime gameTime)
        {
            if (show)
                this.destinationRectangle = new Rectangle ((int)position.X, (int)position.Y, (int)ScaledTextureWidth, (int)ScaledTextureHeight);
        }

        /// <summary>
        /// Mise à jours de l'affichage
        /// Dessin des sprites etc..
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw (SpriteBatch spriteBatch)
        {
            // TODO : prévoir plusieurs méthodes draw
            if (show)
                spriteBatch.Draw (texture, position, sourceRectangle, color, rotation, origin, scale, effect, layerDepth);
        }

        /// <summary>
        /// Libère les ressources non utilisées
        /// </summary>
        public virtual void UnloadContent ()
        {
            if (texture != null)
                texture.Dispose ();
            spriteCount--;
        }
    }
}