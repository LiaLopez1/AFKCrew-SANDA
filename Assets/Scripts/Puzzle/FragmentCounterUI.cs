using UnityEngine;
using TMPro;

public class FragmentCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;

    public void UpdateCounter(int actual, int total)
    {
        if (counterText == null)
        {
            Debug.LogWarning("FragmentCounterUI no tiene un TMP_Text asignado.");
            return;
        }

        counterText.text = $"{actual} / {total}";
    }
}