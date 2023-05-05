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
    [SerializeField] Image FightResult;
    [SerializeField] Sprite WinResult;
    [SerializeField] Sprite DefeatResult;

    static bool isPlayerTurn;

    private void Start()
    {
        FightResult.enabled = false;
        StartBattle();
    }
    public static void StartBattle()
    {
        Player.HP = Player.MaxHP;
        Player.Mana = Player.MaxMana;
        Enemy.HP = Enemy.MaxHP;

        isPlayerTurn = true;
    }

    private void Update()
    {
        PlayerHP.text = Player.HP.ToString();
        EnemyHP.text = Enemy.HP.ToString();

        if (!isPlayerTurn)
        {
            EnemyAttack();
        }

        if (Player.HP < 0)
            Player.HP = 0;
        if (Enemy.HP < 0)
            Enemy.HP = 0;

        if (Player.HP <= 0)
        {
            FightResult.sprite = DefeatResult;
            FightResult.enabled = true;
            Invoke("LoadScene", 3f);
        }
        else if(Enemy.HP <= 0)
        {
            FightResult.sprite = WinResult;
            FightResult.enabled = true;
            Invoke("LoadScene", 3f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    void EnemyAttack()
    {
        var attack = 5 + Random.Range(1f, 20f);

        if (Enemy.Weakness > 0)
        {
            attack -= 5;
            Enemy.Weakness -= 1;
        }
        if (attack > Player.Defence)
            Player.HP -= attack;
        isPlayerTurn = true;
    }

    public void BaseballHit()
    {
        isPlayerTurn = false;
        int powerOfAttack = 5;
        float coef = 1.4f;
        var attack = Random.Range(0, 100)<30 ? powerOfAttack * coef : powerOfAttack;

        if (attack > Enemy.Defence)
            Enemy.HP -= attack;
    }

    public void Choke()
    {
        isPlayerTurn = false;
        for (int i = 0; i < 3; i++)
        {
            var attack = Random.Range(1, 3);

            if (attack > Enemy.Defence)
                Enemy.HP -= attack;
        }
    }

    public void Slap()
    {
        isPlayerTurn = false;
        if (Player.HP<50)
        {
            Player.Mana = Player.Mana >= 50? 100 : Player.Mana+50;
            Player.HP -= 1;
        }

        else if(Player.HP>=50)
        {
            Enemy.HP -= 1;
            Enemy.Weakness += 2;
        }
    }

    public void PutBandage()
    {
        isPlayerTurn = false;
        Player.HP += 0.2f * (Player.MaxHP - Player.HP);
    }
}
public class Player
{
    public const float MaxHP = 100;
    public const float MaxMana = 100;
    public static float HP { get; set; }
    public static float Mana { get; set; }
    public static float Defence { get; set; }
    public static float Skill { get; set; }
}

public class Enemy
{
    public const float MaxHP = 100;
    public static float HP { get; set; }
    public static float Defence { get; set; }
    public static int Weakness { get; set; }
}

