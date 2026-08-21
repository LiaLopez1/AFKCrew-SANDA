using UnityEngine;

public class Interaction : MonoBehaviour
{
    Controls Controls;

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
        if (Controls.Player.Interact.triggered)
        {
            Interact();
        }
    }
    private void Interact()
    {
        Debug.Log("Interacted!");
    }


}
