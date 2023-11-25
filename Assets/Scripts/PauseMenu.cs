using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.PlayerLoop;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

   // Update is called once per frame
    void Update()
    {
    }

    public void Pause()
    {
        Debug.Log("1");
        pauseMenu.SetActive(true);
        open = true;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Debug.Log("2");
        pauseMenu.SetActive(false);
        open = false;
        Time.timeScale = 1;
    }
}
