using Common.Scripts.Controller;
using Singleton;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static readonly float SPEED = 1.5f;

    public static readonly float HEIGHT = 0.0025f;

    private PlayerController myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * SPEED) * HEIGHT + transform.position.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myPlayer.MyItemCount += 1;
            FindObjectOfType<UIControllerInGame>().PlayUISound(4);
            Destroy(gameObject);
        }
    }
}
