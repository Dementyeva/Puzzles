using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
    public partial class Arithmetic : Form
    {
        private TextBox[,] operators;
        private ArithmeticPuzzles puzzle;
        private bool[,] isHint; //де підказка
        private int hintCount; // використані підказки
        private int maxHints = 6;


        private SoundPlayer player;
        private bool isSoundOn = false;
        private bool isDarkTheme = false;

        FormMain menuform = null;

        public void SetFormMain(FormMain menuform)
        {
            this.menuform = menuform;
        }

        public Arithmetic()
        {
            InitializeComponent();
            puzzle = new ArithmeticPuzzles();
        }

        private void Arithmetic_Load(object sender, EventArgs e)
        {
            InitializeOperators();
            DisplayPuzzle();

            hintCount = 0;
            label1.Text = $"Підказки: {maxHints - hintCount}";

            player = new SoundPlayer("sound4.wav");

            if (isSoundOn)
            {
                player.PlayLooping();
                btnSound.BackgroundImage = Properties.Resources.sound_on;
            }
            else
            {
                btnSound.BackgroundImage = Properties.Resources.sound_off;
            }

            btnSound.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void OperatorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] allowedChars = { '+', '-', '*', '/' };
            TextBox box = sender as TextBox;

            if (!char.IsControl(e.KeyChar) &&
                (!allowedChars.Contains(e.KeyChar) || box.Text.Length >= 1))
            {
                e.Handled = true;
            }
        }

        private void DisplayPuzzle()
        {
            int size = puzzle.Size;

            for (int i = 0; i < size - 1; i += 2)
            {
                for (int j = 0; j < size - 1; j += 2)
                {
                    var lbl = new Label
                    {
                        Text = puzzle.Numbers[i, j].ToString(),
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        Font = new Font("Arial", 14, FontStyle.Bold)
                    };
                    tableLayoutPanel1.Controls.Add(lbl, j, i);
                }
            }

            //результати рядків
            for (int i = 0; i < size - 1; i += 2)
            {
                var lbl = new Label
                {
                    Text = puzzle.RowResults[i].ToString(),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.Blue
                };
                tableLayoutPanel1.Controls.Add(lbl, size - 1, i);
            }

            //результати стовпчиків
            for (int j = 0; j < size - 1; j += 2)
            {
                var lbl = new Label
                {
                    Text = puzzle.ColResults[j].ToString(),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.Brown
                };
                tableLayoutPanel1.Controls.Add(lbl, j, size - 1);
            }
        }
        private void InitializeOperators()
        {
            int size = puzzle.Size;
            operators = new TextBox[size, size];
            isHint = new bool[size, size];

            operators[0, 1] = txtOp01;
            operators[0, 3] = txtOp03;
            operators[2, 1] = txtOp21;
            operators[2, 3] = txtOp23;
            operators[4, 1] = txtOp41;
            operators[4, 3] = txtOp43;

            operators[1, 0] = txtOp10;
            operators[1, 2] = txtOp12;
            operators[1, 4] = txtOp14;
            operators[3, 0] = txtOp30;
            operators[3, 2] = txtOp32;
            operators[3, 4] = txtOp34;

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            foreach (var txt in operators)
                if (txt != null)
                    txt.BackColor = Color.White;

            if (puzzle.CheckAnswers(operators))
                MessageBox.Show("Вітаю! Ви впорались!");
        }
        

        private void btnAnswer_Click(object sender, EventArgs e)//підказки
        {
            if (hintCount >= maxHints)
            {
                MessageBox.Show("Ви використали всі підказки!");
                return;
            }

            var (row, col, hint) = puzzle.GetRandomHint(operators);

            if (row >= 0 && col >= 0)
            {
                operators[row, col].Text = hint;
                operators[row, col].BackColor = Color.LightYellow;
                hintCount++;
                label1.Text = $"Підказки: {maxHints - hintCount}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rules = "Правила гри:\n\n" +
                  "Потрібно відновити відсутні арифметичні оператори (+, -, *, /) у сітці так, щоб обчислення" +
                  " по кожному рядку та стовпчику давали вказаний результат.\n";


            MessageBox.Show(rules, "Правила гри", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void btnRestart_Click(object sender, EventArgs e)
        {
            puzzle = new ArithmeticPuzzles();

            //очищення Label
            for (int i = tableLayoutPanel1.Controls.Count - 1; i >= 0; i--)
            {
                var control = tableLayoutPanel1.Controls[i];
                if (control is Label)
                {
                    tableLayoutPanel1.Controls.RemoveAt(i);
                    control.Dispose();
                }
            }

            InitializeOperators();
            DisplayPuzzle();

            foreach (var txt in operators)
            {
                if (txt != null)
                {
                    txt.Clear();
                    txt.BackColor = Color.White;
                }
            }

            hintCount = 0;
            label1.Text = $"Підказки: {maxHints - hintCount}";
        }
       
        private void btnSound_Click(object sender, EventArgs e)
        {
            puzzle.ToggleSound(ref isSoundOn, player, btnSound);
        }
        private void ApplyTheme()   //tema
        {
            puzzle.ApplyTheme(this, isDarkTheme, Properties.Resources.white, Properties.Resources.black, btnTheme);
        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            isDarkTheme = !isDarkTheme;
            ApplyTheme();
        }

        private void Arithmetic_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (player != null)
            {
                player.Stop();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            menuform.Show();
            this.Close();
        }
    }
}
