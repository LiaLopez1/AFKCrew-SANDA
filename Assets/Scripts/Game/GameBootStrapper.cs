using UnityEngine;

public class GameBootStrapper : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    private static void Initialize()
    {
        if(GameManager.Instance!= null) return;

        GameObject managersPrefab = Resources.Load<GameObject>("Managers"); 

        if(managersPrefab != null)
        {
            GameObject.Instantiate(managersPrefab);
        }
        else
        {
            Debug.LogError("No se encontro el prefab Manager en la carpeta ");
            
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
