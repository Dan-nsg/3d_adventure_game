using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int damageAmount = 1;
    public float speed = 50f;

    public List<string> tagsToHit;

    private void Awake() 
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update() 
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    private void OnCollisionEnter(Collision collision) 
    {
        var damageable = collision.transform.GetComponent<IDamageable>();
        
        if (tagsToHit.Contains(collision.transform.tag))
        {
            if(damageable != null)
            {
                Vector3 dir = collision.transform.position - transform.position;
                dir = -dir.normalized;
                dir.y = 0;

                damageable.Damage(damageAmount, dir);
            }

            Destroy(gameObject);
        }
    }
}
