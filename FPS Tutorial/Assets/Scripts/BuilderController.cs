using UnityEngine;

[RequireComponent(typeof(BuilderMotor))]
public class BuilderController : MonoBehaviour
{

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private BuilderMotor motor;

    private void Start()
    {
        motor = GetComponent<BuilderMotor>();
    }

    private void Update()
    {
 
        Vector3 _displacement = getDirection();
        motor.Move(_displacement*speed);

        //Calculate player rotation as a 3D vector (sideways)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector (up & down)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        //Apply rotation
        motor.RotateCamera(_cameraRotation);

        //Keep cursor locked/invisible
        //if (Cursor.lockState != CursorLockMode.Locked)
        //{
            //Cursor.lockState = CursorLockMode.Locked;
        //}

    }

    private Vector3 getDirection()
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.X))
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        return p_Velocity;
    }




}
