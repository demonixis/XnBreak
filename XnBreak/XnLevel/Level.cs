using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using YNA.Interface;
using YNA.Graphics2D;
using YNA.Management;
using YNA.Input;

using XnBreak.XnSprite;
using XnBreak.XnSprite.Event;
using XnBreak.XnSprite.Manager;
using XnBreak.XnLevel.Event;

namespace XnBreak.XnLevel
{
    public class Level : IXnStructure
    {
        public event EventHandler<LevelFinishEventArgs> levelIsFinish;

        private Game game;
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        private string textureName;
        private Texture2D textureLevel;
        private Rectangle rectangleLevel;

        private int screenWidth;
        private int screenHeight;
        private float screenCenter;

        // TODO : touchManager
        private int touchId;
        private TouchCollection touchPoints;

        // TODO : ajouter une classe LevelStates
        // Etat du niveau
        private bool canPlay;
        private bool retry;

        private Texture2D textureGameOver;
        
        // Surface jouable
        // Taille de la surface d'affichage
        private Rectangle playableSurface;
        private Vector2 spriteScale;

        // Objets/composants
        private PlayerInformation _playerInformations;
        private LevelHUD _levelHUD;
        private Balle _balle;
        private Palet _palet;
        private BricksManager _bricksManager;

        // Collection de tous les sprites du niveau
        // index du prochain sprite à supprimer de la collection
        private List<Sprite> allSprites;
        private int removeSpriteIndex;

        public Game Game { get { return game; } }
        public GraphicsDeviceManager Graphics { get { return graphics; } }

        #region set/get 
        public string TextureName
        {
            get { return textureName; }
            set
            {
                if (System.IO.File.Exists (content.RootDirectory + value + ".png"))
                    textureName = value;
                else
                    throw new Exception ("[Level] Impossible de changer la texture, car celle ci n'est pas présente");
            }
        }

        public Texture2D TextureLevel
        {
            get { return TextureLevel; }
            set
            {
                if (value != null)
                    textureLevel = value;
                else
                    throw new Exception ("[Level] L'objet texture doit exister et être instancier");
            }
        }

        public Rectangle PlayableSurface
        {
            get { return playableSurface; }
        }

        public Vector2 SpriteScale 
        { 
            get { return spriteScale; } 
        }

        public int ScreenWidth
        {
            get { return screenWidth; }
        }

        public int ScreenHeight
        {
            get { return screenHeight; }
        }

        public float ScreenCenter
        {
            get { return screenCenter; }
        }
        #endregion

        public Level (Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.screenWidth = graphics.GraphicsDevice.Viewport.Width;
            this.screenHeight = graphics.GraphicsDevice.Viewport.Height;
            this.screenCenter = this.screenWidth / 2;
            this.rectangleLevel = new Rectangle (0, 0, screenWidth, screenHeight);

            /* Le rectangle jouable a une résolution réel de 1000x760
             * La texture d'origine fait 1280x800
             * 1 et 2 : Il y a un décallage de 20px entre le debut de la texture (x ,y)
             *          et la surface de jeu
             *          1280 / 64 = 20 --> adaptation à chaque résulutions
             * 3 et 4 : 1280 / (1280 / 1000) = 1280 / 1.28 = 1000 soit la largeur max de la surface jouable
             *          pour avoir la surface jouable relative complète il faut ajouter les 20px de décalage calculé au début
             *          prenant en compte l'échelle
             */
            this.playableSurface = new Rectangle (
                screenWidth / 64,
                screenHeight / 40,
                (int)(screenWidth / (1280.0f / 1000.0f)),
                (int)(screenHeight / (800.0f / 760.0f)));

            // Dertermination des coefficient de proportionnalité de l'échelle
            // Echelle d'origine 1280x800
            this.spriteScale = new Vector2 (
                (float)screenWidth / 1280.0f,
                (float)screenHeight / 800.0f);

            this.allSprites = new List<Sprite> ();
        }

        public void Initialize ()
        {
            _playerInformations = new PlayerInformation ("Joueur 1");
            _levelHUD = new LevelHUD (this, _playerInformations);
            _palet = new Palet (this);
            _balle = new Balle (this);
            _balle.balleMissed += this.BalleMissed_Event;
            _bricksManager = new BricksManager (this);

            removeSpriteIndex = -1;
            canPlay = true;
            retry = false;

            allSprites.Clear ();

            _levelHUD.Initialize ();
            _palet.Initialize ();
            _balle.Initialize ();
            _bricksManager.Initialize ();

            // Le palet est en première position dans la collection
            // La balle à la deuxième position, cela permet de ne pas itérer toute la collection
            // pour trouver une collision
            allSprites.Insert (0, _palet); 
            allSprites.Insert (1, _balle);

            foreach (Brick b in _bricksManager.ListBricks)
            {
                b.brickDestroyed += this.BrickAsDestroyed_Event;
                allSprites.Add (b);
            }

            foreach (Sprite s in allSprites)
                s.Scale = spriteScale;
        }

