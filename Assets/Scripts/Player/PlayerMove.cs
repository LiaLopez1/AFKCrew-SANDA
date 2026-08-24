using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Controls Controls;

    public Vector3 Direction { get; private set; }
    [SerializeField] private Rigidbody2D rb;
    public float speedMov;
    private Animator animator;

    private bool facingRight = true;

    // NUEVO: Propiedad para activar/desactivar el movimiento desde fuera
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        Controls = new();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable() => Controls.Enable();
    private void OnDisable() => Controls.Disable();

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            Direction = Vector3.zero;
            return;
        }

        // MODIFICADO: Si no puede moverse, forzamos la dirección a cero y apagamos la animación
        if (!CanMove)
        {
            Direction = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            return;
        }

        Direction = Controls.Player.Move.ReadValue<Vector2>();
        animator.SetFloat("Speed", Mathf.Abs(Direction.x) + Mathf.Abs(Direction.y));

        if ((Direction.x > 0 && !facingRight) || (Direction.x < 0 && facingRight))
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        // Al poner Direction en cero arriba, el Rigidbody se detendrá instantáneamente aquí
        rb.linearVelocity = new Vector2(speedMov * Direction.x, speedMov * Direction.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
