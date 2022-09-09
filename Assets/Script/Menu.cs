using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject Options;
    [SerializeField]
    GameObject Main;

    [SerializeField]
    GameObject Title;
    [SerializeField]
    Animator TransitionAnim;
    private void Awake()
    {
        //GetComponentInChildren<Button>().Select();
        Time.timeScale = 0;
    }
    public void GoPlay()
    {
        Time.timeScale = 1;
        Title.SetActive(false);
        TransitionAnim.Play("Transition");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void GoMainScreen()
    {
        Options.SetActive(false);
        Main.SetActive(true);
        Main.GetComponentInChildren<Button>().Select();
    }
}
