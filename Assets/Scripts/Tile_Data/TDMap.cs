using UnityEngine;
using System.Collections.Generic;
public class TDMap
{

    protected class DRoom
    {
        public int left;
        public int top;
        public int width;
        public int height;

        public int Right { get{ return left + width - 1; } }
        public int Bottom { get { return top + height - 1; } }
        public int Center_X { get { return left + width / 2; } }
        public int Center_Y { get { return top + height / 2; } }

        public bool CollidesWith(DRoom other)
        {
            if (left > other.Right)
            {
                return false;
            }
            if (top > other.Bottom)
            {
                return false;
            }
            if (Right < other.left)
            {
                return false;
            }
            if (Bottom < other.height)
            {
                return false;
            }
            return true;
        }
    }

    int Size_x;
    int Size_Y;

    int[,] Map_Data;

    List<DRoom> rooms;

    /*
    * 0 = underground
    * 1 = wall
    * 2 = ground
    * 3 = door
    */

    public TDMap(int Size_X, int Size_Y)
    {
        this.Size_x = Size_X;
        this.Size_x = Size_Y;

        Map_Data = new int[Size_X,Size_Y];

        rooms = new List<DRoom>();

        for (int i = 0; i < 10; i++)
        {
            int RSX = Random.Range(4,8);
            int RSY = Random.Range(4, 8);

            DRoom r = new DRoom();
            r.left = Random.Range(0, Size_x - RSX);
            r.top = Random.Range(0, Size_Y - RSY);
            r.width = RSX;
            r.height = RSY;

            if (!RoomCollides(r))
            {
                rooms.Add(r);
                
            }

            foreach (DRoom r2 in rooms)
            {
                MakeRoom(r2);
            }
            //BuildCorridor(rooms[0], rooms[1]);
        }

        
    }

    bool RoomCollides(DRoom r)
    {
        foreach (DRoom r2 in rooms)
        {
            if (r.CollidesWith(r2))
            {
                return true;
            }

        }
        return false;
    }

    public int GetTileAt(int x, int y)
    {
        return Map_Data[x, y];
    }

    void MakeRoom(DRoom r)
    {
        Debug.Log(Map_Data);
        for (int x=0; x < r.width; x++)
        {
            for (int y = 0; y < r.height; y++)
            {
                if (x == 0 || x == r.width - 1 || y == 0 || y == r.height - 1)
                {
                    Debug.Log(x);
                    //Map_Data[left+x,top+y] = 2;
                    Map_Data[r.left + x, r.top + y] = 1;
                }
                else {
                    Map_Data[r.left + x, r.top + y] = 2;
                }

            }
        }

    }

    void BuildCorridor(DRoom r1, DRoom r2)
    {
        int x = r1.Center_X;
        int y = r1.Center_Y;

        while (x != r2.Center_X)
        {
            while (y != r2.Center_Y)
            {
                Map_Data[x, y] = 1;

                y += y < r2.Center_Y ? 1 : -1;
            }
            x += x < r2.Center_X ? 1 : -1;
        }
    }
    

}