using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PersonController : MonoBehaviour {

    public DialogBox messageBox;
    public InputField chatBox;

    public List<KeyValuePair<string, string>> currentDialog;
    public int dialogIndex;

    public int currentState; //Use this to track the progress in the "story"

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (messageBox.dialogCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(currentDialog != null && dialogIndex < currentDialog.Count)
                {
                    messageBox.displayDialog(currentDialog[dialogIndex].Key, currentDialog[dialogIndex].Value);
                    dialogIndex++;
                }
                else
                {
                    currentDialog = null;
                    dialogIndex = 0;
                    chatBox.interactable = true;
                }
            }
        }
	}

    public void InterpretResponse() {
        string response = chatBox.text;
        Debug.Log(response);
        chatBox.text = "";
        chatBox.interactable = false;

        //Insert Logic for progressing the "story"
        //Replace the text file path with whatever is appropriate
        currentDialog = getDialogFromFile("C:/Users/Roger Liu/Desktop/test.txt");
        messageBox.displayDialog(currentDialog[dialogIndex].Key, currentDialog[dialogIndex].Value);
        dialogIndex++;
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
}
