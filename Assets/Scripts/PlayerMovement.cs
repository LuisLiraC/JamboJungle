using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private float JumpForce;

    private Animator Animator;
    private float Horizontal;
    private Rigidbody2D Rigidbody;
    private bool IsTouchingTheGround;

    private float LastShoot;
    [SerializeField]
    private float ShootDelay;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = new Vector2(Horizontal, Rigidbody.velocity.y);
    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);

        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
            IsTouchingTheGround = true;
        else
            IsTouchingTheGround = false;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + ShootDelay)
        {
            Shoot();
            LastShoot = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsTouchingTheGround)
        {
            Jump();
        }
        

        if (Horizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (Horizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        Animator.SetBool("Running", Horizontal != 0.0f);
        Animator.SetBool("IsTouchingGround", IsTouchingTheGround);
    }

    void Shoot()
    {
        Vector3 direction = transform.localScale.x == 1f ? Vector2.right : Vector2.left;
        var bullet = Instantiate(Bullet, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    void Jump()
    {
        Rigidbody.AddForce(Vector2.up * JumpForce);
    }
}
