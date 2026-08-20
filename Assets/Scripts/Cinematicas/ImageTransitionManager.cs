using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTransitionManager : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private GameObject finalButton; // Botón que aparece al final
    [SerializeField] private GameObject nextButton; // Botón para avanzar a la siguiente imagen

    private int currentImageIndex = 0;

    private void Start()
    {
        for (int i = 0; i < images.Count; i++)
        {
            var canvasGroup = GetOrAddCanvasGroup(images[i]);
            canvasGroup.alpha = (i == 0) ? 1f : 0f;
            images[i].gameObject.SetActive(i == 0);
        }

        if (finalButton != null)
        {
            finalButton.SetActive(false); // Ocultar al inicio
        }
        if (nextButton != null)
        {
            nextButton.SetActive(true);
        }
    }

    public void NextImage()
    {
        print("Siguiente");
        if (currentImageIndex < images.Count - 1)
        {
            int nextIndex = currentImageIndex + 1;
            StartCoroutine(TransitionToImage(currentImageIndex, nextIndex));
            currentImageIndex = nextIndex;
        }

        // Mostrar botón si es la última imagen
        if (currentImageIndex == images.Count - 1 && finalButton != null)
        {
            finalButton.SetActive(true);
            nextButton.SetActive(false);

            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(finalButton);

            // Asegurar que el CanvasGroup esté listo (opcional, si usas CanvasGroup)
            CanvasGroup cg = finalButton.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
        }

    }
    //public void PreviousImage()
    //{
    //    print("Siguiente");
    //    if (currentImageIndex > 0)
    //    {
    //        int prevIndex = currentImageIndex - 1;
    //        StartCoroutine(TransitionToImage(currentImageIndex, prevIndex));
    //        currentImageIndex = prevIndex;

    //        if (finalButton != null)
    //        {
    //            finalButton.SetActive(false); // Oculta si retrocede
    //        }
    //    }
    //}

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
