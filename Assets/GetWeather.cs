using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

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
		text.text = "Getting weather...";
		StartCoroutine(GetText());
	}

	IEnumerator GetText() {
		string city = "Singapore";
		using (UnityWebRequest www = UnityWebRequest.Get(string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&APPID=27ce1401a006f7e0a8c4fc37fd78c13e", city)))
		{
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				text.text = www.error.ToString();
				Debug.Log("error: " + www.error);
			}
			else
			{
				// Show results as text
				Debug.Log("success: " + www.downloadHandler.text);
				Weather w = JsonUtility.FromJson<Weather>(www.downloadHandler.text.ToString());
				text.text = "Temperature: " + (w.main.temp - 273).ToString() + "\nWeather: " + w.weather[0].description;
			}
		}
	}
}