using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
     Destroy(gameObject);
    //hitting enemy
    //damage enemy
        
    }
}
