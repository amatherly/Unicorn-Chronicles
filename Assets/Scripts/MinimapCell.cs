using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapCell : MonoBehaviour
{

    [SerializeField]
    private GameObject myRoom;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ColorChange();
    }

    private void ColorChange()
    {
        bool locked = true;
        DoorController door;

        for (int i = 0; i < 4; i++)
        {
            if (myRoom.GetComponent<Room>().MyDoors[i].name != "no-door")
            {
                door = myRoom.GetComponent<Room>().MyDoors[i].GetComponent<DoorController>();
                if (!door.MyLockState || !door.MyHasAttempted)
                {
                    locked = false;
                }
            }
        }

        Color newColor = Color.white;

        if (locked)
        {
            newColor = Color.red;
        }

        if (myRoom.GetComponent<Room>().MyHasVisited)
        {
            newColor = Color.green;
        }

        if (myRoom.GetComponent<Room>().Equals(GameObject.Find("Maze").GetComponent<Maze>().MyCurrentRoom))
        {
            newColor = Color.blue;
        }

        GetComponent<Image>().color = newColor;
    }
}
