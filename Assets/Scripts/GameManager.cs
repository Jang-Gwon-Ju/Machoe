using UnityEngine;
using System.Collections;

public enum DIALOG
{
    ATTACK, // 총으로 공격
    S_ATTACK, // 칼로 공격
    AVOID,
    SCRIPT,
    MOVE,
}

public enum PLAYER_WEAPON
{
    PISOTL,
    UZI,
    SHOTGUN,
    MINIGUN,
    SWORD,
}

public enum DIRECTION
{
    RIGHT,
    LEFT,
}

public enum ENEMY_TYPE
{
    NMM1,
    NMM2,
    NMM3,
    NMM4,
    NMM5,
}

/*
    케릭터랑 몬스터는 정말 애니메이션과 이펙트로만 나타나고 실질적인 데이터 처리는 이곳에서 하자.
*/

public class GameManager {

    static GameManager _instance;

    int _enemyHelth;
    int _enemyDamage;
    int _playerHelth;
    int _playerDamage;

    bool _playerMissTrigger;
    bool _enemyMissTrigger;
    bool _playerHitTrigger;
    bool _enemyHitTrigger;

    PLAYER_WEAPON _weapon = PLAYER_WEAPON.PISOTL;
    ENEMY_TYPE _enemy = ENEMY_TYPE.NMM1;
    DIALOG _dialogState;

    static public GameManager getInstance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }

    public int enemyHelth { set { _enemyHelth = value; } get { return _enemyHelth; } }
    public int enemyDamage { set { _enemyDamage = value; } get { return _enemyDamage; } }
    public int playerHelth { set { _playerHelth = value; } get { return _playerHelth; } }
    public int playerDamage { set { _playerDamage = value; } get { return _playerDamage; } }

    public bool playerMiss { set { _playerMissTrigger = value; } get { return _playerMissTrigger; } }
    public bool playerHit { set { _playerHitTrigger = value; } get { return _playerHitTrigger; } }
    public bool enemyMiss { set { _enemyMissTrigger = value; } get { return _enemyMissTrigger; } }
    public bool enemyHit { set { _enemyHitTrigger = value; } get { return _enemyHitTrigger; } }

    public PLAYER_WEAPON playerWeapon { set { _weapon = value; } get { return _weapon; } }
    public DIALOG dialogState { set { _dialogState = value; } get { return _dialogState; } }
    public ENEMY_TYPE enemyType { set { _enemy = value; } get { return _enemy; } }

    public void attackEnemy()
    {
        _playerHelth -= _enemyDamage;
    }

    public void attackPlayer()
    {
        _enemyHelth -= _playerDamage;
    }
}
