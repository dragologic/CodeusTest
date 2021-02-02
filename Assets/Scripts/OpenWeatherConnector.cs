using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class OpenWeatherConnector : MonoBehaviour
{
    string AppId = "2593e913cfe95f2067bde60897e16dca";
    string latitude = "43.321126219625036";
    string longitude = "21.89583076867475";
    public InputField city;
    public InputField weatherDescription;
    public InputField temp;
    public float temp_min;
    public float temp_max;
    public float rain;
    public int clouds;
    public float wind;

    public GameObject popupScreen;
    // Start is called before the first frame update
    private void Start()
    {
        popupScreen.SetActive(false);
    }
    public void getWeatherData()
    {
        StartCoroutine(getData());
    }
    public IEnumerator getData()
    {
        UnityWebRequest request = UnityWebRequest.Get(string.Format("api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=metric&appid={2}", latitude,longitude,AppId));
        yield return request.SendWebRequest();
        if (request.error == null || request.error == "")
        {
            setWeatherAttributes(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setWeatherAttributes(string jsonString)
    {
        var weatherJson = JSON.Parse(jsonString);
        city.text = weatherJson["name"].Value;
        weatherDescription.text = weatherJson["weather"][0]["description"].Value;
        temp.text = weatherJson["main"]["temp"].AsFloat.ToString();

        temp_min = weatherJson["main"]["temp_min"].AsFloat;
        temp_max = weatherJson["main"]["temp_max"].AsFloat;
        rain = weatherJson["rain"]["3h"].AsFloat;
        clouds = weatherJson["clouds"]["all"].AsInt;
        wind = weatherJson["wind"]["speed"].AsFloat;

        popupScreen.SetActive(true);
    }
}
