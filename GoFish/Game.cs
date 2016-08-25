using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GoFish
{
    class Game
    {

        public Game(string realPlayer, List<string> computerPlayers, TextBox textBox)
        {
            
        }


        public IEnumerable GetPlayerCards()
        {
            throw new NotImplementedException();
        }

        public string DescribeBooks()
        {
            throw new NotImplementedException();
        }

        internal string DescribePlayerHands()
        {
            throw new NotImplementedException();
        }

        public bool PlayOneRound(int lbHandSelectedIndex)
        {
            throw new NotImplementedException();
        }

        public object GetWinnerName()
        {
            throw new NotImplementedException();
        }
    }
}
