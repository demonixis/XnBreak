using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YNA.Management
{
    public enum Pegi
    {
        ThreeMore = 0x01, SevenMore, Twelve, Sixteen, Eighteen, Violence, 
        BadLanguage, Fear, SexualContent, Drugs, Discrimination, Gambling
    }

    public enum GameType
    {
        BasicGame = 0x10, SimpleGame, ComplexeGame
    }

    public class GameInformation
    {
        private string author;
        private string title;
        private double version;
        private int nbPlayers;
        private bool isSoloGame;
        private bool isMutiplayerGame;
        private bool isOnlineGame;
        private Pegi pegiInfo;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public double Version
        {
            get { return version; }
            set 
            {  
                if (value < 0)
                {
                    version = 0.01;
                    throw new Exception ("[GameInformation] The game version must be highter than zero");
                }
                else
                    version = value;
            }
        }

        public int NbPlayers
        {
            get { return nbPlayers; }
            set
            {
                if (value <= 0)
                {
                    nbPlayers = 1;
                    throw new Exception ("[GameInformation] A game must have at least one player");
                }
                else
                    nbPlayers = value;
            }
        }

        public bool IsSoloGame
        {
            get { return isSoloGame; }
            set
            {
                if (!isMutiplayerGame && !value)
                {
                    isSoloGame = true;
                    throw new Exception ("[GameInformation] A game must be solo OR/AND multi");
                }
                else
                    isSoloGame = value;
            }
        }

        public bool IsMultiplayerGame
        {
            get { return isMutiplayerGame; }
            set { isMutiplayerGame = value; }
        }

        public bool IsOnlineGame
        {
            get { return isOnlineGame; }
            set { isOnlineGame = value; }
        }

        public Pegi PegiInfo
        {
            get { return pegiInfo; }
            set { pegiInfo = value; }
        }

        public GameInformation()
        {
            author = "No author for now";
            title = "No title for now";
            version = 0.01;
            nbPlayers = 1;
            isSoloGame = true;
            isMutiplayerGame = false;
            isOnlineGame = false;
            pegiInfo = Pegi.Twelve;
        }

        public override string  ToString()
        {
            return String.Format("{0} is a game by {1}\nCurrently in version {2}", title, author, version);
        }
    }
}
