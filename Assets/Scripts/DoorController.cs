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
    private bool myHasAttempted;

    [SerializeField]
    private bool myHorizontalState;

    private bool myProximityTrigger;

    private float mySpeed;

    private float myRotationAmount;

    private Vector3 myStartingRotation;

    private GameObject myPlayer;

    private QuestionFactory myQuestionFactory;

    private Coroutine myAnimation;

    [SerializeField]
    private GameObject myNavPopup;


    // TEMP
    private static System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        myOpenState = false;
        myLockState = false;
        myHasAttempted = false;
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

    public bool SetProximityTrigger
    {
        get => myProximityTrigger;
        set => myProximityTrigger = value;
    }

    public bool MyLockState
    {
        get => myLockState;
        set => myLockState = value;
    }

    private void CheckForInput()
    {

        if (Input.GetKeyDown(KeyCode.E) && myProximityTrigger)
        {
            if (!myHasAttempted)
            {
                myHasAttempted = true;
                myQuestionFactory.DisplayWindow();
                //myLockState = !myQuestionFactory.MyQuestionWindowController.MyIsCorrect;


                // TEMP
                int temp = rand.Next(0, 2);
                if (temp == 0)
                {
                    myLockState = true;
                }
                else
                {
                    myLockState = false;
                }

            }

            if (myOpenState)
            {
                Close();
            }
            else if (!myLockState)
            {
                Open(myPlayer.transform.position);
            }
        }

    }

    private IEnumerator DoRotationOpen(Vector3 thePlayerPosition)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (myHorizontalState && (thePlayerPosition.z > transform.position.z)
            || !myHorizontalState && (thePlayerPosition.x > transform.position.x))
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
            myProximityTrigger = true;
            myNavPopup.GameObject().SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myProximityTrigger = false;
            myNavPopup.GameObject().SetActive(false);
        }
    }
}