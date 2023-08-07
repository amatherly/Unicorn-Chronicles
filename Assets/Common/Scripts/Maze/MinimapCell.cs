using System.Collections;
using System.Collections.Generic;
using Singleton;
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
        Color newColor = Color.white;

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
