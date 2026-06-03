using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [Header("Rain Probabilities")]
    [Range(0f, 1f)] public float chanceToRainSpring = 0.3f; // 30%
    [Range(0f, 1f)] public float chanceToRainSummer = 0.00f;
    [Range(0f, 1f)] public float chanceToRainFall = 0.4f;
    [Range(0f, 1f)] public float chanceToRainWinter = 0.7f; // 70%

    [Header("References")]
    public GameObject rainEffect;
    public Material rainSkyBox;

    public bool isSpecialWeather;

    public AudioSource rainChannel;
    public AudioClip rainSound;

    public enum WeatherCondition
    {
        Sunny,
        Rainy
    }

    private WeatherCondition currentWeather = WeatherCondition.Sunny;

    private void Start()
    {
        // TimeManager jab bhi din badle ga, ye function khud chal jaye ga
        TimeManager.Instance.OnDayPass.AddListener(GenerateRandomWeather);
    }

    private void GenerateRandomWeather()
    {
        TimeManager.Season currentSeason = TimeManager.Instance.currentSeason;
        float chanceToRain = 0f;

        // Season k mutabiq baarish ka chance set ho raha hai
        switch (currentSeason)
        {
            case TimeManager.Season.Spring:
                chanceToRain = chanceToRainSpring;
                break;
            case TimeManager.Season.Summer:
                chanceToRain = chanceToRainSummer;
                break;
            case TimeManager.Season.Fall:
                chanceToRain = chanceToRainFall;
                break;
            case TimeManager.Season.Winter:
                chanceToRain = chanceToRainWinter;
                break;
        }

        // Generate random number for the chance of rain
        if (Random.value <= chanceToRain)
        {
            currentWeather = WeatherCondition.Rainy;
            isSpecialWeather = true;

            Invoke("StartRain", 1f);
        }
        else
        {
            currentWeather = WeatherCondition.Sunny;
            isSpecialWeather = false;
            StopRain();
        }
    }

    private void StartRain()
    {
        if (rainChannel.isPlaying == false)
        {
            rainChannel.clip = rainSound;
            rainChannel.loop = true;
            rainChannel.Play();
        }
        RenderSettings.skybox = rainSkyBox;
        rainEffect.SetActive(true);
    }

    private void StopRain()
    {
        if (rainChannel.isPlaying)
        {
           rainChannel.Stop();
        }
        rainEffect.SetActive(false);
    }
}
