using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class CarMovement : MonoBehaviour
{
    public List<Transform> restartPositions = new List<Transform>();

    private CarController con;
    [Header("Car stats(new)")]
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public float liftCoefficient = -50f; //do down force w samochodzie

    [Header("Restart")]
    public float restartTimer = 2.5f;
    public float restartTime = 2.5f;
    public bool restart = false;

    public bool inAir = false;

    public bool canMove = true;

    [SerializeField]
    private List<GameObject> backLights = new List<GameObject>();
    [SerializeField]
    private List<Material> lightMaterials = new List<Material>();

    
    private void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            restartPositions.Add(GameObject.Find("spawn" + i).transform);
        }
        con = GetComponent<CarController>();
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
    }
    public void ApplyLocalRotationToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.rotation = rotation;
    }

    public void IncreaseSpeed(int boost)
    {
        maxMotorTorque += boost;
        Debug.Log(maxMotorTorque);
    }

    private void Update()
    {
        if (restart)
        {
            restartTimer -= Time.deltaTime;
        }
        if(restartTimer <= 0)
        {
            inAir = false;
            // transform.position = new Vector3(transform.position.x, 10, transform.position.z);
            transform.position = restartPositions[(int)Random.Range(0, restartPositions.Count - 1)].position;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            restart = false;
            con.rb.velocity = Vector3.zero;
            restartTimer = restartTime;
            
        }

        if(Input.GetKeyDown(KeyCode.R) && !restart)
        {
            restart = true;
        }
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            liftCoefficient = -15f;
        } else
        {
            liftCoefficient = -125f; 
        }
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        if (!canMove)
        {
            motor = 0;
        }

        for(int i = 0; i < backLights.Count; i++)
        {
            Material mat = lightMaterials[0]; ;
            if(motor < 0)
            {
                mat = lightMaterials[1];
            }
            backLights[i].GetComponent<Renderer>().material = mat;
        }

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
                ApplyLocalRotationToVisuals(axleInfo.rightWheel);
                ApplyLocalRotationToVisuals(axleInfo.leftWheel);

            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
                if (Input.GetAxis("Vertical") <= 0.01f && Input.GetAxis("Vertical") >= -0.01f)
                {
                    axleInfo.leftWheel.brakeTorque = maxMotorTorque;
                    axleInfo.rightWheel.brakeTorque = maxMotorTorque;
                } else
                {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.rightWheel.brakeTorque = 0;
                }
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }

        float lift = liftCoefficient * con.rb.velocity.sqrMagnitude;
        con.rb.AddForceAtPosition(lift * transform.up, transform.position);
        // con.rb.AddForce(con.rb.velocity.magnitude * 100 * -transform.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        inAir = false;
    }
}


[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}