﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ModuloKart.Controls;

public enum PauseMenuOption
{
    resume,
    menu,
    options,
    quit,
}
public class PauseMenu : MonoBehaviour
{
  
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private GameObject ResumeButton;
    private GameObject MenuButton;
    private GameObject OptionsButton;
    private GameObject QuitButton;

    public PauseMenuOption mainMenuOption;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        PauseMenuNext();
        PauseMenuPrev();
        ConfirmPauseMenuOption();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("StartButton_ANYPLAYER"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

            GetMenuOptions();
            InitPauseMenu();

        }
    }

   
        bool isPressPrev;
        bool isPressPrevRelease;
        private void PauseMenuPrev()
        {
            if (Input.GetAxis("LeftJoyStickY_ANYPLAYER") < 0)
            {
                isPressPrev = true;
            }
            if (isPressPrev)
            {
                if (Input.GetAxis("LeftJoyStickY_ANYPLAYER") == 0)
                {
                    isPressPrev = false;
                    isPressPrevRelease = true;
                }
            }
            if (isPressPrevRelease)
            {
                isPressPrevRelease = false;

                switch (mainMenuOption)
                {
                    case PauseMenuOption.resume:
                        mainMenuOption = PauseMenuOption.quit;
                        ResumeButton.SetActive(true);
                        MenuButton.SetActive(false);
                        OptionsButton.SetActive(false);
                        QuitButton.SetActive(false);
                    break;
                    case PauseMenuOption.quit:
                        mainMenuOption = PauseMenuOption.resume;
                    ResumeButton.SetActive(false);
                    MenuButton.SetActive(false);
                    OptionsButton.SetActive(false);
                    QuitButton.SetActive(true);
                    break;
                    default:
                        Debug.Log("Unexpected Player Number Selection Option");
                        break;
                }
            }
        }


        bool isPressNext;
        bool isPressNextRelease;
        private void PauseMenuNext()
        {
            if (Input.GetAxis("LeftJoyStickY_ANYPLAYER") > 0)
            {
                isPressNext = true;
            }
            if (isPressNext)
            {
                if (Input.GetAxis("LeftJoyStickY_ANYPLAYER") == 0)
                {
                    isPressNext = false;
                    isPressNextRelease = true;
                }
            }
            if (isPressNextRelease)
            {
                isPressNextRelease = false;

                switch (mainMenuOption)
                {
                    case PauseMenuOption.resume:
                        mainMenuOption = PauseMenuOption.quit;
                        ResumeButton.SetActive(true);
                        QuitButton.SetActive(false);
                        break;
                    case PauseMenuOption.quit:
                        mainMenuOption = PauseMenuOption.resume;
                        ResumeButton.SetActive(false);
                        QuitButton.SetActive(true);
                        break;
                    default:
                        Debug.Log("Unexpected Player Number Selection Option");
                        break;
                }
            }
        }

        private void InitPauseMenu()
        {
            mainMenuOption = PauseMenuOption.resume;
        ResumeButton.SetActive(false);
            QuitButton.SetActive(true);
        }

        private void GetMenuOptions()
        {
            MenuSelectionOption[] temp = GameObject.FindObjectsOfType<MenuSelectionOption>();

            foreach (MenuSelectionOption t in temp)
            {
                if (t.mainMenuOption.Equals(PauseMenuOption.resume))
                {
                    ResumeButton = t.bg;
                }
                else if (t.mainMenuOption.Equals(PauseMenuOption.quit))
                {
                    QuitButton = t.bg;
                }
            }
        }

        private void ConfirmPauseMenuOption()
        {
            if (Input.GetButtonDown("A_ANYPLAYER"))
            {
                Debug.Log("Do Something");
                switch (mainMenuOption)
                {
                    case PauseMenuOption.resume:
                    Resume();
                        break;
                    case PauseMenuOption.quit:
                    Quit();
                        break;
                    default:
                        Debug.Log("Unexpected Player Number Selection Option");
                        break;
                }
            }
        }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("should resume wooooooooo");
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("BasicMenu");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("should quit i hope it works");
    }

}