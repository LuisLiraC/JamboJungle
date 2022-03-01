using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;

    private Animator Animator;
    private float Horizontal;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (Horizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        Animator.SetBool("Running", Horizontal != 0.0f);
    }

    void Shoot()
    {
        Vector3 direction = transform.localScale.x == 1f ? Vector2.right : Vector2.left;
        var bullet = Instantiate(Bullet, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}
