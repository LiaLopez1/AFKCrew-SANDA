using UnityEngine;

public class CountDown : MonoBehaviour
{
    
    [SerializeField]float remainingTime;
    [SerializeField]float closeTime = 1.5f;


    private void Update()
    {
        if (remainingTime >0)
        {
            remainingTime -= Time.deltaTime; 
        }
        else if(remainingTime < 0) 
        {
            remainingTime = 0;
            ControlVignette vignette = Object.FindFirstObjectByType<ControlVignette>();

            if (vignette != null)
            {
                vignette.IniciarCierre(closeTime);
            }
        }
    }
}
