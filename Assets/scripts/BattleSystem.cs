using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum BattleStates {START, PLAYERTURN, ENEMYTURN, WIN, LOST}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] Image FightResult;
    [SerializeField] Sprite WinResult;
    [SerializeField] Sprite DefeatResult;
    [SerializeField] Transform PlayerPos;
    [SerializeField] Transform PnemyPos;
    [SerializeField] Slider PlayerHP;
    [SerializeField] Slider EnemyHP;
    [SerializeField] private Text Check;
    


    private Animator _playerAnimator;
    private Animator _enemyAnimator;
    
    public BattleStates State;
    static bool isPlayerTurn;

    private Unit playerUnit;
    private Unit enemyUnit;
    
    private Dictionary<int, Func<float>> playerAttack;
    private Dictionary<int, Func<float>> attacks;

    public void Start()
    {
        
        playerAttack = new Dictionary<int, Func<float>>();
        attacks = new Dictionary<int, Func<float>>();

        attacks.Add(10, () => FireBall());
        attacks.Add(2, () => PutBandage());
        
        playerAttack.Add(2, () =>
        {
            var attack = 0;
            for (int i = 0; i < 3; i++)
            {
                attack += Random.Range(1, 3);
            }
            return attack;
        });

        State = BattleStates.START;
        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        PlayerHP.value = playerUnit.CurrentHP;
        EnemyHP.value = enemyUnit.CurrentHP;
    }
    
    IEnumerator SetupBattle()
    {
        var playerGo = Instantiate(PlayerPrefab, PlayerPos);
        playerUnit = playerGo.GetComponent<Unit>();
        _playerAnimator = playerGo.GetComponent<Animator>();
        
        var enemyGO = Instantiate(EnemyPrefab, PnemyPos);
        enemyUnit = enemyGO.GetComponent<Unit>();
        _enemyAnimator = enemyGO.GetComponent<Animator>();
        
        PlayerHP.maxValue = playerUnit.MaxHP;
        EnemyHP.maxValue = enemyUnit.MaxHP;
        
        playerAttack.Add(1, () => {
            var damage = 0f;
            foreach (var kvp in DeckCreate.cards[0].Subsequence)
            {
                damage += attacks[kvp]();
            }
            return damage;
        });

        yield return new WaitForSeconds(2f);  // тест шняга, перед запуском боя, пройдёт 2 секунды
        
        State = BattleStates.PLAYERTURN;
        PlayerTurn();
    }



    void PlayerTurn()
    {
        Check.text = "Player! Your Turn!";
        // UI say chose your card
    }
    IEnumerator PlayerAttack(int i)
    {
        Check.text = $" {playerUnit.UnitName.ToUpper()} KILL HIM!";
        //text что игрок ходит
        var attack = playerAttack[i]();
        var isDead = enemyUnit.TakeDamage(attack);
        // updHud
        _enemyAnimator.SetBool("GetHit",true);
        Check.text = $" He crying {attack}";
        yield return new WaitForSeconds(1f);
        _enemyAnimator.SetBool("GetHit", false);
        
        if (isDead)
        {
            State = BattleStates.WIN;
            EndBattle();
        }
        else
        {
            State = BattleStates.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        //text что противник аткует
        Check.text = $"{enemyUnit.UnitName} is attacking you!";
        yield return new WaitForSeconds(1f);
        var attack = 5 + Random.Range(1, 20) + 20;
        Check.text = $"HE HURT YOU {attack}";
        
        if (playerUnit.Weakness > 0)
        {
            attack -= 5;
            playerUnit.Weakness -= 1;
        }
       
        var isDead = playerUnit.TakeDamage(attack);
        
        // updHud
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            State = BattleStates.LOST;
            EndBattle();
        }
        else
        {
            State = BattleStates.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        switch (State)
        {
            case BattleStates.WIN:
                FightResult.sprite = WinResult;
                FightResult.enabled = true;
                StartCoroutine(LoadSceneAfterDelay());
                break;
            case BattleStates.LOST:
                FightResult.sprite = DefeatResult;
                FightResult.enabled = true;
                StartCoroutine(LoadSceneAfterDelay());
                break;
        }
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Start Location");
    }
    
    public void OnPlayerButton(int i)
    {
        if (State != BattleStates.PLAYERTURN) return;
        State = BattleStates.ENEMYTURN;
        StartCoroutine(PlayerAttack(i));
    }
    
    private float PutBandage()
    {
        Check.text = "You HEALING OWOWOWOW!";
        playerUnit.Heal(0.2f * (playerUnit.MaxHP - playerUnit.CurrentHP));
        return 0;
    }

    private float FireBall()
    {
        return 10;
    }
}
