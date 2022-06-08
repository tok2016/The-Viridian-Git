using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damageToGive;
    public int moneyToSteal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        else if (collision.CompareTag("Player"))
        {
            if (LevelManager.lvlManager.currentCoinsAmount > 0)
                LevelManager.lvlManager.SpendCoins(moneyToSteal);
            else
                PlayerHealth.player.DamagePlayer(1);
        }
    }
}
