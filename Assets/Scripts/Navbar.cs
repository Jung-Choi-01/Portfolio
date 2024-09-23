using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Navbar : MonoBehaviour
{
    private PortfolioEntryList currentList;
    [SerializeField] private float pxSpace; // the number of pixels of space between each dot in the navbar
    [SerializeField] private float cursorMoveTime; // seconds for cursor to move to destination
    private int currentIndex;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private YapGradualText yapTextField;
    [SerializeField] private TextMeshProUGUI titleTextField;
    [SerializeField] private TextMeshProUGUI toolsTextField;
    [SerializeField] private RectTransform cursorTransform;
    [SerializeField] private GameObject dotPrefab;

    private float firstDotPosition;
    private float targetCursorPosition;
    private bool cursorMoving;
    private float cursorMoveStartTime;
    private float cursorMoveStartPosition;
    private List<GameObject> currentDots;
    
    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip click;


    void Start()
    {
        cursorMoving = false;
        currentDots = new List<GameObject>();
    }

    void Update()
    {
        if(!cursorMoving) return;
        float t = (Time.time-cursorMoveStartTime)/(cursorMoveTime);
        // Debug.Log(t);
        if(t > 1f)
        {
            cursorTransform.anchoredPosition = Vector3.right * targetCursorPosition;
            cursorMoving = false;
        }
        else cursorTransform.anchoredPosition = Vector3.right * Mathf.Lerp(cursorMoveStartPosition, targetCursorPosition, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, t)));
    }

    public void SetCurrentList(PortfolioEntryList list)
    {
        currentIndex = 0;
        currentList = list;
        foreach(GameObject obj in currentDots) Destroy(obj); 
        UpdateNavbarDots();
        RefreshNavbar();
        AdjustCursorPosition();
    }

    void UpdateNavbarDots()
    {
        float firstDotMultiplier = (currentList.entries.Count()-1)/2f*-1f;
        firstDotPosition = pxSpace*firstDotMultiplier;

        for(int i = 0; i < currentList.entries.Count(); i++)
        {
            // Debug.Log("Creating at space " + (Vector3.right * firstDotPosition + Vector3.right * pxSpace * i));
            GameObject currentDot = Instantiate(dotPrefab, gameObject.transform.GetChild(2));
            RectTransform rectTransform = currentDot.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector3.right * (pxSpace * i + firstDotPosition);
            currentDots.Add(currentDot);
        }
    }

    public void NavbarNext()
    {
        currentIndex += 1;
        if(currentIndex >= currentList.entries.Count()) currentIndex = 0;
        audioSource.PlayOneShot(click);
        RefreshNavbar();
        AdjustCursorPosition();
        videoPlayer.Play();
    }
    public void NavbarPrev()
    {
        currentIndex -= 1;
        if(currentIndex < 0) currentIndex = currentList.entries.Count() - 1;
        audioSource.PlayOneShot(click);
        RefreshNavbar();
        AdjustCursorPosition();
        videoPlayer.Play();
    }

    // using currentindex, set the value of all the display fields
    void RefreshNavbar()
    {
        videoPlayer.transform.parent.gameObject.SetActive(currentList.entries[currentIndex].videoUrl.Length > 0);
        videoPlayer.url = Application.streamingAssetsPath + "/" + currentList.entries[currentIndex].videoUrl;
        // Debug.Log(Application.streamingAssetsPath + "/" + currentList.entries[currentIndex].videoUrl);

        yapTextField.SetText(currentList.entries[currentIndex].yapText);
        toolsTextField.text = currentList.entries[currentIndex].toolsText;
        titleTextField.text = currentList.entries[currentIndex].titleText;
    }

    void AdjustCursorPosition()
    {
        cursorMoveStartPosition = cursorTransform.anchoredPosition.x;
        targetCursorPosition = pxSpace * currentIndex + firstDotPosition;
        cursorMoving = true;
        cursorMoveStartTime = Time.time;
    }
}
