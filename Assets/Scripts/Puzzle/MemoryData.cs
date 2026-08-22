using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MemoryData", menuName = "Scriptable Objects/MemoryData")]
public class MemoryData : ScriptableObject
{
    [SerializeField] private string id;
    public string Id => id;

    [SerializeField] private List<string> fragmentIds;
    public IReadOnlyList<string> FragmentIds => fragmentIds;

    [SerializeField] private Sprite[] puzzlePieceSprites;
    public IReadOnlyList<Sprite> PuzzlePieceSprites => puzzlePieceSprites;

    [SerializeField] private Sprite finalImageSprite;
    public Sprite FinalImageSprite => finalImageSprite;
    
    [SerializeField] private Sprite[] memorySequenceSprites;
    public IReadOnlyList<Sprite> MemorySequenceSprites => memorySequenceSprites;
}
