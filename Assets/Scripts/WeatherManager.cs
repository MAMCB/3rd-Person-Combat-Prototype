using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] GameObject[] weatherTypes;
    // Start is called before the first frame update
    void Start()
    {
        GameObject currentWeather;
        currentWeather = weatherTypes[Random.Range(0, weatherTypes.Length)];
        currentWeather.SetActive(true);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
