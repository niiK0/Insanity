using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeftDoor : MonoBehaviour
{
    private GameObject Player;

    public bool enemyAlive;

    //Distancia que o jogador se move ao tocar numa porta
    static float MoveDistance = -10f;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
   

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnTriggerEnter(Collider other)
    {
        //Parte da fun��o que verifica se o inimigo ainda esta vivio de modo a poder seguir com o jogo
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

        //Posi�ao do jogador quando ativa o trigger
        Vector3 initialPosition = Player.transform.position;
        //Posi�ao para qual o jogador vai ser movido apos o teleport
        Vector3 TargetPosition = initialPosition + new Vector3(MoveDistance, 0f, 0f);

        //Faz com que o jogador passe para a outra sala
        Debug.Log("Chegou A esquerda");
        Player.transform.position = TargetPosition;
        if (other.CompareTag("Player"))
        {
            Player.transform.position = TargetPosition;
        }
    }
}
