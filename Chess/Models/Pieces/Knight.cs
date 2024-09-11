public class Knight : Piece{
    public Knight(bool isBlack) : base(isBlack) {}

    public override bool ValidateMove(int startX, int startY, int endX, int endY, char[,] board){
        // Calculate the difference in coordinates
        int dx = Math.Abs(startX - endX);
        int dy = Math.Abs(startY - endY);

        // Knight moves in an L-shape: 2+1 or 1+2
        if ((dx == 2 && dy == 1) || (dx == 1 && dy == 2)){
            char destinationPiece = board[endX, endY];
            if (destinationPiece != ' ' && (isBlack == Char.IsLower(destinationPiece))){
                return false;
            }
            return true;
        }

        return false;
    }
}