using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCntrl : MonoBehaviour
{
    [SerializeField] private MazeData mazeData;

    private GameObject mazePathPreFab;
    private GameObject mazeWallPreFab;
    private GameObject mazeWallColumnPreFab;
    private GameObject mazeArenaEntrance;
    private GameObject mazeArenaFloor;
    private GameObject mazeArenaCircle;

    private Transform northAnchor;
    private Transform southAnchor;
    private Transform eastAnchor;
    private Transform westAnchor;

    private Transform northEastAnchor;
    private Transform northWestAnchor;
    private Transform southEastAnchor;
    private Transform southWestAnchor;

    private bool arenaEntranceSw = false;

    // Start is called before the first frame update
    void Start()
    {
        mazePathPreFab = mazeData.mazePathPreFab;
        mazeWallPreFab = mazeData.mazeWallPreFab;
        mazeWallColumnPreFab = mazeData.mazeWallColumnPreFab;
        mazeArenaEntrance = mazeData.mazeArenaEntrance;
        mazeArenaFloor = mazeData.mazeArenaFloor;
        mazeArenaCircle = mazeData.mazeArenaCircle;

        Transform anchors = mazePathPreFab.transform.Find("Anchors");

        northAnchor = anchors.Find("NorthAnchor");
        southAnchor = anchors.Find("SouthAnchor");
        eastAnchor = anchors.Find("EastAnchor");
        westAnchor = anchors.Find("WestAnchor");

        northEastAnchor = anchors.Find("NorthEastAnchor");
        northWestAnchor = anchors.Find("NorthWestAnchor");
        southEastAnchor = anchors.Find("SouthEastAnchor");
        southWestAnchor = anchors.Find("SouthWestAnchor");

        MazeGenerator maze = new MazeGenerator(mazeData);

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
                CreateMazePath(maze, col, row, offset);
                offset.x += cellSize;
            }

            offset.x = 0.0f;
            offset.z += cellSize;
        }

        maze.PickRandomCell().SetMazeCell("Building_Floor_01", Color.red);
        maze.PickRandomCell().SetMazeCell("Building_Floor_01", Color.green);

        CreateArena();
    }

    private void CreateArena()
    {
        Vector3 mazeArenaPos = mazeData.GetMazeCenter();

        GameObject mazeArena = Instantiate(mazeArenaFloor, mazeArenaPos, Quaternion.identity);

        Transform anchors = mazeArena.transform.Find("Anchors");

        Transform center = anchors.Find("Center");

        CreatePreFab(mazeArenaCircle, mazeArena, Vector3.zero, 0.0f, center);
    }

    private void CreateArena1()
    {
        PgObject circle = new PgObject(mazeArenaCircle);
        PgObject floor = new PgObject(mazeArenaFloor);

        Vector3 mazeArenaPos = mazeData.GetMazeCenter();
        PgCreate node1 = new PgCreate(floor, mazeArenaPos, 0.0f);
        

        //PgObject floor1 = (new PgConstruct(node1, "Center", circle, Vector3.zero, 0.0f)).Execute();

        //PgObject floor2 = (new PgRandomPlace(floor, "Center", , 5).Execute();

        //floorBase.Execute();
    }

    private void CreateMazePath(MazeGenerator maze, int col, int row, Vector3 offset) 
    {
        MazeCell mazeCell = maze.GetMazeCell(col, row);

        if ((mazeCell != null) && (mazeCell.IsVisited()))
        {
            GameObject mazePath = Instantiate(mazePathPreFab, offset, Quaternion.identity);

            mazeCell.MazePath = mazePath;

            BuildWalls(col, row, mazeCell, offset, mazePath);

            CheckArenaWall(maze, mazeCell, offset, col, row, mazePath);
        }   
    }

    private void CheckArenaWall(MazeGenerator maze, MazeCell mazeCell, Vector3 offset, int col, int row, GameObject mazePath)
    {
        MazeCell arenaSouthCell = maze.GetMazeCell(col, row-1);

        if ((arenaSouthCell != null) && (arenaSouthCell.IsAArena()))
        {
            CreateWall(mazeCell.SouthWall, mazePath, offset, 90.0f, southAnchor);

            CreateColumn(mazePath, offset, southEastAnchor);
        }
        
        MazeCell arenaWestCell = maze.GetMazeCell(col-1, row);

        if ((arenaWestCell != null) && (arenaWestCell.IsAArena()))
        {
            if (!arenaEntranceSw)
            {
                CreatePreFab(mazeArenaEntrance, mazePath, offset, 0.0f, westAnchor);
                arenaEntranceSw = true;
            } else {
                CreateWall(mazeCell.WestWall, mazePath, offset, 0.0f, westAnchor);
            }
            
            CreateColumn(mazePath, offset, northWestAnchor);
        }
    }

    private void BuildWalls(int col, int row, MazeCell cell, Vector3 offset, GameObject mazePath)
    {
        if (row == 0) 
        {
            if (col == 0) 
            {
                CreateWall(cell.NorthWall, mazePath, offset, 90.0f, northAnchor);
                CreateWall(cell.SouthWall, mazePath, offset, 90.0f, southAnchor);
                CreateWall(cell.EastWall, mazePath, offset, 0.0f, eastAnchor);
                CreateWall(cell.WestWall, mazePath, offset, 0.0f, westAnchor);

                CreateColumn(mazePath, offset, northEastAnchor);
                CreateColumn(mazePath, offset, northWestAnchor);
                CreateColumn(mazePath, offset, southEastAnchor);
                CreateColumn(mazePath, offset, southWestAnchor);
            } else {
                CreateWall(cell.NorthWall, mazePath, offset, 90.0f, northAnchor);
                CreateWall(cell.SouthWall, mazePath, offset, 90.0f, southAnchor);
                CreateWall(cell.EastWall, mazePath, offset, 0.0f, eastAnchor);

                CreateColumn(mazePath, offset, northEastAnchor);
                CreateColumn(mazePath, offset, southEastAnchor);
            }
        } else {
            if (col == 0) 
            {
                CreateWall(cell.NorthWall, mazePath, offset, 90.0f, northAnchor);
                CreateWall(cell.EastWall, mazePath, offset, 0.0f, eastAnchor);
                CreateWall(cell.WestWall, mazePath, offset, 0.0f, westAnchor);

                CreateColumn(mazePath, offset, northEastAnchor);
                CreateColumn(mazePath, offset, northWestAnchor);
            } else {
                CreateWall(cell.NorthWall, mazePath, offset, 90.0f, northAnchor);
                CreateWall(cell.EastWall, mazePath, offset, 0.0f, eastAnchor);

                CreateColumn(mazePath, offset, northEastAnchor);
            }
        }
           
    }

    private void CreatePreFab(GameObject preFab, GameObject parent, Vector3 offset, float angle, Transform anchor)
    {
        GameObject newPreFab = Instantiate(preFab, parent.transform);
        newPreFab.transform.position = anchor.transform.position + offset;
        newPreFab.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void CreateColumn(GameObject parent, Vector3 offset, Transform anchor) 
    {
        GameObject newColumn = Instantiate(mazeWallColumnPreFab, parent.transform);
        newColumn.transform.position = anchor.transform.position + offset;
        newColumn.transform.rotation = Quaternion.identity;
    }

    private void CreateWall(bool isAWall, GameObject parent, Vector3 offset, float angle, Transform anchor) 
    {
        if (isAWall) 
        {
            mazeWallPreFab.GetComponent<WallPreFabDecorator>().SetWallRotation(angle);
            GameObject newWall = Instantiate(mazeWallPreFab, parent.transform);
            newWall.transform.position = anchor.transform.position + offset;
            newWall.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
    }
}
