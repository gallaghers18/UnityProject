using UnityEngine;

public class GunController : MonoBehaviour {

    [SerializeField]
    private float projectileForce = 20.0f;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private GameObject chickenPrefab;
    [SerializeField]
    private Camera cam;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireGun();
        }

        if (Input.GetMouseButtonDown(1))
        {
            FireChicken();
        }

    }

    private void FireGun()
    {
        GameObject projectile = Instantiate(projectilePrefab, cam.transform.position + cam.transform.forward, cam.transform.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), transform.GetComponent<Collider>());
        projectileRB.AddForce(cam.transform.forward*projectileForce, ForceMode.VelocityChange);
    }


    private void FireChicken()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 6.0f))
        {
            Vector3 rawPoint = hit.point + 0.5f * hit.normal;
            Vector3 snappedPoint = new Vector3(Mathf.CeilToInt(rawPoint.x) - 0.5f, Mathf.CeilToInt(rawPoint.y) - 0.5f, Mathf.CeilToInt(rawPoint.z) - 0.5f);
            GameObject chicken = Instantiate(chickenPrefab, snappedPoint, Quaternion.identity);

            chicken.GetComponent<Chicken>().direction = cam.transform.forward;
        }

    }

}
