using UnityEngine;

public class FragmentPickUp : MonoBehaviour, Interactable
{

    [SerializeField] private string fragmentId;
    [SerializeField] private SoundData pickUpSound;
    [SerializeField] private float timeBonus = 6f;

    [Header("Interacción")]

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
            CountDown.Instance.remainingTime += timeBonus;
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