using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float yOffset = 2f;
    [SerializeField] float zOffset = -5f;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = new Vector3(target.position.x, yOffset, zOffset);
            transform.position = newPosition;
        }
    }
}
