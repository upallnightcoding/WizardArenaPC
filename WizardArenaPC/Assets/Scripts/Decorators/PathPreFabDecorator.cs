using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPreFabDecorator : MonoBehaviour
{
    [SerializeField] private GameObject[] grassPreFab;
    [SerializeField] private int nGrassPreFabs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Random.Range(0, nGrassPreFabs); i++)
        {
            createGrassPreFab();
        }
    }

    private void createGrassPreFab()
    {
        int n = Random.Range(0, grassPreFab.Length);
        float rotate = Random.Range(0.0f, 360.0f);

        GameObject go = Instantiate(grassPreFab[n], transform);
        go.transform.Rotate(new Vector3(0.0f, rotate, 0.0f));

        float xVary = Random.Range(-3.0f, 3.0f);
        float zVary = Random.Range(-3.0f, 3.0f);
        Vector3 position = go.transform.position;
        go.transform.position = new Vector3(position.x + xVary, position.y, position.z + zVary);
    }

   
}
