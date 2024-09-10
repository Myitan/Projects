using System;

public class Tetris {
    public int[,] Shape { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Rotation { get; set; }
    const int GridWidth = 10;
    const int GridHeight = 20;
    private int[,] grid = new int[GridWidth, GridHeight];
    
    private static int[][,] tetrominoes = new int[][,]
    {
        new int[,] { {1, 1}, {1, 1} },           // O piece
        new int[,] { {1, 1, 1, 1} },             // I piece
        new int[,] { {0, 1, 1}, {1, 1, 0} },     // S piece
        new int[,] { {1, 1, 0}, {0, 1, 1} },     // Z piece
        new int[,] { {0, 1, 0}, {1, 1, 1} },     // T piece
        new int[,] { {1, 0, 0}, {1, 1, 1} },     // J piece
        new int[,] { {0, 0, 1}, {1, 1, 1} }      // L piece
    };

    private int score = 0;
    private int level = 1;
    private int linesCleared = 0;
    private int linesForNextLevel = 10;

    // The only constructor needed: initializes the game
    public Tetris() {
        SpawnNewPiece();
    }

    public void Rotate() {
        int[,] rotatedShape = new int[Shape.GetLength(1), Shape.GetLength(0)];
        for (int y = 0; y < Shape.GetLength(1); y++) {
            for (int x = 0; x < Shape.GetLength(0); x++) {
                rotatedShape[y, Shape.GetLength(0) - x - 1] = Shape[x, y];
            }
        }

        if (!CheckCollision(rotatedShape, X, Y)) {
            Shape = rotatedShape;
        }
    }

    public void MoveLeft() {
        if (!CheckCollision(Shape, X - 1, Y)) {
            X--;
        }
    }

    public void MoveRight() {
        if (!CheckCollision(Shape, X + 1, Y)) {
            X++;
        }
    }

    public void MoveDown() {
        if (!CheckCollision(Shape, X, Y + 1)) {
            Y++;
        } else {
            LockPiece();
            CheckLines();
            if (CheckGameOver()) {
                return;
            }
            SpawnNewPiece();
        }
    }

    private bool CheckCollision(int[,] shape, int x, int y) {
        for (int i = 0; i < shape.GetLength(1); i++) {
            for (int j = 0; j < shape.GetLength(0); j++) {
                if (shape[j, i] != 0 &&
                    (x + j < 0 || x + j >= GridWidth || y + i >= GridHeight || grid[x + j, y + i] != 0)) {
                    return true;
                }
            }
        }
        return false;
    }

    private void LockPiece() {
        for (int i = 0; i < Shape.GetLength(1); i++) {
            for (int j = 0; j < Shape.GetLength(0); j++) {
                if (Shape[j, i] != 0) {
                    grid[X + j, Y + i] = Shape[j, i];
                }
            }
        }
    }

    private void CheckLines() {
        for (int y = GridHeight - 1; y >= 0; y--) {
            bool lineFilled = true;
            for (int x = 0; x < GridWidth; x++) {
                if (grid[x, y] == 0) {
                    lineFilled = false;
                    break;
                }
            }

            if (lineFilled) {
                ClearLine(y);
                score += 100;
                linesCleared++;
                if (linesCleared >= linesForNextLevel) {
                    level++;
                    linesCleared = 0;
                    linesForNextLevel += 10;
                }
                y++;
            }
        }
    }

    private void ClearLine(int line) {
        for (int y = line; y > 0; y--) {
            for (int x = 0; x < GridWidth; x++) {
                grid[x, y] = grid[x, y - 1];
            }
        }

        for (int x = 0; x < GridWidth; x++) {
            grid[x, 0] = 0;
        }
    }

    private bool CheckGameOver() {
        for (int x = 0; x < GridWidth; x++) {
            if (grid[x, 0] != 0) {
                return true;
            }
        }
        return false;
    }

    private void SpawnNewPiece() {
        Random rand = new Random();
        Shape = tetrominoes[rand.Next(0, tetrominoes.Length)];
        X = GridWidth / 2 - Shape.GetLength(0) / 2;
        Y = 0;
    }

    private void DrawGrid() {
        Console.Clear();
        for (int y = 0; y < GridHeight; y++) {
            for (int x = 0; x < GridWidth; x++) {
                // Draw the grid with existing pieces
                if (grid[x, y] != 0) {
                    Console.Write("#");
                } else {
                    // Draw the falling piece
                    bool isPieceCell = false;
                    for (int py = 0; py < Shape.GetLength(1); py++) {
                        for (int px = 0; px < Shape.GetLength(0); px++) {
                            if (Shape[px, py] != 0 && x == X + px && y == Y + py) {
                                isPieceCell = true;
                                break;
                            }
                        }
                        if (isPieceCell) break;
                    }
                    Console.Write(isPieceCell ? "#" : ".");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Score: {score}, Level: {level}");
    }


    private void HandleInput() {
        if (Console.KeyAvailable) {
            var key = Console.ReadKey(true).Key;
            switch (key) {
                case ConsoleKey.A:
                    MoveLeft();
                    break;
                case ConsoleKey.D:
                    MoveRight();
                    break;
                case ConsoleKey.W:
                    Rotate();
                    break;
                case ConsoleKey.S:
                    MoveDown();
                    break;
            }
        }
    }

    public void StartGame() {
        DateTime lastFallTime = DateTime.Now;
        int fallSpeed = 500;

        while (!CheckGameOver()) {
            DrawGrid();
            HandleInput();

            fallSpeed = Math.Max(50, 500 - (level * 50));

            if ((DateTime.Now - lastFallTime).TotalMilliseconds >= fallSpeed) {
                MoveDown();
                lastFallTime = DateTime.Now;
            }

            System.Threading.Thread.Sleep(50);
        }

        Console.WriteLine("Game Over! Final Score: " + score);
    }
}
