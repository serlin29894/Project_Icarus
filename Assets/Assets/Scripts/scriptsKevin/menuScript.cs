﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

    public PlayerControler playerRef;
    public GameObject menu;
    public GameObject logsRef;
    public GameObject background;
    public Button buttonLog1;
    public Button buttonLog2;
    public Button buttonLog3;
    public Button buttonLog4;
    public Button buttonWeaponLog;
    public Button buttonBack;
    public Text textToShow;
    public int buttonClicked;
    public bool isMainMenu;

	public enum states
    {
        mainMenu,
        logsList,
        readingLog,
    }

    states beforeState;
    states currentState;	

    void Start ()
    {
        currentState = states.mainMenu;
    }

	// Update is called once per frame
	void LateUpdate () 
    {
        if (!isMainMenu)
        {
            if (playerRef.isOnMenu)
            {
                Time.timeScale = 0;
                background.SetActive(true);
                Cursor.visible = true;

                //STATES
                if (currentState == states.mainMenu)
                {
                    menu.SetActive(true);
                    logsRef.SetActive(false);
                    Cursor.visible = true;
                }

                if (currentState == states.logsList)
                {
                    menu.SetActive(false);
                    logsRef.SetActive(true);
                    textToShow.gameObject.SetActive(false);
                    buttonWeaponLog.gameObject.SetActive(true);
                    buttonBack.gameObject.SetActive(true);
                    buttonLog1.gameObject.SetActive(true);
                    buttonLog2.gameObject.SetActive(true);
                    buttonLog3.gameObject.SetActive(true);
                    buttonLog4.gameObject.SetActive(true);
                    Cursor.visible = true;

                    buttonLog1.interactable = false;
                    buttonLog2.interactable = false;
                    buttonLog3.interactable = false;
                    buttonLog4.interactable = false;
                    buttonWeaponLog.interactable = false;


                    foreach (logScript element in playerRef.logList)
                    {
                        if (element.title == "log1")
                        {
                            buttonLog1.interactable = true;
                        }
                        if (element.title == "log2")
                        {
                            buttonLog2.interactable = true;
                        }
                        if (element.title == "log3")
                        {
                            buttonLog3.interactable = true;
                        }
                        if (element.title == "log4")
                        {
                            buttonLog4.interactable = true;
                        }
                        if (element.title == "weaponLog")
                        {
                            buttonWeaponLog.interactable = true;
                        }
                    }
                }

                if (currentState == states.readingLog)
                {
                    menu.SetActive(false);
                    logsRef.SetActive(false);
                    Cursor.visible = true;
                    textToShow.gameObject.SetActive(true);
                }
            }


            //EXIT MENU & RETURN TO GAMEPLAY 
            if (!playerRef.isOnMenu)
            {
                Time.timeScale = 1;
                background.SetActive(false);
                menu.SetActive(false);
            }
        }
	}    


    //Buttons
    public void resume ()
    {        
        Time.timeScale = 1;
        playerRef.isOnMenu = false;
        menu.SetActive(false);
        Cursor.visible = false;
    }

    public void logs ()
    {
        currentState = states.logsList;
        beforeState = states.mainMenu;
    }

    public void weaponLog()
    {
        foreach (logScript element in playerRef.logList)
        {
            if (element.title == "weaponLog")
            {
                currentState = states.readingLog;
                beforeState = states.logsList;
                textToShow.text = element.textInLog;
                buttonBack.gameObject.SetActive(true);
                playerRef.isOnMenu = true;
            }
        }
    }

    public void log1Mission()
    {
        foreach (logScript element in playerRef.logList)
        {
            if (element.title == "log1")
            {
                currentState = states.readingLog;
                beforeState = states.logsList;
                textToShow.text = element.textInLog;
                buttonBack.gameObject.SetActive(true);
                playerRef.isOnMenu = true;
            }
        }        
    }

    public void log2Mission()
    {
        foreach (logScript element in playerRef.logList)
        {
            if (element.title == "log2")
            {
                currentState = states.readingLog;
                beforeState = states.logsList;
                textToShow.text = element.textInLog;
                buttonBack.gameObject.SetActive(true);
                playerRef.isOnMenu = true;
            }
        }
    }

    public void log3Mission()
    {
        foreach (logScript element in playerRef.logList)
        {
            if (element.title == "log3")
            {
                currentState = states.readingLog;
                beforeState = states.logsList;
                textToShow.text = element.textInLog;
                buttonBack.gameObject.SetActive(true);
                playerRef.isOnMenu = true;
            }
        }
    }    
    
    public void log4Mission()
    {
        foreach (logScript element in playerRef.logList)
        {
            if (element.title == "log4")
            {
                currentState = states.readingLog;
                beforeState = states.logsList;
                textToShow.text = element.textInLog;
                buttonBack.gameObject.SetActive(true);
                playerRef.isOnMenu = true;
            }
        }
    }

    public void returnToMainMenu ()
    {
        SceneManager.LoadScene(0);
    }

    public void startGame ()
    {
        SceneManager.LoadScene(1);
    }

    public void exitApplication()
    {
        Application.Quit();
    }

    public void back ()
    {
        currentState = beforeState;

        if(currentState == states.logsList)
        {
            beforeState = states.mainMenu;
        }
        
        if (beforeState == states.mainMenu )
        {
            buttonBack.gameObject.SetActive(false);            
        }
    }
}
