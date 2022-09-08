using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject Options;
    [SerializeField]
    GameObject Main;
    GameObject LastMenu;
    GameObject CurrentMenu;

    [SerializeField]
    GameObject Title;
    [SerializeField]
    Animator TransitionAnim;
    private void Awake()
    {
        CurrentMenu = Main;
        GetComponentInChildren<Button>().Select();
        Time.timeScale = 0;
    }
    public void GoPlay()
    {
        Time.timeScale = 1;
        CurrentMenu.SetActive(false);
        Title.SetActive(false);
        TransitionAnim.Play("Transition");
    }



    public void Quit()
    {
        Application.Quit();
    }

    public void GoOptions()
    {
        LastMenu = CurrentMenu;
        LastMenu.SetActive(false);
        CurrentMenu = Options;
        CurrentMenu.SetActive(true);
        CurrentMenu.GetComponentInChildren<Button>().Select();
    }

    public void Return()
    {
        GameObject tempObject;
        tempObject = CurrentMenu;
        LastMenu.SetActive(true);
        CurrentMenu = LastMenu;
        tempObject.SetActive(false);
        LastMenu = tempObject;
        CurrentMenu.GetComponentInChildren<Button>().Select();
    }
}
