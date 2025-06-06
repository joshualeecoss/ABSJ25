using UnityEngine;
using UnityEngine.UI;

public class Inspiration : MonoBehaviour
{
    public static Inspiration Instance { get; private set; }

    [Header("Inspiration Settings")]
    [SerializeField]
    private int maxInspiration = 100;
    [SerializeField]
    private int currentInspiration = 0;

    [Header("UI")]
    // Assign your UI slider here
    [SerializeField]
    private Slider inspirationSlider; 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;

        }
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddInspiration(int amount)
    {
        currentInspiration = Mathf.Clamp(currentInspiration + amount, 0, maxInspiration);
        UpdateUI();
        Debug.Log($"Inspiration increased to {currentInspiration}");
    }

    private void UpdateUI()
    {
        if (inspirationSlider != null)
        {
            inspirationSlider.maxValue = maxInspiration;
            inspirationSlider.value = currentInspiration;
        }
    }

    // Optional: Get current amount
    public int GetCurrentInspiration() => currentInspiration;
}