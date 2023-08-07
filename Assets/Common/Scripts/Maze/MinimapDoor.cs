using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapDoor : MonoBehaviour
{

    [SerializeField]
    GameObject myDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColorChange();
    }

    private void ColorChange()
    {
        Color newColor = Color.yellow;
        Door door = myDoor.GetComponent<Door>();

        if (door.MyHasAttempted && !door.MyLockState)
        {
            newColor = Color.green;
        }

        if (door.MyHasAttempted && door.MyLockState)
        {
            newColor = Color.red;
        }

        GetComponent<Image>().color = newColor;
    }
}
