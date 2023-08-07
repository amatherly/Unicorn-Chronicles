using System.Collections;
using System.Collections.Generic;
using Singleton;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{

    private const float ROTATE_SPEED = 1f;

    private const float ROTATION_AMOUNT = 90f;

    [SerializeField]
    private bool myOpenState;

    [SerializeField]
    private bool myLockState;

    [SerializeField]
    private bool myHasAttempted;

    [SerializeField]
    private bool myHorizontalState;

    private bool myProximityTrigger;

    private Vector3 myStartingRotation;

    private GameObject myPlayer;

    private Coroutine myAnimation;

    [SerializeField]
    private GameObject myNavPopup;

    // Start is called before the first frame update
    void Start()
    {
        myOpenState = false;
        myLockState = true;
        myHasAttempted = false;
        myProximityTrigger = false;
        myStartingRotation = transform.rotation.eulerAngles;
        myPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    public void Open()
    {
        if (!myOpenState)
        {
            if (myAnimation != null)
            {
                StopCoroutine(myAnimation);
            }

            myAnimation = StartCoroutine(DoRotationOpen());
        }
    }

    public void Close()
    {
        if (myOpenState)
        {
            if (myAnimation != null)
            {
                StopCoroutine(myAnimation);
            }

            myAnimation = StartCoroutine(DoRotationClose());
        }
    }

    public bool MyProximityTrigger
    {
        get => myProximityTrigger;
        set => myProximityTrigger = value;
    }
    
    public Vector3 MyStartingRotation
    {
        get => myStartingRotation; 
    }

        public bool MyOpenState
    {
        get => myOpenState;
        set => myOpenState = value;
    }

    public bool MyLockState
    {
        get => myLockState;
        set => myLockState = value;
    }

    public bool MyHasAttempted
    {
        get => myHasAttempted;
        set => myHasAttempted = value;
    }

    public GameObject MyNavPopup
    {
        get => myNavPopup;
    }

    private IEnumerator DoRotationOpen()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (myHorizontalState && (myPlayer.transform.position.z > transform.position.z)
            || !myHorizontalState && (myPlayer.transform.position.x > transform.position.x))
        {
            endRotation = Quaternion.Euler(new Vector3(0, myStartingRotation.y - ROTATION_AMOUNT, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, myStartingRotation.y + ROTATION_AMOUNT, 0));
        }

        myOpenState = true;
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * ROTATE_SPEED;
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(myStartingRotation);

        myOpenState = false;
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * ROTATE_SPEED;
        }
    }
}