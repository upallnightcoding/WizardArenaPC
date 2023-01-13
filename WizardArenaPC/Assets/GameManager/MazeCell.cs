using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell 
{
    public bool BeenVisited { get; private set; } = false;

    public bool NorthWall { get; set; } = true;
    public bool SouthWall { get; set; } = true;
    public bool EastWall { get; set; } = true;
    public bool WestWall { get; set; } = true;

    public int Col { get; private set; }
    public int Row { get; private set; }

    public MazeCell(int col, int row) 
    {
        this.Col = col;
        this.Row = row;
    }

    public void MarkAsVisited() 
    {
        BeenVisited = true;
    }

    // public override string ToString()
    // {
    //     return $"Col: {Col},Row: {Row} - Visted: {BeenVisited}";
    // }

    public void CollapseWall(MazeCell neighbor)
    {
        int col = neighbor.Col - Col;
        int row = neighbor.Row - Row;

        if (col == 0) 
        {
            if (row == 1) 
            {
                NorthWall = false;
                neighbor.SouthWall = false;
            } else {
                SouthWall = false;
                neighbor.NorthWall = false;
            }
        }

        if (row == 0)
        {
            if (col == 1) 
            {
                EastWall = false;
                neighbor.WestWall = false;
            } else {
                WestWall = false;
                neighbor.EastWall = false;
            }
        }
    }
}
