using UnityEngine;

public class VignetteFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTransform; 
    [SerializeField] Material vignetteMaterial;
    [SerializeField] Camera mainCamera;

    void Update()
    {
        if (playerTransform != null && vignetteMaterial != null)
        {
            Vector3 screenPixelPos = mainCamera.WorldToScreenPoint(playerTransform.transform.position);
            Vector2 uvPos = new Vector2(screenPixelPos.x / Screen.width, screenPixelPos.y / Screen.height);
            vignetteMaterial.SetVector("_PlayerScreenPos", uvPos);
        }
    }
}
