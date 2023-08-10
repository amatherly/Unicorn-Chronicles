using System.Collections;
using UnityEngine;

/// <summary>
/// Class <c>Door</c> contains state and handles open/close animations.
/// </summary>
public class Door : MonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    private static readonly float ROTATE_SPEED = 1f;

    /// <summary>
    /// 
    /// </summary>
    private static readonly float ROTATION_AMOUNT = 90f;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private bool myOpenState;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private bool myLockState;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private bool myHasAttempted;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private bool myHorizontalState;

    /// <summary>
    /// 
    /// </summary>
    private bool myProximityTrigger;

    /// <summary>
    /// 
    /// </summary>
    private Vector3 myStartingRotation;

    /// <summary>
    /// 
    /// </summary>
    private GameObject myPlayer;

    /// <summary>
    /// 
    /// </summary>
    private Coroutine myAnimation;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject myNavPopup;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        myOpenState = false;
        myLockState = true;
        myHasAttempted = false;
        myProximityTrigger = false;
        myStartingRotation = transform.rotation.eulerAngles;
        myPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public bool MyProximityTrigger
    {
        get => myProximityTrigger;
        set => myProximityTrigger = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public Vector3 MyStartingRotation
    {
        get => myStartingRotation; 
    }

    /// <summary>
    /// 
    /// </summary>
    public bool MyOpenState
    {
        get => myOpenState;
        set => myOpenState = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool MyLockState
    {
        get => myLockState;
        set => myLockState = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public Coroutine MyAnimation
    {
        get => myAnimation;
        set => myAnimation = value;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public bool MyHasAttempted
    {
        get => myHasAttempted;
        set => myHasAttempted = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public GameObject MyPlayer
    {
        get => myPlayer;
        set => myPlayer = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public GameObject MyNavPopup
    {
        get => myNavPopup;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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