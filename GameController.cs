using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;

    public Button button;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour {

    private string playerSide, initialSide;
    private int  aiState;
    private TicTacAI gameAI;

    public Text[] buttonList, scoreList;
    public string aiSide;    

    public GameObject gameOverPanel, startInfo;
    public Text gameOverText, aiText;

    public GameObject restartButton, nextRoundButton, aiButton;

    public Player playerX, playerO;
    public PlayerColor activePlayerColor, inactivePlayerColor;

   
    public int winsX, winsO, draws, moveCount;
    public GameObject scoreBoard;


    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        gameOverPanel.SetActive(false);
        moveCount = 0;
        draws = 0;
        winsX = 0;
        winsO = 0;
        aiState = 0;

        restartButton.SetActive(false);
        nextRoundButton.SetActive(false);

    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
        aiButton.SetActive(false);
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        initialSide = startingSide;
        

        if(playerSide == "X")
        {
            aiSide = "O";
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            aiSide = "X";
            SetPlayerColors(playerO, playerX);
        }

        StartGame();
    }


    public void AIToggle()
    {
        aiState++;
        if( aiState > 2)
        {
            aiState = 0;
        }

        if (aiState == 0)//no ai , two player game
        {
            aiText.text = "Two Player";
        }
        else if(aiState == 1)//normal ai
        {
            aiText.text = "AI : Normal";
        }
        else if(aiState == 2)//impossible ai
        {
            aiText.text = "AI : Hard";
        }
    }

    //will handle all logic for making ai choices and moves
    void AIMove()
    {
        int i;
        if(gameAI == null)
        {
            gameAI = new TicTacAI();
            gameAI.SetController(this);
            gameAI.SetGameGrid(buttonList);
        }
        
        gameAI.LogGrid();
        i = gameAI.chooseMove();
        if ( -1 != i)
        {
            Debug.Log(" ai activating button");
            buttonList[i].GetComponentInParent<GridSpace>().SetSpace();
        }
        /*else
        {
            i = Random.Range(0, 9);

            if (buttonList[i].text != "")
            {
                AIMove();
            }
            else
            {
                buttonList[i].GetComponentInParent<GridSpace>().SetSpace();
            }
        }*/
               

        /*if(aiState == 1)
        {

        }
        else if(aiState == 2)
        {

        }*/
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";


        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;

        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;

        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;

    }


    public void EndTurn()
    {        
        moveCount ++;

        //top row
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }
        //middle row
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        //bottom row
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        //left column
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        //middle column
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }
        //right column
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        // \ cross
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        // / cross
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (moveCount >= 9)
        {

            GameOver("draw");

        }
        else {

            ChangeSides();

            if (playerSide == aiSide && aiState != 0)
            {
                AIMove();
            }
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
            
        }
    }

    public void RestartGame()
    {
        playerSide = "X";
        initialSide = "";
        moveCount = 0;
        gameOverPanel.SetActive(false);

        //SetBoardInteractable(true);
        SetPlayerButtons(true);
        restartButton.SetActive(false);
        nextRoundButton.SetActive(false);

        SetPlayerColorsInactive();
        startInfo.SetActive(true);
        aiButton.SetActive(true);

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }

        draws = 0;
        winsX = 0;
        winsO = 0;

        SetScore();

    }

    public void NextRound()
    {
        Debug.Log("starting new round");
        moveCount = 0;
        gameOverPanel.SetActive(false);

        SetPlayerButtons(true);
        restartButton.SetActive(false);
        nextRoundButton.SetActive(false);

        SetPlayerColorsInactive();
        //ChangeSides();

        if(initialSide == "X")
        {
            playerSide = "O";
            initialSide = "O";
            SetPlayerColors(playerO, playerX);
        }
        else
        {
            playerSide = "X";
            initialSide = "X";
            SetPlayerColors(playerX, playerO);
        }

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }

        StartGame();

        Debug.Log("player side = " + playerSide);
        Debug.Log("AI side = " + aiSide);
        if (aiState != 0 && playerSide == aiSide)
        {
            Debug.Log("ai moving");
            AIMove();
        }
    }


    void SetGameOverText( string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    void SetScore()
    {
        scoreList[0].text = "X : " + winsX.ToString();
        scoreList[1].text = "Draws : " + draws.ToString();
        scoreList[2].text = winsO.ToString() + " : O";
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);

        if(winningPlayer == "draw")
        {
            SetGameOverText("It's a Draw");
            SetPlayerColorsInactive();

            draws++;
        }
        else
        {
            SetGameOverText( playerSide + " Wins!");

            if(playerSide == "X")
            {
                winsX++;
            }
            else
            {
                winsO++;
            }
        }

        SetScore();



        restartButton.SetActive(true);
        nextRoundButton.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("exiting application");
        
        Application.Quit();
        
    }
}
