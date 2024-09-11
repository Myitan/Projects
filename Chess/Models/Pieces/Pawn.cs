public class Pawn : Piece{
    public Pawn(bool isBlack) : base(isBlack) {}

    public override bool ValidateMove(int startX, int startY, int endX, int endY, char[,] board){
        int direction = isBlack ? 1 : -1; // Black pawns move down, white pawns move up

        // Basic pawn move validation
        if (startY == endY){
            if (endX == startX + direction && board[endX, endY] == ' ') return true;
            if (startX == (isBlack ? 1 : 6) && endX == startX + 2 * direction && board[endX, endY] == ' ' && board[startX + direction, startY] == ' ') return true;
        }else if(Math.Abs(startX - endX) == 1 && Math.Abs(startY - endY) == 1){
            char destinationPiece = board[endX, endY];
            if (destinationPiece != ' ' && (isBlack != Char.IsLower(destinationPiece)))
                return true;
        }
        return false;
    }
}
