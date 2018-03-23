using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*1) Choose how many directions it should look at - DONE
 2) Scan for objects nearby
 2.5)Get value for strength
 3)Create danger map
 4)Create Interest Map
 5)Select best slots based on 3) and 4)
 */



public class VehicleController : MonoBehaviour {

    public float Speed;
    int nRadials = 16;
    public float detectionDistance = 10.0f;
    public int RadialScale = 1;
    public float maxSpeed = 5.0f;



    // Use this for initialization
    Rigidbody carRigidBody;

    [Range(1, 100)]
  
    GameObject[] Targets;
    GameObject[] Obstacles;


    Vector3[] InterestRadials;
    Vector3[] DangerRadials;

    Vector3 carForward;
   
    float RadialStep;
    int TargetID = 0;
    float maxForce = 1.0f;
    

    //Vector3 velocity;
    void Start ()
    {

        carRigidBody = GetComponent<Rigidbody>();
        
        
        
        RadialStep = 360.0f / nRadials;

        Debug.Log("Radial Step = " +RadialStep);
       
        InterestRadials = new[] {new Vector3(), new Vector3() , new Vector3() , new Vector3(), new Vector3(), new Vector3(), new Vector3(), new Vector3(),
        new Vector3(), new Vector3() , new Vector3() , new Vector3(), new Vector3(), new Vector3(), new Vector3(), new Vector3()};

        DangerRadials = new[] { new Vector3(), new Vector3(), new Vector3(), new Vector3(), new Vector3(), new Vector3(), new Vector3(), new Vector3(),
        new Vector3(), new Vector3() , new Vector3() , new Vector3(), new Vector3(), new Vector3(), new Vector3(), new Vector3()};

        Targets = GameObject.FindGameObjectsWithTag("Target");
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

    }

    

    void Update()
    {
        ///1) Choose how many directions it should look at
        FirstStep();

        ///2) Scan for objects nearby
       



        ///4) Create Interest Map
        InterestMap();
        //InterestDangerMap();
        DangerMap();
        

    }
    void FirstStep()
    {
        carForward = carRigidBody.transform.forward;

        for (int i = 0; i < nRadials; i++)
        {
            SpawnRadials(i);
            //Debug.Log("InterestRadials["+i+"] = "+ InterestRadials[i]);
        }
        //Debug purposes - check if radials are evenly spread
        float resultAngle = GetAngle(InterestRadials[0], InterestRadials[1]);

        Debug.Log("Angle = " + resultAngle);
        //Debug.Log("MyAngle = " + GetAngle(InterestRadials[1], InterestRadials[2]));
    }

    void InterestMap()
    {
        float interestStrength = 0.0f;
        //get targets
        for (int obj = 0; obj < Targets.Length; obj++)
        {
            Vector3 v = Targets[obj].transform.position - carRigidBody.transform.position;
            //Check only targets within detection distance
            if (v.magnitude <= detectionDistance)
            {


                //The closer the targer is, the higher the interest strength for that direction

                //compare angle between each of the radials and the Target-Vehicle vector. 
                for (int angleIterator = 0; angleIterator < nRadials; angleIterator++)
                {
                    float interestAngle = Mathf.Abs(GetAngle(v, InterestRadials[angleIterator]));

                    //Assign interest strength to the closest angle. 
                    if (interestAngle < RadialStep / 2)
                    {

                        //cap interest strength if too close
                        if (v.magnitude < RadialScale)
                        {
                            interestStrength = RadialScale;
                        }
                        else
                        {
                            interestStrength = RadialScale / v.magnitude;
                        }

                        //The higher the interest strength, the longer that radial vector becomes.
                        InterestRadials[angleIterator] *= (1 + interestStrength);
                    }
                }                 
                
            }
        }
    }

