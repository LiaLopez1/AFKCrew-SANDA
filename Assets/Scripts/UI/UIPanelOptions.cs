using UnityEngine;

public class UIPanelOptions : MonoBehaviour
{
    [Tooltip("El panel que este controlador va a mostrar/ocultar (ej: OptionsPanel)")]
    public GameObject panel;

    // Guarda temporalmente qué panel debe reabrirse al cerrar este
    private GameObject callerPanel;

    // Versión simple: abre el panel sin recordar quién lo llamó
    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    // Versión con "retorno": oculta el panel que llamó, y lo recuerda para reabrirlo después
    public void OpenPanel(GameObject panelToHide)
    {
        callerPanel = panelToHide;
        callerPanel.SetActive(false);
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);

        // Si alguien nos llamó ocultándose, lo volvemos a mostrar
        if (callerPanel != null)
        {
            callerPanel.SetActive(true);
            callerPanel = null;
        }
    }
}
