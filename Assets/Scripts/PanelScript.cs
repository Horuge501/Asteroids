using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    [SerializeField] private GameObject playerScore;
    [SerializeField] private GameObject playerLives;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        GetComponent<Animator>().SetTrigger("Close");

    }

    public void SetTimePlay()
    {
        Time.timeScale = 1;
        playerScore.SetActive(true);
        playerLives.SetActive(true);
    }
}
