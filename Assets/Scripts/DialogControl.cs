using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class DialogControl : MonoBehaviour // 각 종 이펙트 처리 및 싱글톤과 인게임 사이의 다리 역할 ㅇㅈㅇㅈㅇㅈㅇㅈ?
{
    public GameObject[] box;
    public GameObject[] enemyHitEffect; // atttack턴에서 공격성공했을때
    public GameObject[] enemyMissEffect; // attack턴에서 공격실패했을때
    public GameObject[] playerHitEffect; // avoid턴에서 못피했을때
    public GameObject[] playerMissEffect; // avoid턴에서 피했을때
    public Transform[] spawnPoint;

    public SpriteAnimator player;
    public SpriteAnimator enemy;
    public SpriteAnimator state;

    int _maxOnScreen;

    List<int> _alreadySpawnedIndex = new List<int>();

    void OnEnable()
    {
        setMaxOnScreen();
        spawnBox();
        playerIdle();
        changeStateAnimation();
    }

    void Update()
    {
        changeStateAnimation();

        if (GameManager.getInstance.playerMiss)
            avoidTurnMiss();
        else if (GameManager.getInstance.playerHit)
            avoidTurnHit();
        else if (GameManager.getInstance.enemyMiss)
            attackTurnMiss();
        else if (GameManager.getInstance.enemyHit)
            attackTurnHit();
    }

    public void attackTurnMiss()
    {
        GameManager.getInstance.enemyMiss = false;
        int randomIndex = Random.Range(0, enemyMissEffect.Length);
        if (!enemyMissEffect[randomIndex].activeSelf)
        {
            enemyMissEffect[randomIndex].SetActive(true);
        }
        else
        {
            attackTurnMiss();
        }
        hideAllBox();
        playerShot();
        changeState();
    }

    public void attackTurnHit()
    {
        GameManager.getInstance.enemyHit = false;
        int randomIndex = Random.Range(0, enemyHitEffect.Length);
        if (!enemyHitEffect[randomIndex].activeSelf)
        {
            enemyHitEffect[randomIndex].SetActive(true);
        }
        else
        {
            attackTurnHit();
        }
        hideAllBox();
        playerShot();
        changeState();
    }

    public void avoidTurnMiss()
    {
        GameManager.getInstance.playerMiss = false;
        int randomIndex = Random.Range(0, playerMissEffect.Length);
        if (!playerMissEffect[randomIndex].activeSelf)
        {
            playerMissEffect[randomIndex].SetActive(true);
        }
        else
        {
            avoidTurnMiss();
        }
        hideAllBox();
        changeState();
    }

    public void avoidTurnHit()
    {
        GameManager.getInstance.playerHit = false;
        int randomIndex = Random.Range(0, playerHitEffect.Length);
        if (!playerHitEffect[randomIndex].activeSelf)
        {
            playerHitEffect[randomIndex].SetActive(true);
        }
        else
        {
            avoidTurnHit();
        }
        hideAllBox();
        changeState();
    }

    void setMaxOnScreen()
    {
        switch (GameManager.getInstance.playerWeapon)
        {
            case PLAYER_WEAPON.PISOTL:
                _maxOnScreen = 1;
                break;

            case PLAYER_WEAPON.UZI:
                _maxOnScreen = 2;
                break;

            case PLAYER_WEAPON.SHOTGUN:
                _maxOnScreen = 3;
                break;

            case PLAYER_WEAPON.MINIGUN:
                _maxOnScreen = 4;
                break;
        }
    }

    void spawnBox()
    {
        for (int i = 0; i < _maxOnScreen; i++)
        {
            if (!box[i].activeSelf)
            {
                box[i].SetActive(true);
                box[i].transform.localPosition = setRandomSpawnPoint().position;
                break;
            }
        }
    }

    Transform setRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoint.Length);
        return spawnPoint[randomIndex];


        //for (int i = 0; i < spawnPoint.Length; i++)
        //{
        //    if (_alreadySpawnedIndex[i].Equals(randomIndex))
        //    {
        //        randomIndex = Random.Range(0, spawnPoint.Length);
        //    }
        //    else
        //    {
        //        _alreadySpawnedIndex.Add(randomIndex);
        //        return spawnPoint[randomIndex];
        //    }
        //}
        //randomIndex = Random.Range(0, spawnPoint.Length);
        //_alreadySpawnedIndex.Clear();
        //_alreadySpawnedIndex.Add(randomIndex);
        //return spawnPoint[randomIndex];
    }

    void hideAllBox()
    {
        for (int i = 0; i < box.Length; i++)
        {
            if (box[i].activeSelf)
                box[i].SendMessage("hideBox");
        }
    }

    void playerShot()
    {
        switch (GameManager.getInstance.playerWeapon)
        {
            case PLAYER_WEAPON.PISOTL:
                player.changeState("Pistol_Shot");
                break;

            case PLAYER_WEAPON.UZI:
                player.changeState("Uzi_Shot");
                break;

            case PLAYER_WEAPON.SHOTGUN:
                player.changeState("Shotgun_Shot");
                break;

            case PLAYER_WEAPON.MINIGUN:
                player.changeState("Minigun_Shot");
                break;
        }
    }

    void playerIdle()
    {
        switch (GameManager.getInstance.playerWeapon)
        {
            case PLAYER_WEAPON.PISOTL:
                player.changeState("Pistol_Idle");
                break;

            case PLAYER_WEAPON.UZI:
                player.changeState("Uzi_Idle");
                break;

            case PLAYER_WEAPON.SHOTGUN:
                player.changeState("Shotgun_Idle");
                break;

            case PLAYER_WEAPON.MINIGUN:
                player.changeState("Minigun_Idle");
                break;
        }
    }

    void changeState()
    {
        switch(GameManager.getInstance.dialogState)
        {
            case DIALOG.ATTACK:
                GameManager.getInstance.dialogState = DIALOG.AVOID;
                break;

            case DIALOG.AVOID:
                GameManager.getInstance.dialogState = DIALOG.SCRIPT;
                break;

            case DIALOG.SCRIPT:
                GameManager.getInstance.dialogState = DIALOG.MOVE;
                break;

            case DIALOG.S_ATTACK:
                GameManager.getInstance.dialogState = DIALOG.AVOID;
                break;
        }
        spawnBox();
    }

    void changeStateAnimation()
    {
        if (GameManager.getInstance.dialogState.Equals(DIALOG.ATTACK))
        {
            state.changeState("Attack");
        }
        else if (GameManager.getInstance.dialogState.Equals(DIALOG.AVOID))
        {
            state.changeState("Avoid");
        }
        //else if (GameManager.getInstance.dialogState.Equals(DIALOG.SCRIPT))
        //{
        //    state.changeState("Script");
        //}
    }
}
