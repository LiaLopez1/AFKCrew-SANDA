using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoImageTransitionManager : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float intervalBetweenImages = 2f;
    [SerializeField] private string nextSceneName; // Nombre de la escena a cargar

    private int currentImageIndex = 0;

    private void Start()
    {
        for (int i = 0; i < images.Count; i++)
        {
            var canvasGroup = GetOrAddCanvasGroup(images[i]);
            canvasGroup.alpha = (i == 0) ? 1f : 0f;
            images[i].gameObject.SetActive(i == 0);
        }

        StartCoroutine(AutoAdvanceImages());
    }

    private IEnumerator AutoAdvanceImages()
    {
        while (currentImageIndex < images.Count - 1)
        {
            yield return new WaitForSecondsRealtime(intervalBetweenImages);

            int nextIndex = currentImageIndex + 1;
            yield return StartCoroutine(TransitionToImage(currentImageIndex, nextIndex));
            currentImageIndex = nextIndex;
        }

        yield return new WaitForSecondsRealtime(intervalBetweenImages); // Espera final antes de cambiar

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No se asign� el nombre de la siguiente escena.");
        }
    }

    private IEnumerator TransitionToImage(int fromIndex, int toIndex)
    {
        CanvasGroup fromGroup = GetOrAddCanvasGroup(images[fromIndex]);
        CanvasGroup toGroup = GetOrAddCanvasGroup(images[toIndex]);

        toGroup.alpha = 0f;
        toGroup.gameObject.SetActive(true);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / fadeDuration;
            fromGroup.alpha = Mathf.Lerp(1f, 0f, t);
            toGroup.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        fromGroup.alpha = 0f;
        toGroup.alpha = 1f;
        fromGroup.gameObject.SetActive(false);
    }

    private CanvasGroup GetOrAddCanvasGroup(Image image)
    {
        CanvasGroup cg = image.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = image.gameObject.AddComponent<CanvasGroup>();
        }
        return cg;
    }
}
