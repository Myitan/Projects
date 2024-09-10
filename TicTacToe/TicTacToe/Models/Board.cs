public class Board{
    private char[] cells = new char[9];

    public Board(){
        for (int i = 0; i < cells.Length; i++){
            cells[i] = '-';
        }
    }

    public char this[int index]
    {
        get { return cells[index]; }
        set { cells[index] = value; }
    }

    public bool IsFull() => Array.IndexOf(cells, '-') == -1;

    public bool IsWinner(char symbol){
        // Check rows
        for (int row = 0; row < 9; row += 3){
            if (cells[row] == symbol && cells[row + 1] == symbol && cells[row + 2] == symbol){
                return true;
            }
        }

        // Check columns
        for (int col = 0; col < 3; col++){
            if (cells[col] == symbol && cells[col + 3] == symbol && cells[col + 6] == symbol){
                return true;
            }
        }

        // Check diagonals
        if (cells[0] == symbol && cells[4] == symbol && cells[8] == symbol){
            return true;
        }
        if (cells[2] == symbol && cells[4] == symbol && cells[6] == symbol){
            return true;
        }

        return false;
    }
    public void Display(){
        Console.WriteLine(" {0} | {1} | {2} ", cells[0], cells[1], cells[2]);
        Console.WriteLine("---|---|---");
        Console.WriteLine(" {0} | {1} | {2} ", cells[3], cells[4], cells[5]);
        Console.WriteLine("---|---|---");
        Console.WriteLine(" {0} | {1} | {2} ", cells[6], cells[7], cells[8]);
    }
}
