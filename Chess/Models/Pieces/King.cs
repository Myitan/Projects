public class King : Piece{
    public King(bool isBlack) : base(isBlack) {}

    public override bool ValidateMove(int startX, int startY, int endX, int endY, char[,] board){
        int dx = Math.Abs(startX - endX);
        int dy = Math.Abs(startY - endY);

        // King moves one square in any direction, so dx and dy should both be 0 or 1
        if (dx <= 1 && dy <= 1){
            char destinationPiece = board[endX, endY];
            if (destinationPiece != ' ' && (isBlack == Char.IsLower(destinationPiece))){
                return false;
            }
            return true;
        }

        return false;
    }
}
