using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class menuScript : MonoBehaviour {

    public PlayerControler playerRef;
    public GameObject menu;
    public GameObject logsRef;
    public GameObject background;
    public Button buttonLog1;
    public Button buttonLog2;
    public Button buttonBack;
    public Text textToShow;
    public int buttonClicked; 

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
	void Update () 
    {
        Debug.Log(currentState);
        Debug.Log(playerRef.isOnMenu);
        if (playerRef.isOnMenu)
        {
            Time.timeScale = 0;
            background.SetActive(true);            

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
                buttonBack.gameObject.SetActive(true);
                buttonLog1.gameObject.SetActive(true);
                buttonLog2.gameObject.SetActive(true);
                Cursor.visible = true;

                buttonLog1.interactable = false;
                buttonLog2.interactable = false;
                

                foreach (logScript element in playerRef.logList)
                {
                    if (element.title == "log1")
                    {
                        buttonLog1.interactable = true;
                    }
                    if (element.title == "log2")
                    {
                        buttonLog1.interactable = true;
                    }
                }

                
            }
            if (currentState== states.readingLog)
            {
                menu.SetActive(false);
                logsRef.SetActive(false);
                Cursor.visible = true;

                if (buttonClicked == 1)
                {
                    foreach (logScript element in playerRef.logList)
                    {
                        try
                        {
                            if (element.title == "log1")
                            {
                                textToShow.gameObject.SetActive(true);
                                textToShow.text = element.textInLog;
                            }
                        }

                        catch
                        {
                            
                        }
                    }
                }
            }
        }


        //EXIT MENU & RETURN TO GAMEPLAY 
        if (!playerRef.isOnMenu )
        {
            Time.timeScale = 1;
            background.SetActive(false);
            menu.SetActive(false);
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

    public void log1Mission()
    {
        foreach (logScript element in playerRef.logList)
        {
            if (element.title == "log1")
            {
                currentState = states.readingLog;
                beforeState = states.logsList;
                buttonClicked = 1;
                buttonBack.gameObject.SetActive(true);
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
                buttonClicked = 2;
                buttonBack.gameObject.SetActive(true);
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
                buttonClicked = 3;
                buttonBack.gameObject.SetActive(true);
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
                buttonClicked = 4;
                buttonBack.gameObject.SetActive(true);
            }
        }
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
