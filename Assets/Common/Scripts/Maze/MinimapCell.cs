/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.Maze
{
    /// <summary>
    /// Script to handle proper color change of the rooms
    /// on the minimap.
    /// </summary>
    /// <author>JJ Coldiron</author>
    /// <author>Caroline El Jazmi</author>
    /// <author>Brodi Matherly</author>
    /// <remarks>
    /// Developed using Unity [Version 2021.3.23f1].
    /// </remarks>
    public class MinimapCell : MonoBehaviour
    {

        /// <summary>
        /// Transparent white color.
        /// </summary>
        private static readonly Color DEFAULT = new(1, 1, 1, .6f);

        /// <summary>
        /// Transparent light green color for visited rooms.
        /// </summary>
        private static readonly Color LIGHT_GREEN = new(.398f, .887f, .414f, .6f);

        /// <summary>
        /// Transparent light blue color for current room.
        /// </summary>
        private static readonly Color LIGHT_BLUE = new(.398f, .769f, .808f, .6f);

        /// <summary>
        /// The <c>Room</c> the minimap cell corresponds to.
        /// </summary>
        [SerializeField]
        private GameObject myRoom;

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
            Color newColor = DEFAULT;

            if (myRoom.GetComponent<Room>().MyHasVisited)
            {
                newColor = LIGHT_GREEN;
            }

            if (myRoom.GetComponent<Room>().Equals(GameObject.Find("Maze").GetComponent<Maze>().MyCurrentRoom))
            {
                newColor = LIGHT_BLUE;
            }

            GetComponent<Image>().color = newColor;
        }
    }
}
