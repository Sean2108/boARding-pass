using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class GetWeather : MonoBehaviour {

	[System.Serializable]
	public class Weather
	{
		public Condition[] weather;
		public WeatherMain main;
	}

	[System.Serializable]
	public class Condition
	{
		public int id;
		public string main;
		public string description;
		public string icon;
	}

	[System.Serializable]
	public class WeatherMain
	{
		public int temp;
		public int pressure;
		public int humidity;
		public int temp_min;
		public int temp_max;
	}

	private TextMesh text;
	void Start()
	{
		text = GetComponent<TextMesh>();
		text.text = "";
		StartCoroutine(GetWeatherApi());
		StartCoroutine(GetCurrencyApi());
	}

	IEnumerator GetWeatherApi() {
		string city = "Singapore";
		using (UnityWebRequest www = UnityWebRequest.Get(string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&APPID=27ce1401a006f7e0a8c4fc37fd78c13e", city)))
		{
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				text.text = www.error.ToString();
			}
			else
			{
				Weather w = JsonUtility.FromJson<Weather>(www.downloadHandler.text.ToString());
				text.text += string.Format("Temperature: {0}\nWeather: {1}\n", (w.main.temp - 273).ToString(), w.weather[0].description);
			}
		}
	}

	IEnumerator GetCurrencyApi()
	{
		string from = "SGD";
		string to = "USD";
		using (UnityWebRequest www = UnityWebRequest.Get(string.Format("https://free.currencyconverterapi.com/api/v6/convert?q={0}_{1}&compact=ultra", from, to)))
		{
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				text.text = www.error.ToString();
			}
			else
			{
				text.text += string.Format("1 {0} is {1} {2}\n", from, www.downloadHandler.text.ToString().Split(':')[1].Trim('}'), to);//c.GetType().GetProperty(query).GetValue(c, null).ToString()
			}
		}
	}
}