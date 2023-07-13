using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    [SerializeField] private List<Image> _numberPlaces = new List<Image>();
    private UIResources _uiResources;
    void Start()
    {
        _uiResources = FindObjectOfType<UIResources>();
        if (_uiResources == null)
        {
            Debug.LogError("Missing 'UIResources' script!");
        }
    }

    public void UpdateScore(int score)
    {
        int index = _numberPlaces.Count - 1;
        foreach (var letter in _numberPlaces)
        {
            letter.sprite = _uiResources._numbers[10];
        }
        while (score > 0)
        {
            int digit = score % 10; // Extract the last digit
            score /= 10; // Remove the last digit from the score

            // assign the image number to the number place
            _numberPlaces[index--].sprite = _uiResources._numbers[digit];
            
        }
    }
}
