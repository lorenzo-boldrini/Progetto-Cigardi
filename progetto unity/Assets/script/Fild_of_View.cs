using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fild_of_View : MonoBehaviour
{
    public Transform Player;
    public float MaxAngle;
    public float MaxRadius;

    NavMeshAgent _NMA;
    public Animator _anim;
    
    private bool isInFov = false;
    private void OnDrawGizmos()
    {
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
        _NMA = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        isInFov = InFOV(transform, Player, MaxAngle, MaxRadius);
        if (isInFov)
        {
            _NMA.destination = Player.position;
        }

        if (_NMA.angularSpeed != 0)
            _anim.SetBool("move", true);
        else
            _anim.SetBool("move", false);

    }
}
