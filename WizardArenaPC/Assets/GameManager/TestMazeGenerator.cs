using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMazeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject path;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator maze = new MazeGenerator(4, 4);

        maze.Generate();

        Instantiate(path);
    }
}
