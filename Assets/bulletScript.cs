using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bulletScript : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float death;

    public void SetDirection(Vector3 dir)
    {
        this.direction = dir;
        rb.velocity = new Vector2(dir.x, dir.y) * speed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null && collision.gameObject.CompareTag("Player"))
        {
            damageable.TakeDamage(damage);
            Die();
        }
    }
    void Update()
    {
        death -= Time.deltaTime;
        if (death < 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

}
