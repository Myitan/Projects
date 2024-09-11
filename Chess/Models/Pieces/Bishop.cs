public class Bishop : Piece{
    public Bishop(bool isBlack) : base(isBlack) {}

    public override bool ValidateMove(int startX, int startY, int endX, int endY, char[,] board){
        int dx = Math.Abs(startX - endX);
        int dy = Math.Abs(startY - endY);

        // Bishop moves diagonally, so the difference in x and y coordinates should be equal
        if (dx == dy){
            // Check for obstacles along the diagonal path
            int stepX = (endX - startX) > 0 ? 1 : -1;
            int stepY = (endY - startY) > 0 ? 1 : -1;
            int x = startX + stepX;
            int y = startY + stepY;

            while (x != endX && y != endY){
                if (board[x, y] != ' ')
                {
                    return false; // There's a piece blocking the path
                }
                x += stepX;
                y += stepY;
            }

            char destinationPiece = board[endX, endY];
            if (destinationPiece != ' ' && (isBlack == Char.IsLower(destinationPiece))){
                return false;
            }
            return true;
        }
        return false;
    }
}
