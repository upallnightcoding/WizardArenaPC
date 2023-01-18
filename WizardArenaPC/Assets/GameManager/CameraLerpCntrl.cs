using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpCntrl : MonoBehaviour
{
    [SerializeField] private Transform player;

    private float pLerp = 0.02f;
    private float rLerp = 0.01f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, rLerp);
    }
}
