
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{
    public void RedirectToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
