using UnityEngine;

public class BuilderMotor : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    private Vector3 displacement = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    

    //Get a displacement
    public void Move(Vector3 _displacement)
    {
        displacement = _displacement;
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
        if (displacement != Vector3.zero)
        {
            transform.Translate(displacement * Time.deltaTime);
        }
    }

    private void PerformRotation()
    {       
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        transform.Rotate(rotation);
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }

    }



}

