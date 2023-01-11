using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public Transform EnemyGroundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    bool isGrounded;
    Vector3 velocity;

    void Update()
    {
        isGrounded = Physics.CheckSphere(EnemyGroundCheck.position, groundDistance, groundMask); // This checks if the enemy is standing on ground or how far the enemy is from ground

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(health);
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
