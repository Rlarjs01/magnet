using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{   
    public Button startButton;
    public Button optionButton;
    public Button ExitButton;

    private UnityAction action;

    void Start(){
        startButton.onClick.AddListener(delegate {OnStartClick();});
        ExitButton.onClick.AddListener(delegate {OnExitClick();});
    }

    public void OnStartClick(){
        SceneManager.LoadScene("Scean");
    }
    public void OnExitClick(){
        Application.Quit();
    }
}
