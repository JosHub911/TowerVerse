using TMPro;
using UnityEngine;

public class CashSystem : MonoBehaviour
{
    public static CashSystem Instance { get; private set; }

    [SerializeField] private int cashAmount = 0;
    [SerializeField] private TextMeshProUGUI cashText;

    private void Awake()
    {
        // When the game starts, store this object as the single "instance"
        Instance = this;
        UpdateCashText();
        DontDestroyOnLoad(gameObject);
    }


    public void AddCash(int amount)
    {
        cashAmount += amount;
        UpdateCashText();
    }

    public bool SpendCash(int amount)
    {
        if (cashAmount >= amount)
        {
            cashAmount -= amount;
            UpdateCashText();
            return true;
        }
        return false;
    }

    private void UpdateCashText()
    {
        cashText.text = "Cash: $" + cashAmount.ToString();
    }
}
