using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCntrl : MonoBehaviour
{
    [SerializeField] private GameObject mazePathPreFab;
    [SerializeField] private GameObject mazeWallPreFab;
    [SerializeField] private GameObject columnPreFab;

    private Transform northAnchor;
    private Transform southAnchor;
    private Transform eastAnchor;
    private Transform westAnchor;

    private Transform northEastAnchor;
    private Transform northWestAnchor;
    private Transform southEastAnchor;
    private Transform southWestAnchor;

    // Start is called before the first frame update
    void Start()
    {
        Transform anchors = mazePathPreFab.transform.Find("Anchors");

        northAnchor = anchors.Find("NorthAnchor");
        southAnchor = anchors.Find("SouthAnchor");
        eastAnchor = anchors.Find("EastAnchor");
        westAnchor = anchors.Find("WestAnchor");

        northEastAnchor = anchors.Find("NorthEastAnchor");
        northWestAnchor = anchors.Find("NorthWestAnchor");
        southEastAnchor = anchors.Find("SouthEastAnchor");
        southWestAnchor = anchors.Find("SouthWestAnchor");

        MazeGenerator maze = new MazeGenerator(10, 10);

        maze.Generate();

        Display(maze);
    }

    private void Display(MazeGenerator maze)
    {
        Vector3 offset = Vector3.zero;

        for (int row = 0; row < maze.Height; row++) 
        {
            for (int col = 0; col < maze.Width; col++)
            {
                CreateMazeCell(maze.GetMazeCell(col, row), offset);
                offset.x += 6.0f;
            }
            offset.x = 0.0f;
            offset.z += 6.0f;
        }
    }

    private void CreateMazeCell(MazeCell cell, Vector3 offset) 
    {
        GameObject mazePath = Instantiate(mazePathPreFab, offset, Quaternion.identity);

        CreateColumn(mazePath, offset, northEastAnchor);
        CreateColumn(mazePath, offset, northWestAnchor);
        CreateColumn(mazePath, offset, southEastAnchor);
        CreateColumn(mazePath, offset, southWestAnchor);

        if(cell.NorthWall)CreateWall(offset, 90.0f, northAnchor);
        if(cell.SouthWall)CreateWall(offset, 90.0f, southAnchor);
        if(cell.EastWall)CreateWall(offset, 0.0f, eastAnchor);
        if(cell.WestWall)CreateWall(offset, 0.0f, westAnchor);
    }

    private void CreateColumn(GameObject parent, Vector3 offset, Transform anchor) 
    {
        GameObject newColumn = Instantiate(columnPreFab, parent.transform);
        newColumn.transform.position = anchor.transform.position + offset;
        newColumn.transform.rotation = Quaternion.identity;
    }

    private void CreateWall(Vector3 offset, float angle, Transform anchor) 
    {
        mazeWallPreFab.GetComponent<WallPreFabDecorator>().SetWallRotation(angle);
        GameObject newWall = Instantiate(mazeWallPreFab, transform);
        newWall.transform.position = anchor.transform.position + offset;
        newWall.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
}
