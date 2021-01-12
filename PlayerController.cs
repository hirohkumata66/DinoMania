using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Animator anim;
    public float moveSpeed, jumpForce;
    private string currentState;
    public GameObject fireball;
    public float offset;
    public float offset2;
    private float nextFireTime = 0;
    public float cooldownTime;

    private float velocity;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whayIsGround;

    public bool isKeyboard2;

    [HideInInspector]
    public float powerUpCounter;
    private float speedStore;
    private float gravStore;


    //Animacoes

    const string PLAYER_IDLE = "Idle";
    const string PLAYER_WALKING = "Walking";
    const string PLAYER_JUMPING = "Jumping";

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameManager.instance.AddPlayer(this);

        speedStore = moveSpeed;
        gravStore = rb2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (isKeyboard2)
        {
            velocity = 0f;
            if (velocity == 0f)
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
            if (Keyboard.current.lKey.isPressed)
            {
                ChangeAnimationState(PLAYER_WALKING);
                velocity += 1f;
            }

            if (Keyboard.current.jKey.isPressed)
            {
                ChangeAnimationState(PLAYER_WALKING);
                velocity = -1f;
            }

            if (isGrounded && Keyboard.current.rightShiftKey.wasPressedThisFrame)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                ChangeAnimationState(PLAYER_JUMPING);
            }

            if (!isGrounded && Keyboard.current.rightShiftKey.wasReleasedThisFrame && rb2d.velocity.y > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * .5f);
                ChangeAnimationState(PLAYER_JUMPING);
            }

            if (Time.time > nextFireTime)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame && transform.localScale.x == 1)
                {
                    Instantiate(fireball, new Vector2(transform.position.x + offset, transform.position.y + offset2), Quaternion.Euler(0, 0, 0));
                    nextFireTime = Time.time + cooldownTime;
                    AudioManager.instance.PlaySFX(1);
                }
                if (Keyboard.current.enterKey.wasPressedThisFrame && transform.localScale.x == -1)
                {
                    Instantiate(fireball, new Vector2(transform.position.x - offset, transform.position.y + offset2), Quaternion.Euler(0, 0, 180));
                    nextFireTime = Time.time + cooldownTime;
                    AudioManager.instance.PlaySFX(1);
                }
            }
        }

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whayIsGround);

        rb2d.velocity = new Vector2(velocity * moveSpeed, rb2d.velocity.y);
        if (Time.timeScale != 0)
        {
            if (rb2d.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb2d.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }

            if (rb2d.velocity.x == 0 && rb2d.velocity.y == 0)
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        

        if (powerUpCounter > 0)
        {
            powerUpCounter -= Time.deltaTime;
            if (powerUpCounter <= 0)
            {
                moveSpeed = speedStore;
                rb2d.gravityScale = gravStore;
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>().x;
        ChangeAnimationState(PLAYER_WALKING);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            ChangeAnimationState(PLAYER_JUMPING);
        }

        if (!isGrounded && context.canceled && rb2d.velocity.y > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * .5f);
            ChangeAnimationState(PLAYER_JUMPING);
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }

        anim.Play(newState);

        currentState = newState;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + cooldownTime;
                if (transform.localScale.x == 1)
                {
                    Instantiate(fireball, new Vector2(transform.position.x + offset, transform.position.y + offset2), Quaternion.Euler(0, 0, 0));
                    
                }
                else if (transform.localScale.x == -1)
                {
                    Instantiate(fireball, new Vector2(transform.position.x - offset, transform.position.y + offset2), Quaternion.Euler(0, 0, 180));
                }

            }

            AudioManager.instance.PlaySFX(1);
        }
       
    }
}
