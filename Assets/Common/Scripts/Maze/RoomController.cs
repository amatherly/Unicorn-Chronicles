using UnityEngine;

namespace Common.Scripts.Maze
{
    /// <summary>
    /// Controller class to handle player interactions with each room's <c>GameObject</c>.
    /// </summary>
    public class RoomController : MonoBehaviour
    {

        /// <summary>
        /// The <c>Room</c> script of the <c>GameObject</c> shared by the <c>RoomController</c>.
        /// </summary>
        private Room myRoom;

        /// <summary>
        /// Reference to the game's <c>Maze</c> script.
        /// </summary>
        private Maze myMaze;

        /// <summary>
        /// Called before the first frame update.
        /// </summary>
        void Start()
        {
            myRoom = GetComponent<Room>();
            myMaze = GameObject.Find("Maze").GetComponent<Maze>();
        }

        /// <summary>
        /// Called when the player enters the room's collider and sets <c>Room</c> and
        /// <c>Maze</c> state accordingly.
        /// </summary>
        /// <param name="theOther"></param>
        private void OnTriggerEnter(Collider theOther)
        {
            if (theOther.CompareTag("Player"))
            {
                myMaze.MyCurrentRoom = myRoom;
                myRoom.MyHasVisited = true;
            }
        }
    }
}
