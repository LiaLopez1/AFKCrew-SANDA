using UnityEngine;

public class Fragment : MonoBehaviour
{
   [SerializeField] private string fragmentName;

    public void CollectFragment()
    {
        Debug.Log($"Collected fragment: {fragmentName}");
        CountDown countDown =Object.FindAnyObjectByType<CountDown>();
        countDown.remainingTime += 60f;
        Destroy(gameObject);
    }
}
