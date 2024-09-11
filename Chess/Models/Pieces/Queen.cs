public class Queen : Piece{
    public Queen(bool isBlack) : base(isBlack) {}

    public override bool ValidateMove(int startX, int startY, int endX, int endY, char[,] board){
        // Check if move is horizontal
        if (startX == endX){
            return IsClearPath(startX, startY, endX, endY, board, true);
        }

        // Check if move is vertical
        if (startY == endY){
            return IsClearPath(startX, startY, endX, endY, board, false);
        }

        // Check if move is diagonal
        if (Math.Abs(startX - endX) == Math.Abs(startY - endY)){
            return IsClearPath(startX, startY, endX, endY, board, true, true);
        }

        return false;
    }

    private bool IsClearPath(int startX, int startY, int endX, int endY, char[,] board, bool isLine = false, bool isDiagonal = false){
        int stepX = 0;
        int stepY = 0;

        if (isLine){
            if (endX > startX) stepX = 1;
            if (endX < startX) stepX = -1;
            if (endY > startY) stepY = 1;
            if (endY < startY) stepY = -1;

            if (stepX != 0 && stepY == 0) // Horizontal movement
                for (int x = startX + stepX; x != endX; x += stepX){
                    if (board[x, startY] != ' ')
                        return false;
                }
            else if (stepX == 0 && stepY != 0) // Vertical movement
                for (int y = startY + stepY; y != endY; y += stepY){
                    if (board[startX, y] != ' ')
                        return false;
                }
        }
        else if (isDiagonal){
            stepX = (endX > startX) ? 1 : -1;
            stepY = (endY > startY) ? 1 : -1;

            for (int x = startX + stepX, y = startY + stepY; x != endX && y != endY; x += stepX, y += stepY){
                if (board[x, y] != ' ')
                    return false;
            }
        }

        char destinationPiece = board[endX, endY];
        if (destinationPiece != ' ' && (isBlack == Char.IsLower(destinationPiece)))
        {
            return false;
        }
        return true;
    }
}