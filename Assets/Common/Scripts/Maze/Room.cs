using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Maze
{
    /// <summary>
    /// Class <c>Room</c> stores room state.
    /// </summary>
    public class Room : MonoBehaviour
    {

        /// <summary>
        /// Boolean indicating whether or not the <c>Room</c> has been visited by the player.
        /// </summary>
        [SerializeField]
        private bool myHasVisited;

        /// <summary>
        /// Boolean indicating whether or not the <c>Room</c> is the maze's designated win room.
        /// </summary>
        [SerializeField]
        private bool myWinRoom;

        /// <summary>
        /// The row number of the <c>Room</c>.
        /// </summary>
        [SerializeField]
        private int myRow;

        /// <summary>
        /// The column number of the <c>Room</c>.
        /// </summary>
        [SerializeField]
        private int myCol;

        /// <summary>
        /// A <c>List</c> of the <c>Door</c> scripts contained in the room. Index 0 corresponds
        /// to the north <c>Door</c>. Index 1 corresponds to the <c>East</c> door. Index 2
        /// corresponds to the South <c>Door</c>. Index 3 corresponds to the West <c>Door</c>.
        /// Directions that do not have a <c>Door</c> are populated by an empty <c>GameObject</c>
        /// named "no-door".
        /// </summary>
        [SerializeField]
        private List<GameObject> myDoors;


        /// <summary>
        /// Called before the first frame update.
        /// </summary>
        void Start()
        {
            myHasVisited = false;
        }
    
        /// <summary>
        /// Compares the row and column number of two <c>Room</c> scripts to see if they
        /// are the same <c>Room</c>.
        /// </summary>
        /// <param name="theOther">The <c>Room</c> being compared against.</param>
        /// <returns>
        /// Boolean indicating whether or not the <c>Room</c> scripts are the same.
        /// </returns>
        public bool Equals(Room theOther)
        {
            return theOther.MyRow == myRow && theOther.MyCol == myCol;
        }

        /// <summary>
        /// Accessor and mutator for the <c>myRow</c> field.
        /// </summary>
        public int MyRow
        {
            get => myRow;
            set => myRow = value;
        }

        /// <summary>
        /// Accessor and mutator for the <c>myCol</c> field.
        /// </summary>
        public int MyCol
        {
            get => myCol;
            set => myCol = value;
        }

        /// <summary>
        /// Accessor and mutator for the <c>myHasVisited</c> field.
        /// </summary>
        public bool MyHasVisited
        {
            get => myHasVisited;
            set => myHasVisited = value;
        }

        /// <summary>
        /// Accessor for the <c>myWinRoom</c> field.
        /// </summary>
        public bool MyWinRoom
        {
            get => myWinRoom;
        }

        /// <summary>
        /// Accessor and mutator for the <c>myDoors</c> field.
        /// </summary>
        public List<GameObject> MyDoors
        {
            get => myDoors;
            set => myDoors = value;
        }
    
    }
}
