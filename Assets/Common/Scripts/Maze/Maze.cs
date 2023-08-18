/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using Common.Scripts.Maze;
using UnityEngine;

/// <summary>
/// Class <c>Maze</c> handles win/lose conditions and game state.
/// </summary>
/// <author>JJ Coldiron</author>
/// <author>Caroline El Jazmi</author>
/// <author>Brodi Matherly</author>
/// <remarks>
/// Developed using Unity [Version 2021.3.23f1].
/// </remarks>
public class Maze : MonoBehaviour
{
    /// <summary>
    /// Number of Rows in the maze.
    /// </summary>
    private static readonly int ROWS = 4;

    /// <summary>
    /// Number of Columns in the maze.
    /// </summary>
    private static readonly int COLS = 4;

    /// <summary>
    /// Boolean value indicating whether or not the game has been lost.
    /// </summary>
    [SerializeField] private bool myLoseCondition;

    /// <summary>
    /// The player's current <c>Room</c> in the maze.
    /// </summary>
    private Room myCurrentRoom;

    /// <summary>
    /// The <c>Door</c> the player is currently in the proximity of.
    /// </summary>
    private Door myCurrentDoor;

    /// <summary>
    /// A 2D array representation of the <c>Room</c> scripts in the maze.
    /// </summary>
    [SerializeField] private Room[,] myRooms;



    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    void Start()
    {
        myRooms = new Room[ROWS, COLS];
        PopulateMaze();
        myCurrentRoom = myRooms[3, 0];
        GameObject.Find("Thinking Unicorn").GetComponent<Animator>().SetBool("isWon", false);
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update()
    {
        if (myLoseCondition)
        {
            UIControllerInGame.MyInstance.SetWinOrLoseWindow(myLoseCondition);
        }

        if (myCurrentRoom.MyWinRoom)
        {
            UIControllerInGame.MyInstance.SetWinOrLoseWindow(myLoseCondition);
            GameObject.Find("Thinking Unicorn").GetComponent<Animator>().SetBool("isWon", true);
        }
    }


    /// <summary>
    /// Private helper method to populate the <c>myRooms</c> field with the
    /// correct <c>Room</c> scripts.
    /// </summary>
    private void PopulateMaze()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                string roomName = $"Room {i + 1}-{j + 1}";
                myRooms[i, j] = GameObject.Find(roomName).GetComponent<Room>();
            }
        }
    }


    /// <summary>
    /// This method checks whether there is any possible path from the maze's win
    /// room to the player's current <c>Room</c>.
    /// </summary>
    /// <param name="theRow">The row number of the room currently being traversed.</param>
    /// <param name="theCol">The column number of the room currently being traversed.</param>
    /// <param name="theCheck">A 2D array of booleans indicating whether a room has
    /// already been traversed.</param>
    /// <returns>Boolean indicating whether or not the lose condition is satisfied.</returns>
    public bool CheckLoseCondition(in int theRow, in int theCol, in bool[,] theCheck)
    {
        const int NORTH = 0;
        const int EAST = 1;
        const int SOUTH = 2;
        const int WEST = 3;

        bool result = true;
        theCheck[theRow - 1, theCol - 1] = true;
        if (theRow == myCurrentRoom.MyRow && theCol == myCurrentRoom.MyCol)
        {
            result = false;
        }
        else
        {
            for (int i = 0; i < 4 && result; i++)
            {
                if (myRooms[theRow - 1, theCol - 1].MyDoors[i].name != "no-door")
                {
                    Door curr = myRooms[theRow - 1, theCol - 1].MyDoors[i].GetComponent<Door>();

                    if (!curr.MyLockState || !curr.MyHasAttempted)
                    {
                        switch (i)
                        {
                            case NORTH:
                                if (!theCheck[theRow - 2, theCol - 1] && result)
                                {
                                    result = CheckLoseCondition(theRow - 1, theCol, theCheck);
                                }

                                break;
                            case EAST:
                                if (!theCheck[theRow - 1, theCol] && result)
                                {
                                    result = CheckLoseCondition(theRow, theCol + 1, theCheck);
                                }

                                break;
                            case SOUTH:
                                if (!theCheck[theRow, theCol - 1] && result)
                                {
                                    result = CheckLoseCondition(theRow + 1, theCol, theCheck);
                                }

                                break;
                            case WEST:
                                if (!theCheck[theRow - 1, theCol - 2] && result)
                                {
                                    result = CheckLoseCondition(theRow, theCol - 1, theCheck);
                                }

                                break;
                        }
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Accessor and mutator for the <c>myCurrentRoom</c> field.
    /// </summary>
    public Room MyCurrentRoom
    {
        get => myCurrentRoom;
        set => myCurrentRoom = value;
    }

    /// <summary>
    /// Accessor for the starting room of the maze.
    /// </summary>
    /// <returns> The starting room of the maze. </returns>
    public Room GetDefaultRoom
    {
        get => myRooms[3, 0];
    }

    /// <summary>
    /// Accessor and mutator for the <c>myCurrentDoor</c> field.
    /// </summary>
    public Door MyCurrentDoor
    {
        get => myCurrentDoor;
        set => myCurrentDoor = value;
    }

    /// <summary>
    /// Accessor and mutator for the <c>myLoseCondition</c> field.
    /// </summary>
    public bool MyLoseCondition
    {
        get => myLoseCondition;
        set => myLoseCondition = value;
    }

    /// <summary>
    /// Accessor and mutator for the <c>myRooms</c> field.
    /// </summary>
    public Room[,] MyRooms
    {
        get => myRooms;
        set => myRooms = value;
    }

}