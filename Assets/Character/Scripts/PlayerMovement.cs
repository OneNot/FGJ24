using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
                SidewaysMovement(-1);
            if (Input.GetKey(KeyCode.D))
                SidewaysMovement(1);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.1f, rb.velocity.y);
        }
            
    }
    
    public void SidewaysMovement(float axis)
    {
        rb.velocity = new Vector2(speed * axis, rb.velocity.y);
    }
}
