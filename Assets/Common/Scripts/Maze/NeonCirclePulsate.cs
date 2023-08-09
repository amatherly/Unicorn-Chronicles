using System.Collections;
using UnityEngine;

namespace Common.Scripts.Maze
{
    public class WaveRippleEffect : MonoBehaviour
    {
        public int numberOfRipples = 3;
        public float rippleSpeed = 1.0f;
        public float maxRippleSize = 3.0f;
        public float rippleSpawnRate = 1.0f; // Time between ripples

        private float rippleTimer = 0f;

        void Update()
        {
            rippleTimer += Time.deltaTime;

            if (rippleTimer >= rippleSpawnRate)
            {
                CreateRipple();
                rippleTimer = 0f;
            }
        }

        void CreateRipple()
        {
            for (int i = 0; i < numberOfRipples; i++)
            {
                GameObject ripple = new GameObject("Ripple");
                ripple.transform.SetParent(transform);
                ripple.transform.localPosition = Vector3.zero;
                ripple.transform.localScale = Vector3.one;

                SpriteRenderer rippleRenderer = ripple.AddComponent<SpriteRenderer>();
                rippleRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
                rippleRenderer.color = new Color(1, 1, 1, 1.0f / (i + 1)); // Lower alpha for outer ripples

                StartCoroutine(RippleEffect(ripple, i));
            }
        }

        IEnumerator RippleEffect(GameObject ripple, int index)
        {
            float size = 1.0f + index * 0.5f;
            while (size < maxRippleSize)
            {
                ripple.transform.localScale = Vector3.one * size;
                size += Time.deltaTime * rippleSpeed;

                // Adjust alpha based on size
                float alpha = 1.0f - (size - 1.0f) / (maxRippleSize - 1.0f);
                ripple.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);

                yield return null;
            }

            Destroy(ripple);
        }
    }
}