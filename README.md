Puzzles is a Windows Forms application written in C# that combines multiple mini-games in one project.
The application includes:
  🧮 Arithmetic Puzzle
  🔢 Sudoku
  🃏 Find the Pair (Memory Card Game)
  🎵 Background Sound Control
  🌙 Light/Dark Theme Switching

The main form acts as a navigation hub for all mini-games.

Sudoku Game
📌 Description
  A fully functional 9×9 Sudoku game with:
  Difficulty levels (Easy / Medium / Hard)
  Mistake tracking (maximum 3 mistakes)
  Hint system (maximum 3 hints)
  Input validation
  Custom grid drawing
  Sound & theme support

Game Logic
-Sudoku grid is dynamically generated.
Pre-filled cells are:
-Read-only
-Highlighted in yellow
Editable cells:
-Validate input (1–9 only)
Highlight:
  🟢 Green — correct
  🔴 Red — incorrect
  🔵 Blue — hint cell

🧮 Arithmetic Puzzle
📌 Description
A logic puzzle where players must restore missing arithmetic operators (+, -, *, /) in a grid so that:
Each row produces the correct result
Each column produces the correct result

🧠 How It Works
  Numbers are pre-generated.
  Result labels are shown:
  Blue → Row results
  Brown → Column results
  User inputs operators into empty cells.
  System checks correctness against solution.

🃏 Find the Pair
A memory card matching game .
Players must match identical card pairs.

🛠 Technologies
    C#
    .NET Framework
    Windows Forms
    System.Media (SoundPlayer)
    GDI+ Drawing
    TableLayoutPanel dynamic grid

![Game Screenshot](1.jpg)
![Game Screenshot](2.png)
![Game Screenshot](3.jpg)
![Game Screenshot](4.png)
