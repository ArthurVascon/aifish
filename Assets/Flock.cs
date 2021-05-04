using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    //Manager do Flocking
    public FlockingManager myManager;
    //Velocidade dos Peixes
    float speed;

    bool turning= false;

    void Start()
    {
        //Aleatoriedade do Speed.
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }// Update is called once per frame
    void Update()
    {
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2); 
        if (!b.Contains(transform.position)) { 
            turning = true; 
        }
        else 
            turning = false;
        if (turning)
        {
            Vector3 direction = myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
        else { 
            if (Random.Range(0, 100) < 10)
            {
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            } if (Random.Range(0, 100) < 20)
            {
                ApplyRules(); 
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
    //Regras de movimentação

    void ApplyRules()
    {
        //Array dos peixes
        GameObject[] gos;
        gos = myManager.allFish;
        //Pontos iniciais de base
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        //Velocidade do grupo
        float gSpeed = 0.01f;
        //Distância da vizinhança
        float nDistance;
        //Tamanho do gruopo
        int groupSize = 0;
        //Para cada objeto no array dos peixes do manager
        foreach (GameObject go in gos)
        {
            //Se o objeto não for esse
            if (go != this.gameObject)
            {
                //A distância da vizinha é igual a posição do outro peixe até esse.
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                //Se a distância for menor ou igual
                if (nDistance <= myManager.neighbourDistance)
                {
                    //Ele aumenta a direção até o centro
                    vcentre += go.transform.position;
                    //E aumenta o número do grupo
                    groupSize++;
                    //Se a distância da vizinha for menor que 1
                    if (nDistance < 1.0f)
                    {
                        //Ele evita o peixe
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    //Pega o outro flock
                    Flock anotherFlock = go.GetComponent<Flock>();
                    //E aumenta sua velocidade para desviar dele
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        //Se o grupo for maior que 0
        if (groupSize > 0)
        {
            //O valor do centro tem que ser dividido pelo tamanho do grupo
            vcentre = vcentre / groupSize;
            //Sua velocidade tem que ser dividido pelo grupo
            gSpeed = gSpeed / groupSize;
            //A didereção tem que ser a soma do valor até o centro mais o valor de desvio menos a posição atual
            Vector3 direction = (vcentre + vavoid) - transform.position;
            //Se a direção for diferente de 0, ele muda a rotação do peixe para não bater
            if (direction != Vector3.zero) 
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
    }
}

