using UnityEngine;

public class Interaction : MonoBehaviour
{
    Controls Controls;
    Fragment currentFragment;

    private void Awake()
    {
        Controls = new();
        Controls.Player.Interact.performed += ctx => TryInteract();
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }
    private void TryInteract()
    {
        if (currentFragment != null)
        {
            currentFragment.CollectFragment();
            currentFragment = null; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Fragment>(out Fragment fragment))
        {
            this.currentFragment = fragment;
            Debug.Log("Presiona el botón para recoger el objeto.");
        }
    }

    // Detecta cuando el jugador se aleja del objeto
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Fragment>(out Fragment item))
        {
            if (currentFragment == item)
            {
                currentFragment = null;
            }       
        }
    }
}
