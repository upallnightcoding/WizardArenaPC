using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator 
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int ArenaSize { get; private set; }

    private IDictionary<MazeIndex, MazeCell> maze;
    private Stack<MazeCell> mazeStack;
    private List<MazeCell> mazeList;

    public MazeGenerator(MazeData mazeData) {
        this.Width = mazeData.width;
        this.Height = mazeData.height;
        this.ArenaSize = mazeData.arenaSize;

        mazeStack = new Stack<MazeCell>();
        maze = new Dictionary<MazeIndex, MazeCell>();
    }

    public void Generate() 
    {
        InitMazeGenerator();
        
        while (!MazeStackEmpty()) 
        {
            MazeCell currentMazeCell = mazeStack.Peek();

            MazeCell neighbor = PickAValidNeighbor(currentMazeCell);

            WalkMaze(neighbor);
        }

        mazeList = new List<MazeCell>(maze.Values);
    }

    public MazeCell GetMazeCell(int col, int row)
    {
        MazeCell mazeCell = null;

        if (!maze.TryGetValue(new MazeIndex(col, row), out mazeCell)) {
            mazeCell = null;
        } 

        return(mazeCell);
    }

    public MazeCell PickRandomCell()
    {
        return(mazeList[UnityEngine.Random.Range(0, mazeList.Count)]);
    }

    private void WalkMaze(MazeCell neighbor)
    {
        if (neighbor != null) 
        {
            neighbor.MarkAsVisited();
            mazeStack.Push(neighbor);
        } else {
            mazeStack.Pop();
        }
    }

    private MazeCell PickAValidNeighbor(MazeCell currentMazeCell)
    {
        Tuple<int, int>[] neighbors = {
            Tuple.Create(0, -1),    // North
            Tuple.Create(0, 1),     // South
            Tuple.Create(1, 0),     // East
            Tuple.Create(-1, 0)     // West
        };

        List<MazeCell> validNeighborList = new List<MazeCell>();

        foreach (Tuple<int, int> neighbor in neighbors) 
        {
            int col = currentMazeCell.Col + neighbor.Item1;
            int row = currentMazeCell.Row + neighbor.Item2;

            MazeCell candidate = GetMazeCell(col, row);

            if (candidate != null && (candidate.IsUnVisited())) 
            {
                validNeighborList.Add(candidate);
            }
        }

        MazeCell validNeighbor = null;
        int nNeighbors = validNeighborList.Count;

        if (nNeighbors > 0) 
        {
            validNeighbor = validNeighborList[GetRandom(nNeighbors)];
            currentMazeCell.CollapseWall(validNeighbor);
        }

        return(validNeighbor);
    }

    private void InitMazeGenerator()
    {
        BuildMazeDictionary();

        SetStartingCell();

        SetupArena();
    }

    private void BuildMazeDictionary()
    {
        for (int  col = 0;  col < Width;  col++) 
        {
            for (int row = 0; row < Height; row++)
            {
                maze.Add(new MazeIndex(col, row), new MazeCell(col, row));
            }
        }
    }

    private void SetupArena()
    {
        int offset = (Width - ArenaSize) / 2;

        for (int col = 0; col < ArenaSize; col++)
        {
            for (int row = 0; row < ArenaSize; row++)
            {
                MazeCell arenaCell = GetMazeCell(col+offset, row+offset);

                if (arenaCell != null) 
                {
                    arenaCell.SetAsArena();
                }
            }
        }
    }

    private void SetStartingCell()
    {
        MazeCell cell = GetMazeCell(0, 0);

        if (cell != null) 
        {
            cell.MarkAsVisited();
            mazeStack.Push(cell);
        }
    }

    private bool MazeStackEmpty()
    {
        return(mazeStack.Count == 0);
    }

    private MazeCell GetRandomMazeCell() {
        int col = GetRandom(Width);
        int row = GetRandom(Height);

        return(GetMazeCell(col, row));
    }

    private int GetRandom(int n) {
        return(UnityEngine.Random.Range(0, n));
    }
}
