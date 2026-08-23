using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    public static LevelConfig Instance { get; private set; }

    [Tooltip("Nombre exacto de la escena a cargar al completar el recuerdo")]
    public string nextSceneName;

    [Header("Audio")]
    [Tooltip("Música ambiente de esta escena")]
    public SoundData ambientMusic;

    private void Awake()
    {
        Instance = this;
    }
}