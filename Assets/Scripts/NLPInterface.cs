using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Services.AlchemyAPI.v1;
using IBM.Watson.DeveloperCloud.Logging;

public class NLPInterface : MonoBehaviour {
	static AlchemyAPI m_AlchemyLanguage = new AlchemyAPI();
	public bool isInterpreting {get; private set;}
	public string curSentiment {get; private set;}

	public void GetTextSentiment(string input)
	{
		isInterpreting = true;
		if(!m_AlchemyLanguage.GetTextSentiment(OnGetTextSentiment, input))
			print("ExampleAlchemyLanguage "+"Failed to get sentiment by text POST");
	}

	private void OnGetTextSentiment(SentimentData sentimentData, string data)
	{
		if(sentimentData != null)
		{
			/*
			print("ExampleAlchemyLanguage "+"status: {0} "+sentimentData.status);
			print("ExampleAlchemyLanguage "+"url: {0} "+sentimentData.url);
			print("ExampleAlchemyLanguage "+"language: {0} "+sentimentData.language);
			print("ExampleAlchemyLanguage "+"text: {0} "+sentimentData.text);*/
			if(sentimentData.docSentiment == null)
				print("ExampleAlchemyLanguage "+"No sentiment found!");
			else
				if(sentimentData.docSentiment != null && !string.IsNullOrEmpty(sentimentData.docSentiment.type))
					print("ExampleAlchemyLanguage "+"Sentiment: {0}, Score: {1} "+sentimentData.docSentiment.type+" "+sentimentData.docSentiment.score);
			curSentiment = sentimentData.docSentiment.type;
		}
		else
		{
			print("ExampleAlchemyLanguage "+"Failed to find Relations!");
		}
		isInterpreting = false;
	}
}
