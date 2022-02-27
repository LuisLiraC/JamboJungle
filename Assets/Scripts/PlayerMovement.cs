using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator Animator;
    private float Horizontal;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal < 0) GetComponent<SpriteRenderer>().flipX = true;
        if (Horizontal > 0) GetComponent<SpriteRenderer>().flipX = false;

        Animator.SetBool("Running", Horizontal != 0.0f);
    }
}
