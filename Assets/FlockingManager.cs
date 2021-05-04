using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    //Prefab do Cardume
    public GameObject fishPrefab;
    //Número de Peixes
    public int numFish= 20;
    //Todos os peixes
    public GameObject[] allFish; 
    //Limite do quanto eles podem nadar
    public Vector3 swinLimits = new Vector3(5, 5, 5);

    [Header("Configurações do Cardume")] 
    [Range(0.0f, 5.0f)] public float minSpeed;
    [Range(0.0f, 5.0f)] public float maxSpeed;
    [Range(1.0f, 10.0f)] public float neighbourDistance;
    [Range(0.0f, 5.0f)] public float rotationSpeed;

    void Start() { 
        //Todos os peixes com o tamanho do número dos peixes
        allFish = new GameObject[numFish];
        //Loop para todos os peixes
        for (int i = 0; i < numFish; i++) { 
            //Modificar a posição, mais a movimentação sendo o limite da movimentação sendo o positivo e o negativo aleatório de cada orientação (X,Y,Z)
            Vector3 pos = this.transform.position 
                + new Vector3(Random.Range(-swinLimits.x, swinLimits.x), 
                Random.Range(-swinLimits.y, swinLimits.y), Random.Range(-swinLimits.z, swinLimits.z));
            //Instancia os peixes no array.
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        } 
    }
    void Update() {
    }
}
