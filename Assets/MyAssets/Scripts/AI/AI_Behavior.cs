using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class AI_Behavior : MonoBehaviour
{
    public enum Behavior { wandering, searching, hunting, attacking }
    public Behavior currentBehavior = Behavior.wandering;

    Transform target;
    NavMeshAgent agent => GetComponent<NavMeshAgent>();

    [Header("Navigation settings")]
    [SerializeField] Transform[] wayPoints;
    [SerializeField] int wayPointIndex = 0;
    [SerializeField] float minimumDistance = 2f;

    void Start()
    {
        if (wayPoints.Length == 0)
        {
            Debug.LogError("Set at least one waypoint in the waypoint list.");
            return;
        }
        target = wayPoints[wayPointIndex];
        agent.destination = target.position;
        SetBehavior(Behavior.wandering);
    }



    #region Coroutines
    IEnumerator Wander()
    {
        //yield return null zorgt ervoor dat we mee ticken met de UpdateLoop. Echter is dit eentje
        //waar we uit kunnen breken.
        while (true)
        {
            UpdateTarget(FindViableWaypoint());
            yield return new WaitForSeconds(10f);
        }
    }


    IEnumerator HuntPlayer()
    {
        while (true)
        {
            yield return null;
        }
    }
    IEnumerator SearhingPlayer()
    {
        while (true)
        {
            yield return null;
        }
    }
    #endregion

    #region logic methods
    public void SetBehavior(Behavior behaviorToSet)
    {
        StopAllCoroutines();
        currentBehavior = behaviorToSet;
        UpdateBehavior();
    }
    void UpdateBehavior()
    {
        switch (currentBehavior)
        {
            case Behavior.wandering:
                StartCoroutine(Wander());
                break;
            case Behavior.searching:
                break;
            case Behavior.hunting:
                break;
            case Behavior.attacking:
                break;
            default:
                SetBehavior(Behavior.wandering);
                break;

        }
    }

    void UpdateTarget(Transform setTarget)
    {
        target = setTarget;
        agent.destination = target.position;
    }

    Transform FindViableWaypoint()
    {
        //Als we dichtbij een waypoint zijn ga naar de volgende, of wanneer deze volledig onbereikbaar is.
        if (agent.remainingDistance <= minimumDistance )
        {
            wayPointIndex++;
            wayPointIndex = wayPointIndex % wayPoints.Length;
        }
        return wayPoints[wayPointIndex];

    }
    #endregion

}
