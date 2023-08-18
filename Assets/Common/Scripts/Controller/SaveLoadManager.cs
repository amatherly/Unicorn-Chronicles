/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using System;
using System.Collections.Generic;
using Cinemachine;
using Common.Scripts.Maze;
using UnityEngine;


/// <summary>
/// Manages saving and loading game state for the Trivia Maze game.
/// </summary>
/// <author>JJ Coldiron</author>
/// <author>Caroline El Jazmi</author>
/// <author>Brodi Matherly</author>
/// <remarks>
/// Developed using Unity [Version 2021.3.23f1].
/// </remarks>
public class SaveLoadManager : MonoBehaviour
{
    /// <summary>
    /// GameObject representing the sun object.
    /// </summary>
    [SerializeField] private GameObject mySun;

    /// <summary>
    /// GameObject representing the "No Save" menu.
    /// </summary>
    [SerializeField] public GameObject myNoSaveMenu;

    /// <summary>
    /// The GameObject representing the options menu.
    /// </summary>
    [SerializeField] public GameObject myOptionsMenu;

    /// <summary>
    /// A reference to the Cinemachine Virtual Camera used in the scene.
    /// This allows for programmatic control and manipulation of the camera's properties and behaviors.
    /// </summary>
    [SerializeField] private CinemachineVirtualCamera myVirtualCamera;

    /// <summary>
    /// DoorController associated with this SaveLoadManager.
    /// </summary>
    private DoorController myDoorController;

    /// <summary>
    /// Global Maze object used in the game.
    /// </summary>
    private Maze MAZE;

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

    // /// <summary>
    // /// Initializes references and components during the start of the game.
    // /// </summary>
    private void Start()
    {
        MAZE = GameObject.Find("Maze").GetComponent<Maze>();
        myPlayerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        myCollectibleController = FindObjectOfType<CollectibleController>();
    }

    /// <summary>
    /// Saves the current game state.
    /// </summary>
    public void SaveGame()
    {
        // Save player state in maze
        PlayerPrefs.SetString("PlayerPosition",
            JsonUtility.ToJson(myPlayerController.transform.position));
        PlayerPrefs.SetInt("PlayerItemCount", myPlayerController.MyItemCount);

        // Save door states in maze
        foreach (DoorController currDoor in MAZE.GetComponentsInChildren<DoorController>())
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
            // set no save menu object activity state to off
            myNoSaveMenu.SetActive(false);

            // Load Player state in maze
            myPlayerController.transform.position =
                JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerPosition"));
            myPlayerController.MyItemCount =
                PlayerPrefs.GetInt("PlayerItemCount", myPlayerController.MyItemCount);

            // Load door states in maze
            foreach (DoorController currDoor in MAZE.GetComponentsInChildren<DoorController>())
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
        // SceneManager.LoadScene("Game 2");
    }

    /// <summary>
    /// Saves the state of a door within the maze.
    /// </summary>
    /// <param name="theDoor">The DoorController of the door to save.</param>
    private void SaveDoorState(in DoorController theDoor)
    {
        Door door = theDoor.GetComponent<Door>();

        string currDoorID = door.MyDoorID;

        PlayerPrefs.SetInt(currDoorID + "_LockState", door.MyLockState ? 1 : 0);
        PlayerPrefs.SetInt(currDoorID + "_HasAttempted", door.MyHasAttempted ? 1 : 0);
        SaveTransformState(currDoorID, theDoor.transform);

    }

