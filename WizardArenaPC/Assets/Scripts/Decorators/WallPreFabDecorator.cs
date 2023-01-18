using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPreFabDecorator : MonoBehaviour
{
    [SerializeField] private GameObject columnPreFab;
    [SerializeField] private PreFabRank[] wallSegListPreFab;

    public float rotateWallSegment = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        initPreFabRank(wallSegListPreFab);

        Transform anchors = this.gameObject.transform.Find("Anchors");

        Transform wall1Anchor = anchors.Find("Wall1Anchor");
        Transform wall2Anchor = anchors.Find("Wall2Anchor");
        Transform columnAnchor = anchors.Find("ColumnAnchor");

        Orientation(Instantiate(columnPreFab, transform), columnAnchor);
        Orientation(Instantiate(choosePreFab(wallSegListPreFab), transform), wall1Anchor);
        Orientation(Instantiate(choosePreFab(wallSegListPreFab), transform), wall2Anchor);
    }

    private GameObject choosePreFab(PreFabRank[] list) 
    {
        int n = list.Length;
        int r = Random.Range(0, 100);
        int index = -1;

        for (int i = (n-1); (i >= 0) && (index == -1); i--) 
        {
            if (r >= list[i].rank)
            {
                index = i;
            }
        }

        return(list[index].preFab);
    }

    private void initPreFabRank(PreFabRank[] list) 
    {
        int value = 0;
        int n = list.Length;

        for (int i = 0; i < n; i++)
        {
            list[i].rank = value;
            value += list[i].percentage;
        }
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

[System.Serializable]
public class PreFabRank
{
    public GameObject preFab;
    public int percentage;
    public int rank;
}