    void DangerMap()
    {
        float dangerStrength = 0.0f;
        //get targets
        for (int obj = 0; obj < Obstacles.Length; obj++)
        {

            Vector3 v = Obstacles[obj].transform.position - carRigidBody.transform.position;
            //Check only targets within detection distance
            if (v.magnitude <= detectionDistance)
            {


                //The closer the targer is, the higher the interest strength for that direction

                //compare angle between each of the radials and the Target-Vehicle vector. 
                for (int angleIterator = 0; angleIterator < nRadials; angleIterator++)
                {
                    float interestAngle = Mathf.Abs(GetAngle(v, DangerRadials[angleIterator]));

                    //Assign interest strength to the closest angle. 
                    if (interestAngle < RadialStep / 2)
                    {

                        //cap interest strength if too close
                        if (v.magnitude < RadialScale)
                        {
                            dangerStrength = RadialScale;
                        }
                        else
                        {
                            dangerStrength = RadialScale / v.magnitude;
                        }

                        //The higher the interest strength, the longer that radial vector becomes.
                        DangerRadials[angleIterator] *= (1 + dangerStrength);
                    }
                }

            }
        }
    }
    private void OnDrawGizmos()
    {
        //Vector3 v = Targets[0].transform.position - carRigidBody.transform.position;
        Gizmos.color = Color.green;
        for (int target = 0; target < Targets.Length; target++)
        {
            Vector3 targetPos = Targets[target].transform.position;
            Debug.Log("targetPos = "+targetPos);
            Gizmos.DrawLine(carRigidBody.transform.position, targetPos);
        }

        //Debug.Log("Obstacle Position: " + Obstacles[0].transform.position);
        Gizmos.color = Color.red;
        for (int obstacle = 0; obstacle < Obstacles.Length; obstacle++)
        {
            Vector3 obstaclePos = Obstacles[obstacle].transform.position;
            Gizmos.DrawLine(carRigidBody.transform.position, Obstacles[obstacle].transform.position);
        }
        Debug.Log("DrawGizmos...");



        //Draw Interest Radials
        for (int i = 0; i < nRadials; i++)
        {            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(carRigidBody.transform.position, carRigidBody.transform.position + InterestRadials[i]);
        }

        //Draw Danger Radials
        for (int i = 0; i < nRadials; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(carRigidBody.transform.position, carRigidBody.transform.position + DangerRadials[i]);
        }
    }

    private void SpawnRadials(int i)
    {
        //rotating a v1(x1, y1) some 'alfa; degrees yields =  v2(x2, y2)
        //x2 = x1*cos(alfa) - y1*sin(alfa)
        //y2 = x1*sin(alfa) + y1*cos(alfa)

        //Alfa == the rotation at each iteration, which will be evenly spaced around the circle.
        //Debug.Log("RadialStep in degrees" + RadialStep*i + " Radians = "+ RadialStep * Mathf.Deg2Rad * i);
        //carForward.Normalize();
        float alfa = RadialStep * Mathf.Deg2Rad * i;
        //Debug.Log("Alfa = " + alfa);
        //Debug.Log("Car forward = " + carForward);
        InterestRadials[i] = new Vector3(carForward.x * Mathf.Cos(alfa) - carForward.z * Mathf.Sin(alfa),0,
                                    carForward.x * Mathf.Sin(alfa) + carForward.z * Mathf.Cos(alfa));
        InterestRadials[i].Normalize();
        DangerRadials[i] = new Vector3(carForward.x * Mathf.Cos(alfa) - carForward.z * Mathf.Sin(alfa), 0,
                                    carForward.x * Mathf.Sin(alfa) + carForward.z * Mathf.Cos(alfa));
        DangerRadials[i].Normalize();

    }
    // Update is called once per frame
    void FixedUpdate ()
    {



        Movement();

        
    }

