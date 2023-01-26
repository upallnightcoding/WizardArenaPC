using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeIndex 
{
    public int col { get; private set; }
    public int row { get; private set; }

    public MazeIndex(int col, int row) 
    {
        this.col = col;
        this.row = row;
    }

    public override bool Equals(object obj)
    {
        return obj is MazeIndex index &&
               col == index.col &&
               row == index.row;
    }

    public override int GetHashCode()
    {
        int hashCode = -1831622508;
        hashCode = hashCode * -1521134295 + col.GetHashCode();
        hashCode = hashCode * -1521134295 + row.GetHashCode();
        return hashCode;
    }
}
