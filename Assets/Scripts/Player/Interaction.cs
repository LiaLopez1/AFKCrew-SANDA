using UnityEngine;

public class Interaction : MonoBehaviour
{
    Controls Controls;
    private Interactable currentInteractable;
    Animator animator;

    // NUEVO: Referencia directa a tu script de movimiento
    private PlayerMove playerMove;

    private void Awake()
    {
        Controls = new();
        animator = GetComponentInChildren<Animator>();

        // Obtiene el componente de movimiento
        playerMove = GetComponent<PlayerMove>();
    }

    private void OnEnable() => Controls.Enable();
    private void OnDisable() => Controls.Disable();

    void Update()
    {
        // Bloqueamos la entrada del botón de interactuar si el jugador ya está interactuando
        if (playerMove != null && !playerMove.CanMove) return;

        if (Controls.Player.Interact.WasPressedThisFrame())
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (currentInteractable == null) return;

        // 1. BLOQUEAR MOVIMIENTO
        if (playerMove != null) playerMove.CanMove = false;

        animator.SetTrigger("Interact");
        currentInteractable.Interaction();

        if (currentInteractable is Component comp && (comp == null || !comp.gameObject.activeInHierarchy))
        {
            currentInteractable = null;
        }
    }

    // 2. FUNCIÓN PARA EL ANIMATION EVENT
    // Recuerda colocar este evento al final de tu animación "Interact" en Unity
    public void EndInteractionAnimation()
    {
        if (playerMove != null) playerMove.CanMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Interactable>(out var interactable))
        {
            if (currentInteractable != null && (object)currentInteractable != (object)interactable)
                currentInteractable.HidePrompt();

            currentInteractable = interactable;
            currentInteractable.ShowPrompt();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Interactable>(out var interactable) &&
            (object)interactable == (object)currentInteractable)
        {
            currentInteractable.HidePrompt();
            currentInteractable = null;
        }
    }
}
