namespace blackjack_game
{
    public partial class Main : Form
    {
        int balance = 100;
        int betAmount = 5;
        int wins;
        int gamesPlayed;

        int playerCardSum;
        int dealerCardSum;
        bool gameStarted;

        Random random = new Random();

        List<int> usedCards = new List<int>();
        List<Card> playerCards = new List<Card>()
        {
            new Card() {Value = 0, Name = "null", Image = "null"}
        };
        List<Card> dealerCards = new List<Card>()
        {
            new Card() {Value = 0, Name = "null", Image = "null"}
        };
        List<Card> cardDeck = new List<Card>()
        {
                // Clubs
                new Card() {Value = 2, Name = "Two Clubs", Image = "Resources/Images/2C.png"},
                new Card() {Value = 3, Name = "Three Clubs", Image = "Resources/Images/3C.png"},
                new Card() {Value = 4, Name = "Four Clubs", Image = "Resources/Images/4C.png"},
                new Card() {Value = 5, Name = "Five Clubs", Image = "Resources/Images/5C.png"},
                new Card() {Value = 6, Name = "Six Clubs", Image = "Resources/Images/6C.png"},
                new Card() {Value = 7, Name = "Seven Clubs", Image = "Resources/Images/7C.png"},
                new Card() {Value = 8, Name = "Eight Clubs", Image = "Resources/Images/8C.png"},
                new Card() {Value = 9, Name = "Nine Clubs", Image= "Resources/Images/9C.png"},
                new Card() {Value = 10, Name = "Ten Clubs", Image = "Resources/Images/10C.png"},
                new Card() {Value = 10, Name = "Jack Clubs", Image = "Resources/Images/JC.png"},
                new Card() {Value = 10, Name = "Queen Clubs", Image = "Resources/Images/QC.png"},
                new Card() {Value = 10, Name = "King Clubs", Image = "Resources/Images/KC.png"},
                new Card() {Value = 11, Name = "Ace Clubs", Image = "Resources/Images/AC.png"},

                // Spades
                new Card() {Value = 2, Name = "Two Spades", Image = "Resources/Images/2S.png"},
                new Card() {Value = 3, Name = "Three Spades", Image = "Resources/Images/3S.png"},
                new Card() {Value = 4, Name = "Four Spades", Image = "Resources/Images/4S.png"},
                new Card() {Value = 5, Name = "Five Spades", Image = "Resources/Images/5S.png"},
                new Card() {Value = 6, Name = "Six Spades", Image = "Resources/Images/6S.png"},
                new Card() {Value = 7, Name = "Seven Spades", Image = "Resources/Images/7S.png"},
                new Card() {Value = 8, Name = "Eight Spades", Image = "Resources/Images/8S.png"},
                new Card() {Value = 9, Name = "Nine Spades", Image = "Resources/Images/9S.png"},
                new Card() {Value = 10, Name = "Ten Spades", Image = "Resources/Images/10S.png"},
                new Card() {Value = 10, Name = "Jack Spades", Image = "Resources/Images/JS.png"},
                new Card() {Value = 10, Name = "Queen Spades", Image = "Resources/Images/QS.png"},
                new Card() {Value = 10, Name = "King Spades", Image = "Resources/Images/KS.png"},
                new Card() {Value = 11, Name = "Ace Spades", Image = "Resources/Images/AS.png"},

                // Diamonds
                new Card() {Value = 2, Name = "Two Diamonds", Image = "Resources/Images/2D.png"},
                new Card() {Value = 3, Name = "Three Diamonds", Image = "Resources/Images/3D.png"},
                new Card() {Value = 4, Name = "Four Diamonds", Image = "Resources/Images/4D.png"},
                new Card() {Value = 5, Name = "Five Diamonds", Image = "Resources/Images/5D.png"},
                new Card() {Value = 6, Name = "Six Diamonds", Image = "Resources/Images/6D.png"},
                new Card() {Value = 7, Name = "Seven Diamonds", Image = "Resources/Images/7D.png"},
                new Card() {Value = 8, Name = "Eight Diamonds", Image = "Resources/Images/8D.png"},
                new Card() {Value = 9, Name = "Nine Diamonds", Image = "Resources/Images/9D.png"},
                new Card() {Value = 10, Name = "Ten Diamonds", Image = "Resources/Images/10D.png"},
                new Card() {Value = 10, Name = "Jack Diamonds", Image = "Resources/Images/JD.png"},
                new Card() {Value = 10, Name = "Queen Diamonds", Image = "Resources/Images/QD.png"},
                new Card() {Value = 10, Name = "King Diamonds", Image = "Resources/Images/KD.png"},
                new Card() {Value = 11, Name = "Ace Diamonds", Image = "Resources/Images/AD.png"},

                // Hearts
                new Card() {Value = 2, Name = "Two Hearts", Image = "Resources/Images/2H.png"},
                new Card() {Value = 3, Name = "Three Hearts", Image = "Resources/Images/3H.png"},
                new Card() {Value = 4, Name = "Four Hearts", Image = "Resources/Images/4H.png"},
                new Card() {Value = 5, Name = "Five Hearts", Image = "Resources/Images/5H.png"},
                new Card() {Value = 6, Name = "Six Hearts", Image = "Resources/Images/6H.png"},
                new Card() {Value = 7, Name = "Seven Hearts", Image = "Resources/Images/7H.png"},
                new Card() {Value = 8, Name = "Eight Hearts", Image = "Resources/Images/8H.png"},
                new Card() {Value = 9, Name = "Nine Hearts", Image = "Resources/Images/9H.png"},
                new Card() {Value = 10, Name = "Ten Hearts", Image = "Resources/Images/10H.png"},
                new Card() {Value = 10, Name = "Jack Hearts", Image = "Resources/Images/JH.png"},
                new Card() {Value = 10, Name = "Queen Hearts", Image = "Resources/Images/QH.png"},
                new Card() {Value = 10, Name = "King Hearts", Image = "Resources/Images/KH.png"},
                new Card() {Value = 11, Name = "Ace Hearts", Image = "Resources/Images/AH.png"}
        };

