using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    [SerializeField] private GameObject puzzleRoot;
    [SerializeField] private RectTransform piecesContainer;
    [SerializeField] private PuzzlePiece piecePrefab;
    [SerializeField] private UnityEngine.UI.Image finalImageDisplay;

    [SerializeField] private float positionTolerance = 30f;
    [SerializeField] private float rotationTolerance = 15f;
    public float PositionTolerance => positionTolerance;
    public float RotationTolerance => rotationTolerance;

    [SerializeField] private float puzzleTimeScale = 0f;
    [SerializeField] private UnityEvent onPuzzleCompleted;

    [Header("Secuencia del recuerdo")]
    [SerializeField] private float sequenceInterval = 2f;
    [SerializeField] private float sequenceFadeDuration = 1f;
    [SerializeField] private GameObject continueButton;

    private int totalPieces;
    private int placedPieces;

    private void Awake()
    {
        Instance = this;

        if (puzzleRoot != null)
            puzzleRoot.SetActive(false);

        if (continueButton != null)
            continueButton.SetActive(false);
    }

    public void StartPuzzle(MemoryData memory)
    {
        if (memory == null)
        {
            Debug.LogError("PuzzleManager.StartPuzzle() recibió un MemoryData nulo.");
            return;
        }

        placedPieces = 0;
        totalPieces = memory.PuzzlePieces.Count;

        puzzleRoot.SetActive(true);
        Time.timeScale = puzzleTimeScale;
        CountDown  countDown = FindAnyObjectByType<CountDown>();
        countDown.PauseTimer(true);
        

        foreach (var pieceData in memory.PuzzlePieces)
        {
            PuzzlePiece piece = Instantiate(piecePrefab, piecesContainer);
            RectTransform pieceRect = piece.GetComponent<RectTransform>();

            pieceRect.anchoredPosition = GetRandomStartPosition();
            pieceRect.eulerAngles = new Vector3(0, 0, pieceData.targetRotation);
            pieceRect.localPosition = new Vector3(pieceRect.localPosition.x, pieceRect.localPosition.y, 0f);

            piece.Initialize(pieceData.sprite, pieceData.targetPosition, pieceData.targetRotation);
        }
    }

    private Vector2 GetRandomStartPosition()
    {
        float halfWidth = piecesContainer.rect.width / 2f;
        float halfHeight = piecesContainer.rect.height / 2f;

        return new Vector2(
            Random.Range(-halfWidth, halfWidth),
            Random.Range(-halfHeight, halfHeight)
        );
    }

    public void NotifyPiecePlaced()
    {
        placedPieces++;

        if (placedPieces >= totalPieces)
        {
            ShowFinalImage();
            onPuzzleCompleted?.Invoke();
        }
    }

    private void ShowFinalImage()
    {
        if (MemoryFragmentManager.Instance == null || MemoryFragmentManager.Instance.CurrentMemory == null)
        {
            Debug.LogError("No se pudo mostrar la imagen final: falta el MemoryData activo.");
            return;
        }

        if (piecesContainer != null)
            piecesContainer.gameObject.SetActive(false);

        finalImageDisplay.sprite = MemoryFragmentManager.Instance.CurrentMemory.FinalImageSprite;

        Color color = finalImageDisplay.color;
        color.a = 1f;
        finalImageDisplay.color = color;

        StartCoroutine(PlayMemorySequence());
    }

    private IEnumerator PlayMemorySequence()
    {
        var sequence = MemoryFragmentManager.Instance.CurrentMemory.MemorySequenceSprites;

        foreach (var sprite in sequence)
        {
            yield return new WaitForSecondsRealtime(sequenceInterval);
            yield return StartCoroutine(FadeToSprite(sprite));
        }

        if (continueButton != null)
            continueButton.SetActive(true);
    }

    private IEnumerator FadeToSprite(Sprite nextSprite)
    {
        yield return StartCoroutine(FadeImageAlpha(1f, 0f));
        finalImageDisplay.sprite = nextSprite;
        yield return StartCoroutine(FadeImageAlpha(0f, 1f));
    }

    private IEnumerator FadeImageAlpha(float from, float to)
    {
        float elapsed = 0f;
        Color color = finalImageDisplay.color;

        while (elapsed < sequenceFadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(from, to, elapsed / sequenceFadeDuration);
            finalImageDisplay.color = color;
            yield return null;
        }

        color.a = to;
        finalImageDisplay.color = color;
    }

    public void OnContinueClicked()
    {
        if (SceneTransitionManager.Instance == null || LevelConfig.Instance == null ||
            string.IsNullOrEmpty(LevelConfig.Instance.nextSceneName))
        {
            Debug.LogError("Falta SceneTransitionManager, LevelConfig, o nextSceneName sin asignar.");
            return;
        }

        SceneTransitionManager.Instance.TransitionToScene(LevelConfig.Instance.nextSceneName);
    }
}