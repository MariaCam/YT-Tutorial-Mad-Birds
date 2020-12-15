using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] GameObject _poofAnimation;

    private void OnCollisionEnter2D(Collision2D collision){
        //if you collide with a bird then destroy the monster
        Bird bird = collision.collider.GetComponent<Bird>();
        Enemy enemy = collision.collider.GetComponent<Enemy>();

        if(bird != null){
            Instantiate(_poofAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        if(enemy != null){
             return;
        }

        //collision records how many things it collides with, so contact[0] is the first point of contact and normal is the nagle the collision is at.
        if(collision.contacts[0].normal.y < -0.5){
            Instantiate(_poofAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
