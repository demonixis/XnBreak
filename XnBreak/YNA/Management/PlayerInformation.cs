using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YNA.Management
{
    public class PlayerInformation
    {
        private int currentScore;   // Score actuel
        private int lastScore;      // Dernier score
        private int lastBestScore;  // Meilleur score

        private float achievement;  // Taux d'achevement du jeu en pourcentage
        private int live;         // nombre de vie

        private string playerName;  // Nom du joueur

        #region setter/getter des attributs score
        public int CurrentScore
        {
            get { return currentScore; }
            set 
            {
                if (value <= 0)
                    currentScore = 0;
                else
                    currentScore = value; 
            }
        }

        public int LastScore
        {
            get { return lastScore; }
            set 
            {
                if (value <= 0)
                    lastScore = 0;
                else
                    lastScore = value; 
            }
        }

        public int LastBestScore
        {
            get { return lastBestScore; }
            set 
            {
                if (value <= 0)
                    lastBestScore = 0;
                else
                    lastBestScore = value; 
            }
        }

        public float Achievement
        {
            get { return achievement; }
            set 
            {
                if (value <= 0.0f)
                    achievement = 0.0f;
                else
                    achievement = value; 
            }
        }

        public int Live
        {
            get { return live; }
            set 
            {
                if (value <= 0)
                    live = 0;
                else
                    live = value; 
            }
        }

        public string PlayerName
        {
            get { return playerName; }
            set 
            {
                if (value == string.Empty)
                    playerName = "Player One";
                else
                    playerName = value; 
            }
        }
        #endregion


        public PlayerInformation (string playerName)
        {
            this.currentScore = 0;
            this.lastScore = 0;
            this.lastBestScore = 0;
            this.achievement = 0.0f;
            this.live = 3;
            this.playerName = playerName;
        }

        public override string ToString ()
        {
            return string.Format ("Player name : {0}\nCurrent Score : {1}\nLast score is {2} and last best score is {3}\nAchievement : {4}\nLive : {5}",
                playerName, currentScore, lastScore, lastBestScore, achievement, live);
        }
    }
}
