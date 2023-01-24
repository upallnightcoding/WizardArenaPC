using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MazeData", menuName = "Maze Wizard/MazeData", order = 0)]
public class MazeData : ScriptableObject {

    [Header("Maze Data")]
    public int height;
    public int width;
    public int arenaSize;

    public float cellSize;

    [Header("Maze Pre Fabs")]
    public GameObject mazePathPreFab;
    public GameObject mazeWallPreFab;
    public GameObject mazeWallColumnPreFab;
    public GameObject mazeArenaEntrance;
    public GameObject mazeArenaFloor;
    public GameObject mazeArenaCircle;

    public Vector3 GetMazeCenter() 
    {
        Vector3 center = new Vector3();

        center.x = ((cellSize * width) / 2.0f) - (cellSize / 2.0f);
        center.y = 0.0f;
        center.z = ((cellSize * height) / 2.0f) - (cellSize / 2.0f);

        return(center);
    }
    
}
