using UnityEngine;

// Este script va SOLO en el GameObject raíz "Managers" del prefab.
// Su única responsabilidad es asegurar que todo el prefab sobreviva entre escenas.
public class PersistentManagers : MonoBehaviour
{
    private static PersistentManagers instance;

    private void Awake()
    {
        if (instance != null)
        {
            // Ya existe un set de Managers persistente → este duplicado sobra
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Este SÍ es el objeto raíz, así que funciona correctamente
    }
}