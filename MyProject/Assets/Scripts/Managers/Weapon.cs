using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Weapon : MonoBehaviour
{
    public GameObject bulletprefab;
    public Transform firePoint;
    [SerializeField] private float _fireForce;



    public void Fire()
    {

        GameObject bullet = Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.right *  _fireForce, ForceMode.Impulse);
    }


}
