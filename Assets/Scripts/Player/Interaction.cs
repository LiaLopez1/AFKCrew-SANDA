using UnityEngine;

public class Interaction : MonoBehaviour
{
    Controls Controls;
    private Interactable currentInteractable;

    private void Awake()
    {
        Controls = new();
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }

   void Update()
    {
        if (Controls.Player.Interact.WasPressedThisFrame())
        {
            Interact();
        }
    }
    private void Interact()
    {
        if (currentInteractable == null) return;

        currentInteractable.Interaction();

        // Si el interactable se desactivó a sí mismo (ej. fragmento recogido),
        // OnTriggerExit nunca se dispara para objetos inactivos, así que limpiamos acá.
        if (currentInteractable is Component comp && (comp == null || !comp.gameObject.activeInHierarchy))
        {
            currentInteractable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out var interactable))
        {
            // Si ya había otro interactable en rango (colliders solapados), le ocultamos su ícono.
            if (currentInteractable != null && (object)currentInteractable != (object)interactable)
                currentInteractable.HidePrompt();

            currentInteractable = interactable;
            currentInteractable.ShowPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out var interactable) &&
            (object)interactable == (object)currentInteractable)
        {
            currentInteractable.HidePrompt();
            currentInteractable = null;
        }
    }




}