using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour {

    public Transform shootOrigin;
    public GameObject hitParticles;

    public int damage = 1;

    public Vector3 force;
    public float forceStrength = 2f;

    public float explosionPower;
    public float explosionRadius = 20f;

    Vector3 explosionPos;
    Collider[] colliders;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Shoot();
	}

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1")) {
            RaycastHit hit;
            
            if(Physics.Raycast(shootOrigin.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                force = shootOrigin.forward * forceStrength;

                Debug.DrawRay(shootOrigin.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Hit!");

                hit.collider.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);

                if (hit.rigidbody)
                    hit.rigidbody.AddForceAtPosition(force, hit.point);
                else
                {
                    explosionPos = hit.point;
                    colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
                    foreach(Collider affected in colliders)
                    {
                        Rigidbody rb = affected.GetComponent<Rigidbody>();

                        if (rb != null)
                            rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 1.5f);
                    }
                }

                Vector3 incomingVec = hit.point - shootOrigin.position;
                Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
                GameObject clone = Instantiate(hitParticles, hit.point, Quaternion.Euler(reflectVec));
            }
            else
            {
                Debug.DrawRay(shootOrigin.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
                Debug.Log("Did not hit.");
            }
        }
    }
}
