using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    // Use this for initialization
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    private Rigidbody2D aiRigidBody;
    //public LayerMask playerMask; -- testing out without a layer for the player, maybe just the Player Tag will be enough...
    public int obstacleMask = 1 << 8;
    public bool playerVisible;
    public Transform ghostPos;
    public List<Transform> visibleTargets;

    private void Awake()
    {
        aiRigidBody = GetComponent<Rigidbody2D>();
        Debug.Log("Field of view Awake!");
        playerVisible = false;
        StartCoroutine("FindTargetWithDelay", .2f);
    }

   
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    public void FindVisibleTargets()//Try to find player
    {
        visibleTargets.Clear();
        playerVisible = false;
        GameObject[] Ghosts = GameObject.FindGameObjectsWithTag("Player");
       for(int i = 0; i < Ghosts.Length; i++)
        {
            if (i > 0)
            {
                Debug.LogError("more than one Ghost???" + i);
            }
               ghostPos = Ghosts[i].transform;
            
            
            Vector3 distanceToTarget = (ghostPos.position - transform.position);
            if(distanceToTarget.magnitude < viewRadius)//Check if Ghost is near NPC
            {
                Debug.Log("Ghost in view radius");

                Vector3 dirToTarget = distanceToTarget.normalized;
                if(Vector3.Angle(transform.right, dirToTarget) < viewAngle/2)//Check if Ghost is in field of view of NPC
                {

                    Ray ray1 = new Ray(transform.position, dirToTarget);

                    Debug.Log("obstacle mask: " + obstacleMask);

                    //Debug.DrawLine(transform.position, ghostPos.position, Color.red);

                    RaycastHit2D hit = new RaycastHit2D();

                    hit = CheckRaycast(dirToTarget);

                  
                    if(hit.collider.name == "Obstacle")
                    {
                        Debug.Log("Lalala Player hidden");
                    }
                    else if(hit.collider.name == "Player")
                    {
                        Debug.Log("Oh no! Ghost was seen!");
                    }

                    //else
                    //{
                    //    playerVisible = true;
                    //    visibleTargets.Add(ghostPos);
                    //    Debug.Log("Ghost Spotted");
                    //}
                }
            }            
        }
    }
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)//turn angle to a global one
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);


    }

    private void OnDrawGizmos()
    {
        
    }
    //if(fow.playerVisible == true)
    //{
    //    Debug.Log("Drawing...");
    //    Handles.DrawLine(fow.transform.position, fow.ghostPos.position);
    //}

    private RaycastHit2D CheckRaycast(Vector3 direction)
    {
       Debug.DrawLine(transform.position + direction, ghostPos.position, Color.green);
       RaycastHit2D amen = new RaycastHit2D();
       amen = Physics2D.Raycast(transform.position + direction, direction, 10.0f);


       //Debug.Log("hit point: " + amen.point);

        Debug.Log("collider name: " + amen.collider.name);
      

        return amen;
    }
}
