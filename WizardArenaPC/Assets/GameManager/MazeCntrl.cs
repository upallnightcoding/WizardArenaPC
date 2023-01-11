using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCntrl : MonoBehaviour
{
    [SerializeField] private MazeData mazeData;

    private GameObject mazePathPreFab;
    private GameObject mazeWallPreFab;
    private GameObject mazeWallColumnPreFab;

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
        mazePathPreFab = mazeData.mazePathPreFab;
        mazeWallPreFab = mazeData.mazeWallPreFab;
        mazeWallColumnPreFab = mazeData.mazeWallColumnPreFab;

        Transform anchors = mazePathPreFab.transform.Find("Anchors");

        northAnchor = anchors.Find("NorthAnchor");
        southAnchor = anchors.Find("SouthAnchor");
        eastAnchor = anchors.Find("EastAnchor");
        westAnchor = anchors.Find("WestAnchor");

        northEastAnchor = anchors.Find("NorthEastAnchor");
        northWestAnchor = anchors.Find("NorthWestAnchor");
        southEastAnchor = anchors.Find("SouthEastAnchor");
        southWestAnchor = anchors.Find("SouthWestAnchor");

        MazeGenerator maze = new MazeGenerator(mazeData.width, mazeData.height);

        maze.Generate();

        Display(maze);
    }

    private void Display(MazeGenerator maze)
    {
        Vector3 offset = Vector3.zero;
        float cellSize = mazeData.cellSize;

        for (int row = 0; row < maze.Height; row++) 
        {
            for (int col = 0; col < maze.Width; col++)
            {
                CreateMazeCell(maze, col, row, offset);
                offset.x += cellSize;
            }
            offset.x = 0.0f;
            offset.z += cellSize;
        }
    }

    private void CreateMazeCell(MazeGenerator maze, int col, int row, Vector3 offset) 
    {
        GameObject mazePath = Instantiate(mazePathPreFab, offset, Quaternion.identity);
        MazeCell cell = maze.GetMazeCell(col, row);

        if (row == 0) 
        {
            if (col == 0) 
            {
                if(cell.NorthWall)CreateWall(mazePath, offset, 90.0f, northAnchor);
                if(cell.SouthWall)CreateWall(mazePath, offset, 90.0f, southAnchor);
                if(cell.EastWall)CreateWall(mazePath, offset, 0.0f, eastAnchor);
                if(cell.WestWall)CreateWall(mazePath, offset, 0.0f, westAnchor);

                CreateColumn(mazePath, offset, northEastAnchor);
                CreateColumn(mazePath, offset, northWestAnchor);
                CreateColumn(mazePath, offset, southEastAnchor);
                CreateColumn(mazePath, offset, southWestAnchor);
            } else {
                if(cell.NorthWall)CreateWall(mazePath, offset, 90.0f, northAnchor);
                if(cell.SouthWall)CreateWall(mazePath, offset, 90.0f, southAnchor);
                if(cell.EastWall)CreateWall(mazePath, offset, 0.0f, eastAnchor);

                CreateColumn(mazePath, offset, northEastAnchor);
                CreateColumn(mazePath, offset, southEastAnchor);
            }
        } else {
            if (col == 0) 
            {
                if(cell.NorthWall)CreateWall(mazePath, offset, 90.0f, northAnchor);
                if(cell.EastWall)CreateWall(mazePath, offset, 0.0f, eastAnchor);
                if(cell.WestWall)CreateWall(mazePath, offset, 0.0f, westAnchor);

                CreateColumn(mazePath, offset, northEastAnchor);
                CreateColumn(mazePath, offset, northWestAnchor);
            } else {
                if(cell.NorthWall)CreateWall(mazePath, offset, 90.0f, northAnchor);
                if(cell.EastWall)CreateWall(mazePath, offset, 0.0f, eastAnchor);

                CreateColumn(mazePath, offset, northEastAnchor);
            }
        }
    }

    private void CreateColumn(GameObject parent, Vector3 offset, Transform anchor) 
    {
        GameObject newColumn = Instantiate(mazeWallColumnPreFab, parent.transform);
        newColumn.transform.position = anchor.transform.position + offset;
        newColumn.transform.rotation = Quaternion.identity;
    }

    private void CreateWall(GameObject parent, Vector3 offset, float angle, Transform anchor) 
    {
        mazeWallPreFab.GetComponent<WallPreFabDecorator>().SetWallRotation(angle);
        GameObject newWall = Instantiate(mazeWallPreFab, parent.transform);
        newWall.transform.position = anchor.transform.position + offset;
        newWall.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
}
