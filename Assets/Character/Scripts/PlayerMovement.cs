using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    //references
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerAudio playerAudio;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponTip;
    [SerializeField] GameObject midairVault;
    [SerializeField] GameObject groundVault;
    [SerializeField] CharacterInputManager characterInputManager;
    [SerializeField] GameObject faceSprite; 

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
    //[SerializeField] float groundedAngleBounce;
    //[SerializeField] float midairAngleBounce;
    
    
    //dash
    [SerializeField] private float DashMult = 1;
    [SerializeField] bool canDashAgain = false;
    [SerializeField] float originalGravityScale = 2;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        inGameUIManager.SetLifeAmount(lives);
        weaponOnCooldown = false;
        canBounceAgain = true;
    }
    [SerializeField] GameObject removeme;
    void Update () {
        RaycastHit2D hit = Physics2D.Raycast(rb.transform.position, -Vector2.up, 10.6f);
        removeme.transform.position = hit.point;

         if (hit.collider != null)
         {
            weaponAttackAvailable = true;
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
         inGameUIManager.SetDashState(canDashAgain && grounded == false);
         inGameUIManager.SetVaultState(!weaponOnCooldown && canBounceAgain && weaponAttackAvailable);
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
    bool weaponAttackAvailable = true;
    public void Vault () {
        if (weaponAttackAvailable)
            WeaponAttackPlay();
    }
    
    public void Dash (Vector2 direction) {
        if (grounded == false && canDashAgain == true)
        {
            playerAudio.PlayRandomDash();
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
        weaponAttackAvailable = false;
        if (weaponOnCooldown == false)
        {
            canBounceAgain = true;
            weaponOnCooldown = true;
            spinnercount = 0;
        }
    }
    
    public void WeaponTrigger ()
    {
        if(weaponOnCooldown && canBounceAgain)
        {
            weaponAttackAvailable = true;
            playerAudio.PlayRandomRubberChicken();
            canBounceAgain = false;
            canDashAgain = true;
            Vector2 direction = (grounded? groundVault : midairVault).transform.position-transform.position;
            
            /* if(grounded)
            {
                groundedAngleBounce=0;
                if (facingRight)
                    direction = Quaternion.AngleAxis(groundedAngleBounce, Vector3.forward).eulerAngles * direction;
                else
                    direction = Quaternion.AngleAxis(groundedAngleBounce, Vector3.forward).eulerAngles * direction;
            }
            else
            {
                direction = Quaternion.AngleAxis(midairAngleBounce, Vector3.forward).eulerAngles;
            }
            Debug.Log(direction); */

            rb.AddForce(direction * (100 * bounceMult));
        }
    }
    //[SerializeField] private GameObject LevelStartUIThing;

    [SerializeField] private InGameUIManager inGameUIManager;
    public void EntryAnimationEnded () 
    {
        characterInputManager.AllowInput = true;
        inGameUIManager.StartGameTimer();

        animator.SetBool("entryanimplaying", false);
        //LevelStartUIThing.SetActive(true); //do later if got time
    }

    int lives = 3;
    bool canTakeDamage = true;
    public void takeDamage (Vector3 enemyPosition) {
        if(canTakeDamage)
        {
            faceSprite.transform.localScale = new Vector3(0.3f,0.3f,1);
            ApplyKnockback(enemyPosition);
            canTakeDamage = false;
            StartCoroutine(InvulnPeriod());
            lives--;
            inGameUIManager.SetLifeAmount(lives);
            if (lives < 1)
            {
                characterInputManager.AllowInput = false;
                inGameUIManager.GameOver(false);
            }
        }
    }

    private void ApplyKnockback(Vector3 enemyPosition){
        Vector2 direction = enemyPosition-transform.position;
        rb.velocity = Vector3.zero;
        rb.AddForce(-direction * 250);
    }

    private IEnumerator InvulnPeriod ()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
        faceSprite.transform.localScale = new Vector3(1,1,1);
    }

    private void OnDisable() {
        StopAllCoroutines();
    }
}
