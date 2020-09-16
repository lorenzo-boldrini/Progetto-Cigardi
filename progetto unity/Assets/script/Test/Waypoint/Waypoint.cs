using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<GameObject> waypoint = new List<GameObject>();
    public GameObject obj;
    public GameObject SpownManager;
    public float areaWaypoint = 1;

    public void BuildObj()
    {
        if (waypoint.Count > 0)
        {
            var OBJ = Instantiate(obj, waypoint[waypoint.Count - 1].transform.position, Quaternion.identity);
            OBJ.transform.SetParent(this.gameObject.transform);
            OBJ.name = "Waypoint" + waypoint.Count;
            waypoint.Add(OBJ);
        }
        else
        {
            var OBJ = Instantiate(obj, transform.position, Quaternion.identity);
            OBJ.transform.SetParent(this.gameObject.transform);
            OBJ.name = "Waypoint" + waypoint.Count;
            waypoint.Add(OBJ);
        }
    }


    public void RemoveObj()
    {
        DestroyImmediate(waypoint[waypoint.Count -1]);
        waypoint.Remove(waypoint[waypoint.Count - 1]);
    }


    private void OnDrawGizmos()
    {
        int waypointcounter = 0;
        Gizmos.color = Color.red;
        foreach(GameObject SWaypoint in waypoint)
        {
            if (waypointcounter < waypoint.Count)
            {
                waypointcounter++;
                Gizmos.DrawSphere(SWaypoint.transform.position, 0.5f);
                Gizmos.DrawLine(SWaypoint.transform.position, waypoint[waypointcounter].transform.position);
            }
            else
            {
                break;
            }

            Gizmos.DrawWireSphere(SWaypoint.transform.position, areaWaypoint);
        }
        
    }
}
