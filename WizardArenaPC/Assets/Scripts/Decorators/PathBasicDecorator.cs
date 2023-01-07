using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBasicDecorator : MonoBehaviour
{
    [SerializeField] private GameObject wallPreFab;

    private Transform rightSlot;
    private Transform leftSlot;

    // Start is called before the first frame update
    void Start()
    {
        rightSlot = this.gameObject.transform.GetChild(1);
        leftSlot = this.gameObject.transform.GetChild(2);

        Vector3 pos = Vector3.zero;

        for (int i = 0; i < 5; i++) {
            Instantiate(wallPreFab, rightSlot.transform.position + pos, Quaternion.Euler(0.0f, 90.0f, 0.0f));
            Instantiate(wallPreFab, leftSlot.transform.position + pos, Quaternion.Euler(0.0f, 90.0f, 0.0f));

            pos.z = pos.z + 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
