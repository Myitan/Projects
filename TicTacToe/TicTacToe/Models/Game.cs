public class Game{
    private Board board;
    private Player player1;
    private Player player2;
    private Player currentPlayer;

    public Game(){
        board = new Board();
        player1 = new Player('X');
        player2 = new Player('O');
        currentPlayer = player1;
    }

    public void Play(){
        while (!board.IsFull() && !board.IsWinner(currentPlayer.Symbol)){
            board.Display();
            Console.WriteLine($"{currentPlayer.Symbol}'s turn. Choose a position (1-9):");
            string input = Console.ReadLine();
            int position = int.Parse(input) - 1;

            if (string.IsNullOrWhiteSpace(input)){
                Console.WriteLine("Empty input is not allowed. Try again.");
                continue;
            }

            if (position >= 0 && position < 9 && board[position] == '-'){
                board[position] = currentPlayer.Symbol;
                if (board.IsWinner(currentPlayer.Symbol)){
                    board.Display();
                    Console.WriteLine($"{currentPlayer.Symbol} wins!");
                    return;
                }
                currentPlayer = currentPlayer == player1 ? player2 : player1;
            }
            else{
                Console.WriteLine("Invalid move. Try again.");
            }
        }

        board.Display();
        if (!board.IsWinner(player1.Symbol) && !board.IsWinner(player2.Symbol)){
            Console.WriteLine("It's a draw!");
        }
    }
}
