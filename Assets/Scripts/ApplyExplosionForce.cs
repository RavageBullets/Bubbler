using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyExplosionForce : MonoBehaviour
{

    private List<Collider2D> Colliders = new List<Collider2D>();
    
    public float explosiveForce = 30000f;
    public float explosiveForceOnPlayer = 100f;
    private void OnTriggerEnter2D(Collider2D objectInCollider)
    {
        if (!Colliders.Contains(objectInCollider)) { Colliders.Add(objectInCollider); }
    }

    private void OnTriggerExit2D(Collider2D objectLeavingCollider)
    {
        Colliders.Remove(objectLeavingCollider);
    }

    public void Explode()
    {
        foreach (Collider2D objectInCollider in Colliders)
        {
            var rigidbodyInCollider = objectInCollider.gameObject.GetComponent<Rigidbody2D>();
            if (rigidbodyInCollider != null){
                if(objectInCollider.gameObject.tag == "Player")
                {
                    rigidbodyInCollider.AddForce((objectInCollider.transform.position - this.transform.position).normalized * ( 1 / (objectInCollider.transform.position - this.transform.position).magnitude )  * explosiveForceOnPlayer, ForceMode2D.Impulse);
                }
                else
                {
                    rigidbodyInCollider.AddForce((objectInCollider.transform.position - this.transform.position).normalized * explosiveForce, ForceMode2D.Impulse);
                }
            }
        }
    }

}
