using System.Collections;
using UnityEngine;

namespace Common.Scripts.Maze
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DefaultMarker : MonoBehaviour
    {
        public float pulseSpeed = 1.0f; // How fast the glow pulses.
        public float minEmission = 0.2f; // Minimum emission value.
        public float maxEmission = 1.0f; // Maximum emission value.

        private Material material; // The material of the sprite renderer.
        private Color initialEmissionColor; // The initial emission color.

        void Start()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            material = renderer.material; // Assuming the material is not shared with other objects.
            initialEmissionColor = material.GetColor("_EmissionColor");
        }

        void Update()
        {
            float emissionFactor = Mathf.PingPong(Time.time * pulseSpeed, maxEmission - minEmission) + minEmission;
            material.SetColor("_EmissionColor", initialEmissionColor * emissionFactor);
        }
    }
}


