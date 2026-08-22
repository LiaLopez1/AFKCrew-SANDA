using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MemoryFragmentManager : MonoBehaviour
{
    public static MemoryFragmentManager Instance {get; private set;}

    [SerializeField] private MemoryData currentMemory;
    [SerializeField] private UnityEvent<int, int> onFragmentCollected; // actual / total
    [SerializeField] private UnityEvent onAllFragmentsCollected;

    private readonly HashSet<string> collectedFragments = new();

    void Awake()
    {
        Instance = this;

        if (currentMemory == null)
        {
            Debug.LogError("MemoryFragmentManager no tiene un MemoryData asignado.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start()
    {
        if (currentMemory != null)
        {
            onFragmentCollected?.Invoke(0, currentMemory.FragmentIds.Count);
        }
    }

    public void CollectFragment(string id)
    {
        if (!currentMemory.FragmentIds.Contains(id))
        {
            Debug.LogWarning($"Fragmento '{id}' no está en currentMemory.FragmentIds.");
            return;
        }

        if (collectedFragments.Contains(id)) return;

        collectedFragments.Add(id);
        onFragmentCollected?.Invoke(collectedFragments.Count, currentMemory.FragmentIds.Count);

        if (collectedFragments.Count >= currentMemory.FragmentIds.Count)
        {
            Debug.Log($"Recuerdo '{currentMemory.Id}' completado — todos los fragmentos recolectados.");
            onAllFragmentsCollected?.Invoke();
        }
    }
}