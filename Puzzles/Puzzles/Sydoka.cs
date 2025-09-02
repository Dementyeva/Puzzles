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
    public partial class Sydoka : Form
    {
        private TextBox[,] textBoxes = new TextBox[9, 9];
        private Sudoku sudoku;

        public Sydoka()
        {
            InitializeComponent();
        }

        private int mistakeCount = 0;
        private const int maxMistakes = 3;

        private int hintCount = 0;
        private const int maxHints = 3;

        private HashSet<(int row, int col)> mistakePositions = new HashSet<(int, int)>();

        private void Sydoka_Load(object sender, EventArgs e)
        {
            sudoku = new Sudoku();
            InitGrid();
        }

        // створення TextBox для кожної клітинки
        private void InitGrid()
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = 9;
            tableLayoutPanel1.RowCount = 9;

            for (int i = 0; i < 9; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11f));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11f));
            }

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var tb = new TextBox //поля для відповідей
                    {
                        MaxLength = 1,
                        TextAlign = HorizontalAlignment.Center,
                        Font = new Font("Arial", 16),
                        Dock = DockStyle.Fill
                    };

                    tb.KeyPress += (s, e) =>
                    {
                        if (!char.IsControl(e.KeyChar) && (e.KeyChar < '1' || e.KeyChar > '9'))
                        {
                            e.Handled = true;
                        }
                    };

                    int value = sudoku.Grid[row, col];
                    if (value != 0)
                    {
                        tb.Text = value.ToString();
                        tb.ReadOnly = true;
                        tb.BackColor = Color.LightGray;
                    }

                    textBoxes[row, col] = tb;
                    tableLayoutPanel1.Controls.Add(tb, col, row);
                }
            }
        }
        // Вимкнути редагування сітки 
        private void DisableGrid()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    textBoxes[row, col].ReadOnly = true;
                }
            }
        }
        private void EnableGrid()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    textBoxes[row, col].ReadOnly = false;
                }
            }
        }
//скидання данних
        private void ResetGame()
        {
            // Обнуляємо кількість помилок
            mistakeCount = 0;
            mistakePositions.Clear();

            label4.Text = $"{mistakeCount} / {maxMistakes}";

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    textBoxes[row, col].Text = "";
                    textBoxes[row, col].BackColor = Color.White;
                }
            }
            EnableGrid();
        }

//кнопка перевірки        
        private void btnCh_Click(object sender, EventArgs e)
        {

            int correctCount = 0;
            int totalEditableCells = 0;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (textBoxes[row, col].ReadOnly) continue;

                    totalEditableCells++;

                    string input = textBoxes[row, col].Text.Trim();
                    if (!int.TryParse(input, out int userValue) || userValue < 1 || userValue > 9)
                    {
                        textBoxes[row, col].BackColor = Color.White;
                        continue;
                    }

                    if (sudoku.IsCorrect(row, col, userValue))
                    {
                        textBoxes[row, col].BackColor = Color.LightGreen;
                        correctCount++;
                        mistakePositions.Remove((row, col));
                    }
                    else
                    {
                        textBoxes[row, col].BackColor = Color.LightCoral;
                        if (!mistakePositions.Contains((row, col)))
                        {
                            mistakeCount++;
                            mistakePositions.Add((row, col));
                        }
                    }
                }
            }
            label4.Text = $"{mistakeCount}/{maxMistakes}";

            // Програш
            if (mistakeCount > maxMistakes)
            {
                MessageBox.Show("На жаль, ви не впорались. Можете проглянути ще раз та перезапустіть гру.", "Гру закінчено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DisableGrid();
                return;
            }


            if (correctCount == totalEditableCells)
            {
                MessageBox.Show("Вітаю, ви впоралися! Можете обрати інший рівень або зіграти ще раз. ", "Перемога",MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisableGrid(); 
            }
        }
//кнопка оновлення 
        private void btnUpd_Click(object sender, EventArgs e)
        {
            int emptyCells = 25; //легкий рівень

            hintCount = 0;
            label5.Text = $"{hintCount}/{maxHints}";


            if (rbMedium.Checked)
                emptyCells = 30;
            else if (rbHard.Checked)
                emptyCells = 40;

            sudoku = new Sudoku(emptyCells);

            mistakeCount = 0;
            hintCount = 0;
            mistakePositions.Clear();

            label4.Text = $"{mistakeCount}/{maxMistakes}";
            label5.Text = $"{hintCount}/{maxHints}";

            ResetGame();
            InitGrid();
        }

//оновлення складності
        private void UpdateGameByDifficulty()
        {
            int emptyCells = 25;

            if (rbMedium.Checked)
                emptyCells = 30;
            else if (rbHard.Checked)
                emptyCells = 40;

            sudoku = new Sudoku(emptyCells);

            mistakeCount = 0;
            hintCount = 0;
            mistakePositions.Clear();

            label4.Text = $"{mistakeCount}/{maxMistakes}";
            label5.Text = $"{hintCount}/{maxHints}";

            InitGrid();
        }

        private void rbEasy_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGameByDifficulty();
        }
//правила
        private void bntRule_Click(object sender, EventArgs e)
        {
            string rules = "Правила Судоку:\n\n" +
                   "1. Заповніть усі білі клітинки цифрами від 1 до 9.\n" +
                   "2. Кожен рядок, кожен стовпець і кожен блок 3x3\n" +
                   "   повинні містити всі цифри від 1 до 9 без повторень.\n"  +
                   " * Ви можете припуститися трьох помилок , а також взяти 3 підказки.\n"+
                   "                                 Успіхів!";

            MessageBox.Show(rules, "Правила гри", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
//кнопка підказки
        private void btnP_Click(object sender, EventArgs e)
        {
            if (hintCount >= maxHints)
            {
                MessageBox.Show("Ви використали всі підказки!", "Підказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //доступні клітинки
            List<(int row, int col)> emptyCells = new List<(int, int)>();

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (!textBoxes[row, col].ReadOnly && string.IsNullOrWhiteSpace(textBoxes[row, col].Text))
                    {
                        emptyCells.Add((row, col));
                    }
                }
            }

            if (emptyCells.Count == 0)
            {
                MessageBox.Show("Немає доступних клітинок для підказки!", "Підказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Випадкова порожня клітинка
            Random rnd = new Random();
            var (r, c) = emptyCells[rnd.Next(emptyCells.Count)];

            // правильне число
            int correctValue = sudoku.Solution[r, c];
            textBoxes[r, c].Text = correctValue.ToString();
            textBoxes[r, c].BackColor = Color.LightBlue;
            textBoxes[r, c].ReadOnly = true;

            hintCount++;
            label5.Text = $"{hintCount}/{maxHints}";
        }
    }
}
