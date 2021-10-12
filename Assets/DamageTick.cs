using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;


public class DamageTick : MonoBehaviour
{
    public EntityState state;
    public Collider2D damageCollider;

    void TakeDamage(Vector2 enemyPos, int damage = 1) {
        state.CurrentHealth -= damage;
        if (state.CurrentHealth <= 0) {
            Destroy(gameObject);
        }
        else {
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            var es = transform.gameObject.GetComponent<EntityState>();
            var mt = transform.gameObject.GetComponent<MovementTick>();

            mt.maxSpeed = 100f;

            int xDirection = 1;

            es.currentVelocity = 14 * new Vector2(xDirection, 0);
        }
    }

    IEnumerator TakeDamageAfter(Vector2 enemyPos, int damage, float secs)
    {
        yield return new WaitForSeconds(secs);
        TakeDamage(enemyPos, damage);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Debug.Log("DamageTick <attack tag>: " + other.gameObject.name);
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            StartCoroutine(TakeDamageAfter(other.transform.position, 1, 0.2f));
        }
    }

}
