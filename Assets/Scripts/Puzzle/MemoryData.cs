using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PuzzlePieceData
{
    public Sprite sprite;
    public Vector2 targetPosition;
    public float targetRotation;
}

[CreateAssetMenu(fileName = "MemoryData", menuName = "Recuerdos/MemoryData")]
public class MemoryData : ScriptableObject
{
    [SerializeField] private string id;
    public string Id => id;

    [SerializeField] private List<string> fragmentIds;
    public IReadOnlyList<string> FragmentIds => fragmentIds;

    [SerializeField] private List<PuzzlePieceData> puzzlePieces;
    public IReadOnlyList<PuzzlePieceData> PuzzlePieces => puzzlePieces;

    [SerializeField] private Sprite finalImageSprite;
    public Sprite FinalImageSprite => finalImageSprite;

    [SerializeField] private Sprite[] memorySequenceSprites;
    public IReadOnlyList<Sprite> MemorySequenceSprites => memorySequenceSprites;
}