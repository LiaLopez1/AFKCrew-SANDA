using UnityEngine;

public class FragmentPickUp : MonoBehaviour, Interactable
{

    [SerializeField] private string fragmentId;
    [SerializeField] private SoundData pickUpSound;
    [SerializeField] private float timeBonus = 60f;

    [Header("Interacción")]
    [Tooltip("GameObject hijo (ej. sprite/canvas con el ícono 'E') que se muestra cuando el jugador está en rango.")]
    [SerializeField] private GameObject interactionIcon;

    private void Awake()
    {
        if (interactionIcon != null)
            interactionIcon.SetActive(false);
    }

    public void Interaction()
    {
        if (MemoryFragmentManager.Instance == null)
        {
            Debug.LogError("No se encontró MemoryFragmentManager en la escena.");
            return;
        }

        MemoryFragmentManager.Instance.CollectFragment(fragmentId);

        if (CountDown.Instance != null)
        {
            CountDown.Instance.AddTimeSmooth(timeBonus);
        }

        pickUpSound?.Play(); // usa el SoundData.Play() que ya armamos
        HidePrompt();
        gameObject.SetActive(false);
    }

    public void ShowPrompt()
    {
        if (interactionIcon != null)
            interactionIcon.SetActive(true);
    }

    public void HidePrompt()
    {
        if (interactionIcon != null)
            interactionIcon.SetActive(false);
    }
}