using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBasicDecorator : MonoBehaviour
{
    [SerializeField] private GameObject mazePathPreFab;
    [SerializeField] private GameObject mazeWallPreFab;

    private Transform northAnchor;
    private Transform southAnchor;

    // Start is called before the first frame update
    void Start()
    {
        WallPreFabDecorator wbd = mazeWallPreFab.GetComponent<WallPreFabDecorator>();

        Transform anchors = this.gameObject.transform.Find("Anchors");

        Transform northAnchor = anchors.Find("NorthAnchor");
        Transform southAnchor = anchors.Find("SouthAnchor");
        Transform eastAnchor = anchors.Find("EastAnchor");
        Transform westAnchor = anchors.Find("WestAnchor");

        Vector3 pos = Vector3.zero;

        // for (int i = 0; i < 1; i++) {
        //     GameObject go1 = Instantiate(wallPreFab, northAnchor.transform.position + pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        //     GameObject go2 = Instantiate(wallPreFab, southAnchor.transform.position + pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        //     Instantiate(wallPreFab, eastAnchor.transform.position + pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        //     Instantiate(wallPreFab, westAnchor.transform.position + pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));

        //     go1.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        //     go2.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        //     pos.z = pos.z + 6;
        // }

        //CreateWall(wbd, 90.0f, northAnchor);
        //CreateWall(wbd, 90.0f, southAnchor);
        //CreateWall(wbd, 0.0f, eastAnchor);
        //CreateWall(wbd, 0.0f, westAnchor);
    }

    private void CreateWall(WallPreFabDecorator wbd, float angle, Transform anchor) 
    {
        wbd.SetWallRotation(angle);
        GameObject newWall = Instantiate(mazeWallPreFab, transform);
        newWall.transform.position = anchor.transform.position;
        newWall.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
}
