
using UnityEngine;

public class FragmentPickUp : MonoBehaviour, Interactable
{

    [SerializeField] private string fragmentId;
    [SerializeField] private SoundData pickUpSound;
    [SerializeField] private GameObject interactionIcon;
 

    public void Interaction()
    {
        Debug.Log("FragmentPickUp.Interaction() llamado");
        
        if (MemoryFragmentManager.Instance == null)
        {
            Debug.LogError("No se encontró MemoryFragmentManager en la escena.");
            return;
        }

        MemoryFragmentManager.Instance.CollectFragment(fragmentId);
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
