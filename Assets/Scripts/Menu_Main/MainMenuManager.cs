﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModuloKart.MainMenu
{
    public enum MainMenuOption
    {
        play,
        credit,
        quit,
    }
    public class MainMenuManager : MonoBehaviour
    {
        public GameObject Credit_Text;
        public GameObject Play_B;
        public GameObject Credit_B;
        public GameObject Quit_B;
        private GameObject bg_Play;
        private GameObject bg_Quit;
        private GameObject bg_Credit;


        public MainMenuOption mainMenuOption;


        private void Awake()
        {
            GetMenuOptions();
            InitMainMenu();
        }

        void Update()
        {
            MainMenuNext();
            MainMenuPrev();
            ConfirmMenuOption();
        }


        bool isPressPrev;
        bool isPressPrevRelease;
        private void MainMenuPrev()
        {
            if (Input.GetAxis("LeftJoyStickY_ANYPLAYER") < 0)
            {
                //AudioManager.instance.Play("Move_Through_Menu_Panels");
                isPressPrev = true;
            }
            if (isPressPrev)
            {
                if (Input.GetAxis("LeftJoyStickY_ANYPLAYER") == 0)
                {
                    isPressPrev = false;
                    isPressPrevRelease = true;
                    AudioManager.instance.Play("Move_Through_Menu_Panels");
                }
            }
            if (isPressPrevRelease)
            {
                isPressPrevRelease = false;

                switch (mainMenuOption)
                {
                    case MainMenuOption.play:
                        mainMenuOption = MainMenuOption.quit;
                        bg_Play.SetActive(true);
                        bg_Credit.SetActive(true);
                        bg_Quit.SetActive(false);
                        break;
                    case MainMenuOption.credit:
                        mainMenuOption = MainMenuOption.play;
                        bg_Play.SetActive(false);
                        bg_Credit.SetActive(true);
                        bg_Quit.SetActive(true);
                        break;
                    case MainMenuOption.quit:
                        mainMenuOption = MainMenuOption.credit;
                        bg_Play.SetActive(true);
                        bg_Credit.SetActive(false);
                        bg_Quit.SetActive(true);
                        break;
                    default:
                        Debug.Log("Unexpected Player Number Selection Option");
                        break;
                }
            }
        }


        bool isPressNext;
        bool isPressNextRelease;

        private void MainMenuNext()
        {
            if (Input.GetAxis("LeftJoyStickY_ANYPLAYER") > 0)
            {
                //AudioManager.instance.Play("Move_Through_Menu_Panels");
                isPressNext = true;
            }
            if (isPressNext)
            {
                if (Input.GetAxis("LeftJoyStickY_ANYPLAYER") == 0)
                {
                    AudioManager.instance.Play("Move_Through_Menu_Panels");
                    isPressNext = false;
                    isPressNextRelease = true;
                }
            }
            if (isPressNextRelease)
            {
                isPressNextRelease = false;

                switch (mainMenuOption)
                {
                    case MainMenuOption.play:
                        mainMenuOption = MainMenuOption.credit;
                        bg_Play.SetActive(true);
                        bg_Credit.SetActive(false);
                        bg_Quit.SetActive(true);
                        break;
                    case MainMenuOption.credit:
                        mainMenuOption = MainMenuOption.quit;
                        bg_Play.SetActive(true);
                        bg_Credit.SetActive(true);
                        bg_Quit.SetActive(false);
                        break;
                    case MainMenuOption.quit:
                        mainMenuOption = MainMenuOption.play;
                        bg_Play.SetActive(false);
                        bg_Credit.SetActive(true);
                        bg_Quit.SetActive(true);
                        break;
                    default:
                        Debug.Log("Unexpected Player Number Selection Option");
                        break;
                }
            }
        }

        private void InitMainMenu()
        {
            mainMenuOption = MainMenuOption.play;
            bg_Play.SetActive(false);
            bg_Credit.SetActive(true);
            bg_Quit.SetActive(true);
        }

        private void GetMenuOptions()
        {
            MenuSelectionOption[] temp = GameObject.FindObjectsOfType<MenuSelectionOption>();

            foreach (MenuSelectionOption t in temp)
            {
                if (t.mainMenuOption.Equals(MainMenuOption.play))
                {
                    bg_Play = t.bg;
                }
                else if (t.mainMenuOption.Equals(MainMenuOption.credit))
                {
                    bg_Credit = t.bg;
                }
                else if (t.mainMenuOption.Equals(MainMenuOption.quit))
                {
                    bg_Quit = t.bg;
                }
            }
        }

        private void ConfirmMenuOption()
        {
            if (Input.GetButtonDown("A_ANYPLAYER"))
            {
                AudioManager.instance.Play("Confirm_Option");
                Debug.Log("Do Something");
                switch (mainMenuOption)
                {
                    case MainMenuOption.play:
                        ButtonBehavior_LoadPlayerSelectionScene();
                        break;
                    case MainMenuOption.credit:
                        ButtonBehavior_Credit();
                        break;
                    case MainMenuOption.quit:
                        ButtonBehavior_Quit();
                        break;
                    default:
                        Debug.Log("Unexpected Player Number Selection Option");
                        break;
                }
            }
        }

        public void ButtonBehavior_Quit()
        {
            Application.Quit();
        }

        public void ButtonBehavior_LoadPlayerSelectionScene()
        {
            SceneManager.LoadScene(2);
        }
        public void ButtonBehavior_Credit()
        {
            SceneManager.LoadScene("Credit_Scene");
            /*bg_Play.SetActive(false);
            bg_Credit.SetActive(false);
            bg_Quit.SetActive(false);
            Credit_Text.SetActive(true);
            Play_B.SetActive(false);
            Credit_B.SetActive(false);
            Quit_B.SetActive(false);
            }*/
          
        }



    }
}