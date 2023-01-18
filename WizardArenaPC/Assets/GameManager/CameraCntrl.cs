using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntrl : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = player.transform.position - offset;
    }
}
