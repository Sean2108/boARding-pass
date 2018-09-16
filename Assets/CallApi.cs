using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class CallApi : MonoBehaviour {

	[System.Serializable]
	public class Weather
	{
		public WeatherInfo[] weatherForecast;
	}

	[System.Serializable]
	public class WeatherInfo
	{
		public string phrase;
		public string lowTemperatureValue;
		public string highTemperatureValue;
		public string probabilityOfPrecip;
	}

	[System.Serializable]
	public class WaitTime
	{
		public WaitTimeQueue[] current;
	}

	[System.Serializable]
	public class WaitTimeQueue
	{
		public string queueName;
		public string projectedWaitTime;
	}

	private TextMesh text;
	void Start()
	{
		text = GetComponent<TextMesh>();
		text.text = "";
		StartCoroutine(GetWeatherApi("SIN"));
		StartCoroutine(GetCurrencyApi("SGD", "USD"));
		StartCoroutine(GetWaitTimeApi("SIN"));
	}

	IEnumerator GetWeatherApi(string city) {
		using (UnityWebRequest www = UnityWebRequest.Get(string.Format("https://weather.api.aero/weather/v1/forecast/{0}?duration=5&temperatureScale=C", city)))
		{
			www.SetRequestHeader("X-apiKey", "89e15931434731aefdaa04920ec60e44");
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				text.text = www.error.ToString();
			}
			else
			{
				Weather w = JsonUtility.FromJson<Weather>(www.downloadHandler.text.ToString());
				text.text += string.Format("Temperature: {0}-{1}°C\nWeather: {2}\n", w.weatherForecast[0].lowTemperatureValue, w.weatherForecast[0].highTemperatureValue, w.weatherForecast[0].phrase);
			}
		}
	}

	IEnumerator GetWaitTimeApi(string city) {
		using (UnityWebRequest www = UnityWebRequest.Get(string.Format("https://waittime-qa.api.aero/waittime/v1/current/{0}", city)))
		{
			www.SetRequestHeader("X-apiKey", "8e2cff00ff9c6b3f448294736de5908a");
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				text.text = www.error.ToString();
			}
			else
			{
				WaitTime w = JsonUtility.FromJson<WaitTime>(www.downloadHandler.text.ToString());
				foreach (WaitTimeQueue queue in w.current) 
				{
					text.text += string.Format("Queue: {0} -  {1} minutes\n", queue.queueName, Int32.Parse(queue.projectedWaitTime) / 60);
				}
			}
		}
	}

	IEnumerator GetCurrencyApi(string from, string to)
	{
		using (UnityWebRequest www = UnityWebRequest.Get(string.Format("https://free.currencyconverterapi.com/api/v6/convert?q={0}_{1}&compact=ultra", from, to)))
		{
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				text.text = www.error.ToString();
			}
			else
			{
				text.text += string.Format("1 {0} - {1} {2}\n", from, float.Parse(www.downloadHandler.text.ToString().Split(':')[1].Trim('}')).ToString("n2"), to);
			}
		}
	}
}