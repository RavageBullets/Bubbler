using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsDead;

    public void Die (){
            this.IsDead = true;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

    }
}
