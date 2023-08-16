using System;
using System.Collections.Generic;
using Cinemachine;
using Common.Scripts.Maze;
using Common.Scripts.Question;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Common.Scripts.Controller
{
    /// <summary>
    /// Manages saving and loading game state for the Trivia Maze game.
    /// </summary>
    public class SaveLoadManager : MonoBehaviour
    {
        /// <summary>
        /// DoorController associated with this SaveLoadManager.
        /// </summary>
        private DoorController myDoorController;
        
        /// <summary>
        /// Global Maze object used in the game.
        /// </summary>
        private global::Common.Scripts.Maze.Maze MAZE;
        
        /// <summary>
        /// PlayerController managing the player's actions and state.
        /// </summary>
        private PlayerController myPlayerController;
        
        /// <summary>
        /// Door object used for door-related operations.
        /// </summary>
        private Door myDoor;
        
        /// <summary>
        /// ItemController managing individual collectable items in the game.
        /// </summary>
        private ItemController myItemController;
       
        /// <summary>
        /// CollectibleController responsible for handling collectibles.
        /// </summary>
        private CollectibleController myCollectibleController;

        /// <summary>
        /// GameObject representing the sun object.
        /// </summary>
        [SerializeField] private GameObject mySun;

        /// <summary>
        /// GameObject representing the "No Save" menu.
        /// </summary>
        public GameObject myNoSaveMenu;
        
        /// <summary>
        /// The GameObject representing the options menu.
        /// </summary>
        public GameObject myOptionsMenu;
        
    
        [SerializeField] private CinemachineVirtualCamera myVirtualCamera;

        
    
        /// <summary>
        /// Initializes references and components during the start of the game.
        /// </summary>
        private void Start()
        {
            MAZE = GameObject.Find("Maze").GetComponent<global::Common.Scripts.Maze.Maze>();
            myPlayerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
            myCollectibleController = FindObjectOfType<CollectibleController>();
        }
        
        /// <summary>
        /// Saves the current game state.
        /// </summary>
        public void SaveGame()
        {
            if (MAZE == null)
            {
                Debug.LogError("Maze object is not initialized.");
                return;
            }

            // Save player state in maze
            PlayerPrefs.SetInt("PlayerItemCount", myPlayerController.MyItemCount);
            PlayerPrefs.SetString("PlayerPosition",
                JsonUtility.ToJson(myPlayerController.transform.position));

            // Save door states in maze
            foreach (var currDoor  in MAZE.GetComponentsInChildren<DoorController>())
            {
                SaveDoorState(currDoor);
            }
            
            // Save the virtual camera's FOV
            PlayerPrefs.SetFloat("VirtualCameraFOV", myVirtualCamera.m_Lens.FieldOfView);

            // Save the sun toggle state
            PlayerPrefs.SetInt("SunToggle", mySun.activeSelf ? 1 : 0);
            
            // Save the player's speed
            PlayerPrefs.SetFloat("PlayerSpeed", myPlayerController.MySpeed);

            SaveItemState();
            SaveMinimap();

            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Loads a previously saved game state.
        /// </summary>
        public void LoadGame()
        {
            if (PlayerPrefs.HasKey("PlayerPosition"))
            {
                myNoSaveMenu.SetActive(false);
                // Load Player state in maze
                myPlayerController.transform.position =
                    JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerPosition"));
                myPlayerController.MyItemCount =
                    PlayerPrefs.GetInt("PlayerItemCount", myPlayerController.MyItemCount);

                // Load door states in maze
                foreach (var currDoor in MAZE.GetComponentsInChildren<DoorController>())
                {
                    LoadDoorState(currDoor);
                }

                // Load the virtual camera's FOV
                if (PlayerPrefs.HasKey("VirtualCameraFOV"))
                {
                    myVirtualCamera.m_Lens.FieldOfView = PlayerPrefs.GetFloat("VirtualCameraFOV");
                }

                // Load the sun toggle state
                if (PlayerPrefs.HasKey("SunToggle"))
                {
                    mySun.SetActive(PlayerPrefs.GetInt("SunToggle") == 1);
                }
                
                // Load the player's speed
                if (PlayerPrefs.HasKey("PlayerSpeed"))
                {
                    myPlayerController.MySpeed = PlayerPrefs.GetFloat("PlayerSpeed");
                }
                
                LoadItemState();
                LoadMinimap();
                

                // Load question states in maze
                QuestionFactory.MyInstance.InitializeQuestionsFromSave();
            }
            else
            {
                myOptionsMenu.SetActive(true);
                myNoSaveMenu.SetActive(true);
            }
        }

        /// <summary>
        /// Starts a new game by deleting all saved data.
        /// </summary>
        public void NewGame()
        {
            PlayerPrefs.DeleteAll();
            UIControllerInGame.MyInstance.GetComponent<AudioSource>().Stop();
            UIControllerInGame.MyInstance.PauseGame();
            SceneManager.LoadScene("Game 2");
            myPlayerController.MyCanMove = true;
        }


        /// <summary>
        /// Saves the state of a door within the maze.
        /// </summary>
        /// <param name="theDoor">The DoorController of the door to save.</param>
        private void SaveDoorState(DoorController theDoor)
        {
            myDoor = theDoor.GetComponent<Door>();

            string currDoorID = myDoor.myDoorID;

            PlayerPrefs.SetInt(currDoorID + "_LockState", myDoor.MyLockState ? 1 : 0);
            PlayerPrefs.SetInt(currDoorID + "_HasAttempted", myDoor.MyHasAttempted ? 1 : 0);

            Transform myDoorTransform = theDoor.transform;

            // Save position
            var currDoorPos = myDoorTransform.position;
            PlayerPrefs.SetFloat(currDoorID + "_PosX", currDoorPos.x);
            PlayerPrefs.SetFloat(currDoorID + "_PosY", currDoorPos.y);
            PlayerPrefs.SetFloat(currDoorID + "_PosZ", currDoorPos.z);

            // Save rotation
            var currDoorRot = myDoorTransform.eulerAngles;
            PlayerPrefs.SetFloat(currDoorID + "_RotX", currDoorRot.x);
            PlayerPrefs.SetFloat(currDoorID + "_RotY", currDoorRot.y);
            PlayerPrefs.SetFloat(currDoorID + "_RotZ", currDoorRot.z);

            // Save scale
            var currDoorScale = myDoorTransform.localScale;
            PlayerPrefs.SetFloat(currDoorID + "_ScaleX", currDoorScale.x);
            PlayerPrefs.SetFloat(currDoorID + "_ScaleY", currDoorScale.y);
            PlayerPrefs.SetFloat(currDoorID + "_ScaleZ", currDoorScale.z);
        }
        
        /// <summary>
        /// Loads the state of a door within the maze.
        /// </summary>
        /// <param name="theDoor">The DoorController of the door to load state for.</param>
        private void LoadDoorState(DoorController theDoor)
        {
            myDoor = theDoor.GetComponent<Door>();

            string currDoorID = myDoor.myDoorID;
            
            if (PlayerPrefs.HasKey(currDoorID + "_LockState"))
            {
                myDoor.MyLockState = PlayerPrefs.GetInt(currDoorID + "_LockState") == 1;
                myDoor.MyHasAttempted = PlayerPrefs.GetInt(currDoorID + "_HasAttempted") == 1;

                // Load position
                Vector3 position;
                position.x = PlayerPrefs.GetFloat(currDoorID + "_PosX");
                position.y = PlayerPrefs.GetFloat(currDoorID + "_PosY");
                position.z = PlayerPrefs.GetFloat(currDoorID + "_PosZ");
                myDoor.transform.position = position;

                // Load rotation
                Vector3 eulerAngles;
                eulerAngles.x = PlayerPrefs.GetFloat(currDoorID + "_RotX");
                eulerAngles.y = PlayerPrefs.GetFloat(currDoorID + "_RotY");
                eulerAngles.z = PlayerPrefs.GetFloat(currDoorID + "_RotZ");
                theDoor.transform.rotation = Quaternion.Euler(eulerAngles);

                // Load scale
                Vector3 scale;
                scale.x = PlayerPrefs.GetFloat(currDoorID + "_ScaleX");
                scale.y = PlayerPrefs.GetFloat(currDoorID + "_ScaleY");
                scale.z = PlayerPrefs.GetFloat(currDoorID + "_ScaleZ");
                theDoor.transform.localScale = scale;
            }
            else
            {
                Debug.Log("No saved state found for door with ID: " + currDoorID);
            }
        }
        
        /// <summary>
        /// Saves the state of the minimap, including visited rooms and their doors.
        /// </summary>
        private void SaveMinimap()
        {
            Room[] allRooms = FindObjectsOfType<Room>();
            List<string> visitedRooms = new List<string>();
            foreach (Room currRoom in allRooms)
            {
                if (currRoom.MyHasVisited)
                {
                    visitedRooms.Add($"{currRoom.MyRow},{currRoom.MyCol}");
                }
            }
            PlayerPrefs.SetString("VisitedRooms", string.Join(";", visitedRooms));
            SaveMinimapDoors();
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Loads the state of the minimap, restoring visited rooms and their doors.
        /// </summary>
        private void LoadMinimap()
        {
            if (PlayerPrefs.HasKey("VisitedRooms"))
            {
                string savedRoom = PlayerPrefs.GetString("VisitedRooms");
                List<Vector2Int> visitedRooms = new List<Vector2Int>();

                foreach (string currRoomPos in savedRoom.Split(';'))
                {
                    string[] currPos = currRoomPos.Split(',');
                    visitedRooms.Add(new Vector2Int(int.Parse(currPos[0]), int.Parse(currPos[1])));
                }
                Room[] allRooms = FindObjectsOfType<Room>();
                foreach (Room currRoom in allRooms)
                {
                    currRoom.MyHasVisited = visitedRooms.Contains(new Vector2Int(currRoom.MyRow, currRoom.MyCol));
                }
            }
            LoadMinimapDoors();
        }
        
        /// <summary>
        /// Saves the state of doors within the minimap.
        /// </summary>
        private void SaveMinimapDoors()
        {
            Door[] allDoors = FindObjectsOfType<Door>();
            foreach (Door currDoor in allDoors)
            {
                string doorKeyBase =
                    $"Door_{currDoor.transform.position}"; // Unique ID for doors 
                PlayerPrefs.SetInt($"{doorKeyBase}_HasAttempted", currDoor.MyHasAttempted ? 1 : 0);
                PlayerPrefs.SetInt($"{doorKeyBase}_LockState", currDoor.MyLockState ? 1 : 0);
            }
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Loads the state of doors within the minimap.
        /// </summary>
        private void LoadMinimapDoors()
        {
            Door[] allDoors = FindObjectsOfType<Door>();
            foreach (Door currDoor in allDoors)
            {
                string doorKeyBase = $"Door_{currDoor.transform.position}"; // Unique ID for doors 
                if (PlayerPrefs.HasKey($"{doorKeyBase}_HasAttempted"))
                {
                    currDoor.MyHasAttempted = PlayerPrefs.GetInt($"{doorKeyBase}_HasAttempted") == 1;
                    currDoor.MyLockState = PlayerPrefs.GetInt($"{doorKeyBase}_LockState") == 1;
                }
            }
        }

        /// <summary>
        /// Saves the state of collectible items in the scene.
        /// </summary>
        private void SaveItemState()
        {
            if(myCollectibleController == null)
            {
                Debug.LogError("myCollectibleController is null");
                return;
            }
            
            // Collect active key IDs
            List<int> allActiveItems = new List<int>();
            foreach (var currItem  in myCollectibleController.allMyItems)
            {
                if(currItem == null)
                {
                    // Debug.LogError("A key in myAllItems list is null");
                    continue;
                }
                if (currItem.gameObject.activeSelf)
                {
                    allActiveItems.Add(Convert.ToInt32(currItem.myItemID.ToString()));
                }
            }
            // Save active keys
            PlayerPrefs.SetString("keysInScene", string.Join(",", allActiveItems));
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Loads the state of collectible items in the scene.
        /// </summary>
        private void LoadItemState()
        {
            string savedItem = PlayerPrefs.GetString("keysInScene", "");
            List<int> savedItemIDs = new List<int>(
                Array.ConvertAll(savedItem.Split(','), s => int.TryParse(s, out int result) ? result : -1)
            );

            // Set the keys to active or inactive based on the saved data
            foreach (var key in myCollectibleController.allMyItems)
            {
                if(key != null)
                key.gameObject.SetActive(savedItemIDs.Contains(key.myItemID));
            }
        }
        
    }
    
    
}

