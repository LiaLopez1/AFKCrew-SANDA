using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR

public class Reset : MonoBehaviour
{
    Controls Controls;
      private void Awake()
    {
        Controls = new();
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }

    void Update()
    {
        if (Controls.UI.Reset.WasPressedThisFrame())
        {
            ResetLevel();
        }
    }
    private void ResetLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    }
# endif