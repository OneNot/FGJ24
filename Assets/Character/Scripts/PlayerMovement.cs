using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 1;
    [SerializeField] bool grounded = false;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponTip;
    bool weaponOnCooldown = false;
    Quaternion startRotation;
    int spinnercount = 0;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = weapon.transform.rotation;
        weaponOnCooldown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //spinning weapon
        if (weaponOnCooldown){
            spinnercount++;
            weapon.transform.Rotate(new Vector3(0,0,-10));
            if(spinnercount >= 36)
            {
                weaponOnCooldown = false;
                weapon.transform.rotation = startRotation;
            }
        }
    }

    public void SidewaysMovement(float axis)
    {
        if (axis < 0.5 && axis > -0.5)
            rb.velocity = new Vector2(Time.deltaTime * rb.velocity.x * 0.1f, rb.velocity.y);
        else
            rb.velocity = new Vector2(speed * axis, rb.velocity.y);
            
    }
    public void Vault () {
        //if not grounded add movement forwards and do below
        WeaponAttackPlay();
    }

    public void WeaponAttackPlay () 
    {
        if (!weaponOnCooldown)
        {
            weaponOnCooldown = true;
            spinnercount = 0;
        }
    }
    
    public void WeaponTriggered ()
    {
        rb.AddForce(weaponTip.transform.position * 100);
    }
}
