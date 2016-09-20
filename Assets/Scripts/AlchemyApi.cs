
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class AlchemyApi : MonoBehaviour {

	private static string urlEndPoint = "https://gateway-a.watsonplatform.net/calls/text/TextGetEmotion?";
	private static string apiKey = "e020a384f0a0b95df20ffbd8060dd9ac7b7cbc96";

	void Start() { // Run automatically
		StartCoroutine(AnalyzeEmotion("test"));
	}

	public IEnumerator AnalyzeEmotion(string text) {
		UnityWebRequest req = UnityWebRequest.Post (urlEndPoint, "test"); // We just append the form data later.. filling it here for avoid build errors
		req.url = req.url + "&apikey=" + apiKey;
		req.url = req.url + "&outputMode=json";
		req.url = req.url + "&text=I am really, really mad";


		Debug.Log (req.url);

		yield return req.Send ();

		if (req.isError) {
			Debug.Log (req.error);
		}
		else {
			Debug.Log(req.downloadHandler.text);
		}

	}

}

