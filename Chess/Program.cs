class Program{
    static void Main(string[] args){
        Chessboard chessboard = new Chessboard();
        chessboard.Initialize();
        while (true){
            chessboard.Display();
            Console.WriteLine("Enter your move (e.g., 'e2 e4'): ");
            string input = Console.ReadLine();

            // Split the input into start and end positions
            string[] parts = input.Split(' ');
            if (parts.Length != 2){
                Console.WriteLine("Invalid input. Please enter a move in the format 'e2 e4'.");
                continue;
            }
            var (startX, startY) = chessboard.ParsePosition(parts[0]);
            var (endX, endY) = chessboard.ParsePosition(parts[1]);
            chessboard.MovePiece(startX, startY, endX, endY);
        }
        
    }
}