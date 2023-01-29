using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell 
{
    public MazeCellType type    { get; private set; } = MazeCellType.UNVISITED;

    public MazeCell NorthWall   { get; private set; } = null;
    public MazeCell SouthWall   { get; private set; } = null;
    public MazeCell EastWall    { get; private set; } = null;
    public MazeCell WestWall    { get; private set; } = null;

    public int Col              { get; private set; }
    public int Row              { get; private set; }

    public GameObject MazePath  { get; set; }

    public void SetAsArena()    { type = MazeCellType.ARENA; }
    public void MarkAsVisited() { type = MazeCellType.VISITED; }
    public bool IsUnVisited()   { return(type == MazeCellType.UNVISITED); }
    public bool IsVisited()     { return(type == MazeCellType.VISITED); }
    public bool IsAArena()      { return(type == MazeCellType.ARENA); }

    public bool HasNorthWall()  { return(NorthWall == null); }
    public bool HasSouthWall()  { return(SouthWall == null); }
    public bool HasEastWall()   { return(EastWall == null); }
    public bool HasWestWall()   { return(WestWall == null); }

    public Vector3 Position()   { return(MazePath.transform.position); }

    public MazeCell(int col, int row) 
    {
        this.Col = col;
        this.Row = row;
    }

    public List<MazeCell> ListFreeNeighbor()
    {
        List<MazeCell> freeList = new List<MazeCell>(); 

        if (!HasNorthWall()) freeList.Add(NorthWall);
        if (!HasSouthWall()) freeList.Add(SouthWall);
        if (!HasEastWall()) freeList.Add(EastWall);
        if (!HasWestWall()) freeList.Add(WestWall);

        return(freeList);
    }

    public bool IsEqual(MazeCell target)
    {
        return((target.Col == Col) && (target.Row == Row));
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
                NorthWall = neighbor;
                neighbor.SouthWall = this;
            } else {
                SouthWall = neighbor;
                neighbor.NorthWall = this;
            }
        }

        if (row == 0)
        {
            if (col == 1) 
            {
                EastWall = neighbor;
                neighbor.WestWall = this;
            } else {
                WestWall = neighbor;
                neighbor.EastWall = this;
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