        List<PictureBox> dealerPictureBox = new List<PictureBox>();
        List<PictureBox> playerPictureBox = new List<PictureBox>();

        int ModifyBalance(int amount)
        {
            balance += amount;
            return balance;
        }

        void UpdateBetLabel()
        {
            lbl_betAmount.Text = $"${betAmount}";
        }

        void LogToHistory(string message)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");

            historyList.Items.Add($"[{currentTime}: {message}]");

        }

        void DisplayCardBack(PictureBox pictureBox)
        {
            pictureBox.ImageLocation = "Resources/Images/BR.png";
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        void ResetGame()
        {
            gameStarted = false;
            playerCardSum = 0;
            dealerCardSum = 0;
            usedCards.Clear();
            playerCards.Clear();
            dealerCards.Clear();

            foreach (PictureBox pb in playerPictureBox)
            {
                this.Controls.Remove(pb);
            }
            playerPictureBox = new List<PictureBox>();

            foreach (PictureBox pb in dealerPictureBox)
            {
                this.Controls.Remove(pb);
            }
            dealerPictureBox = new List<PictureBox>();


            DisplayCardBack(pb_banker);
            DisplayCardBack(pb_player);
            DisplayCardBack(pb_player1);

            btn_hit.Text = "Start";
        }

        void SumCards(List<Card> cards, ref int sum)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                sum += cards[i].Value;
            }

