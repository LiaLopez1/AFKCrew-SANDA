using UnityEditor;
using UnityEngine;

public class CountDown : MonoBehaviour
{

    [SerializeField] float totalTime;
    [SerializeField]public float remainingTime;
    
    ControlVignette vignette;

    private void Start()
    {
        remainingTime = totalTime;
        vignette = Object.FindFirstObjectByType<ControlVignette>();
    }

    private void Update()
    {
        if (remainingTime >0)
        {
            remainingTime -= Time.deltaTime;
            float progress = 1 - (remainingTime / totalTime);
            float vignetteIntensity = Mathf.Lerp(-1f, 1f, progress);
            if (vignette != null)
            {
                vignette.UpdateVignette(vignetteIntensity);
            }
        }
        else if(remainingTime < 0) 
        {
            remainingTime = 0;
            if (vignette != null)
            {
                vignette.UpdateVignette(1f);
            }

            Debug.Log("Game Over");
        }
    }
}
