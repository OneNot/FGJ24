using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //animation
    [SerializeField] private Animator animator;


    //references
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponTip;

    //ground movement
    [SerializeField] float speed = 1;
    [SerializeField] float maxWalkSpeed = 10;
    [SerializeField] bool grounded = false;
    [SerializeField] bool facingRight = true;

    //vault
    [SerializeField] bool weaponOnCooldown = false;
    [SerializeField] public bool canBounceAgain = true;
    [SerializeField] float spinnercount = 0;
    [SerializeField] float bounceMult = 1;
    [SerializeField] float groundedAngleBounce;
    [SerializeField] float midairAngleBounce;
    
    
    //dash
    [SerializeField] private float DashMult = 1;
    [SerializeField] bool canDashAgain = false;
    [SerializeField] float originalGravityScale = 2;

    // Start is called before the first frame update
    void Start()
    {
        weaponOnCooldown = false;
        canBounceAgain = true;
    }
    [SerializeField] GameObject removeme;
    void Update () {
        RaycastHit2D hit = Physics2D.Raycast(rb.transform.position, -Vector2.up, 3.6f);
        removeme.transform.position = hit.point;

         if (hit.collider != null)
         {
            if (grounded == false)
            {
                animator.SetBool("grounded", true);
                canDashAgain = false;
                grounded = true;
            }
         }
         else
         {
            if (grounded == true)
            {
                animator.SetBool("grounded", false);
                canDashAgain = true;
                grounded = false;
            }
         }
        //angle = Vector2.Angle(transform.right, weaponTip.transform.position - transform.position);
        //Debug.Log(angle);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //spinning weapon
        if (weaponOnCooldown){
            spinnercount += 100 * Time.deltaTime;
            weapon.transform.Rotate(new Vector3(0,0,(-1000 * Time.deltaTime)));
            if(spinnercount >= 36)
            {
                canBounceAgain = true;
                weaponOnCooldown = false;
                weapon.transform.rotation = facingRight ? Quaternion.identity : Quaternion.Euler(new Vector3(0,180,0));
            }
        }
    }

    public void SidewaysMovement(float axis)
    {
        if (!weaponOnCooldown)
        {
            if(facingRight == false && axis > 0)
            {
                facingRight = true;
                TurnAround();
            }
            else if(facingRight == true && axis < 0)
            {
                facingRight = false;
                TurnAround();
            }
        }

        if(grounded == true)
        {
            animator.SetFloat("walkspeed", Mathf.Abs(rb.velocity.x));
            if ( Mathf.Abs(rb.velocity.x) < maxWalkSpeed )
                rb.AddForce(new Vector2((axis*10) * (1000 * Time.deltaTime),0));
            //if (axis < 0.5 && axis > -0.5)
                //rb.velocity = new Vector2(Time.deltaTime * rb.velocity.x * 0.1f, rb.velocity.y);
            //else
                //rb.velocity = new Vector2(speed * axis, rb.velocity.y);
        }
    }
    private void TurnAround () {
        transform.Rotate(new Vector3(0,180,0));
    }
    public void Vault () {
        //if not grounded add movement forwards and do below
        WeaponAttackPlay();
    }
    
    public void Dash (Vector2 direction) {
        if (grounded == false && canDashAgain == true)
        {
            Debug.Log(direction);
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;

            canDashAgain = false;
            rb.AddForce(direction * DashMult);
            StartCoroutine(SetGravityScaleAfterDelay());
        }
    }

    private IEnumerator SetGravityScaleAfterDelay ()
    {
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = originalGravityScale;
    }
        
    private void WeaponAttackPlay () 
    {
        if (weaponOnCooldown == false)
        {
            canBounceAgain = true;
            weaponOnCooldown = true;
            spinnercount = 0;
        }
    }
    
    public void WeaponTrigger ()
    {
        Debug.Log("" + weaponOnCooldown + " " + canBounceAgain);
        if(weaponOnCooldown && canBounceAgain)
        {
            canBounceAgain = false;
            canDashAgain = true;

            Vector2 direction = weaponTip.transform.position-transform.position;
            if(grounded)
            {
                direction = Quaternion.AngleAxis(groundedAngleBounce, direction).eulerAngles;
            }
            else
            {
                direction = Quaternion.AngleAxis(midairAngleBounce, direction).eulerAngles;
            }
            Debug.Log(direction);

            rb.AddForce(direction * (100 * bounceMult));
        }
        
    }

    private void OnDisable() {
        StopAllCoroutines();
    }
}
