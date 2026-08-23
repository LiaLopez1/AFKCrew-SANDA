using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Image image;
    private CanvasGroup canvasGroup;
    private Canvas parentCanvas;

    private Vector2 targetPosition;
    private float targetRotation;
    private bool isPlaced;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public void Initialize(Sprite sprite, Vector2 targetPos, float targetRot)
    {
        image.sprite = sprite;
        targetPosition = targetPos;
        targetRotation = targetRot;
        isPlaced = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        rectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        canvasGroup.blocksRaycasts = true;

        float distance = Vector2.Distance(rectTransform.anchoredPosition, targetPosition);
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(rectTransform.eulerAngles.z, targetRotation));

        if (distance <= PuzzleManager.Instance.PositionTolerance &&
            angleDifference <= PuzzleManager.Instance.RotationTolerance)
        {
            SnapToTarget();
        }
    }

    private void SnapToTarget()
    {
        rectTransform.anchoredPosition = targetPosition;
        rectTransform.eulerAngles = new Vector3(0, 0, targetRotation);
        isPlaced = true;

        PuzzleManager.Instance.NotifyPiecePlaced();
    }
}