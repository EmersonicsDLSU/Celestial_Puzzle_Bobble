using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WinWindow : MonoBehaviour
{
    [SerializeField] private List<Image> _secondsPlaces = new List<Image>();
    [SerializeField] private List<Image> _pointsPlaces = new List<Image>();
    private UIResources _uiResources;
    // Start is called before the first frame update
    void Start()
    {
        _uiResources = FindObjectOfType<UIResources>();
        if (_uiResources == null)
        {
            Debug.LogError("Missing 'UIResources' script!");
        }
    }
    
    public void UpdateTime(int time)
    {
        foreach (var letter in _secondsPlaces)
        {
            letter.sprite = _uiResources._numbers[10];
        }
        int index = _secondsPlaces.Count - 1;
        while (time > 0)
        {
            int digit = time % 10; // Extract the last digit
            time /= 10; // Remove the last digit from the score

            // assign the image number to the number place
            _secondsPlaces[index--].sprite = _uiResources._numbers[digit];
            
        }
    }
    public void UpdatePoints(int score)
    {
        foreach (var letter in _pointsPlaces)
        {
            letter.sprite = _uiResources._numbers[10];
        }
        int index = _pointsPlaces.Count - 1;
        while (score > 0)
        {
            int digit = score % 10; // Extract the last digit
            score /= 10; // Remove the last digit from the score

            // assign the image number to the number place
            _pointsPlaces[index--].sprite = _uiResources._numbers[digit];
            
        }
    }
}
