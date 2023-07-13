using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] private List<GameObject> _playerTabs = new List<GameObject>();
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;
    private enum UI_Windows
    {
        NONE = -1, PLAYER1, PLAYER2, PAUSE_MENU, WIN_WINDOW, LOSE_WINDOW, ENUM_SIZE
    };
    
    private bool _isPaused = false;
    void Start()
    {
        // hide at first
        _pauseMenu.SetActive(false);
        _winWindow.SetActive(false);
        _loseWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            // Pause the game by setting time scale to 0
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
            FindObjectOfType<AudioManager>().Stop(SoundCode.MAIN_MENU_BG);
        }
        else
        {
            // Resume the game by setting time scale back to 1
            Time.timeScale = 1f;
            _pauseMenu.SetActive(false);
            FindObjectOfType<AudioManager>().Play(SoundCode.MAIN_MENU_BG);
        }
    }

    public bool IsPaused()
    {
        return _isPaused;
    }

    public void OpenWinWindow()
    {
        TogglePause();
        _winWindow.SetActive(true);
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        FindObjectOfType<UI_WinWindow>().UpdateTime((int)playerStatus._currentLevelTime);
        FindObjectOfType<UI_WinWindow>().UpdatePoints(playerStatus._score);
    }
    public void OpenLoseWindow()
    {
        TogglePause();
        _loseWindow.SetActive(true);
    }
}
