using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float Gold = 1000f;
    private int life = 100, enemiesLeft, timer = 20;
    public static int enemiesCount = 10;
    public Text enemiesCountText, currentGoldText, lifeText, levelText;
    Spawner spawner;
    private string level;
    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().name;
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        spawner = spawnPoint.GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesLeft = enemiesCount - spawner.enemySpawned;
        enemiesCountText.text = enemiesLeft.ToString();
        currentGoldText.text = Gold.ToString();
        lifeText.text = life.ToString();
        levelText.text = level;
    }

    public void EnemyDefeated()
    {
        enemiesCount--;
    }

    public void GetGold(int gold)
    {
        Gold += gold;
    }

    public void BuyTurret(int gold)
    {
        Gold -= gold;
    }

    public void SellTurret(int gold)
    {
        Gold += gold;
    }
}
