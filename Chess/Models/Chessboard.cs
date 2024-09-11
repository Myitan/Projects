public class Chessboard{
    private char[,] board = new char[8, 8];
    private Dictionary<char, Piece> pieces = new Dictionary<char, Piece>();
    private bool isWhiteTurn = true;
    private List<char> capturedWhitePieces = new List<char>();
    private List<char> capturedBlackPieces = new List<char>();
    public void Initialize(){
        pieces['P'] = new Pawn(false);
        pieces['p'] = new Pawn(true);
        pieces['N'] = new Knight(false);
        pieces['n'] = new Knight(true);
        pieces['R'] = new Rook(false);
        pieces['r'] = new Rook(true);
        pieces['B'] = new Bishop(false);
        pieces['b'] = new Bishop(true);
        pieces['Q'] = new Queen(false);
        pieces['q'] = new Queen(true);
        pieces['K'] = new King(false);
        pieces['k'] = new King(true);

        // Set up the initial board state
        string initialSetup =
            "rnbqkbnr" +
            "pppppppp" +
            "        " +
            "        " +
            "        " +
            "        " +
            "PPPPPPPP" +
            "RNBQKBNR";

        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                board[i, j] = initialSetup[i * 8 + j];
            }
        }
    }


    public void Display(){       
        // Display the board with row and column labels
        for (int i = 0; i < 8; i++){
            Console.Write(8 - i + "| ");
            for (int j = 0; j < 8; j++){
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("   - - - - - - - -");
        Console.WriteLine("   a b c d e f g h");

        Console.WriteLine("\nCaptured Pieces:");
        Console.WriteLine("White: " + string.Join(" ", capturedWhitePieces));
        Console.WriteLine("Black: " + string.Join(" ", capturedBlackPieces));
    }

    public bool MovePiece(int startX, int startY, int endX, int endY){
        char piece = board[startX, startY];
        Piece pieceObj;
        
        // Retrieve the piece object from the dictionary
        if (!pieces.TryGetValue(piece, out pieceObj)){
            Console.WriteLine("Invalid piece.");
            return false;
        }

        // Validate the move for the piece
        bool isValid = pieceObj.ValidateMove(startX, startY, endX, endY, board);

        if (!isValid){
            Console.WriteLine("Invalid move.");
            return false;
        }

        char destinationPiece = board[endX, endY];
        if (destinationPiece != ' '){
            if (Char.IsUpper(destinationPiece))
                capturedBlackPieces.Add(destinationPiece);
            else
                capturedWhitePieces.Add(destinationPiece);
        }

        // Perform the move
        board[endX, endY] = piece;
        board[startX, startY] = ' ';
        return true;
        isWhiteTurn = !isWhiteTurn;
    }

    private bool IsKingInCheck(bool isWhite){
        // Find the king's position
        char king = isWhite ? 'K' : 'k';
        (int kingX, int kingY) = FindPiece(king);

        if (kingX == -1) return false; // King not found, error case

        // Check if any of the opponent's pieces can attack the king
        foreach (var piece in pieces){
            char pieceChar = piece.Key;
            Piece pieceObj = piece.Value;
            if (Char.IsUpper(pieceChar) != isWhite){
                for (int x = 0; x < 8; x++){
                    for (int y = 0; y < 8; y++){
                        if (pieceObj.ValidateMove(x, y, kingX, kingY, board)){
                            return true; // King is in check
                        }
                    }
                }
            }
        }
        return false;
    }

    private bool IsCheckmate(bool isWhite){
        if (!IsKingInCheck(isWhite)) return false; // King is not in check

        // Check if there are any legal moves to escape check
        for (int x = 0; x < 8; x++){
            for (int y = 0; y < 8; y++){
                if (board[x, y] == (isWhite ? 'P' : 'p') || 
                    board[x, y] == (isWhite ? 'N' : 'n') || 
                    board[x, y] == (isWhite ? 'R' : 'r') || 
                    board[x, y] == (isWhite ? 'B' : 'b') || 
                    board[x, y] == (isWhite ? 'Q' : 'q') || 
                    board[x, y] == (isWhite ? 'K' : 'k')){
                        Piece piece = pieces[board[x, y]];
                        for (int endX = 0; endX < 8; endX++){
                            for (int endY = 0; endY < 8; endY++){
                                if (piece.ValidateMove(x, y, endX, endY, board)){
                                    // Temporarily move the piece
                                    char temp = board[endX, endY];
                                    board[endX, endY] = board[x, y];
                                    board[x, y] = ' ';
                                    if (!IsKingInCheck(isWhite))
                                    {
                                        // Revert the move
                                        board[x, y] = board[endX, endY];
                                        board[endX, endY] = temp;
                                        return false; // There is a move that removes check
                                    }
                                    // Revert the move
                                    board[x, y] = board[endX, endY];
                                    board[endX, endY] = temp;
                                }
                            }
                        }
                }
            }
        }
        return true; // No move can escape check, so it's checkmate
    }

    private bool IsStalemate(bool isWhite){
        if (IsKingInCheck(isWhite)) return false; // Player is in check

        // Check if there are any legal moves
        for (int x = 0; x < 8; x++){
            for (int y = 0; y < 8; y++){
                if (board[x, y] == (isWhite ? 'P' : 'p') || 
                    board[x, y] == (isWhite ? 'N' : 'n') || 
                    board[x, y] == (isWhite ? 'R' : 'r') || 
                    board[x, y] == (isWhite ? 'B' : 'b') || 
                    board[x, y] == (isWhite ? 'Q' : 'q') || 
                    board[x, y] == (isWhite ? 'K' : 'k')){
                        Piece piece = pieces[board[x, y]];
                        for (int endX = 0; endX < 8; endX++)
{
                            for (int endY = 0; endY < 8; endY++)
                            {
                                if (piece.ValidateMove(x, y, endX, endY, board))
                                {
                                    // Temporarily move the piece
                                    char temp = board[endX, endY];
                                    board[endX, endY] = board[x, y];
                                    board[x, y] = ' ';
                                    if (!IsKingInCheck(isWhite))
                                    {
                                        // Revert the move
                                        board[x, y] = board[endX, endY];
                                        board[endX, endY] = temp;
                                        return false; // There is a move available
                                    }
                                    // Revert the move
                                    board[x, y] = board[endX, endY];
                                    board[endX, endY] = temp;
                                }
                            }
                        }
                }
            }
        }
        return true; // No move available, so it's stalemate
    }

    private (int, int) FindPiece(char piece){
        for (int x = 0; x < 8; x++){
            for (int y = 0; y < 8; y++){
                if (board[x, y] == piece)
                    return (x, y);
            }
        }
        return (-1, -1); // Piece not found
    }
    public (int, int) ParsePosition(string position){
        int row = 8 - int.Parse(position[1].ToString());
        int col = position[0] - 'a';
        Console.WriteLine($"Parsed position: {position} -> ({row}, {col})");
        return (row, col);
    }

    private bool IsWithinBoard(int x, int y){
        return x >= 0 && x < 8 && y >= 0 && y < 8;
    }
}
