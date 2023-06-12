using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownDoor : MonoBehaviour
{
    private GameObject Player;

    public bool enemyAlive;

    //Distancia que o jogador se move ao tocar numa porta
    static float MoveDistance = -10f;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        //Posiçao do jogador quando ativa o trigger
        Vector3 initialPosition = Player.transform.position;
        //Posiçao para qual o jogador vai ser movido apos o teleport
        Vector3 TargetPosition = initialPosition + new Vector3(0f, 0f, MoveDistance);

        //Faz com que o jogador passe para a outra sala
        Debug.Log("Chegou A direita");
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Play("Door");
            Player.transform.position = TargetPosition;
        }
    }

    public void MakeAbleToLeave()
    {
        GetComponent<Collider>().enabled = true;
        transform.parent.GetChild(1).gameObject.SetActive(true);
    }
}
