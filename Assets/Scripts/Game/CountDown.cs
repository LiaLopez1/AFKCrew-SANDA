using UnityEngine;
using UnityEngine.SceneManagement; // NUEVO: Necesario para detectar cambios de escena

public class CountDown : MonoBehaviour
{
    public static CountDown Instance {get; private set;}
    [SerializeField] private float totalTime = 60f;
    [SerializeField] public float remainingTime;

    [SerializeField] private float speedSmooth = 5f;
    private float targetRemainingTime;

    private ControlVignette vignette;

    private bool isTimerRunning = true;

        private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        vignette = Object.FindFirstObjectByType<ControlVignette>();
        ResetTimer();
    }

    private void Update()
    {
        if (!isTimerRunning) return;

        if (targetRemainingTime > 0)
        {
            targetRemainingTime -= Time.deltaTime;
        }

        remainingTime = Mathf.MoveTowards(remainingTime, targetRemainingTime, speedSmooth * Time.deltaTime);

        if (remainingTime > 0)
        {
            float progress = 1 - (remainingTime / totalTime);
            float vignetteIntensity = Mathf.Lerp(-1f, 1f, progress);
            if (vignette != null)
            {
                vignette.UpdateVignette(vignetteIntensity);
            }
        }
        else
        {
            ExecuteGameOver();
        }
    }

    public void ResetTimer()
    {
        remainingTime = totalTime;
        targetRemainingTime = totalTime;
        isTimerRunning = true;

        if (vignette != null)
        {
            vignette.UpdateVignette(-1f);
        }
    }

    public void PauseTimer(bool pause)
    {
        isTimerRunning = !pause;
    }

    private void ExecuteGameOver()
    {
        remainingTime = 0;
        targetRemainingTime = 0;
        isTimerRunning = false;

        if (vignette != null)
        {
            vignette.UpdateVignette(1f);
        }
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddTimeSmooth(float amount)
    {
        targetRemainingTime += amount;
        if (targetRemainingTime > totalTime)
        {
            targetRemainingTime = totalTime;
        }
    }
}
