using System.Collections;
using System.Collections.Generic;
using Singleton;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField]
    private bool myOpenState;

    [SerializeField]
    private bool myLockState;

    [SerializeField]
    private bool myHorizontalState;

    private bool myProximityTrigger;


    private float mySpeed;

    private float myRotationAmount;

    [SerializeField]
    private Vector3 myStartingRotation;

    private GameObject myPlayer;
    
    private QuestionFactory myQuestionFactory;

    private Coroutine myAnimation;

    [SerializeField]
    private GameObject myNavPopup;

    // Start is called before the first frame update
    void Start()
    {
        myOpenState = false;
        myLockState = false;
        myProximityTrigger = false;
        mySpeed = 1f;
        myRotationAmount = 90f;
        myStartingRotation = transform.rotation.eulerAngles;
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        myQuestionFactory = QuestionFactory.MyInstance;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
    }

    public void Open(Vector3 theUserPosition)
    {
        if (!myOpenState)
        {
            if (myAnimation != null)
            {
                StopCoroutine(myAnimation);
            }

            myAnimation = StartCoroutine(DoRotationOpen(theUserPosition));
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

    public bool SetProximityTrigger(bool theProximity)
    {
        myNavPopup.GameObject().SetActive(theProximity);
        return myProximityTrigger = theProximity;
    }

    public bool MyLockState
    {
        get => myLockState;
        set => myLockState = value;
    }

    private void CheckForInput()
    {

        if (Input.GetKeyDown(KeyCode.E) && myProximityTrigger && !myOpenState)
        {
            myQuestionFactory.DisplayWindow();

            if (myLockState)
            {
                myLockState = false;
                Open(myPlayer.transform.position);
            }
            else
            {
                if (!myOpenState)
                {
                    Open(myPlayer.transform.position);
                }
                else
                {
                    Close();
                }
            }
        }

    }

    private IEnumerator DoRotationOpen(Vector3 thePlayerPosition)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if ((myHorizontalState && (thePlayerPosition.z - transform.position.z) < 0)
            || (!myHorizontalState && (thePlayerPosition.x - transform.position.x) < 0))
        {
            endRotation = Quaternion.Euler(new Vector3(0, myStartingRotation.y - myRotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, myStartingRotation.y + myRotationAmount, 0));
        }

        myOpenState = true;
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * mySpeed;
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
            time += Time.deltaTime * mySpeed;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetProximityTrigger(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           SetProximityTrigger(false);
        }
    }
}