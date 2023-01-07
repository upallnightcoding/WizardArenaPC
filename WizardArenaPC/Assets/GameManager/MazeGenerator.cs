using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator 
{
    private IDictionary<MazeIndex, MazeCell> maze;
    private Stack<MazeCell> mazeStack;
    private int width, height;

    public MazeGenerator(int width, int height) {
        this.width = width;
        this.height = height;

        mazeStack = new Stack<MazeCell>();
        maze = new Dictionary<MazeIndex, MazeCell>();
    }

    public void Generate() 
    {
        InitMazeGenerator();
        
        while (!MazeStackEmpty()) 
        {
            MazeCell currentMazeCell = mazeStack.Peek();

            Debug.Log($"Current Maze Cell: {currentMazeCell}");

            MazeCell neighbor = PickAValidNeighbor(currentMazeCell);

            WalkMaze(neighbor);
        }
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

            if (candidate != null && (!candidate.BeenVisited)) 
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
        BuildMaze();

        SetStartingCell();
    }

    private void BuildMaze()
    {
        for (int  col = 0;  col < width;  col++) 
        {
            for (int row = 0; row < height; row++)
            {
                maze.Add(new MazeIndex(col, row), new MazeCell(col, row));
            }
        }
    }

    private void SetStartingCell()
    {
        MazeCell cell = GetRandomMazeCell();
        cell.MarkAsVisited();
        mazeStack.Push(cell);
    }

    private bool MazeStackEmpty()
    {
        return(mazeStack.Count == 0);
    }

    private MazeCell GetMazeCell(int col, int row)
    {
        MazeCell mazeCell = null;

        if (!maze.TryGetValue(new MazeIndex(col, row), out mazeCell)) {
            mazeCell = null;
        } 

        return(mazeCell);
    }

    private MazeCell GetRandomMazeCell() {
        int col = GetRandom(width);
        int row = GetRandom(height);

        return(GetMazeCell(col, row));
    }

    private int GetRandom(int n) {
        return(UnityEngine.Random.Range(0, n));
    }
}
