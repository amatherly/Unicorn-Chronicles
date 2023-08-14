using System;
using System.Collections.Generic;
using Cinemachine;
using Common.Scripts.Maze;
using Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Common.Scripts.Controller
{
    public class SaveLoadManager : MonoBehaviour
    {
        private DoorController myDoorController;
        private global::Maze myMaze;
        private PlayerController myPlayerController;
        private Door myDoor;
        private ItemController myItemController;
        private CollectibleController myCollectibleController;
        [SerializeField] private GameObject mySun;
        public GameObject myNoSaveMenu;
        public GameObject myOptionsMenu;
        
        
        [SerializeField] private CinemachineVirtualCamera myVirtualCamera;


        private void Start()
        {
            myMaze = GameObject.Find("Maze").GetComponent<global::Maze>();
            myPlayerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
            myCollectibleController = FindObjectOfType<CollectibleController>();
        }
        

        public void SaveGame()
        {
            if (myMaze == null)
            {
                Debug.LogError("Maze object is not initialized.");
                return;
            }

            // Save player state in maze
            PlayerPrefs.SetInt("PlayerItemCount", myPlayerController.MyItemCount);
            PlayerPrefs.SetString("PlayerPosition",
                JsonUtility.ToJson(myPlayerController.transform.position));

            // Save door states in maze
            foreach (var currDoor  in myMaze.GetComponentsInChildren<DoorController>())
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
                foreach (var currDoor in myMaze.GetComponentsInChildren<DoorController>())
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



        public void NewGame()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Game 2");
            UIControllerInGame.MyInstance.GetComponent<AudioSource>().Stop();
            UIControllerInGame.MyInstance.ResumeGame();
            myPlayerController.MyCanMove = true;
        }
        
        
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
                //
                // if (key.myItemID == null)
                // {
                //     Debug.LogError("key.myItemID is null for a key!");
                //     continue;
                // }

                if (currItem.gameObject.activeSelf)
                {
                    allActiveItems.Add(Convert.ToInt32(currItem.myItemID.ToString()));
                }
            }
            // Save active keys
            PlayerPrefs.SetString("keysInScene", string.Join(",", allActiveItems));
            PlayerPrefs.Save();
        }
        
        
        private void LoadItemState()
        {
            string savedItem = PlayerPrefs.GetString("keysInScene", "");
            List<int> savedItemIDs = new List<int>(
                System.Array.ConvertAll(savedItem.Split(','), s => int.TryParse(s, out int result) ? result : -1)
            );

            // Set the keys to active or inactive based on the saved data
            foreach (var key in myCollectibleController.allMyItems)
            {
                key.gameObject.SetActive(savedItemIDs.Contains(key.myItemID));
            }
        }
        
    }
    
    
}

