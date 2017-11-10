using System;

namespace XnBreak
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main (string[] args)
        {
            using (XnBreak game = new XnBreak ())
            {
                game.Run ();
            }
        }
    }
}

