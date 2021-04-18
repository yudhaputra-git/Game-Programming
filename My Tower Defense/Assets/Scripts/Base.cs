using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Base : MonoBehaviour
{
    private int price, swordTurretPrice = 100, scytheTurretPrice = 150, hammerTurretPrice = 200;
    private string clickedObject, spawnedTurret, activeTurret;
    public Transform turretSpawnPoint;
    public GameObject turretList;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        GameObject myCanvas = GameObject.Find("Canvas");
        gameController = myCanvas.GetComponent<GameController>();
        if (transform.Find("TurretList") != null)
        {
            transform.Find("TurretList").gameObject.SetActive(false);
        }
        else
        {
            return;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        clickedObject = gameObject.name;
        if (transform.Find("TurretList") != null)
        {
            if (!transform.Find("TurretList").gameObject.activeSelf)
            {
                transform.Find("TurretList").gameObject.SetActive(true);
            }
            else if (transform.Find("TurretList").gameObject.activeSelf)
            {
                transform.Find("TurretList").gameObject.SetActive(false);
            }
        }
        if (clickedObject.Equals("TurretSpawnPoint"))
        {
            return;
        }
        else
        {
            if (clickedObject.Equals("Hammer"))
            {
                price = hammerTurretPrice;
                spawnedTurret = "HammerTurret";
                turretList.SetActive(false);
            }
            else if (clickedObject.Equals("Sword"))
            {
                price = swordTurretPrice;
                spawnedTurret = "KnightTurret";
                turretList.SetActive(false);
            }
            else if (clickedObject.Equals("Scythe"))
            {
                price = scytheTurretPrice;
                spawnedTurret = "ScytheTurret";
                turretList.SetActive(false);
            }
            foreach (Transform child in turretSpawnPoint)
            {
                child.gameObject.SetActive(false);
            }
            Debug.Log(activeTurret);
            Debug.Log(spawnedTurret);
            if (spawnedTurret != null)
            {
                if (activeTurret != null && activeTurret.Equals(spawnedTurret))
                {
                    turretSpawnPoint.Find(spawnedTurret).gameObject.SetActive(true);
                    activeTurret = spawnedTurret;
                    Debug.Log(activeTurret);
                    Debug.Log(spawnedTurret);
                }
                else
                {
                    gameController.BuyTurret(price);
                    turretSpawnPoint.Find(spawnedTurret).gameObject.SetActive(true);
                    activeTurret = spawnedTurret;
                    Debug.Log(activeTurret);
                    Debug.Log(spawnedTurret);
                }
            }
        }
    }
}
