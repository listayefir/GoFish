using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoFish
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbName.Text))
            {
                MessageBox.Show("Please enter your name", "Can't start the game");
                return;
            }
            game = new Game(TbName.Text, new List<string>() {"Joe", "Bob"});
            BtnStart.IsEnabled = false;
            TbName.IsEnabled = false;
            BtnAskForCard.IsEnabled = true;
            UpdateForm();

        }

        private void UpdateForm()
        {
            LbHand.Items.Clear();
            foreach (var card in  game.GetPlayerCards())
            {
                LbHand.Items.Add(card);
            }
            TbBooks.Text += game.DescribeBooks();
            TbGameProcess.Text += game.DescribePlayerSteps();
            TbGameProcess.SelectionStart = TbGameProcess.Text.Length;
            TbGameProcess.ScrollToEnd();
        }

        private void BtnAskForCard_Click(object sender, RoutedEventArgs e)
        {
            TbGameProcess.Text = "";
            if (LbHand.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a card");
                return;
            }
            if (game.PlayOneRound(((Card)LbHand.SelectedItem).Value))
            {
                TbGameProcess.Text += game.GetWinnerName();
                TbBooks.Text = game.DescribeBooks();
                BtnAskForCard.IsEnabled = false;
            }
            else
            {
                UpdateForm();
            }
        }
    }
}
