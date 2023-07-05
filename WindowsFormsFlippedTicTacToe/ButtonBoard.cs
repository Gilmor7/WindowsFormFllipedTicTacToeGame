using System;
using System.Drawing;
using System.Windows.Forms;
using FlippedTicTacToe;

namespace WindowsFormsFlippedTicTacToe
{
    public class ButtonBoard : Button
    {
        public const int k_ButtonSize = 50;

        public ButtonBoard(int i_Row, int i_Col, Point i_Location)
        {
            this.Size = new Size(k_ButtonSize, k_ButtonSize);
            this.Location = i_Location;
            this.Tag = new Cell((uint)i_Row, (uint)i_Col);
            this.TabStop = false;
        }
    }
}
