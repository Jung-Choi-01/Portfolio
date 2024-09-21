using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class YapGradualText : MonoBehaviour
{
    [SerializeField] private float secondsBetweenChars;
    [SerializeField] private float secondsBetweenCursorBlink;
    [SerializeField] private TextMeshProUGUI textField;
    private float startTime;
    private string targetText;
    private int lastLength;
    private bool writing;

    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        writing = false;
        lastLength = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // cursor stuff
        string cursorText = "";
        int cursorBlink = Mathf.FloorToInt((Time.time-startTime)/secondsBetweenCursorBlink);
        if(cursorBlink%2==0) cursorText = "_";

        // text stuff
        int charsToWrite;
        charsToWrite = Mathf.Min(Mathf.FloorToInt((Time.time - startTime)/secondsBetweenChars), targetText.Length);
        textField.text = targetText.Substring(0, charsToWrite) + cursorText;
        if(charsToWrite == targetText.Length) audioSource.Stop();
    }

    public void SetText(string text)
    {
        startTime = Time.time;
        targetText = text;
        writing = true;
        audioSource.Play();
    }
}
