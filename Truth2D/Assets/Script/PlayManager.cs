using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    static public PlayManager instance;

    public PlayerController pc;
    public BossMove boss;

    public GameObject HUDCanvas;

    public GameObject stageObject0;
    public GameObject stageObject1;
    public GameObject stageObject2;

    public Deer m_Deer;
    public Deer m_Lion;

    public int stage = 0;

    private void Awake()
    {
        if(instance == null)
            instance = this;

    }

    private void Start()
    {
        stage = 1;

        if (stage == 0)
        {
            stageObject0.SetActive(true);
        }
        if (stage == 1)
        {
            stageObject1.SetActive(true);
        }
        if (stage == 2)
        {
            stageObject2.SetActive(true);
        }

        if (stage != 2)
        {
            HUDCanvas.SetActive(false);
            boss.gameObject.SetActive(false);
        }
    }

    public void ReGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    int countRabbit = 0;
    public void LionGetRabbit()
    {
        countRabbit += 1;

        if(countRabbit == 3)
        {
            StartCoroutine(ClearStage2());
        }
    }

    IEnumerator ClearStage2()
    {
        yield return new WaitForSeconds(1);
        stage = 2;
        ReGame();
    }

    public void ClearBoss()
    {
        stage  = 3;
    }
}
