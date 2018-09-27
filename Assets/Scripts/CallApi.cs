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

	[System.Serializable]
	public class Pnr
	{
		public ResponseBody responseBody;
	}

	[System.Serializable]
	public class ResponseBody
	{
		public Flight[] flights;
	}

	[System.Serializable]
	public class Flight
	{
		public FlightSegment[] flightSegment;
	}

	[System.Serializable]
	public class FlightSegment
	{
		public string estimatedDepartureTime;
	}

	public Done_GameController gameController;

	// private TextMesh text;
	void Start()
	{
		// text = GetComponent<TextMesh>();
		// text.text = "";
		// StartCoroutine(GetWeatherApi("SIN"));
		// StartCoroutine(GetCurrencyApi("SGD", "USD"));
		// StartCoroutine(GetWaitTimeApi("SIN"));
		StartCoroutine(GetPnr("Greig", "JWRKQC"));
	}

	IEnumerator GetPnr(string name, string reference)
	{
		System.DateTime departure = new System.DateTime(2050, 9, 25, 17, 55, 0);
        WWWForm form = new WWWForm();
		Debug.Log("dep " + departure.ToString());
        form.AddField("bookingReference", reference);
        form.AddField("bookingLastName", name);
		using (UnityWebRequest www = UnityWebRequest.Post("https://apigw.singaporeair.com/appchallenge/api/pax/pnr", form))
		{
			www.SetRequestHeader("apikey", "aghk73f4x5haxeby7z24d2rc");
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				// text.text = www.error.ToString();
			}
			else
			{
				Pnr pnr = JsonUtility.FromJson<Pnr>(www.downloadHandler.text.ToString());
				departure = Convert.ToDateTime(pnr.responseBody.flights[0].flightSegment[0].estimatedDepartureTime).Add(new System.TimeSpan(0,-30,0));
				System.DateTime now = new System.DateTime(2018, 9, 25, 17, 59, 45);
				while (System.DateTime.Compare(now, departure) < 0) {
					now = now.AddSeconds(1);
					yield return new WaitForSeconds(1);
				}
				gameController.ShowWarning();
			}
		}
	}

	// IEnumerator GetWeatherApi(string city) {
	// 	using (UnityWebRequest www = UnityWebRequest.Get(string.Format("https://weather.api.aero/weather/v1/forecast/{0}?duration=5&temperatureScale=C", city)))
	// 	{
	// 		www.SetRequestHeader("X-apiKey", "89e15931434731aefdaa04920ec60e44");
	// 		yield return www.SendWebRequest();

	// 		if (www.isNetworkError || www.isHttpError)
	// 		{
	// 			text.text = www.error.ToString();
	// 		}
	// 		else
	// 		{
	// 			Weather w = JsonUtility.FromJson<Weather>(www.downloadHandler.text.ToString());
	// 			text.text += string.Format("Temperature: {0}-{1}°C\nWeather: {2}\n", w.weatherForecast[0].lowTemperatureValue, w.weatherForecast[0].highTemperatureValue, w.weatherForecast[0].phrase);
	// 		}
	// 	}
	// }

	// IEnumerator GetWaitTimeApi(string city) {
	// 	using (UnityWebRequest www = UnityWebRequest.Get(string.Format("https://waittime-qa.api.aero/waittime/v1/current/{0}", city)))
	// 	{
	// 		www.SetRequestHeader("X-apiKey", "8e2cff00ff9c6b3f448294736de5908a");
	// 		yield return www.SendWebRequest();

	// 		if (www.isNetworkError || www.isHttpError)
	// 		{
	// 			text.text = www.error.ToString();
	// 		}
	// 		else
	// 		{
	// 			WaitTime w = JsonUtility.FromJson<WaitTime>(www.downloadHandler.text.ToString());
	// 			foreach (WaitTimeQueue queue in w.current) 
	// 			{
	// 				text.text += string.Format("Queue: {0} -  {1} minutes\n", queue.queueName, Int32.Parse(queue.projectedWaitTime) / 60);
	// 			}
	// 		}
	// 	}
	// }

	// IEnumerator GetCurrencyApi(string from, string to)
	// {
	// 	using (UnityWebRequest www = UnityWebRequest.Get(string.Format("https://free.currencyconverterapi.com/api/v6/convert?q={0}_{1}&compact=ultra", from, to)))
	// 	{
	// 		yield return www.SendWebRequest();

	// 		if (www.isNetworkError || www.isHttpError)
	// 		{
	// 			text.text = www.error.ToString();
	// 		}
	// 		else
	// 		{
	// 			text.text += string.Format("1 {0} - {1} {2}\n", from, float.Parse(www.downloadHandler.text.ToString().Split(':')[1].Trim('}')).ToString("n2"), to);
	// 		}
	// 	}
	// }
}