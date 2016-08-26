using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GoFish
{
    public class Game
    {
        public Player HumanPlayer { get; set; }
        public Player CompPlayer1 { get; set; }
        public Player CompPlayer2 { get; set; }
        public Deck Source { get; set; }
        public string Request { get; private set; }
        private Random rnd = new Random();


        public Game()
        {
        }

        public Game(string realPlayerName, List<string> computerPlayerNames)
        {
            Source = new Deck();
            HumanPlayer = new Player(realPlayerName,Source);
            CompPlayer1 = new Player(computerPlayerNames[0],Source);
            CompPlayer2 = new Player(computerPlayerNames[1],Source);
        }


        public IEnumerable GetPlayerCards()
        {
            return HumanPlayer.Hand.Cards;
        }

        public string DescribeBooks()
        {
            var text = new StringBuilder();
            text.Append(HumanPlayer.RemoveBook());
            text.Append("\n" + CompPlayer1.RemoveBook());
            text.Append("\n" + CompPlayer2.RemoveBook());
            return text.ToString();

        }

        internal string DescribePlayerSteps()
        {
            return Request;
        }

        public bool PlayOneRound(Values value)
        {
            Request = string.Empty;
            var questionTextBuilder = new StringBuilder();
            if (RoundforPlayer(HumanPlayer,value))
            {
                Request = HumanPlayer.Question;
                return true;
            }
            if (RoundforPlayer(CompPlayer1))
            {
                Request =CompPlayer1.Question;
                return true;
            }
            if (RoundforPlayer(CompPlayer2))
            {
                Request = CompPlayer2.Question;
                return true;
            }

            else
            {
                questionTextBuilder.Append(HumanPlayer.Question + "\n");
                questionTextBuilder.Append(CompPlayer1.Question + "\n");
                questionTextBuilder.Append(CompPlayer2.Question + "\n");
                Request = questionTextBuilder.ToString();
                return false;
            }
        }
        
        private bool RoundforPlayer(Player player, params Values[] value)
        {
            Values currentValue;
            List<Player> anotherPlayers; 
            if (player == HumanPlayer)
            {
                currentValue = value[0];
                anotherPlayers = new List<Player>() { CompPlayer1, CompPlayer2 };
            }
            else
            {
                currentValue = (Values)rnd.Next(13);
                if (player == CompPlayer1)
                    anotherPlayers = new List<Player>() { HumanPlayer, CompPlayer2 };
                else
                    anotherPlayers = new List<Player>() { HumanPlayer, CompPlayer1 };
            }
               
            if (player.AskForCard(anotherPlayers[0],anotherPlayers[1], currentValue))
            {
                player.MakeUpWithBooks();
                if (player.Hand.Cards.Count == 0)
                {
                    if (Source.Cards.Count < 5)
                    {
                        player.TakeCardFromDeck(Source.Cards.Count,Source);
                        player.MakeUpWithBooks();
                        return true;
                    }
                    else
                    {
                        player.TakeCardFromDeck(5,Source);
                        player.MakeUpWithBooks();
                    }
                }
            }
            else
            {
               player.TakeCardFromDeck(1,Source);
            }
            
            return false;
        }

        public string GetWinnerName()
        {
            if (CompPlayer1.BooksCount == CompPlayer2.BooksCount)
                return string.Format("We have two winners: {0} and {1}", CompPlayer1.Name, CompPlayer2.Name);
            if (CompPlayer1.BooksCount == HumanPlayer.BooksCount)
                return string.Format("We have two winners: {0} and {1}", CompPlayer1.Name, HumanPlayer.Name);
            if (CompPlayer2.BooksCount == HumanPlayer.BooksCount)
                return string.Format("We have two winners: {0} and {1}", CompPlayer2.Name, HumanPlayer.Name);
            var players = new List<Player>() { HumanPlayer, CompPlayer1, CompPlayer2 };
            var maxOfTwo = Math.Max(CompPlayer1.BooksCount, CompPlayer2.BooksCount);
            var max = Math.Max(maxOfTwo, HumanPlayer.BooksCount);
            return string.Format("{0} wins!",players.Where(x => x.BooksCount == max).Select(x => x.Name).ToString());
        }
    }
}
