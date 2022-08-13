using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [Range(0, 360)] [SerializeField] float angle;
    public float _angle { get { return angle; } set { angle = value; } }
    [SerializeField] float radius;
    public float _radius { get { return radius; } }
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;
    [SerializeField] bool canSeePlayer;
    public bool _canSeePlayer { get { return canSeePlayer; } }
    [SerializeField] Collider2D playerWithinRange;
    public Collider2D _playerWithinRange { get { return playerWithinRange; } }
    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FOVCheck();
        }

    }

    void FOVCheck()
    {
        playerWithinRange = Physics2D.OverlapCircle(transform.position, radius, targetMask, -10, 10);

        if (playerWithinRange != null)
        {
            Transform target = playerWithinRange.transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, directionToTarget) < angle)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}
