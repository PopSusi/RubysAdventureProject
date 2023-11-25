using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject myCanvas;
    public GameObject pauseMenu;
    public GameObject lostMenu;
    public GameObject HealthBar;
    public bool levelWin = false;
    public static GameManager instance {get; private set;}
    public bool pauseOpen = false;
    public bool lostOpen = false;

    private ILevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        instance = this;
        levelManager = instance.GetComponent<ILevelManager>();
        lostMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Pause()
    {
        Debug.Log("1");
        pauseMenu.SetActive(true);
        pauseOpen = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void Continue()
    {
        Debug.Log("2");
        pauseMenu.SetActive(false);
        pauseOpen = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void Reset()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void Lost()
    {
        lostMenu.SetActive(true);
        lostOpen = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        HealthBar.SetActive(false);
    }

    public void UpdateObjective()
    {
        levelManager.UpdateObjective();
    }
}
