using System;

namespace FlippedTicTacToe
{
    public class Player
    {
        private uint m_Score = 0;
        private readonly eSymbols r_Symbol;
        private readonly bool r_IsComputer;
        private readonly string r_Name;

        public Player(eSymbols i_Symbol, bool i_IsComputer, string i_Name)
        {
            r_Symbol = i_Symbol;
            r_IsComputer = i_IsComputer;
            r_Name = i_Name;
        }

        public uint Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public eSymbols Symbol
        {
            get
            {
                return r_Symbol;
            }
        }

        public bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }
    }
}
