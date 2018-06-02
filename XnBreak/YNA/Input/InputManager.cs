using Microsoft.Xna.Framework.Input;

namespace YNA.Input
{
    public class InputManager
    {
        private KeyboardState currentKeyboadState;
        private KeyboardState lastKeyBoardState;

        private GamePadState currentGamePadState;
        private GamePadState lastGamePadState;

        // Membre gérant le singleton
        private static InputManager instance;
        private static readonly object instanceLocker = new object();

        private InputManager () { }

        public static InputManager GetInstance ()
        {
            lock (instanceLocker)
            {
                if (instance == null)
                    instance = new InputManager ();
                return instance;
            }
        }

        public void Update (KeyboardState keyboardState)
        {
            lastKeyBoardState = currentKeyboadState;
            currentKeyboadState = keyboardState;
            
            
        }

        public void Update (GamePadState gamePadState)
        {
            currentGamePadState = gamePadState;
            lastGamePadState = gamePadState;
        }

        public void Update (GamePadState gamePadState, KeyboardState keyboardState)
        {
            Update (gamePadState);
            Update (keyboardState);
        }

        public bool GetPressedKey(Keys key)
        {
            if (currentKeyboadState.IsKeyDown (key))
                return true;
            else 
                return false;
        }

        public bool GetOnePressedKey (Keys key)
        {
            if (currentKeyboadState.IsKeyUp (key) && lastKeyBoardState.IsKeyDown (key))
                return true;
            else
                return false;
        }

        public bool GetPressedButton (Buttons button)
        {
            if (currentGamePadState.IsButtonDown (button))
                return true;
            else
                return false;
        }

        public bool GetOnePressedButton (Buttons button)
        {
            if (currentGamePadState.IsButtonDown (button) && lastGamePadState.IsButtonDown (button))
                return true;
            else
                return false;
        }

        #region Get sur les touches une fois : Clavier uniquement
        public bool OneKeyUp
        {
            get
            {
                if (currentKeyboadState != null)
                {
                    if (currentKeyboadState.IsKeyDown (Keys.Up) && lastKeyBoardState.IsKeyUp (Keys.Up))
                        return true;
                }
                return false;
            }
        }

        public bool OneKeyDown
        {
            get
            {
                if (currentKeyboadState != null)
                {
                    if (currentKeyboadState.IsKeyDown (Keys.Down) && lastKeyBoardState.IsKeyUp (Keys.Down))
                        return true;
                }
                return false;
            }
        }

        public bool OneKeyLeft
        {
            get
            {
                if (currentKeyboadState != null)
                {
                    if (currentKeyboadState.IsKeyDown (Keys.Left) && lastKeyBoardState.IsKeyUp (Keys.Left))
                        return true;
                }
                return false;
            }
        }

        public bool OneKeyRight
        {
            get
            {
                if (currentKeyboadState != null)
                {
                    if (currentKeyboadState.IsKeyDown (Keys.Right) && lastKeyBoardState.IsKeyUp (Keys.Right))
                        return true;
                }
                return false;
            }
        }

        public bool OneKeySpace
        {
            get
            {
                if (currentKeyboadState != null)
                {
                    if (currentKeyboadState.IsKeyDown (Keys.Space) && lastKeyBoardState.IsKeyUp (Keys.Space))
                        return true;
                }
                return false;
            }
        }

        public bool OneKeyLeftControl
        {
            get
            {
                if (currentKeyboadState != null)
                {
                    if (currentKeyboadState.IsKeyDown (Keys.LeftControl) && lastKeyBoardState.IsKeyUp (Keys.LeftControl))
                        return true;
                }
                return false;
            }
        }

        public bool OneKeyLeftAlt
        {
            get
            {
                if (currentKeyboadState != null)
                {
                    if (currentKeyboadState.IsKeyDown (Keys.LeftAlt) && lastKeyBoardState.IsKeyUp (Keys.LeftAlt))
                        return true;
                }
                return false;
            }
        }
        #endregion

        #region Get sur les touches et le pad en même temps : Répétition
        public bool Up
        {
            get
            {
                if (currentKeyboadState != null && currentGamePadState != null)
                {
                    if (currentGamePadState.IsButtonDown (Buttons.DPadUp) || currentKeyboadState.IsKeyDown (Keys.Up))
                        return true;
                }
                return false;
            }
        }

        public bool Down
        {
            get
            {
                if (currentKeyboadState != null && currentGamePadState != null)
                {
                    if (currentGamePadState.IsButtonDown (Buttons.DPadDown) || currentKeyboadState.IsKeyDown (Keys.Down))
                        return true;
                }
                return false;
            }
        }

        public bool Left
        {
            get
            {
                if (currentKeyboadState != null && currentGamePadState != null)
                {
                    if (currentGamePadState.IsButtonDown (Buttons.DPadLeft) || currentKeyboadState.IsKeyDown (Keys.Left))
                        return true;
                }
                return false;
            }
        }

        public bool Right
        {
            get
            {
                if (currentKeyboadState != null && currentGamePadState != null)
                {
                    if (currentGamePadState.IsButtonDown (Buttons.DPadRight) || currentKeyboadState.IsKeyDown (Keys.Right))
                        return true;
                }
                return false;
            }
        }
        #endregion
    }
}
