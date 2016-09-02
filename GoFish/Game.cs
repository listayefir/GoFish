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
        private readonly Random _rnd = new Random();


        public Game()
        {
        }

        public Game(string realPlayerName, IReadOnlyList<string> computerPlayerNames)
        {
            Source = new Deck();
            HumanPlayer = new Player(realPlayerName,Source);
            CompPlayer1 = new Player(computerPlayerNames[0],Source);
            CompPlayer2 = new Player(computerPlayerNames[1],Source);
        }

        #region MyRegion


        #endregion

        public IEnumerable GetPlayerCards()
        {
            return HumanPlayer.Hand.Cards;
        }

        public string DescribeBooks()
        {
            var text = new StringBuilder();
            text.Append(HumanPlayer.BookInfo);
            text.Append(CompPlayer1.BookInfo);
            text.Append(CompPlayer2.BookInfo);
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
            if (HumanPlayer.PlayRound(new List<Player>() {CompPlayer1, CompPlayer2}, value, Source))
            {
                Request = HumanPlayer.Question;
                return true;
            }
            
            if (CompPlayer1.Hand.Count != 0)
              if (CompPlayer1.PlayRound(new List<Player>() { HumanPlayer, CompPlayer2 },
                                                                  CompPlayer1.Hand.Cards.Select(x => x.Value).ToArray()[_rnd.Next(CompPlayer1.Hand.Count - 1)],
                                                                  Source))
                {
                    questionTextBuilder.Append(HumanPlayer.Question + "\n");
                    questionTextBuilder.Append(CompPlayer1.Question + "\n");
                    Request = questionTextBuilder.ToString();
                    return true;
                }
            if (CompPlayer2.Hand.Count != 0)
                if (CompPlayer2.PlayRound(new List<Player>() { HumanPlayer, CompPlayer1 },
                                                                   CompPlayer2.Hand.Cards.Select(x => x.Value).ToArray()[_rnd.Next(CompPlayer2.Hand.Count - 1)],
                                                                   Source))
                {
                    questionTextBuilder.Append(HumanPlayer.Question + "\n");
                    questionTextBuilder.Append(CompPlayer1.Question + "\n");
                    questionTextBuilder.Append(CompPlayer2.Question + "\n");
                    Request = questionTextBuilder.ToString();
                    return true;
                }
            
                questionTextBuilder.Append(HumanPlayer.Question + "\n");
                questionTextBuilder.Append(CompPlayer1.Question + "\n");
                questionTextBuilder.Append(CompPlayer2.Question + "\n");
                Request = questionTextBuilder.ToString();
                return false;
            
        }
        

        public string GetWinnerName()
        {
            var players = new List<Player> {HumanPlayer, CompPlayer1, CompPlayer2};
            var maxCount = players.Max(player => player.BooksCount);
            var winners = players.Where(player => player.BooksCount == maxCount).ToArray();

            return winners.Length > 1
                ? string.Format("We have {0} winners: {1}", winners.Length.ToString(), string.Join(", ", winners.Select(p => p.Name)))
                : string.Format("{0} wins!", winners.First().Name);

             /*var wd = from player in lst
                      group player by player.BooksCount into g*/

            /*if (CompPlayer1.BooksCount == CompPlayer2.BooksCount && CompPlayer1.BooksCount > HumanPlayer.BooksCount)
                return string.Format("We have two winners: {0} and {1}", CompPlayer1.Name, CompPlayer2.Name);
            if (CompPlayer1.BooksCount == HumanPlayer.BooksCount && HumanPlayer.BooksCount > CompPlayer2.BooksCount)
                return string.Format("We have two winners: {0} and {1}", CompPlayer1.Name, HumanPlayer.Name);
            if (CompPlayer2.BooksCount == HumanPlayer.BooksCount && HumanPlayer.BooksCount > CompPlayer1.BooksCount)
                return string.Format("We have two winners: {0} and {1}", CompPlayer2.Name, HumanPlayer.Name);
            var players = new List<Player>() { HumanPlayer, CompPlayer1, CompPlayer2 };
            var maxOfTwo = Math.Max(CompPlayer1.BooksCount, CompPlayer2.BooksCount);
            var max = Math.Max(maxOfTwo, HumanPlayer.BooksCount);
            return string.Format("{0} wins!", players.Where(x => x.BooksCount == max).Select(x => x.Name).FirstOrDefault());*/
        }
    }
}
