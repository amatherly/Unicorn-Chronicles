using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.Maze
{
    /// <summary>
    /// Script to handle color change of the doors on the
    /// minimap.
    /// </summary>
    public class MinimapDoor : MonoBehaviour
    {

        /// <summary>
        /// The <c>Door</c> the minimap cell corresponds to.
        /// </summary>
        [SerializeField]
        GameObject myDoor;

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            ColorChange();
        }

        /// <summary>
        /// Changes the color of the cell depending on the state
        /// of its corresponding <c>Door</c>. Yellow if the door has
        /// not been attempted, green if the question was answered correctly,
        /// and red if the question was answered incorrectly.
        /// </summary>
        private void ColorChange()
        {
            Color newColor = Color.yellow;
            Door door = myDoor.GetComponent<Door>();

            if (door.MyHasAttempted && !door.MyLockState)
            {
                newColor = Color.green;
            }

            if (door.MyHasAttempted && door.MyLockState)
            {
                newColor = Color.red;
            }

            GetComponent<Image>().color = newColor;
        }
    }
}
