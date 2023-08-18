/*
 * Unicorn Chronicles: Dark Forest Trivia
 * Summer 2023
 */

using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class to handle options functions and behaviors.
/// </summary>
/// <author>JJ Coldiron</author>
/// <author>Caroline El Jazmi</author>
/// <author>Brodi Matherly</author>
/// <remarks>
/// Developed using Unity [Version 2021.3.23f1].
/// </remarks>
public class OptionsController : MonoBehaviour
{
    /// <summary>
    /// Minimum value for the FOV slider.
    /// </summary>
    private static readonly float FOV_MIN_VALUE = 40f;

    /// <summary>
    /// Maximum value for the FOV slider.
    /// </summary>
    private static readonly float FOV_MAX_VALUE = 80f;

    /// <summary>
    /// Minimum value for the speed slider.
    /// </summary>
    private static readonly float SPEED_MIN_VALUE = 50f;

    /// <summary>
    /// Maximum value for the speed slider.
    /// </summary>
    private static readonly float SPEED_MAX_VALUE = 100f;

    /// <summary>
    /// Minimum value for the key slider.
    /// </summary>
    private static readonly float KEY_MIN_VALUE = 0;

    /// <summary>
    /// Maximum value for the key slider.
    /// </summary>
    private static readonly float KEY_MAX_VALUE = 50;

    /// <summary>
    /// Slider to control the field of view.
    /// </summary>
    [SerializeField] private Slider myFOVSlider;

    /// <summary>
    /// Slider to control the speed of the player.
    /// </summary>
    [SerializeField] private Slider mySpeedSlider;

    /// <summary>
    /// Slider to control the count of keys.
    /// </summary>
    [SerializeField] private Slider myKeySlider;

    /// <summary>
    /// Toggle to control the sun's state (on/off).
    /// </summary>
    [SerializeField] private Toggle mySunToggle;

    /// <summary>
    /// Reference to the Sun GameObject.
    /// </summary>
    [SerializeField] private GameObject mySun;

    /// <summary>
    /// Reference to the PlayerController to control player properties.
    /// </summary>
    [SerializeField] private PlayerController myPlayer;

    /// <summary>
    /// Reference to the Cinemachine virtual camera to control the camera properties.
    /// </summary>
    [SerializeField] private CinemachineVirtualCamera myCamera;

    /// <summary>
    /// Text component to display the key count.
    /// </summary>
    [SerializeField] private TMP_Text myKeyCount;

    /// <summary>
    /// Start is called before the first frame update. Initializes sliders and other UI elements.
    /// </summary>
    void Start()
    {
        myFOVSlider.minValue = FOV_MIN_VALUE;
        myFOVSlider.maxValue = FOV_MAX_VALUE;
        myFOVSlider.value = myCamera.m_Lens.FieldOfView;
        myFOVSlider.onValueChanged.AddListener(UpdateFOV);

        mySpeedSlider.minValue = SPEED_MIN_VALUE;
        mySpeedSlider.maxValue = SPEED_MAX_VALUE;
        mySpeedSlider.value = myPlayer.MySpeed;
        mySpeedSlider.onValueChanged.AddListener(UpdateSpeed);

        myKeySlider.minValue = KEY_MIN_VALUE;
        myKeySlider.maxValue = KEY_MAX_VALUE;
        myKeySlider.value = myPlayer.MyItemCount;
        myKeySlider.onValueChanged.AddListener(UpdateKeyCount);
        myKeyCount.SetText(myPlayer.MyItemCount.ToString());

        mySunToggle.isOn = mySun.activeSelf;
    }


    /// <summary>
    /// Updates the field of view based on the given value.
    /// </summary>
    /// <param name="theValue">The new value for the field of view.</param>
    private void UpdateFOV(float theValue)
    {
        myCamera.m_Lens.FieldOfView = Mathf.Clamp(theValue, FOV_MIN_VALUE, FOV_MAX_VALUE);
    }

    /// <summary>
    /// Updates the player's speed based on the given value.
    /// </summary>
    /// <param name="theValue">The new value for the player's speed.</param>
    private void UpdateSpeed(float theValue)
    {
        myPlayer.MySpeed = theValue;
    }

    /// <summary>
    /// Updates the player's key count based on the given value.
    /// </summary>
    /// <param name="theValue">The new value for the player's key count.</param>
    private void UpdateKeyCount(float theValue)
    {
        myPlayer.MyItemCount = (int)theValue;
        myKeyCount.SetText(myPlayer.MyItemCount.ToString());
    }

}