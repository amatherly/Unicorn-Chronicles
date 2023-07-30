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

    private Maze myMaze;

    private QuestionFactory myQuestionFactory;

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
        mySpeed = 1f;
        myRotationAmount = 90f;
        myStartingRotation = transform.rotation.eulerAngles;
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        myMaze = GameObject.FindGameObjectWithTag("Maze").GetComponent<Maze>();
        myQuestionFactory = QuestionFactory.MyInstance;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
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

    public bool MyHasAttempted
    {
        get => myHasAttempted;
    }

    private void CheckForInput()
    {

        if (Input.GetKeyDown(KeyCode.E) && myProximityTrigger)
        {
            if (!myHasAttempted)
            {
                myHasAttempted = true;
                myQuestionFactory.DisplayWindow();
            }

            if (myOpenState)
            {
                Close();
            }
            else if (!myLockState)
            {
                Open();
            }
        }

    }

    private IEnumerator DoRotationOpen()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (myHorizontalState && (myPlayer.transform.position.z > transform.position.z)
            || !myHorizontalState && (myPlayer.transform.position.x > transform.position.x))
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
            myMaze.MyCurrentDoor = this;
            myNavPopup.GameObject().SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myProximityTrigger = false;
            myMaze.MyCurrentDoor = null;
            myNavPopup.GameObject().SetActive(false);
        }
    }
}