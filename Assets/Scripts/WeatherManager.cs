using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] GameObject[] weatherTypes;
    [SerializeField] int weatherIndex;
    GameObject currentWeather;
    PlayerStateMachine playerStateMachine;

    private void Awake()
    {
        playerStateMachine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        currentWeather = weatherTypes[SetWeather()];
        currentWeather.SetActive(true);
        playerStateMachine.InputReader.DodgeEvent += OnDodge;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int SetWeather()
    {
        
        weatherIndex = Random.Range(0, weatherTypes.Length);
        return weatherIndex;
    }

    public void ChangeWeather()
    {
        currentWeather.SetActive(false);
        if(weatherIndex==weatherTypes.Length-1)
        {
            weatherIndex = 0;
        }
        else
        {
            weatherIndex++;
        }
        currentWeather = weatherTypes[weatherIndex];
        currentWeather.SetActive(true);
    }

    private void OnDodge()
    {
        ChangeWeather();
    }
}
