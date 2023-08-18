/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using System.Collections;
using UnityEngine;

namespace Common.Scripts.Maze
{
    /// <summary>
    /// Fades out the <c>GameObject</c> the script is attached to
    /// so that it will not obscure the player's view.
    /// </summary>
    /// <author>JJ Coldiron</author>
    /// <author>Caroline El Jazmi</author>
    /// <author>Brodi Matherly</author>
    /// <remarks>
    /// Developed using Unity [Version 2021.3.23f1].
    /// </remarks>
    public class ObjectFade : MonoBehaviour
    {

        /// <summary>
        /// Speed of the fade in/out animation.
        /// </summary>
        private static readonly float SPEED = 2.5f;

        /// <summary>
        /// Minimum opacity for the <c>GameObject</c> being faded.
        /// </summary>
        private static readonly float MIN_OPACITY = 0.3f;

        /// <summary>
        /// Maximum opacity for the <c>GameObject</c> being faded.
        /// </summary>
        private static readonly float MAX_OPACITY = 1f;

        /// <summary>
        /// The animation currently underway.
        /// </summary>
        private Coroutine myAnimation;

        /// <summary>
        /// The color of the attached <c>GameObject</c>.
        /// </summary>
        private Color myColor;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            myColor = GetComponent<Renderer>().material.color;
        }

        /// <summary>
        /// Reduces the opacity of the <c>GameObject</c> so the player
        /// remains visible.
        /// </summary>
        /// <returns>
        /// Yield return null so that the animation can be resumed on the
        /// next frame update.
        /// </returns>
        private IEnumerator FadeOut()
        {
            while (myColor.a > MIN_OPACITY)
            {
                float fadeAmount = myColor.a - (SPEED * Time.deltaTime);
                myColor = new Color(myColor.r, myColor.g, myColor.b, fadeAmount);
                GetComponent<Renderer>().material.color = myColor;
                yield return null;
            }
        }

        /// <summary>
        /// Reduces the opacity of the <c>GameObject</c> so the player
        /// remains visible.
        /// </summary>
        /// <returns>
        /// Yield return null so that the animation can be resumed on the
        /// next frame update.
        /// </returns>
        private IEnumerator FadeIn()
        {
            while (myColor.a < MAX_OPACITY)
            {
                float fadeAmount = myColor.a + (SPEED * Time.deltaTime);
                myColor = new Color(myColor.r, myColor.g, myColor.b, fadeAmount);
                GetComponent<Renderer>().material.color = myColor;
                yield return null;
            }
        }

        /// <summary>
        /// Called when the player enters the collider of the attached
        /// <c>GameObject</c>.
        /// </summary>
        /// <param name="theOther">The entity entering the collider.</param>
        private void OnTriggerEnter(Collider theOther)
        {
            if (theOther.CompareTag("Player"))
            {
                if (myAnimation != null)
                {
                    StopCoroutine(myAnimation);
                }
                myAnimation = StartCoroutine(FadeOut());
            }
        }

        /// <summary>
        /// Called when the player enters the collider of the attached
        /// <c>GameObject</c>.
        /// </summary>
        /// <param name="theOther">The entity exiting the collider.</param>
        private void OnTriggerExit(Collider theOther)
        {
            if (theOther.CompareTag("Player"))
            {
                if (myAnimation != null)
                {
                    StopCoroutine(myAnimation);
                }
                myAnimation = StartCoroutine(FadeIn());
            }
        }
    }
}
