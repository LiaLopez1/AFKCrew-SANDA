using UnityEngine;
using System.Collections;

public class ControlVignette : MonoBehaviour
{
    public Material materialVignette;

    private string nombrePropiedad = "_Intensity";

    private void Start()
    {
        // Nos aseguramos de que el juego siempre empiece con la pantalla abierta (-1)
        if (materialVignette != null)
        {
            materialVignette.SetFloat(nombrePropiedad, -1f);
        }
    }

    // Esta función inicia la transición para cerrar la pantalla a negro (0)
    public void IniciarCierre(float duracion)
    {
        StartCoroutine(TransicionVignette(-1f, 1f, duracion));
    }

    private IEnumerator TransicionVignette(float inicio, float fin, float tiempo)
    {
        float transcurrido = 0f;
        while (transcurrido < tiempo)
        {
            transcurrido += Time.deltaTime;
            float progreso = transcurrido / tiempo;

            // Calculamos el valor intermedio entre 1 y 0
            float valorActual = Mathf.Lerp(inicio, fin, progreso);

            // Modificamos el shader en tiempo real
            materialVignette.SetFloat(nombrePropiedad, valorActual);
            yield return null;
        }
        materialVignette.SetFloat(nombrePropiedad, fin);
    }
}