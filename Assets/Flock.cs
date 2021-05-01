using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    //Manager do Flocking
    public FlockingManager myManager;
    //Velocidade dos Peixes
    float speed;

    void Start(){
        //Aleatoriedade do Speed.
        speed= Random.Range(myManager.minSpeed,myManager.maxSpeed);
    }// Update is called once per frame
    void Update(){
        //Movimentação
        transform.Translate(0, 0, Time.deltaTime* speed);
    }
}

