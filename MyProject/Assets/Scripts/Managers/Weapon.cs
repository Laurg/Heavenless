using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Weapon : MonoBehaviour
{
    public GameObject bulletprefab;
    public Transform firePoint;
    public AudioSource source;
    public AudioClip clip;

    [SerializeField] private float _fireForce;
    public void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.right *  _fireForce, ForceMode.Impulse);
        source.PlayOneShot(clip);
    }
}