    void Movement()
    {

        Vector3 seekDir = DiscreteSeek();
        Vector3 fleeDir = DiscreteFlee();



        carRigidBody.velocity = Truncate(seekDir + fleeDir, maxSpeed);
        carRigidBody.position += carRigidBody.velocity * Time.deltaTime;

        //Loop though all Obstacle Radials
        //Mark that 

        //Loop through all Interest Radials
        //Choose the radial with highest interest
        //Call Seek() to the target in that direction



        ////Continuous direction Flee
        //Vector3 flee = new Vector3();
        //flee = Flee(Obstacles[0].transform.position);
        //carRigidBody.velocity = Truncate(carRigidBody.velocity + flee, maxSpeed);
        //carRigidBody.position += carRigidBody.velocity * Time.deltaTime;

        ////Discrete direction Flee
        //Vector3 steering = new Vector3();
        //steering = Seek(Targets[0].transform.position);   
        //carRigidBody.velocity = Truncate(carRigidBody.velocity + steering, maxSpeed);
        //carRigidBody.position += carRigidBody.velocity * Time.deltaTime;
    }

    //continuous Seek
    Vector3 Seek(Vector3 TargetPos)
    {
        Vector3 desiredVelocity = new Vector3();
        //Debug.Log("car pos = "+carRigidBody.transform.position);
        desiredVelocity = TargetPos - carRigidBody.transform.position;
        
        desiredVelocity = desiredVelocity.normalized * Speed;

        Vector3 steering = desiredVelocity - carRigidBody.velocity;

        //Truncate steering to max Force
        steering = Truncate(steering, maxForce);

        steering = steering / carRigidBody.mass;

      
        return steering;
    }

    //continous Flee
    Vector3 Flee(Vector3 TargetPos)
    {
        Vector3 desiredVelocity = new Vector3();
        desiredVelocity =  carRigidBody.transform.position - TargetPos;
        desiredVelocity = desiredVelocity.normalized * Speed;

        Vector3 flee = desiredVelocity - carRigidBody.velocity;
        flee = Truncate(flee, maxForce);
        flee = flee / carRigidBody.mass;

        return flee;
    }

    //returns the direction of the higher interest, normalized
    Vector3 DiscreteSeek()
    {
        float greatestInterest = 0.0f;
        int radialId = -1;

       for(int radial = 0; radial < nRadials; radial++)
        {
            if(InterestRadials[radial].magnitude > greatestInterest)
            {
                greatestInterest = InterestRadials[radial].magnitude;
                radialId = radial;
            }
        }
        if (radialId < 0)
        {
            Debug.LogError("RADIAL ID IS NEGATIVEEEE");
            return new Vector3();
        }

        Vector3 seekVelocity = InterestRadials[radialId].normalized * Speed;
        seekVelocity = Truncate(seekVelocity, maxForce);
        seekVelocity /= carRigidBody.mass;

        return seekVelocity;
        
        //
    }

    Vector3 DiscreteFlee()
    {
        float greatestDanger = 0.0f;
        int radialId = -1;

        for (int radial = 0; radial < nRadials; radial++)
        {
            if (DangerRadials[radial].magnitude > greatestDanger)
            {
                greatestDanger = DangerRadials[radial].magnitude;
                radialId = radial;
            }
        }
        if (radialId < 0)
        {
            Debug.LogError("RADIAL ID IS NEGATIIIIIVE");
            return new Vector3();
        }

        Vector3 fleeVelocity = DangerRadials[radialId].normalized*Speed;
        fleeVelocity = Truncate(fleeVelocity, maxForce);
        fleeVelocity /= carRigidBody.mass;

        //Because it is AVOIDING that direction, right?
        fleeVelocity *= -1;

        return fleeVelocity;
    }

    Vector3 Truncate(Vector3 vec, float mag)
    {
        if (vec.magnitude > mag)
        {
            vec.Normalize();
            vec *= mag;
        }
        return vec;

    }
    //returns value in degrees
    float GetAngle(Vector3 v1, Vector3 v2)
    {
        v1.Normalize();
        v2.Normalize();

     //Since it is moving on the XZ plane, had to switch y's for z's

        float angle = 0;
        float dot = v1.x*v2.x + v1.z*v2.z;
        float det = v1.x*v2.z - v1.z*v2.x;

        angle = Mathf.Atan2(det, dot);

       

        angle *= Mathf.Rad2Deg;
        //if (angle < 0)
        //    angle += 360.0f;
        //Debug.Log("v1 = " + v1 + " v2 = " + v2 +" + Angle in degrees: " + angle);
        

        return angle;
    }
}
