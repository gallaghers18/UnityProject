using UnityEngine;

public class Grenade : MonoBehaviour {

    //public float delay = 2f;
    public float radius = 7f;
    public float force = 2000f;

    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;

	// Use this for initialization
	void Start () {
        //countdown = delay;
	}
	
	// Update is called once per frame
	void Update () {
        //countdown -= Time.deltaTime;
        //if (countdown <= 0f && !hasExploded)
        //{
            //Explode();
            //hasExploded = true;
        //}
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerMade")
        {
            Explode();
            hasExploded = true;
        }
    }


    void Explode()
    {
        // Show effect
        GameObject explosionGraphic = Instantiate(explosionEffect, transform.position, transform.rotation);

        // Get nearby objects to add force and damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        

        // Remove grenade
        Destroy(gameObject);
        Destroy(explosionGraphic, 1.9f);
       
    }
}
