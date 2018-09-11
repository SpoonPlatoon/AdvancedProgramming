using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : MonoBehaviour
{
    public float range;
    public float duration;
    public int damage;

    public Enemy enemy;
	// Use this for initialization
	protected virtual void Start ()
    {
        range = 5f;
        damage = 50;
        duration = 1f;
	}
	
    protected virtual void OnEnable()
    {
        Ray shootRay = new Ray(transform.position, transform.forward);
        RaycastHit ray;
        Debug.DrawLine(shootRay.origin, shootRay.origin + shootRay.direction * range);
        Debug.Break();
        if(Physics.Raycast(shootRay.origin, shootRay.direction, out ray, range))
        {
            if(ray.collider.gameObject.CompareTag("Enemy"))
            {
                enemy = ray.collider.gameObject.GetComponent<Enemy>();
                DealDamage(enemy);
            }
        }
        StartCoroutine(Delay());
    }

    protected IEnumerator Delay()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
	
    protected virtual void DealDamage(Enemy enemy)
    {
       enemy.DealDamage(damage);
    }
}
