using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    
    void onCollisionEnter(Collision collision) {
        
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if(gameObject.tag != "Player")
        {
            Debug.Log("Player hit");
            player.PlayerTakeDamage(damage);
        }
}
}
