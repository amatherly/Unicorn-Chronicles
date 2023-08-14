using System;
using System.Net.Http.Headers;
using Cinemachine;
using Common.Scripts.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private Slider myFOVSlider;
    [SerializeField] private Slider mySpeedSlider;
    [SerializeField] private Slider myKeySlider;
    [SerializeField] private Toggle mySunToggle;
    [SerializeField] private GameObject mySun;
    [SerializeField] private PlayerController myPlayer;
    [SerializeField] private CinemachineVirtualCamera myCamera;
    [SerializeField] private TMP_Text myKeyCount;

    void Start()
    {
        myFOVSlider.minValue = 2f;
        myFOVSlider.maxValue = 20f;
        myFOVSlider.value = myCamera.m_Lens.FieldOfView;
        myFOVSlider.onValueChanged.AddListener(UpdateFOV);

        mySpeedSlider.minValue = 50f;
        mySpeedSlider.maxValue = 100f;
        mySpeedSlider.value = myPlayer.MySpeed;
        mySpeedSlider.onValueChanged.AddListener(UpdateSpeed);
        
        myKeySlider.minValue = 0;
        myKeySlider.maxValue = 50;
        myKeySlider.value = myPlayer.MyItemCount;
        myKeySlider.onValueChanged.AddListener(UpdateKeyCount);
        myKeyCount.SetText(myPlayer.MyItemCount.ToString());

        mySunToggle.isOn = mySun.activeSelf;
    }


    private void UpdateFOV(float theValue)
    {
        myCamera.m_Lens.FieldOfView = theValue;
    }

    void UpdateSpeed(float theValue)
    {
        myPlayer.MySpeed = theValue;
    }

    void UpdateKeyCount(float theValue)
    {
        myPlayer.MyItemCount = (int)theValue;
        myKeyCount.SetText( myPlayer.MyItemCount.ToString());
    }
}