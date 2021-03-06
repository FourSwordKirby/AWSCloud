﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;


public class DialogBox : MonoBehaviour {
    public Text dialogField;
    public Text speakerFace;
    public bool dialogCompleted;

    public string dialog = "";
    private int dialogTracker = 0;

    private float textDisplaySpeed;
    private float textDisplayTimer;

    private const  float FAST_DISPLAY_SPEED = 0.0f;
    private const float SLOW_DISPLAY_SPEED = 0.03f;

    // Use this for initialization
	void Awake () {
  	    this.dialogField.text = "";
        dialogCompleted = true;
	}

	// Update is called once per frame
	void Update () {
        //Do something where text appears according to the textDisplaySpeed
        if (textDisplayTimer> 0)
        {
            textDisplayTimer -= Time.deltaTime;
            return;
        }

        if (this.dialogField.text != dialog)
        {
            char[] dialogCharArray = new char[dialog.Length];
            dialogCharArray = this.dialogField.text.ToCharArray();
            dialogCharArray[dialogTracker] = dialog[dialogTracker];
            this.dialogField.text = new string(dialogCharArray);
            dialogTracker++;

            textDisplayTimer = textDisplaySpeed;
            dialogCompleted = false;
        }
        else
            dialogCompleted = true;
	}

    public void displayDialog(string speaker, string dialog, DisplaySpeed displaySpeed = DisplaySpeed.fast)
    {
        dialogCompleted = false;

        this.gameObject.SetActive(true);
        this.speakerFace.text = speaker;
        this.dialog = dialog;
        this.dialogTracker = 0;

        string taggedText = "";
        for(int i = 0; i < dialog.Length; i++)
        {
            if (dialog[i] == '<')
            {
                string insertTag = "";
                while (dialog[i] != '>')
                {
                    insertTag += dialog[i];
                    i++;
                }
                insertTag += dialog[i];
                taggedText += insertTag;
            }
            else
            {
                taggedText += " ";
            }
        }
        //Prevents the name from flickering
        this.dialogField.text = taggedText;


        if (displaySpeed == DisplaySpeed.immediate)
        {
            this.dialogField.text = dialog;
        }
        else if (displaySpeed == DisplaySpeed.fast)
        {
            textDisplaySpeed = FAST_DISPLAY_SPEED;
        }
        else if (displaySpeed == DisplaySpeed.slow)
        {
            textDisplaySpeed = SLOW_DISPLAY_SPEED;
        }
    }

    public void closeDialog()
    {
        this.gameObject.SetActive(false);
        //this.dialog = "";
        this.dialogTracker = 0;
    }

    public void resolveDialog()
    {
       this.dialogField.text = dialog;
    }
}

public enum DisplaySpeed{
    immediate,
    slow,
    fast
}