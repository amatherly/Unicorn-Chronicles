using Common.Scripts.Controller;
using Singleton;
using UnityEngine;

/// <summary>
/// Simple class to increment the <c>myItemCount</c> field of the
/// <c>Player</c> script and handle animation.
/// </summary>
public class ItemController : MonoBehaviour
{

    /// <summary>
    /// Speed of the item's bobbing animation.
    /// </summary>
    public static readonly float SPEED = 1.5f;

    /// <summary>
    /// Height variance of the item's bobbing animation.
    /// </summary>
    public static readonly float HEIGHT = 0.0025f;

    /// <summary>
    /// Reference to the <c>Player</c> script.
    /// </summary>
    private PlayerController myPlayer;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        float newY = Mathf.Sin(Time.time * SPEED) * HEIGHT + transform.position.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    /// <summary>
    /// Called when the player enters the item's collider to increment
    /// the player's <c>myItemCount</c> and destroy the <c>GameObject</c>.
    /// </summary>
    /// <param name="other"></param>
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
