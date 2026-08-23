using UnityEngine;

public class SceneButton : MonoBehaviour
{
    [Tooltip("Nombre exacto de la escena a la que este botón debe llevar")]
    public string targetSceneName;

    public void GoToThisScene()
    {
        CountDown countDown = FindAnyObjectByType<CountDown>();
        countDown.ResetTimer();
        SceneTransitionManager.Instance.TransitionToScene(targetSceneName);
    }
}