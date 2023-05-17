using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //SerializeField ======================================================================================================================================================
    [SerializeField] private GameObject[] _listeEnemyPrefabs = default;
    [SerializeField] private GameObject _enemyCountainer =default;
    //Variables ======================================================================================================================================================
    private bool _stopSpawn = false;
    //Start ======================================================================================================================================================e
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    //Update ======================================================================================================================================================
    void Update()
    {
        
    }
    //Coroutines ======================================================================================================================================================
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (!_stopSpawn)
        {
            int randomEnnemy = Random.Range(0, 3);
            Vector3 positionSpawn = new Vector3(14f, randomEtage(), 0f);
            GameObject NewEnnemy = Instantiate(_listeEnemyPrefabs[randomEnnemy], positionSpawn, Quaternion.identity);
            NewEnnemy.transform.parent = _enemyCountainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    //Méthodes public ======================================================================================================================================================
    public float randomEtage()
    {
        float random = Random.Range(1, 5);
        switch(random)
        {
            case 1: return -4.99f;
            case 2: return -1.99f;
            case 3: return 1.026f;
            case 4: return 4.1f;
        }
        return -10f;
    }
}
