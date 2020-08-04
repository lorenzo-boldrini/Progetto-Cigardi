using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Fild_of_View : MonoBehaviour
{
    public Transform Player;
    public float MaxAngle;
    public float MaxRadius;
    public float MaxRadiusRun;
    public float MaxRadiusWalk;

    public List<GameObject> WAYPOINT;
    List<GameObject> listaWA;


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


        // area suono corsa
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, MaxRadiusRun);


        // Area suono camminata
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, MaxRadiusWalk);
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
        reorderWaypoint();

        animationController();

        var Distance = Vector3.Distance(transform.position, Player.position);
        isInFov = InFOV(transform, Player, MaxAngle, MaxRadius);
        if (isInFov)
        {
            _NMA.speed = 20;
            _NMA.destination = Player.position;
        }
        else if (Distance < MaxRadiusWalk)
        {
            if (Input.GetAxis("Vertical") == 0)
                stop();
            else
                followPlayer();

        }
        else
        {
            _NMA.speed = 10;
            WayPoint();
            counter = 0;
        }

        RunFollow(Distance);



    }


    int destination = -1;
    void WayPoint()
    {
        if (destination == -1)
            destination = Random.Range(0, 2);
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




        _anim.SetFloat("move", Mathf.Abs(currentspeed));
    }



    void reorderWaypoint()
    {
        listaWA = WAYPOINT.OrderBy(o => o.GetComponent<Waypoint_manager>().distance).ToList();
        WAYPOINT.Clear();
        WAYPOINT = listaWA;
    }



    void RunFollow(float distance)
    {
        if(distance < MaxRadiusRun && Input.GetButton("Run"))
        {
            _NMA.SetDestination(Player.transform.position);
        }
    }


    void followPlayer()
    {
        _NMA.speed = 8;
        _NMA.SetDestination (Player.position);
    }


    float counter = 0;
    bool Canmove;
    void stop()
    {
        int randomT = Random.Range(3, 8);

        if(counter <= randomT && Input.GetAxis("Vertical") == 0)
        {
            counter += 1 * Time.deltaTime;
            _NMA.isStopped = true;
            _NMA.speed = 0;
        }
        else if (counter >= randomT)
        {
            _NMA.isStopped = false;
            _NMA.speed = 10;

        }
    }
}
