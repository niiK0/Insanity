using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDoor : MonoBehaviour
{

    [SerializeField] GameObject Player;
    
    public bool enemyAlive;
    //Distancia que o jogador se move ao tocar numa porta
    static float MoveDistance = 300000f;

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Parte da função que verifica se o inimigo ainda esta vivio de modo a poder seguir com o jogo
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy != null)
        {
            // Enemy is found, set the EnemyAlive variable to true
            enemyAlive = true;
        }
        else
        {
            // Enemy is not found, set the EnemyAlive variable to false
            enemyAlive = false;
        }

        //Posiçao do jogador quando ativa o trigger
        Vector3 initialPosition = Player.transform.position;
        //Posiçao para qual o jogador vai ser movido apos o teleport
        Vector3 TargetPosition = initialPosition + new Vector3(MoveDistance, 0f, 0f);

        
        if (other.CompareTag("Player") && !enemyAlive)
        {
            Debug.Log("Direita }");
            Player.transform.position = TargetPosition;
        }
    }
}
