using System;
using System.Windows.Forms;

namespace WindowsFormsFlippedTicTacToe
{
    public partial class FormSettings : Form
    {
        private string m_TextBoxPlayer2PrevName = null;

        public FormSettings()
        {
            InitializeComponent();
        }

        public string Player1Name
        {
            get
            {
                return textBoxPlayer1.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return textBoxPlayer2.Text;
            }
        }

        public bool CheckBoxPlayer2Checked
        {
            get
            {
                return checkBoxPlayer2.Checked;
            }
        }

        public uint BoardWidth
        {
            get
            {
                return (uint)numericUpDownRows.Value;
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPlayer2.Checked == true)
            {
                textBoxPlayer2.Enabled = true;
                if(m_TextBoxPlayer2PrevName != null)
                {
                    textBoxPlayer2.Text = m_TextBoxPlayer2PrevName;
                }
            }
            else
            {
                m_TextBoxPlayer2PrevName = textBoxPlayer2.Text;
                textBoxPlayer2.Text = "Computer";
                textBoxPlayer2.Enabled = false;
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if(sender == numericUpDownCols)
            {
                numericUpDownRows.Value = numericUpDownCols.Value;
            }
            else
            {
                numericUpDownCols.Value = numericUpDownRows.Value;
            }
        }
    }
}
