using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class Mission1Objective : MonoBehaviour, ILevelManager
{
    [SerializeField] private GameObject objvUI;
    public int enemyFixed = 0;
    public int enemyTotal;
    private GameObject[] enemies;
    private TextMeshProUGUI objvText;

    public GameObject youWinUI;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyTotal = enemies.Length;
        enemies = GameObject.FindGameObjectsWithTag("Cat");
        enemyTotal += enemies.Length;
        objvText = objvUI.GetComponent<TextMeshProUGUI>();
        objvText.text = enemyFixed.ToString() + " out of " + enemyTotal.ToString();
        youWinUI.SetActive(false);
    }
    public void UpdateObjective()
    {
        enemyFixed++;
        objvText.text = enemyFixed.ToString() + " out of " + enemyTotal.ToString();
        if(enemyFixed == enemyTotal) FinishedObjective();
    }
    
    public void FinishedObjective()
    {
        youWinUI.SetActive(true);
        GameManager.instance.Win();
    }
}
