using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fild_of_View : MonoBehaviour
{
    public Transform Player;
    public float MaxAngle;
    public float MaxRadius;

    public List<GameObject> WAYPOINT;

    NavMeshAgent _NMA;
    public Animator _anim;

    
    private bool isInFov = false;

    [SerializeField] float areaWaypoint = 1;

    Vector3 lastframePos;

    private void Awake()
    {
        WAYPOINT = GameObject.FindGameObjectWithTag("Waypoint").GetComponent<Waypoint>().waypoint;
    }
    private void OnDrawGizmos()
    {

        //area di vista
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, MaxRadius);

        Vector3 Fovline1 = Quaternion.AngleAxis(MaxAngle, transform.up) * transform.forward * MaxRadius;
        Vector3 Fovline2 = Quaternion.AngleAxis(-MaxAngle, transform.up) * transform.forward * MaxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Fovline1);
        Gizmos.DrawRay(transform.position, Fovline2);

        if (!isInFov)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, Player.position);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, (Player.position - transform.position).normalized * MaxRadius);

        //area cambio waypoint
        foreach(GameObject GO in WAYPOINT)
        {
            Gizmos.DrawWireSphere(GO.transform.position, areaWaypoint);
        }
    }

    public static bool InFOV(Transform checkInObject, Transform target, float maxAngle,float maxRadius)
    {
        Collider[] ovelaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkInObject.position, maxRadius, ovelaps);

        for(int i = 0; i < count + 1; i++)
        {
            if(ovelaps[i] != null)
            {
                if (ovelaps[i].transform == target)
                {
                    Vector3 directionbetween = (target.position - checkInObject.position).normalized;
                    directionbetween.y *= 0;

                    float angle = Vector3.Angle(checkInObject.forward, directionbetween);

                    if(angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkInObject.position, target.position - checkInObject.position);
                        RaycastHit hit;

                        if(Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if (hit.transform == target)
                                return true;
                        }
                    }
                }
            }
        }
 
        return false;
    }


    private void Start()
    {
        lastframePos = transform.position; 
        _NMA = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        animationController();
        isInFov = InFOV(transform, Player, MaxAngle, MaxRadius);
        if (isInFov)
        {
            _NMA.speed = 20;
            _NMA.destination = Player.position;
        }
        else
        {
            _NMA.speed = 10;
            WayPoint();
        }

        

        
    }


    int destination = -1;
    void WayPoint()
    {
        if (destination == -1)
            destination = Random.Range(0, WAYPOINT.Count);
        var distance = Vector3.Distance(WAYPOINT[destination].transform.position, transform.position);
        _NMA.SetDestination (WAYPOINT[destination].transform.position);

        if (distance < areaWaypoint)
        {
            destination = -1;
        }
    }

    

    void animationController()
    {
        var currentFramePos = transform.position;
        var distance = Vector3.Distance(lastframePos, currentFramePos);

        lastframePos = currentFramePos;

        float currentspeed = Mathf.Abs(distance) / Time.deltaTime;

        Debug.Log(currentspeed);


        _anim.SetFloat("move", Mathf.Abs(currentspeed));
    }


}
