using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MazeData", menuName = "MazeWizard/MazeData", order = 0)]
public class MazeData : ScriptableObject {

    [Header("Maze Data")]
    public int height;
    public int width;

    public float cellSize;

    [Header("Maze Pre Fabs")]
    public GameObject mazePathPreFab;
    public GameObject mazeWallPreFab;
    public GameObject mazeWallColumnPreFab;
    
}
