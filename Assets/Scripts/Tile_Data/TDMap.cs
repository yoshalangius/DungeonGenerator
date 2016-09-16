public class TDMap
{
    int Size_x;
    int Size_Y;

    int[,] Map_Data;

    public TDMap(int Size_X, int Size_Y)
    {
        this.Size_x = Size_X;
        this.Size_x = Size_Y;

        Map_Data = new int[Size_X,Size_Y];
    }


}