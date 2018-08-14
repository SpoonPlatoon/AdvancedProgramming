using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public float duration = 1f;
    public int damage = 50;

    // This function is called when the object becomes enabled and active
    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect enemy
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            //deal damage
            enemy.DealDamage(damage);

            //disable weapon
            gameObject.SetActive(false);
        }
    }
}
