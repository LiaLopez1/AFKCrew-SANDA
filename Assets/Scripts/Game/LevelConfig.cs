using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    [Tooltip("Nombre exacto de la escena a cargar al completar el recuerdo")]
    public string nextSceneName;

    [Header("Audio")]
    [Tooltip("Música ambiente de esta escena")]
    public SoundData ambientMusic;

}
