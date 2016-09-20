using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;

public class PersonController : MonoBehaviour {

    public DialogBox messageBox;
    public InputField chatBox;
	NLPInterface nlp;
	string curPath;

	// Stuff on screen that changes based on emotion
	public FaceBodyController faceBody;
	public FaceBodyController flowers;

    public List<KeyValuePair<string, string>> currentDialog;
    public int dialogIndex;

    public int currentState = 1; //Use this to track the progress in the "story"

	// Use this for initialization
	void Start () {
		nlp = GetComponent<NLPInterface>();
	}
	
	// Update is called once per frame
	void Update () {
        if (messageBox.dialogCompleted)
        {
			if(currentDialog != null && dialogIndex < currentDialog.Count && Input.GetKeyDown(KeyCode.Space))
			{
				messageBox.displayDialog(currentDialog[dialogIndex].Key, currentDialog[dialogIndex].Value);
				dialogIndex++;
			}
			else if (currentDialog != null && dialogIndex == currentDialog.Count)
			{
//				currentDialog = null;
				chatBox.interactable = true;
			}
        }
	}

    public void InterpretResponse() {
		StartCoroutine(DoInterpretResponse());
    }

	IEnumerator DoInterpretResponse() {
		dialogIndex = 0;
		string response = chatBox.text;
//		Debug.Log(response);
		chatBox.text = "";
		chatBox.interactable = false;

		//map response to emotion
		nlp.GetTextSentiment(response);
		while (nlp.isInterpreting) yield return new WaitForEndOfFrame();
		print(nlp.curSentiment);

		AnimateEmotionChange ();

		//Insert Logic for progressing the "story"
		FindNextDialogPath();

		currentDialog = getDialogFromFile(curPath);
		messageBox.displayDialog(currentDialog[dialogIndex].Key, currentDialog[dialogIndex].Value);
		dialogIndex++;
	}

	void FindNextDialogPath() {
		currentState++;
		string newPath = Application.streamingAssetsPath + "/" + nlp.curSentiment + currentState + ".txt";
		if (System.IO.File.Exists (newPath))
			curPath = newPath;
	}

    public List<KeyValuePair<string, string>> getDialogFromFile(string filename)
    {
        List<KeyValuePair<string, string>> dialog = new List<KeyValuePair<string, string>>();
        System.IO.StreamReader file = new System.IO.StreamReader(filename);

        string line = "";
        while ((line = file.ReadLine()) != null)
        {
            string speaker = line.Split('=')[0];
            string words = line.Split('=')[1];
            dialog.Add(new KeyValuePair<string, string>(speaker, words));
        }
        return dialog;
    }

	void AnimateEmotionChange() {
		if (nlp.curSentiment == "positive") {
			faceBody.makePositive ();
			flowers.makePositive ();
		}
		if (nlp.curSentiment == "negative") {
			faceBody.makeNegative();
			flowers.makeNeutral ();
		}
		if (nlp.curSentiment == "neutral") {
			faceBody.makeNeutral ();
			flowers.makeNeutral ();
		}

	}

}

