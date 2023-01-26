using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcCntrl : MonoBehaviour
{
    [SerializeField] private MazeData mazeData;
    [SerializeField] private float idleTime;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnDistance;

    private CharacterController charCntrl;

    private MazeGenerator maze;
    private MazeCell targetPoint;

    private OrcStateType state = OrcStateType.IDLE;
    private float idleWait = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        maze = mazeData.GetMaze();

        MazeCell startPoint = maze.PickRandomCell();
        transform.position = startPoint.MazePath.transform.position;

        targetPoint = startPoint.PickFreeNeighbor();

        charCntrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) 
        {
            case OrcStateType.IDLE:
                state = CheckIdleTime(Time.deltaTime);
                break;
            case OrcStateType.PATROL:
                state = Patrol(Time.deltaTime);
                break;
        }
    }

    private OrcStateType CheckIdleTime(float dt) 
    {
        idleWait += dt;

        return((idleWait >= idleTime) ? OrcStateType.PATROL : OrcStateType.IDLE);
    }

    private OrcStateType Patrol(float dt) 
    {
        float distance = Vector3.Distance(targetPoint.MazePath.transform.position, transform.position);

        if (distance < turnDistance) 
        {
            targetPoint = targetPoint.PickFreeNeighbor();
        }

        PatrolMaze(targetPoint.MazePath.transform.position, dt);

        return(OrcStateType.PATROL);
    }

    private void PatrolMaze(Vector3 target, float dt)
    {
        float distance = Vector3.Distance(target, transform.position);
        Vector3 direction = (target - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * dt);

        transform.rotation = rotation;

        charCntrl.Move(transform.forward * moveSpeed * dt);
    }

    private void OnDrawGizmos() 
    {
        if (targetPoint != null) 
        {
            Gizmos.DrawLine(targetPoint.MazePath.transform.position, transform.position);
        }
    }
}

public enum OrcStateType
{
    IDLE,
    PATROL
}
