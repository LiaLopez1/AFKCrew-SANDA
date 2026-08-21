using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Tooltip("El panel que contiene Continuar/Menú/Opciones")]
    public GameObject pausePanel;

    private bool isPaused = false;

    // Llamado por el botón de Pausa (el que está siempre visible en el HUD)
    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // congela el juego (movimiento, física, animaciones)
    }

    // Llamado por el botón "Continuar" dentro del panel
    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // reanuda el juego con normalidad
    }
}