using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
    public partial class FindCard : Form
    {
        private List<Card> cards = new List<Card>();
        private Card firstClicked = null;
        private Card secondClicked = null;
        private int secondsElapsed = 0;

        private int timeLeft = 60;  //середній рівень
        private int attemptsLeft = 10;  //складний рівень

        public FindCard()
        {
            InitializeComponent();
            InitCards();
            StartGame();
        }

        private void InitCards()
        {
            cards.Clear();

            cards.Add(new Card(btncircle, "circle"));
            cards.Add(new Card(btncircle2, "circle"));
            cards.Add(new Card(btntrapezoid, "trapezoid"));
            cards.Add(new Card(btntrapezoid2, "trapezoid"));
            cards.Add(new Card(btnrectangle, "rectangle"));
            cards.Add(new Card(btnrectangle2, "rectangle"));
            cards.Add(new Card(btnblue, "blue"));
            cards.Add(new Card(btnblue2, "blue"));
            cards.Add(new Card(btnoval, "oval"));
            cards.Add(new Card(btnoval2, "oval"));
            cards.Add(new Card(btnsquare, "square"));
            cards.Add(new Card(btnsquare2, "square"));
            cards.Add(new Card(btntriangle, "triangle"));
            cards.Add(new Card(btntriangle2, "triangle"));
            cards.Add(new Card(btnrhombus, "rhombus"));
            cards.Add(new Card(btnrhombus2, "rhombus"));
        }

        // Запуск гри
        private void StartGame()
        {
            Card2.Shuffle(cards);
            foreach (var card in cards)
            {
                card.Show();               
                card.Button.Enabled = false; 
            }

            hideAllTimer.Interval = 10000;      // 10 секунд
            hideAllTimer.Tick += HideAllCards;

            showTimer.Interval = 1000;
            showTimer.Tick -= timer1_Tick_1; 
            showTimer.Tick += timer1_Tick_1;

            timerDo.Interval = 1000;
            timerDo.Tick -= timer1_Tick;
            timerDo.Tick += timer1_Tick;

            foreach (var card in cards)
            {
                card.Button.Click += CardButton_Click; 
            }
            hideAllTimer.Start();
        }

        // Приховати після старту
        private void HideAllCards(object sender, EventArgs e)
        {
            hideAllTimer.Stop();

            foreach (var card in cards) //стають білими
            {
                card.Hide();
                card.Button.Enabled = true; 
            }

            secondsElapsed = 0;
            timerDo.Start();


            if (rbMedium.Checked || rbHard.Checked)
            {
                timeLeft = 60;
                labelTime.Text = $"Час: {timeLeft}";
                timerMedium.Start(); 
            }
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Button clickedButton = sender as Button;
            Card clickedCard = cards.Find(c => c.Button == clickedButton);

            if (clickedCard == null || clickedCard.IsMatched || clickedCard == firstClicked)
                return;

            clickedCard.Show();

            if (firstClicked == null)
            {
                firstClicked = clickedCard;
                return;
            }

            secondClicked = clickedCard;

            Card2.DisableAllButtons(cards);

            // Якщо збіг
            if (firstClicked.ImageKey == secondClicked.ImageKey)
            {
                firstClicked.IsMatched = true;
                secondClicked.IsMatched = true;

                firstClicked.Button.Enabled = false;
                secondClicked.Button.Enabled = false;

                Card2.ResetClicks(ref firstClicked, ref secondClicked, cards);

                if (cards.All(c => c.IsMatched))
                {
                    timerDo.Stop();
                    timerMedium.Stop();
                    MessageBox.Show($"Вітаємо! Ви впорались!\nЧас: {secondsElapsed} сек.");
                }
                else
                {
                    Card2.EnableAllButtons(cards);
                }
            }
            else
            {
                showTimer.Start();
            }
        }

        private void btnRule_Click(object sender, EventArgs e) //правила
        {
            string rules = "Правила гри:\n\n" +
                   "1. Запам'ятайте розташуваня карточок за 10 секунд.\n" +
                   "2. Знайти пару до кожної картки \n";

            MessageBox.Show(rules, "Правила гри", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAbout_Click(object sender, EventArgs e) //складність
        {
            string hard = "Детальніше про складність:\n\n" +
                   "Простий рівень - немає ніяких ускладнень.\n" +
                   "Середній рівень - обмеження в часі (60 секунд) \n" +
                    "Складний рівень - обмеження в часі (60 секунд) + тільки 10 спроб щоб знайти всі пари\n";
            MessageBox.Show(hard, "Складність", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            btnStart.Visible = false;  

            cards.Clear();
            InitCards();
            StartGame();  

            hideAllTimer.Stop();
            hideAllTimer.Start();
        }

        private void ResetGame()
        {
            hideAllTimer.Stop();
            showTimer.Stop();

            secondsElapsed = 0;
            labelElapsedTime.Text = "Час пошуку: 0 сек.";
            timerDo.Stop();

            timerMedium.Stop();

            timerMedium.Tick -= timerMedium_Tick;

            firstClicked = null;
            secondClicked = null;

            foreach (var card in cards)
            {
                card.Hide();
                card.Button.Enabled = true;
                card.IsMatched = false;
            }
            Card2.Shuffle(cards);
        }

        private void timer1_Tick(object sender, EventArgs e) //час проходження - легкий рівень
        {
            secondsElapsed++;
            labelElapsedTime.Text = $"Час пошуку: {secondsElapsed} сек.";
        }

        private void timerMedium_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            labelTime.Text = $"Час: {timeLeft}";

            if (timeLeft <= 0)
            {
                timerMedium.Stop();
                timerDo.Stop();
                Card2.DisableAllButtons(cards);
                MessageBox.Show("Час вичерпано! Ви програли.");
                return;
            }

            if (cards.All(c => c.IsMatched))
            {
                timerMedium.Stop();
                timerDo.Stop();
                MessageBox.Show("Ви впорались з середнім рівнем");
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e) //для затримки перед приховуванням невірної пари
        {
            showTimer.Stop();

            if (firstClicked != null)
                firstClicked.Hide();

            if (secondClicked != null)
                secondClicked.Hide();

            if (rbHard.Checked)
            {
                attemptsLeft--;
                labelAttempt.Text = $"Спроб залишилось: {attemptsLeft}";

                if (attemptsLeft <= 0)
                {
                    Card2.DisableAllButtons(cards);
                    timerMedium.Stop();
                    timerDo.Stop();
                    MessageBox.Show("Спроби вичерпано! Ви програли.");
                    return;
                }
            }

            Card2.ResetClicks(ref firstClicked, ref secondClicked, cards);
            Card2.EnableAllButtons(cards);
        }

        private void rbMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMedium.Checked)
            {
                ResetGame();
                StartGame();

                timerDo.Stop();

                labelTime.Visible = true;
                labelTime.Text = "Час: 60";
                timeLeft = 60;
                labelAttempt.Visible = false;
                hideAllTimer.Stop();
                hideAllTimer.Start();
                
                timerMedium.Stop();   
                timerMedium.Interval = 1000; 
                timerMedium.Tick += timerMedium_Tick;
            }
        }

        private void rbEasy_CheckedChanged(object sender, EventArgs e) //легкий рівень
        {
            if (rbEasy.Checked)
            {
                ResetGame();
                StartGame();

               // timerDo.Start();

                hideAllTimer.Stop();
                hideAllTimer.Start();

                labelAttempt.Visible = false;
                labelTime.Visible = false;
            }
        }

        private void rbHard_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHard.Checked)
            {
                ResetGame();
                StartGame();

                timerDo.Stop();

                attemptsLeft = 10;
                labelAttempt.Text = $"Спроб залишилось: {attemptsLeft}";
                labelAttempt.Visible = true;

                labelTime.Text = "Час: 60";
                labelTime.Visible = true;
                timerDo.Stop();
                timerMedium.Stop();
                timerMedium.Interval = 1000;
                timerMedium.Tick -= timerMedium_Tick;
                timerMedium.Tick += timerMedium_Tick;
            }
        }

        private void FindCard_Load(object sender, EventArgs e)
        {

        }
    }
}
