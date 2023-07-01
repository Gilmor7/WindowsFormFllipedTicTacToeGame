using System;
using System.Windows.Forms;

namespace WindowsFormsFlippedTicTacToe
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            GameManager gameManager = new GameManager();
            gameManager.Run();
        }
    }
}
