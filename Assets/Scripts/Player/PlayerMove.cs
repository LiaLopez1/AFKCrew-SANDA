using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Controls Controls;

    public Vector3 Direction { get; private set; }
    [SerializeField]private Rigidbody rb;
    public float speedMov;
    //private Animator animator;

    //public bool isMoving = false;
    private bool facingRight = true;
    //private JumpPlayer jumpPlayer;
    private void Awake()
    {
        Controls = new();
        //animator = GetComponentInChildren<Animator>();
        //jumpPlayer = GetComponent<JumpPlayer>();
    }
    private void OnEnable()
    {
        Controls.Enable();
    }
    private void OnDisable()
    {
        Controls.Disable();
    }
    private void Update()
    {
        Direction = Controls.Player.Move.ReadValue<Vector2>();
        //animator.SetFloat("Speed", Mathf.Abs(Direction.x));
        if ((Direction.x > 0 && !facingRight) || (Direction.x < 0 && facingRight))
        {
            Flip();
        }
        //if (Mathf.Abs(Direction.x) > 0.1f && jumpPlayer.IsGrounded)
        //{
        //    if (!isMoving)
        //    {
        //        isMoving = true;
        //        PlayWalkSound();
        //    }

        //}
        //else
        //{
        //    if (isMoving)
        //    {
        //        isMoving = false;
        //        StopWalkSound();
        //    }

        //}
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(speedMov * Direction.x,/* rb.linearVelocity.y,*/ speedMov * Direction.y);
    }


    private void Flip()
    {
        facingRight = !facingRight; // Cambia el estado de dirección
        Vector3 localScale = transform.localScale; // Obtiene la escala actual
        localScale.x *= -1; // Invierte el eje X
        transform.localScale = localScale; // Aplica la nueva escala
    }
}
