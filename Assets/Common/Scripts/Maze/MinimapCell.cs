using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to handle proper color change of the rooms
/// on the minimap.
/// </summary>
public class MinimapCell : MonoBehaviour
{

    /// <summary>
    /// The <c>Room</c> the minimap cell corresponds to.
    /// </summary>
    [SerializeField]
    private GameObject myRoom;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        ColorChange();
    }

    /// <summary>
    /// Changes the color of the cell depending on the state of its
    /// corresponding <c>Room</c>. White for unvisited rooms, green if
    /// a room has been visited, and blue if the player is currently in
    /// the room.
    /// </summary>
    internal void ColorChange()
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
