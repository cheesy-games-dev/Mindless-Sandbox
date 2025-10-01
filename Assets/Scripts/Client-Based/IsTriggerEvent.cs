using UltEvents;
using UnityEngine;
using UnityEngine.Events;
public class IsTriggerEvent : MonoBehaviour
{
    public bool isDeadly;
    public float damage = 100;
    public string TriggereTag;
    public UltEvent UnityEvent;

    public float fireRate = 2.5f;

    private float nextTimeToFire = 0f;
    Collider otherCollision;

    private void OnCollisionEnter(Collision collision)
    {
        otherCollision = collision.collider;
        if (collision.gameObject.tag == TriggereTag)
        {
            UnityEvent.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        otherCollision = null;
    }

    private void OnTriggerEnter(Collider collision)
    {
        otherCollision = collision;
        if (collision.gameObject.tag == TriggereTag)
        {
            UnityEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        otherCollision = null;
    }

    private void Update()
    {
        if (isDeadly && otherCollision != null)
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                otherCollision.gameObject.GetComponent<IDamagable>().Damage(new(damage));
            }
        }
    }
}