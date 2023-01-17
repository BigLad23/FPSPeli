using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Enemy health is " + health);
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        GameController.instance.EnemyKilled();
        Destroy(gameObject);
    }
}
