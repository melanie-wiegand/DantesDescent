using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public Transform[] initialWaypoints;
    public Transform[] randomWaypoints; 

    void Awake()
    {
        if (initialWaypoints.Length == 0)
        {
            initialWaypoints = new Transform[2];
            initialWaypoints[0] = transform.GetChild(0);
            initialWaypoints[1] = transform.GetChild(1);
        }

        // if (randomWaypoints.Length == 0)
        // {
        //     randomWaypoints = new Transform[transform.childCount - 2];
        //     for (int i = 2; i < transform.childCount; i++)
        //     {
        //         randomWaypoints[i - 2] = transform.GetChild(i);
        //     }
        // }
    }
}