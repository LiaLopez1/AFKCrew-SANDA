using UnityEngine;

// Este método se ejecuta automáticamente ANTES de que cargue cualquier escena,
// sin importar si es MainMenu, Escenario_01, Escenario_03, la que sea. nos ayuda 
// a probar las escenas sin necesidad de pasar por el MainManeu

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{ get; private set;}

    public int currentMemoryIndex =0;


    private void Awake()
    {
        /*if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }*/

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
