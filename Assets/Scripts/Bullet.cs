using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private Vector2 Direction;

    private Rigidbody2D Rigidbody;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Rigidbody.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }
}
