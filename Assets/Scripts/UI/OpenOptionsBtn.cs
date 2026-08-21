using UnityEngine;

public class OpenOptionsBtn : MonoBehaviour
{

    [Tooltip("Opcional: si este botón está dentro de otro panel (ej: Pausa), " +
             "arrástralo aquí para que se oculte mientras Opciones está abierto. " +
             "Déjalo vacío si el botón no necesita ocultar nada (ej: desde el MainMenu).")]
    public GameObject panelToHide;

    public void OnClick()
    {
        UIPanelOptions panelController = FindFirstObjectByType<UIPanelOptions>();
        
        if (panelController == null)
        {
            Debug.LogError("No se encontró UIPanelController. ¿Está el Prefab 'Managers' instanciado?");
            return;
        }

        if (panelToHide != null)
            panelController.OpenPanel(panelToHide);
        else
            panelController.OpenPanel();
    }
}
