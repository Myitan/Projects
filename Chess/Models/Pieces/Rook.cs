public class Rook : Piece{
    public Rook(bool isBlack) : base(isBlack) {}

public override bool ValidateMove(int startX, int startY, int endX, int endY, char[,] board)
{
    // Check if move is horizontal or vertical
    if (startX != endX && startY != endY)
    {
        return false;
    }

    // Horizontal movement
    if (startX == endX)
    {
        int step = (endY - startY) > 0 ? 1 : -1;
        for (int y = startY + step; y != endY; y += step)
        {
            if (y < 0 || y >= 8 || board[startX, y] != ' ')
            {
                return false;
            }
        }
    }
    // Vertical movement
    else if (startY == endY)
    {
        int step = (endX - startX) > 0 ? 1 : -1;
        for (int x = startX + step; x != endX; x += step)
        {
            if (x < 0 || x >= 8 || board[x, startY] != ' ')
            {
                return false;
            }
        }
    }

    // Check if destination is occupied by a piece of the same color
    char destinationPiece = board[endX, endY];
    if (destinationPiece != ' ' && (isBlack == Char.IsLower(destinationPiece)))
    {
        return false;
    }

    return true;
}   

}