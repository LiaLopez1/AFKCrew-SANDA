using UnityEngine;
using System.Collections;

public class ControlVignette : MonoBehaviour
{
    [SerializeField] Material materialVignette;
    private string nombrePropiedad = "_Intensity";

    [Header("Editor")]
    [Range(-1f, 1f)]
    public float previewIntensity = -1f;

    private void Start()
    {
        if (materialVignette != null)
        {
            materialVignette.SetFloat(nombrePropiedad, -1f);
            previewIntensity = -1f;
        }
    }
    private void OnValidate()
    {
        if (materialVignette != null)
        {
            materialVignette.SetFloat(nombrePropiedad, previewIntensity);
        }
    }
    public void UpdateVignette(float newValor)
    {
       if (materialVignette != null)
        {
            previewIntensity = newValor;
            materialVignette.SetFloat(nombrePropiedad, newValor);
        }
    }
}
//{
//    public Material materialVignette;
//    private string nombrePropiedad = "_Intensity";

//    private void Start()
//    {
//        if (materialVignette != null)
//        {
//            materialVignette.SetFloat(nombrePropiedad, -1f);
//        }
//    }
//    public void IniciarCierre(float duracion)
//    {
//        StartCoroutine(TransicionVignette(-1f, 1f, duracion));
//    }

//    private IEnumerator TransicionVignette(float inicio, float fin, float tiempo)
//    {
//        float transcurrido = 0f;
//        while (transcurrido < tiempo)
//        {
//            transcurrido += Time.deltaTime;
//            float progreso = transcurrido / tiempo;
//            float valorActual = Mathf.Lerp(inicio, fin, progreso);
//            materialVignette.SetFloat(nombrePropiedad, valorActual);
//            yield return null;
//        }
//        materialVignette.SetFloat(nombrePropiedad, fin);
//    }
//}