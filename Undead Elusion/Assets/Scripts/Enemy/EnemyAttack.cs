using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    // Configuration parameters
    [SerializeField] float attackDamage = 40f;


    // Cached references
    PlayerHealth targetPlayerHealth = null;

    // Start is called before the first frame update
    void Start() {
        targetPlayerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void AttackHitEvent() {
        if (targetPlayerHealth == null) { return; }

        Debug.Log("Zombie Attack");
        targetPlayerHealth.DamagePlayer(attackDamage);

    }

}
