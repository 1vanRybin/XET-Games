using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] Text PlayerHP;
    [SerializeField] Text EnemyHP;
    [SerializeField] Text FightResult;

    int playerHP;
    int enemyHP;
    bool isPlayerTurn;

    private void Start()
    {
        FightResult.enabled = false;
        isPlayerTurn = true;
        playerHP = 100;
        enemyHP = 100;
    }
    private void Update()
    {
        PlayerHP.text = playerHP.ToString();
        EnemyHP.text = enemyHP.ToString();

        if (!isPlayerTurn)
        {
            playerHP -= Random.Range(1, 10) + 5;
            isPlayerTurn = true;
        }

        if(playerHP<=0)
        {
            FightResult.text = "YOU DIED";
            FightResult.color = Color.red;
            FightResult.enabled = true;
            Invoke("LoadScene", 3f);
        }
        else if(enemyHP<=0)
        {
            FightResult.text = "YOU WIN";
            FightResult.color = Color.green;
            FightResult.enabled = true;
            Invoke("LoadScene", 3f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void Attack1()
    {
        enemyHP -= 5;
        isPlayerTurn = false;
    }

    public void Attack2()
    {
        enemyHP -= 10;
        isPlayerTurn = false;
    }

    public void Attack3()
    {
        enemyHP -= 20;
        isPlayerTurn = false;
    }

    public void Skip()
    {
        isPlayerTurn = false;
    }
}
