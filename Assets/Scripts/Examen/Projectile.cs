using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 25;
    public float speed = 30f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damage damageable = other.GetComponent<Damage>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Debug.Log($"Si tocó");
            }
            Destroy(gameObject);
        }
    }
}