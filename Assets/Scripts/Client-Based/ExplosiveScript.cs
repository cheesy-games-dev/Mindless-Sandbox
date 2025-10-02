using System.Collections;
using System.Collections.Generic;
using MindlessSDK;
using UnityEngine;

public class ExplodeScript : MonoBehaviour, ICrateBarcode
{
    public float delay = 3f;
    public float radius = 6f;
    public float force = 500f;
    public float damage = 50f;
    public CrateBarcode<SpawnableCrate> explosionVFX;
    public GameObject explosionEffect;

    float countdown;
    bool detonate = false;
    bool hasExploded = false;

    // Update is called once per frame
    private void Update() {
        if (!detonate)
            return;
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded) {
            Explode();
            hasExploded = true;
        }
    }

    public void DetonateEvent() {
        detonate = true;
    }

    public void Explode()
    {
        ExplodeCallback();
    }

    public void ExplodeCallback()
    {
        GameObject explosion = AssetSpawner.Instance.Spawn(explosionVFX.Crate, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
            IDamagable damagable = nearbyObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.Damage(new(damage));
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision == null) return;
        if(GetComponent<Rigidbody>().linearVelocity.magnitude > 10) {
            DetonateEvent();
        }
    }

    void Start()
    {
        Validate();
        countdown = delay;
    }

    public void Validate()
    {
        if (explosionVFX.Crate)
            explosionVFX.Barcode = explosionVFX.Crate.Barcode;
        AssetWarehouse.Instance.TryGetCrate<Crate>(explosionVFX.Barcode, out var crate);
    }
}
