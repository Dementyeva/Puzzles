using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

//судока
//знайти пару
//Головоломки з арифметики 

namespace Puzzles
{
//-----------------------------------------------------------------------
    public class Sudoku
    {
        public int[,] Grid { get; private set; } = new int[9, 9];         // Ігрова сітка
        public int[,] Solution { get; private set; } = new int[9, 9];     // Розв'язана сітка

        private Random rand = new Random();

        public Sudoku(int emptyCells = 25)
        {
            GeneratePuzzle(emptyCells);
        }

        

        public void GeneratePuzzle(int emptyCells)
        {
            Grid = new int[9, 9];
            FillDiagonalBlocks();
            SolveGrid(Grid);
            Solution = (int[,])Grid.Clone(); 
            RemoveNumbers(emptyCells);       
        }

        private void FillDiagonalBlocks()
        {
            for (int i = 0; i < 9; i += 3)
                FillBlock(i, i);
        }

        private void FillBlock(int row, int col)
        {
            int num;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    do
                    {
                        num = rand.Next(1, 10);
                    } while (!IsSafeInBlock(row, col, num));

                    Grid[row + i, col + j] = num;
                }
        }


        private bool IsSafeInBlock(int rowStart, int colStart, int num)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (Grid[rowStart + i, colStart + j] == num)
                        return false;
            return true;
        }


        public bool SolveGrid(int[,] grid)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (grid[row, col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsSafe(grid, row, col, num))
                            {
                                grid[row, col] = num;
                                if (SolveGrid(grid))
                                    return true;
                                grid[row, col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsSafe(int[,] grid, int row, int col, int num)
        {
            for (int x = 0; x < 9; x++)
                if (grid[row, x] == num || grid[x, col] == num)
                    return false;

            int startRow = row - row % 3;
            int startCol = col - col % 3;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (grid[startRow + i, startCol + j] == num)
                        return false;

            return true;
        }

        private void RemoveNumbers(int count)
        {
            while (count > 0)
            {
                int row = rand.Next(9);
                int col = rand.Next(9);

                if (Grid[row, col] != 0)
                {
                    Grid[row, col] = 0;
                    count--;
                }
            }
        }

        public bool IsCorrect(int row, int col, int userValue)
        {
            return Solution[row, col] == userValue;
        }

        public bool IsCompletedCorrectly(int[,] userGrid)
        {
            for (int row = 0; row < 9; row++)
                for (int col = 0; col < 9; col++)
                    if (userGrid[row, col] != Solution[row, col])
                        return false;
            return true;
        }

        public (int row, int col, int value)? GetHint(int[,] currentGrid)
        {
            List<(int row, int col)> empty = new List<(int, int)>();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (currentGrid[row, col] == 0)
                    {
                        empty.Add((row, col));
                    }
                }
            }

            if (empty.Count == 0) return null;

            var (r, c) = empty[rand.Next(empty.Count)];
            return (r, c, Solution[r, c]);
        }
    }
//-------------------------------------------------------------------
    public class Card  //знайти другу карточку
    {
        public Button Button { get; private set; }
        public string ImageKey { get; private set; }
        public bool IsMatched { get; set; }

        private Image originalImage;

        public Card(Button button, string key)
        {
            Button = button;
            ImageKey = key;
            originalImage = button.BackgroundImage;
        }

        // Відкрити картку
        public void Show()
        {
            Button.BackgroundImage = originalImage;
        }

        // Закрити картку
        public void Hide()
        {
            Button.BackgroundImage = null;
            Button.BackColor = Color.White;
        }
    }
//-------------------------------------------------------------------------
    public static class Card2 //знайти другу карточку
    {
        public static void Shuffle(List<Card> list) // Перемішування карток
        {
            Random rand = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Button.Location = new Point(50 + (i % 4) * 100, 50 + (i / 4) * 100);
            }
        }

        public static void ResetClicks(ref Card firstClicked, ref Card secondClicked, List<Card> cards) // Скинути вибрані картки
        {
            firstClicked = null;
            secondClicked = null;
            EnableAllButtons(cards);
        }

        public static void DisableAllButtons(List<Card> cards) // Вимкнути кнопки
        {
            foreach (var card in cards)
            {
                card.Button.Enabled = false;
            }
        }

        public static void EnableAllButtons(List<Card> cards) // Увімкнути всі незнайдені картки
        {
            foreach (var card in cards)
            {
                if (!card.IsMatched)
                    card.Button.Enabled = true;
            }
        }
    }

