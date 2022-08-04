using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    // Configuration parameters
    [SerializeField] float maxHitPoints = 100f;

    // State variables
    float currentHitPoints;
    bool isDead = false;

    // Cached references
    Animator myAnimator = null;

    // Start is called before the first frame update
    private void Start() {
        currentHitPoints = maxHitPoints;
        myAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) {
        currentHitPoints -= damage;
        BroadcastMessage("OnDamageTaken");

        if (currentHitPoints <= 0) {
            Die();
        }
    }

    private void Die() {
        if (isDead) { return; }
        isDead = true;
        myAnimator.SetTrigger("startDeath");
    }

    public bool IsDead() {
        return isDead;
    }

    

}