    /// <summary>
    /// Loads the state of a door within the maze.
    /// </summary>
    /// <param name="theDoor">The DoorController of the door to load state for.</param>
    private void LoadDoorState(in DoorController theDoor)
    {
        Door door = theDoor.GetComponent<Door>();
        string currDoorID = door.MyDoorID;

        if (PlayerPrefs.HasKey(currDoorID + "_LockState"))
        {
            door.MyLockState = PlayerPrefs.GetInt(currDoorID + "_LockState") == 1;
            door.MyHasAttempted = PlayerPrefs.GetInt(currDoorID + "_HasAttempted") == 1;

            LoadTransformState(currDoorID, theDoor.transform);
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
            if (currRoom == null) continue;

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
        HashSet<Vector2Int> visitedRooms = new HashSet<Vector2Int>();

        if (PlayerPrefs.HasKey("VisitedRooms"))
        {
            string savedRoomData = PlayerPrefs.GetString("VisitedRooms");

            foreach (string currRoomPos in savedRoomData.Split(';'))
            {
                Vector2Int? parsedPos = ParseVector2Int(currRoomPos);

                if (parsedPos.HasValue)
                {
                    visitedRooms.Add(parsedPos.Value);
                }
            }
        }

        Room[] allRooms = FindObjectsOfType<Room>();
        foreach (Room currRoom in allRooms)
        {
            if (currRoom == null) continue;

            currRoom.MyHasVisited = visitedRooms.Contains(new Vector2Int(currRoom.MyRow, currRoom.MyCol));
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
            string theDoorKey =
                $"Door_{currDoor.transform.position}"; // Unique ID for doors 
            PlayerPrefs.SetInt($"{theDoorKey}_HasAttempted", currDoor.MyHasAttempted ? 1 : 0);
            PlayerPrefs.SetInt($"{theDoorKey}_LockState", currDoor.MyLockState ? 1 : 0);
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
        // Collect active key IDs
        List<int> allActiveItems = new List<int>();
        foreach (ItemController currItem in myCollectibleController.allMyItems)
        {
            if (currItem == null)
            {
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
        foreach (ItemController key in myCollectibleController.allMyItems)
        {
            if (key != null)
                key.gameObject.SetActive(savedItemIDs.Contains(key.myItemID));
        }
    }

    /// <summary>
    /// Saves a Vector3 value to PlayerPrefs.
    /// </summary>
    /// <param name="key">The key used for saving the vector's components.</param>
    /// <param name="value">The Vector3 value to be saved.</param>
    private static void SaveVector3(string key, Vector3 value)
    {
        PlayerPrefs.SetFloat(key + "_x", value.x);
        PlayerPrefs.SetFloat(key + "_y", value.y);
        PlayerPrefs.SetFloat(key + "_z", value.z);
    }

    /// <summary>
    /// Retrieves a Vector3 value from PlayerPrefs.
    /// </summary>
    /// <param name="key">The key used for fetching the vector's components from PlayerPrefs.</param>
    /// <returns>A Vector3 containing the values from PlayerPrefs.</returns>
    private static Vector3 GetVector3(string key)
    {
        Vector3 result;
        result.x = PlayerPrefs.GetFloat(key + "_x");
        result.y = PlayerPrefs.GetFloat(key + "_y");
        result.z = PlayerPrefs.GetFloat(key + "_z");
        return result;
    }

    /// <summary>
    /// Saves the position, rotation, and scale of a Transform to PlayerPrefs.
    /// </summary>
    /// <param name="id">A unique identifier for the Transform being saved. It acts as a prefix to distinguish different Transform data.</param>
    /// <param name="transform">The Transform whose state you want to save.</param>
    private static void SaveTransformState(string id, Transform transform)
    {
        SaveVector3(id + "_Pos", transform.position);
        SaveVector3(id + "_Rot", transform.eulerAngles);
        SaveVector3(id + "_Scale", transform.localScale);
    }

    /// <summary>
    /// Loads the position, rotation, and scale of a Transform from PlayerPrefs.
    /// </summary>
    /// <param name="id">The unique identifier for the Transform data you want to load.</param>
    /// <param name="transform">The Transform where the loaded data will be applied.</param>
    private static void LoadTransformState(string id, Transform transform)
    {
        transform.position = GetVector3(id + "_Pos");
        transform.rotation = Quaternion.Euler(GetVector3(id + "_Rot"));
        transform.localScale = GetVector3(id + "_Scale");
    }

    /// <summary>
    /// Converts a comma-separated string to a Vector2Int value.
    /// </summary>
    /// <param name="data">A string containing two comma-separated integer values.</param>
    /// <returns>A Vector2Int containing the parsed values, or null if the parsing fails.</returns>
    private static Vector2Int? ParseVector2Int(string data)
    {
        string[] parts = data.Split(',');

        if (parts.Length != 2) return null;

        if (int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
        {
            return new Vector2Int(x, y);
        }

        return null;
    }

}


