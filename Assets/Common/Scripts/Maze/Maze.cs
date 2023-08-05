using UnityEngine;

namespace Singleton
{
    
    public class Maze : MonoBehaviour
    {
        public static Maze myInstance = null;
        
        private bool myLoseCondition;
        private Room myCurrentRoom;
        private DoorController myCurrentDoor;

        [SerializeField] private Room[,] myRooms;



        // Start is called before the first frame update
        void Start()
        {
            myRooms = new Room[4, 4];
            PopulateMaze();
            myCurrentRoom = myRooms[3, 0];
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private void Awake()
        {
            if (MyInstance != null && MyInstance != this)
            {
                Debug.Log("There is already an instance of the MAZE in the scene");
            }
            else
            {
                MyInstance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (myLoseCondition)
            {
                Debug.Log("wow you're bad at this");
            }
        }

        public void SetDoorLockState(bool theLockState)
        {
            MyCurrentDoor.MyLockState = !theLockState;
            MyLoseCondition = CheckLoseCondition(1, 4, new bool[4, 4]);
            if (theLockState)
            {
                MyCurrentDoor.Open();
            }
        }

        public Room MyCurrentRoom
        {
            get => myCurrentRoom;
            set => myCurrentRoom = value;
        }

        public Room[,] MyRooms
        {
            get => myRooms;

            set => myRooms = value;
        }

        public DoorController MyCurrentDoor
        {
            get => myCurrentDoor;
            set => myCurrentDoor = value;
        }

        public bool MyLoseCondition
        {
            get => myLoseCondition;
            set => myLoseCondition = value;
        }

        // stupid code but whatever
        public void PopulateMaze()
        {
            myRooms[0, 0] = GameObject.Find("Room 1-1").GetComponent<Room>();
            myRooms[0, 1] = GameObject.Find("Room 1-2").GetComponent<Room>();
            myRooms[0, 2] = GameObject.Find("Room 1-3").GetComponent<Room>();
            myRooms[0, 3] = GameObject.Find("Room 1-4").GetComponent<Room>();
            myRooms[1, 0] = GameObject.Find("Room 2-1").GetComponent<Room>();
            myRooms[1, 1] = GameObject.Find("Room 2-2").GetComponent<Room>();
            myRooms[1, 2] = GameObject.Find("Room 2-3").GetComponent<Room>();
            myRooms[1, 3] = GameObject.Find("Room 2-4").GetComponent<Room>();
            myRooms[2, 0] = GameObject.Find("Room 3-1").GetComponent<Room>();
            myRooms[2, 1] = GameObject.Find("Room 3-2").GetComponent<Room>();
            myRooms[2, 2] = GameObject.Find("Room 3-3").GetComponent<Room>();
            myRooms[2, 3] = GameObject.Find("Room 3-4").GetComponent<Room>();
            myRooms[3, 0] = GameObject.Find("Room 4-1").GetComponent<Room>();
            myRooms[3, 1] = GameObject.Find("Room 4-2").GetComponent<Room>();
            myRooms[3, 2] = GameObject.Find("Room 4-3").GetComponent<Room>();
            myRooms[3, 3] = GameObject.Find("Room 4-4").GetComponent<Room>();
        }

        public bool CheckLoseCondition(int theRow, int theCol, bool[,] theCheck)
        {
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
                        DoorController curr = myRooms[theRow - 1, theCol - 1].MyDoors[i].GetComponent<DoorController>();

                        if (!curr.MyLockState || !curr.MyHasAttempted)
                        {
                            switch (i)
                            {
                                // NORTH
                                case 0:
                                    if (!theCheck[theRow - 2, theCol - 1] && result)
                                    {
                                        result = CheckLoseCondition(theRow - 1, theCol, theCheck);
                                    }

                                    break;
                                // EAST
                                case 1:
                                    if (!theCheck[theRow - 1, theCol] && result)
                                    {
                                        result = CheckLoseCondition(theRow, theCol + 1, theCheck);
                                    }

                                    break;
                                // SOUTH
                                case 2:
                                    if (!theCheck[theRow, theCol - 1] && result)
                                    {
                                        result = CheckLoseCondition(theRow + 1, theCol, theCheck);
                                    }

                                    break;
                                // WEST
                                case 3:
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

        public static Maze MyInstance
        {
            get => myInstance;
            set => myInstance = value;
        }
    }
}
