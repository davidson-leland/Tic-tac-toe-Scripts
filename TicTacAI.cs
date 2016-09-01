using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TicTacAI{

    private GameController controller;
    //private string[,] gameGrid;// = new string[3,3];
    private Text[,] gameGrid;


	void Start () {
        //Debug.Log(" ai has been activated");
        gameGrid = new Text[3, 3];

    }

    public TicTacAI()
    {
        Start();
    }

    public void SetController( GameController inC)
    {
        controller = inC;
    }

    public void SetGameGrid(Text[] buttonList)//too late did i realise this was backwards from normal graphing
    {
        gameGrid[0, 0] = buttonList[0];//.text;
        gameGrid[0, 1] = buttonList[1];//.text;
        gameGrid[0, 2] = buttonList[2];//.text;

        gameGrid[1, 0] = buttonList[3];//.text;
        gameGrid[1, 1] = buttonList[4];//.text;
        gameGrid[1, 2] = buttonList[5];//.text;

        gameGrid[2, 0] = buttonList[6];//.text;
        gameGrid[2, 1] = buttonList[7];//.text;
        gameGrid[2, 2] = buttonList[8];//.text;
    }

    public void LogGrid()
    {/*
        Debug.Log(gameGrid[0, 0].text + gameGrid[0, 1].text + gameGrid[0, 2].text);
        Debug.Log(gameGrid[1, 0].text + gameGrid[1, 1].text + gameGrid[1, 2].text);
        Debug.Log(gameGrid[2, 0].text + gameGrid[2, 1].text + gameGrid[2, 2].text);*/
    }

    public int chooseMove()
    {
        int i;
        //first checking to see if we can get three in a row

        Debug.Log(" checking for win");
        i = CheckBlanks(controller.aiSide);

        if( i != -1)
        {
            //do something here
            Debug.Log(" sending " + i);
            return i;
        }

        //next checking to see if player is clsoe to getting 3 in a row
        Debug.Log(" checking to block win");
        if ( controller.aiSide == "X")
        {
            i = CheckBlanks("O");
        }
        else
        {
            i = CheckBlanks("X");
        }

        if (i != -1)
        {
            Debug.Log(" sending " + i);
            return i;
        }

        //otherwise we have set rules for picking which space to go to next. this is all for hard/impossible ai atm.
       /* Debug.Log(" move count =  " + controller.moveCount);
        Debug.Log("look after this for issues with ai ");
        Debug.Log("look after this for issues with ai ");
        Debug.Log("look after this for issues with ai ");*/
        if (controller.moveCount == 0)//pick a corner if going first
        {
            //return -1;
            return 0;// will randomise later
        }
        else if (controller.moveCount == 1)//pick center if going 2nd
        {
            if (gameGrid[1, 1].text == "")
            {
                return 4;
            }
            else//pick corner if can't pick center
            {
                return 0;//randomise which corner later
            }
            return -1;
        }
        else if (controller.moveCount == 2)
        {
            //Debug.Log("ai 2nd turn after going first ");

            //else if you went first and it is turn 3 (2)

            /* if player picked center
                 pick corner diaganal your first choice*/

            if (gameGrid[1, 1].text != controller.aiSide && gameGrid[1, 1].text != "")
            {
                //Debug.Log("player picked center ");
                if (gameGrid[0, 0].text == controller.aiSide)
                {
                    return 8;
                }
                else if (gameGrid[2, 0].text == controller.aiSide)
                {
                    return 2;
                }
                else if (gameGrid[0, 2].text == controller.aiSide)
                {
                    return 6;
                }
                else if (gameGrid[2, 2].text == controller.aiSide)
                {
                    return 0;
                }
                return -1;
            }
            else if (gameGrid[0, 0].text == controller.aiSide)//going to set the ai for only picking 00 on first choice, expand later.
            {
                /*if player picked corner diaganol your first choice
                    pick either other corner*/
               
                if (gameGrid[2, 2].text != controller.aiSide && gameGrid[2, 2].text != "")
                {
                    //Debug.Log("player picked corner diagonal our first choice ");
                    return 2;
                }
                /*else if player picked corner on the row or column of your first choice
                    pick corner diaganal players corner*/
                else if (gameGrid[0, 2].text != controller.aiSide && gameGrid[0, 2].text != "")
                {
                    //Debug.Log("player picked corner on same row");
                    return 6;
                }
                else if (gameGrid[2, 0].text != controller.aiSide && gameGrid[2, 0].text != "")
                {
                    //Debug.Log("player picked corner on same column");
                    return 2;
                }

                /*else if player picked an outer center space
                    if player picked a touching sapace
                        if player picked space on same row
                            pick corner on same column

                        if player picked space on same column
                            pick corner on same row*/

                else if (gameGrid[0, 1].text != controller.aiSide && gameGrid[0, 1].text != "")
                {
                    //Debug.Log("player picked center space on same row");
                    return 6;
                }
                else if (gameGrid[1, 0].text != controller.aiSide && gameGrid[1, 0].text != "")
                {
                    //Debug.Log("player picked center space on same column");
                    return 2;
                }
                /*else
                    pick corner not touching player space*/
                else if (gameGrid[1, 2].text != controller.aiSide && gameGrid[1, 2].text != "")
                {
                    //Debug.Log("player picked position 5");
                    return 6;
                }
                else if (gameGrid[2, 1].text != controller.aiSide && gameGrid[2, 1].text != "")
                {
                    //Debug.Log("player picked position 7");
                    return 2;
                }
                //Debug.Log("error can't tell what palyer picked ");
                return -1;
            }
           
        }
        else if(controller.moveCount == 3)// only way to block with our strategy is to pick a middle space on the outere edge
        {

            if(gameGrid[0, 1].text == "")
            {
                return 1;
            }
            else if (gameGrid[1, 0].text == "")
            {
                return 3;
            }
            else if (gameGrid[1, 2].text == "")
            {
                return 5;
            }
            else if (gameGrid[2, 1].text == "")
            {
                return 7;
            }
            return -1;
        }
        else //pick any available corner space if turn 4 or higher with preference for one not touching an enemy space
        {
            if (gameGrid[0, 0].text == "" && gameGrid[0, 1].text == "" && gameGrid[1, 0].text == "")
            {
                return 0;
            }
            else if (gameGrid[2, 0].text == "" && gameGrid[1, 0].text == "" && gameGrid[2, 1].text == "")
            {
                return 6;
            }
            else if (gameGrid[0, 2].text == "" && gameGrid[0, 1].text == "" && gameGrid[1, 2].text == "")
            {
                return 2;
            }
            else if (gameGrid[2, 2].text == "" && gameGrid[2, 1].text == "" && gameGrid[1, 2].text == "")
            {
                return 8;
            }

            //Debug.Log("attempting to pick any available corner space");
            if (gameGrid[0, 0].text == "")
            {
                return 0;
            }
            else if (gameGrid[2, 0].text == "")
            {
                return 6;
            }
            else if (gameGrid[0, 2].text == "")
            {
                return 2;
            }
            else if (gameGrid[2, 2].text == "")
            {
                return 8;
            }

            return -1;
        }


        /*else if you went first and it is turn 3 (2)

            if player picked center
                pick corner diaganal your first choice

            else if player picked corner diaganol your first choice
                pick either corner

            else if player picked corner on the row or column of your first choice
                pick corner diaganal players corner
            
            else if player picked an outer center space
                if player picked a touching sapace
                    if player picked space on same row
                        pick corner on same column
                
                    if player picked space on same column
                        pick corner on same row
               
                else
                    pick corner not touching player space*/

        //Debug.Log(" no matches");
        return -1;

    }

    private int CheckBlanks( string inStr)
    {
        //Debug.Log("checking blanks");
        if (gameGrid[0, 0].text == "")
        {
            if ((gameGrid[0, 1].text == inStr && gameGrid[0, 2].text == inStr) || (gameGrid[1, 0].text == inStr && gameGrid[2, 0].text == inStr) || (gameGrid[1, 1].text == inStr && gameGrid[2, 2].text == inStr))
            {
                //Debug.Log(" returning 0");
                return 0;
            }
        }

       if (gameGrid[0, 1].text == "")
        {
            if ((gameGrid[0, 0].text == inStr && gameGrid[0, 2].text == inStr) || (gameGrid[1, 1].text == inStr && gameGrid[2, 1].text == inStr))
            {
                //Debug.Log(" returning 1");
                return 1;
            }
        }

        if (gameGrid[0, 2].text == "")
        {
            if ((gameGrid[0, 0].text == inStr && gameGrid[0, 1].text == inStr) || (gameGrid[1, 2].text == inStr && gameGrid[2, 2].text == inStr) || (gameGrid[1, 1].text == inStr && gameGrid[2, 0].text == inStr))
            {
                //Debug.Log(" returning 2");
                return 2;
            }
        }

        if (gameGrid[1, 0].text == "")
        {
            if ((gameGrid[0, 0].text == inStr && gameGrid[2, 0].text == inStr) || (gameGrid[1, 1].text == inStr && gameGrid[1, 2].text == inStr))
            {
                //Debug.Log(" returning 3");
                return 3;
            }
        }

        if (gameGrid[1, 1].text == "")
        {
            if ((gameGrid[0, 0].text == inStr && gameGrid[2, 2].text == inStr) || (gameGrid[0, 2].text == inStr && gameGrid[2, 0].text == inStr) || (gameGrid[1, 0].text == inStr && gameGrid[1, 2].text == inStr) || (gameGrid[0, 1].text == inStr && gameGrid[2, 1].text == inStr))
            {
                //Debug.Log(" returning 4");
                return 4;
            }
        }

        if (gameGrid[1, 2].text == "")
        {
            if ((gameGrid[1, 0].text == inStr && gameGrid[1, 1].text == inStr) || (gameGrid[0, 2].text == inStr && gameGrid[2, 2].text == inStr))
            {
                //Debug.Log(" returning 5");
                return 5;
            }
        }

        if (gameGrid[2, 0].text == "")
        {
            if ((gameGrid[0, 0].text == inStr && gameGrid[1, 0].text == inStr) || (gameGrid[1, 1].text == inStr && gameGrid[0, 2].text == inStr) || (gameGrid[2, 1].text == inStr && gameGrid[2, 2].text == inStr))
            {
                //Debug.Log(" returning 6");
                return 6;
            }
        }

        if (gameGrid[2, 1].text == "")
        {
            if ((gameGrid[0, 1].text == inStr && gameGrid[1, 1].text == inStr) || (gameGrid[2, 0].text == inStr && gameGrid[2, 2].text == inStr))
            {
                //Debug.Log(" returning 7");
                return 7;
            }
        }

        if (gameGrid[2, 2].text == "")
        {
            if ((gameGrid[0, 0].text == inStr && gameGrid[1, 1].text == inStr) || (gameGrid[0, 2].text == inStr && gameGrid[1, 2].text == inStr) || (gameGrid[2, 0].text == inStr && gameGrid[2, 1].text == inStr))
            {
                //Debug.Log(" returning 8");
                return 8;
            }
        }

        //Debug.Log(" not matches");

        return -1;
    }
	
}