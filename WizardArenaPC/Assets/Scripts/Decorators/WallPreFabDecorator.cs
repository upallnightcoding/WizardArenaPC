using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPreFabDecorator : MonoBehaviour
{
    [SerializeField] private GameObject columnPreFab;
    [SerializeField] private GameObject wallSegPreFab;

    public float rotateWallSegment = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Transform anchors = this.gameObject.transform.Find("Anchors");

        Transform wall1Anchor = anchors.Find("Wall1Anchor");
        Transform wall2Anchor = anchors.Find("Wall2Anchor");
        Transform columnAnchor = anchors.Find("ColumnAnchor");

        Orientation(Instantiate(columnPreFab, transform), columnAnchor);
        Orientation(Instantiate(wallSegPreFab, transform), wall1Anchor);
        Orientation(Instantiate(wallSegPreFab, transform), wall2Anchor);
    }

    public void SetWallRotation(float rotateWallSegment) 
    {
        this.rotateWallSegment = rotateWallSegment;
    }

    private void Orientation(GameObject go, Transform anchor) 
    {
        go.transform.position = anchor.transform.position;
        
        go.transform.rotation = Quaternion.Euler(0.0f, rotateWallSegment, 0.0f);
    }
}
