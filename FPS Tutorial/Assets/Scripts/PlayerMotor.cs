using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private float distToGround;

    private Rigidbody rb;
    private Collider hitBox;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hitBox = GetComponent<Collider>();
        distToGround = hitBox.bounds.extents.y;
        
    }

    //Get a velocity
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Get a rotation
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Get a camera rotation
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    //Run every physics iteration
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Perform movement based on velocity variable
    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity*Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(hitBox.bounds.center, -Vector3.up, distToGround + 0.1f);
    }
    

    public void Jump(Vector3 jumpVector)
    {
        if (IsGrounded())
        {
            rb.AddForce(jumpVector, ForceMode.VelocityChange);
        }
    }

}

