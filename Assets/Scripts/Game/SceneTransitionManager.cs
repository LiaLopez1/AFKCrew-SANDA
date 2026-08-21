using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance{get; private set;}

    [Header("Referencias")]
    public CanvasGroup fadeCanvasGroup; // el telon negro
    public float fadeDuration = 1f;

    private void Awake()
    {
        /*if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }*/

        Instance = this;
        //DontDestroyOnLoad(gameObject);
        
        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false;
    }
 
    

    public void TransitionToScene(string sceneName)
    {
        Time.timeScale = 1f;
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(Fade(0f, 1f)); // se ocurece la pantalla

        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while(!AsyncLoad.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(Fade(1f, 0f)); //la pantalla se aclara
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        fadeCanvasGroup.alpha = startAlpha;
        
        // Activamos el CanvasGroup para que bloquee clicks mientras está en pantalla
        fadeCanvasGroup.blocksRaycasts = true;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;


            // Mathf.Lerp interpola suavemente entre startAlpha y endAlpha
            // según qué tan avanzado vamos en el tiempo (elapsed / fadeDuration)
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null; // espera al siguiente frame
        }

        fadeCanvasGroup.alpha = endAlpha; // asegura que termine exacto en el valor final

        // Si terminamos en transparente (0), desactivamos el bloqueo de clicks
        if (endAlpha == 0f)
            fadeCanvasGroup.blocksRaycasts = false;
    }
}