            lbl_status.Text = $"[{Environment.UserName} {playerCardSum} / {dealerCardSum} Gregor]";
        }

        int DrawRandomCard()
        {
            int randomCard;
            randomCard = random.Next(0, cardDeck.Count);

            while (usedCards.Contains(randomCard))
            {
                randomCard = random.Next(0, cardDeck.Count);
            }

            usedCards.Add(randomCard);

            return randomCard;
        }

        void WinGame()
        {
            lbl_status.Text = $"[You won! {Environment.UserName} {playerCardSum} / {dealerCardSum} Gregor]";
            LogToHistory($"won {playerCardSum} / {dealerCardSum}");
        }

        void LoseGame()
        {
            lbl_status.Text = $"[You lost! {Environment.UserName} {playerCardSum} / {dealerCardSum} Gregor]";
            LogToHistory($"tied {playerCardSum} / {dealerCardSum}");
        }

        void TieGame()
        {
            lbl_status.Text = $"[Standoff! {Environment.UserName} {playerCardSum} / {dealerCardSum} Gregor]";
            LogToHistory($"lost {playerCardSum} / {dealerCardSum}");
        }

        public Main()
        {
            InitializeComponent();
            lbl_balance.Text = $"Balance: ${balance}";
            lbl_wins.Text = $"Wins: {wins}";
            lbl_status.Text = $"[Begin the game by pressing 'Start']";
            UpdateBetLabel();
            ResetGame();
        }

        private void btn_increaseBet_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                betAmount += 1;
            }

            else if (ModifierKeys.HasFlag(Keys.Control))
            {
                betAmount += 10;
            }

            else
            {
                betAmount += 5;
            }
            betAmount = Math.Clamp(betAmount, 5, balance);
            UpdateBetLabel();
        }

        private void btn_decreaseBet_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                betAmount -= 1;
            }

            else if (ModifierKeys.HasFlag(Keys.Control))
            {
                betAmount -= 10;
            }

            else
            {
                betAmount -= 5;
            }
            betAmount = Math.Clamp(betAmount, 5, balance);
            UpdateBetLabel();
        }

        private void btn_hit_Click(object sender, EventArgs e)
        {
            if (gameStarted)
            {
                playerCards.Clear();
                dealerCards.Clear();
                usedCards.Clear();

                int randomPick = DrawRandomCard();
                Card card = cardDeck[randomPick];

                PictureBox pb = new PictureBox();
                pb.Width = 108;
                pb.Height = 144;
                pb.Location = new Point(240 + playerPictureBox.Count * 115, 178);
                pb.ImageLocation = card.Image;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                this.Controls.Add(pb);

                playerPictureBox.Add(pb);

                playerCards.Add(card);
            }
            else
            {
                gameStarted = true;
                btn_stand.Enabled = true;
                btn_hit.Text = "Hit";

                int holeCard1 = DrawRandomCard();
                Card card1 = cardDeck[holeCard1];

                int holeCard2 = DrawRandomCard();
                Card card2 = cardDeck[holeCard2];

                playerCards.Add(card1);
                pb_player.ImageLocation = card1.Image;
                pb_player.SizeMode = PictureBoxSizeMode.StretchImage;

                playerCards.Add(card2);
                pb_player1.ImageLocation = card2.Image;
                pb_player1.SizeMode = PictureBoxSizeMode.StretchImage;

                int dealerHoleCard = DrawRandomCard();

                Card card3 = cardDeck[dealerHoleCard];

                dealerCards.Add(card3);
                pb_banker.ImageLocation = card3.Image;
                pb_banker.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            SumCards(playerCards, ref playerCardSum);
            SumCards(dealerCards, ref dealerCardSum);

            if (playerCardSum > 21)
            {
                LoseGame();
                ResetGame();
            }
        }

        private void btn_stand_Click(object sender, EventArgs e)
        {
            if (dealerCardSum <= 16)
            {
                int randomCard = DrawRandomCard();
                Card card = cardDeck[randomCard];

                PictureBox pb = new PictureBox();
                pb.Width = 108;
                pb.Height = 144;
                pb.Location = new Point(126 + dealerPictureBox.Count * 115, 28);
                pb.ImageLocation = card.Image;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                this.Controls.Add(pb);

                dealerPictureBox.Add(pb);
                dealerCards.Add(card);
            }

            SumCards(playerCards, ref playerCardSum);
            SumCards(dealerCards, ref dealerCardSum);

            if (playerCardSum == 21 || dealerCardSum == 21)
            {
                TieGame();
                ResetGame();
            }
            else if (dealerCardSum > 21 || playerCardSum > dealerCardSum)
            {
                WinGame();
                ResetGame();
            }
            else
            {
                LoseGame();
                ResetGame();
            }
        }
    }
}