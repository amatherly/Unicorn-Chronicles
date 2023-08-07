using System.Collections;

using UnityEngine;

public class ObjectFade : MonoBehaviour
{

    private Coroutine myAnimation;

    private Color myColor;

    private float mySpeed;

    // Start is called before the first frame update
    void Start()
    {
        mySpeed = 2.5f;
        myColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FadeOut()
    {
        while (myColor.a > 0.3)
        {
            float fadeAmount = myColor.a - (mySpeed * Time.deltaTime);
            myColor = new Color(myColor.r, myColor.g, myColor.b, fadeAmount);
            GetComponent<Renderer>().material.color = myColor;
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        while (myColor.a < 1)
        {
            float fadeAmount = myColor.a + (mySpeed * Time.deltaTime);
            myColor = new Color(myColor.r, myColor.g, myColor.b, fadeAmount);
            GetComponent<Renderer>().material.color = myColor;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (myAnimation != null)
            {
                StopCoroutine(myAnimation);
            }
            myAnimation = StartCoroutine(FadeOut());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (myAnimation != null)
            {
                StopCoroutine(myAnimation);
            }
            myAnimation = StartCoroutine(FadeIn());
        }
    }
}
