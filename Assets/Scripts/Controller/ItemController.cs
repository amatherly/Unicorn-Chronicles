using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class ItemController : MonoBehaviour
{
    private PlayerController myPlayer;

    private GameObject myItem;

    private float mySpeed;

    private float myHeight;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        mySpeed = 1.5f;
        myHeight = 0.0025f;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * mySpeed) * myHeight + transform.position.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myPlayer.MyItemCount += 1;
            Destroy(gameObject);
        }
    }

}