//-----------------------------------------------------------------------------------
    public class ArithmeticPuzzles //Головоломки з арифметики 
    {
        public int[,] Numbers { get; private set; }
        public int[] RowResults { get; private set; }
        public int[] ColResults { get; private set; }
        public int Size { get; private set; } // 6x6 сітка
        public string[,] RowOperators { get; private set; }
        public string[,] ColOperators { get; private set; }
        private int maxNumber;
        private Random random = new Random();

        public ArithmeticPuzzles()
        {
            Size = 6;
            Numbers = new int[Size, Size];
            RowResults = new int[Size];
            ColResults = new int[Size];
            RowOperators = new string[Size, Size];
            ColOperators = new string[Size, Size];

            GeneratePuzzle();
        }

        public void GeneratePuzzle()
        {
            for (int i = 0; i < Size; i += 2)
            {
                for (int j = 0; j < Size; j += 2)
                {
                    Numbers[i, j] = random.Next(1, 10); 
                }
            }

            //оператори для рядків і обчислюємо результати
            for (int i = 0; i < Size; i += 2)
            {
                int result = Numbers[i, 0];
                for (int j = 1; j < Size - 1; j += 2)
                {
                    int nextNumber = Numbers[i, j + 1];
                    string op = GetValidOperator(result, nextNumber);

                    RowOperators[i, j] = op;
                    result = Calculate(result, nextNumber, op);
                }
                RowResults[i] = result;
            }

            //оператори для стовпців і обчислюємо результати
            for (int j = 0; j < Size; j += 2)
            {
                int result = Numbers[0, j];
                for (int i = 1; i < Size - 1; i += 2)
                {
                    int nextNumber = Numbers[i + 1, j];
                    string op = GetValidOperator(result, nextNumber);

                    ColOperators[i, j] = op;
                    result = Calculate(result, nextNumber, op);
                }
                ColResults[j] = result;
            }
        }

        private string GetValidOperator(int a, int b)
        {
            List<string> possibleOps = new List<string>();

            possibleOps.Add("+");
            possibleOps.Add("*");
            if (a - b >= 0)
                possibleOps.Add("-");
            if (b != 0 && a % b == 0)
                possibleOps.Add("/");

            return possibleOps[random.Next(possibleOps.Count)];
        }

        public int Calculate(int a, int b, string op)
        {
            switch (op)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "*":
                    return a * b;
                case "/":
                    if (b == 0)
                        throw new Exception("Division by zero!");
                    return a / b; 
                default:
                    throw new Exception($"Невідомий оператор: {op}");
            }
        }

        public int CalculateExpression(string expression)
        {
            var parts = new List<string>();
            string currentNumber = "";

            foreach (var c in expression)
            {
                if (char.IsDigit(c))
                {
                    currentNumber += c; 
                }
                else
                {
                    if (currentNumber != "")
                    {
                        parts.Add(currentNumber);
                        currentNumber = "";
                    }
                    parts.Add(c.ToString()); 
                }
            }

            if (currentNumber != "")
            {
                parts.Add(currentNumber); 
            }

            // Спочатку ділення та множення 
            for (int i = 1; i < parts.Count; i += 2)
            {
                if (parts[i] == "*" || parts[i] == "/")
                {
                    int a = int.Parse(parts[i - 1]);
                    int b = int.Parse(parts[i + 1]);
                    int result = Calculate(a, b, parts[i]);
                    parts[i - 1] = result.ToString();
                    parts.RemoveRange(i, 2); 
                    i -= 2; 
                }
            }

            //додавання та віднімання
            int finalResult = int.Parse(parts[0]);
            for (int i = 1; i < parts.Count; i += 2)
            {
                int b = int.Parse(parts[i + 1]);
                finalResult = Calculate(finalResult, b, parts[i]);
            }

            return finalResult;
        }
        

        public (int, int, string) GetRandomHint(TextBox[,] operators)
        {
            var emptyPositions = new List<(int, int)>();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (operators[i, j] != null && string.IsNullOrWhiteSpace(operators[i, j].Text))
                    {
                        emptyPositions.Add((i, j));
                    }
                }
            }
            if (emptyPositions.Count == 0)
                return (-1, -1, "");

            var (row, col) = emptyPositions[random.Next(emptyPositions.Count)];
            string hint = "";

            if (row % 2 == 0 && col % 2 == 1)
                hint = RowOperators[row, col];
            else if (row % 2 == 1 && col % 2 == 0)
                hint = ColOperators[row, col];

            return (row, col, hint);
        }
    }


}