        public void LoadContent ()
        {
            if (textureName != null)
                textureLevel = content.Load<Texture2D> (textureName);
            else
                throw new Exception ("[Level] Impossible de charger une texture ayant une référence nulle");
            foreach (Sprite s in allSprites)
                if (s.Texture == null)
                {
                    try
                    {
                        s.LoadContent ();
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine (e.Message);
                    }
                }
#if WINDOWS_PHONE
            textureGameOver = content.Load<Texture2D> ("screen/gameover-mobile");
#else
            textureGameOver = content.Load<Texture2D> ("screen/gameover");
#endif
        }

        public void LoadContent (string textureName)
        {
            this.textureName = textureName;
            LoadContent ();
        }

        public void UnloadContent ()
        {
            foreach (Sprite s in allSprites)
                s.UnloadContent ();
            allSprites = null;
            textureLevel.Dispose ();
            textureLevel = null;
        }

        public void Update () { }

        /// <summary>
        /// Mise à jour de la logique du niveau
        /// </summary>
        /// <param name="gameTime">Temps de jeu</param>
        public void Update (GameTime gameTime)
        {
            if (retry)      // On relance la partie
                Initialize ();

            if (canPlay)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    OnLevelisFinish(new LevelFinishEventArgs(LevelFinish.loose));

                if (allSprites.Count == 2)
                    OnLevelisFinish(new LevelFinishEventArgs(LevelFinish.win));  // Le joueur à gagné 

                 _levelHUD.Update (gameTime);

                // Suppression des sprites non visibles
                for (int i = 0; i < allSprites.Count; i++)
                {
                    if (!allSprites[i].Show)
                    {
                        Sprite s = allSprites[i];
                        allSprites.RemoveAt (i);
                        s = null;
                    }
                }

#if WINDOWS_PHONE
                touchPoints = TouchPanel.GetState();
                if (!_balle.Moving)
                {
                    foreach (var point in touchPoints)
                    {
                        if (point.State == TouchLocationState.Pressed)
                        {
                            if (_palet.DestinationRectangle.Contains((int)point.Position.X, (int)point.Position.Y))
                            {
                                _balle.Moving = true;
                            }
                        }
                    }
                }
#else

                // Lance la balle
                if (!_balle.Moving && Keyboard.GetState ().IsKeyDown (Keys.Space))
                    _balle.Moving = true;
#endif

                // ---
                // --- Mise à jour de la collection de sprite
                // ---
                foreach (Sprite s in allSprites)
                {
                    if (s.Show)
                    {
                        if (s is Balle)
                        {
                            Balle b = (Balle)s;
                            b.Update (gameTime, allSprites);
                        }
                        else
                            s.Update (gameTime);
                    }
                }
            }
            else        // Fin de partie
            {
#if WINDOWS_PHONE
                touchPoints = TouchPanel.GetState();

                foreach (var point in touchPoints)
                {
                    if (point.State == TouchLocationState.Pressed)
                        OnLevelisFinish(new LevelFinishEventArgs(LevelFinish.loose));
                }
#else
                if (Keyboard.GetState ().IsKeyDown (Keys.Enter))
                {
                    retry = true;
                    canPlay = true;
                }
                else if (Keyboard.GetState ().IsKeyDown (Keys.Space))
                {
                    OnLevelisFinish (new LevelFinishEventArgs (LevelFinish.loose));
                }
#endif
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            if (!canPlay)
            {
                spriteBatch.Draw (textureGameOver, new Rectangle (0, 0, screenWidth, screenHeight), Color.White);
            }
            else
            {
                spriteBatch.Draw (textureLevel, rectangleLevel, Color.White);
                _levelHUD.Draw (spriteBatch);
                foreach (Sprite s in allSprites)
                {
                    // La classe mère Sprite permet de ne déssiner que les sprite visible (Show = true)
                    s.Draw (spriteBatch);
                }
            }
        }

        #region Evénements traités par le niveau
        /// <summary>
        /// Appelée à chaque fois qu'une brique est détruite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrickAsDestroyed_Event (object sender, BrickDestroyedEventArgs e)
        {
            removeSpriteIndex = e.index;
            _playerInformations.CurrentScore += e.brick.Score;
        }

        /// <summary>
        /// Appelée à chaque fois que la balle entre en collision avec le sol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BalleMissed_Event (object sender, BalleMissedEventArgs e)
        {
            if (_playerInformations.Live == 0)  // Niveau terminé, plus de vie
            {
                canPlay = false;
            }
            else
            {
                _playerInformations.Live -= 1;
                _balle.Initialize ();
                _balle.Moving = false;
            }
        }
        #endregion

        #region Evénements envoyés par le niveau
        private void OnLevelisFinish (LevelFinishEventArgs e) 
        {
            levelIsFinish (this, e);
        }
        #endregion
    }
}
