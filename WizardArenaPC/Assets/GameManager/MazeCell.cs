using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell 
{
    public MazeCellType type { get; private set; } = MazeCellType.UNVISITED;

    public bool NorthWall { get; set; } = true;
    public bool SouthWall { get; set; } = true;
    public bool EastWall { get; set; } = true;
    public bool WestWall { get; set; } = true;

    public int Col { get; private set; }
    public int Row { get; private set; }

    public GameObject MazePath { get; set; }

    //public bool BeenVisited() { return(type == MazeCellType.VISITED); } 
    public void SetAsArena()    { type = MazeCellType.ARENA; }
    public void MarkAsVisited() { type = MazeCellType.VISITED; }
    public bool IsUnVisited()   { return(type == MazeCellType.UNVISITED); }
    public bool IsVisited()     { return(type == MazeCellType.VISITED); }
    public bool IsAArena()      { return(type == MazeCellType.ARENA); }

    public MazeCell(int col, int row) 
    {
        this.Col = col;
        this.Row = row;
    }

    public void SetMazeCell(string title, Color color) 
    {
        GameObject go = MazePath.transform.Find(title).gameObject;

        go.GetComponent<Renderer>().material.color = color;
    }

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

public enum MazeCellType
{
    UNVISITED,
    VISITED,
    ARENA
}
