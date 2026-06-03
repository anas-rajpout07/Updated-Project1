using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; set; }

    public UnityEvent OnDayPass = new UnityEvent();

    // --- Seasons System ---
    public enum Season { Spring, Summer, Fall, Winter }
    public Season currentSeason = Season.Spring;
    private int daysPerSeason = 2;
    private int daysInCurrentSeason = 1;

    // --- Months System (Naya Add Kia Hai) ---
    public enum Month
    {
        January, February, March, April, May, June,
        July, August, September, October, November, December
    }
    public Month currentMonth = Month.January;
    private int daysPerMonth = 3;
    private int daysInCurrentMonth = 1;

    public int dayInGame = 1;
    public int yearInGame = 2050;
    public TextMeshProUGUI dayUI;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void TriggerNextDay()
    {
        dayInGame += 1;
        daysInCurrentSeason += 1;
        daysInCurrentMonth += 1; // Month k din barhao

        // Season Change Logic
        if (daysInCurrentSeason > daysPerSeason)
        {
            daysInCurrentSeason = 1;
            currentSeason = GetNextSeason();
        }

        // Month Change Logic (Naya)
        if (daysInCurrentMonth > daysPerMonth)
        {
            daysInCurrentMonth = 1;
            currentMonth = GetNextMonth();
        }

        UpdateUI();
        OnDayPass.Invoke();
    }

    private Season GetNextSeason()
    {
        int currentSeasonIndex = (int)currentSeason;
        int nextSeasonIndex = (currentSeasonIndex + 1) % 4;
        return (Season)nextSeasonIndex;
    }

    // Month rotate karne ka method (Naya)
    private Month GetNextMonth()
    {
        int currentMonthIndex = (int)currentMonth;
        int nextMonthIndex = (currentMonthIndex + 1) % 12; // 12 mahine hote hain isliye % 12
        //saal brao 1
        if (nextMonthIndex == 0)
        {
            yearInGame += 1;
        }
        return (Month)nextMonthIndex;
    }

    private void UpdateUI()
    {
        // UI mein Day aur Month dono show honge
        dayUI.text = $"Day: {daysInCurrentMonth}-{currentMonth}-{yearInGame}/{currentSeason}";

    }
}











