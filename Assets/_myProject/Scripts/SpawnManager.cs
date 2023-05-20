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
    private UI _ui;
    //Start ======================================================================================================================================================e
    void Start()
    {
        _ui = FindObjectOfType<UI>();
        StartCoroutine(SpawnEnemyRoutine());
    }

    //Update ======================================================================================================================================================
    void Update()
    {
        
    }
    //Coroutines ======================================================================================================================================================
    IEnumerator SpawnEnemyRoutine()
    {
        int randomEnnemy = 0;
        yield return new WaitForSeconds(5.0f);
        while (!_stopSpawn)
        {
            if(_ui.GetPointage() <= 150)
            {
                randomEnnemy = 0;
                SpawnEnnemy(randomEnnemy);
                //Debug.Log("niveau 1");
                yield return new WaitForSeconds(5.0f);
            }
            else if(_ui.GetPointage() <= 350)
            {
                randomEnnemy = 0;
                SpawnEnnemy(randomEnnemy);
                //Debug.Log("niveau 2");
                yield return new WaitForSeconds(4.0f);
            }
            else if(_ui.GetPointage() <= 600)
            {
                randomEnnemy = Random.Range(0, 2);
                SpawnEnnemy(randomEnnemy);
                //Debug.Log("niveau 3");
                yield return new WaitForSeconds(4.0f);
            }
            else if(_ui.GetPointage() <= 1000)
            {
                randomEnnemy = Random.Range(0, 3);
                SpawnEnnemy(randomEnnemy);
                //Debug.Log("niveau 4");
                yield return new WaitForSeconds(4.0f);
            }
            else if(_ui.GetPointage() <= 2000)
            {
                randomEnnemy = Random.Range(0, 3);
                SpawnEnnemy(randomEnnemy);
                //Debug.Log("niveau 5");
                yield return new WaitForSeconds(2.0f);
            }
            else if(_ui.GetPointage() <= 3000)
            {
                randomEnnemy = Random.Range(0, 3);
                SpawnEnnemy(randomEnnemy);
                //Debug.Log("niveau 6");
                yield return new WaitForSeconds(1.0f);
            }
            else if (_ui.GetPointage() > 4000)
            {
                randomEnnemy = Random.Range(0, 3);
                SpawnEnnemy(randomEnnemy);
                //Debug.Log("niveau 7");
                yield return new WaitForSeconds(0.5f);
            }

        }
    }
    //Méthodes private ======================================================================================================================================================
    private void SpawnEnnemy(int randomEnnemy)
    {
        Vector3 positionSpawn = new Vector3(14f, randomEtage(), 0f);
        GameObject NewEnnemy = Instantiate(_listeEnemyPrefabs[randomEnnemy], positionSpawn, Quaternion.identity);
        NewEnnemy.transform.parent = _enemyCountainer.transform;
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
