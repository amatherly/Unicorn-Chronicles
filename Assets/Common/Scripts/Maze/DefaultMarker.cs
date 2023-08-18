using UnityEngine;
namespace Common.Scripts.Maze
{
    /// <summary>
    /// Represents a glowing neon green circle marker under the game
    /// character's default position that pulsates in intensity.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class DefaultMarker : MonoBehaviour
    {
        /// <summary>
        /// The shader property ID for the emission color.
        /// </summary>
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        
        /// <summary>
        /// How fast the glow pulsates.
        /// </summary>
        private static readonly float PULSE_SPEED = 1.0f;
        
        /// <summary>
        /// Minimum emission value for the glow.
        /// </summary>
        private static readonly float MIN_EMISS = 0.2f;
        
        /// <summary>
        /// Maximum emission value for the glow.
        /// </summary>
        private static readonly float MAX_EMISS = 1.0f; 

        /// <summary>
        /// The material associated with the SpriteRenderer for controlling emission.
        /// </summary>
        private Material myMaterial;
        
        /// <summary>
        /// The initial emission color used for pulsating effect.
        /// </summary>
        private Color myInitColor;
        
        
        /// <summary>
        /// Initializes the marker's material and initial emission color.
        /// </summary>
        void Start()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            myMaterial = renderer.material; 
            myInitColor = myMaterial.GetColor("_EmissionColor");
        }

        /// <summary>
        /// Updates the emission color to create a pulsating glow effect.
        /// </summary>
        void Update()
        {
            float emissionFactor = Mathf.PingPong(Time.time * PULSE_SPEED, MAX_EMISS - MIN_EMISS) + MIN_EMISS;
            myMaterial.SetColor(EmissionColor, myInitColor * emissionFactor);
        }
    }
}


