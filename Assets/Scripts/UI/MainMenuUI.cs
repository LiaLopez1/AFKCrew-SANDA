using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Tooltip("Nombre exacto de la escena de juego")]
    public string gameSceneName = "Escenario_02";

    public void OnPlayButtonPressed()
    {
        SceneTransitionManager.Instance.TransitionToScene(gameSceneName);
    }
}


