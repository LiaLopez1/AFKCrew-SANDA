using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "Music/Sound Data")]
public class SoundData : ScriptableObject
{
    [Header("Clips de audio (elige uno al azar si hay varios)")]
    public AudioClip[] clips;

    [Header("Configuración")]
    [Range(0f, 1f)] public float volume = 1f;
    [Range(-0.3f, 0.3f)] public float pitchVariation = 0.05f;

    //Elige un clip al azar del array. Úsalo tanto para música como para SFX.
  
    public AudioClip GetClip()
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning($"SoundData '{name}' no tiene clips asignados.");
            return null;
        }
        return clips[Random.Range(0, clips.Length)];
    }

    public float GetPitch() => 1f + Random.Range(-pitchVariation, pitchVariation);

    //Para SFX puntuales (caminar, recolectar fragmentos...)
    // La música se maneja con AudioManager.PlayMusic().
    public void Play()
    {
        var clip = GetClip();
        if (clip == null) return;
        AudioManager.Instance.PlaySFX(clip, volume, GetPitch());
    }
}