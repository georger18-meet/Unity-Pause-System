using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingsMenu;
    public bool IsPaused;
    public bool InSettings;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        PauseMode(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPaused();
    }


    private void CheckIfPaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            PauseMode(IsPaused);
        }
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            IsPaused = false;
        }
        else
        {
            IsPaused = true;
        }
    }

    private void ChangeCursorMode(bool unlocked)
    {
        if (unlocked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void PauseMode(bool paused)
    {
        IsPaused = paused;
        OnPause(paused);
        ChangeCursorMode(paused);
        // Comment Following If There's No Settings Menu
        if (!paused)
        {
            SetSettingsMode(false);
        }
    }

    private void OnPause(bool paused)
    {
        if (paused)
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ToggleSettings()
    {
        if (InSettings)
        {
            _pauseMenu.SetActive(true);
            SetSettingsMode(false);
        }
        else
        {
            _pauseMenu.SetActive(false);
            SetSettingsMode(true);
        }
    }

    private void SetSettingsMode(bool status)
    {
        InSettings = status;
        _settingsMenu.SetActive(status);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
