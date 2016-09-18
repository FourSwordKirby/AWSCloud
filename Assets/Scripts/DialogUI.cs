using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public class DialogUI : MonoBehaviour
{
    public DialogBox dialogBox;

    public DialogBox SpeakerImage;
    
    private float displayDelay = 0.5f;

    public void Update()
    {
        if (!dialogCompleted())
        {
            displayDelay = 0.5f;
        }
        else if (dialogBox.dialogField.text != "")
        {
            displayDelay -= Time.deltaTime;
        }
    }
    

	public void clearLoveInterests() {
	}

    public bool dialogCompleted()
    {
        return dialogBox.dialogCompleted;
    }

    public void displayNormal()
    {
        dialogBox.dialogField.fontStyle = FontStyle.Normal;
    }

    public void displayDialog(string name, string text, DisplaySpeed speed = DisplaySpeed.fast)
    {
        dialogBox.displayDialog(name, text, speed);
    }

    public void resolveDialog()
    {
        dialogBox.resolveDialog();
    }

    public void clearDialogBox()
    {
        dialogBox.displayDialog("", "");
        dialogBox.dialogCompleted = true;
    }

	public void closeDialogBox() {
		dialogBox.closeDialog ();
	}
}