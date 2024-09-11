public abstract class Piece{
    protected bool isBlack;

    protected Piece(bool isBlack){
        this.isBlack = isBlack;
    }

    public abstract bool ValidateMove(int startX, int startY, int endX, int endY, char[,] board);
}
