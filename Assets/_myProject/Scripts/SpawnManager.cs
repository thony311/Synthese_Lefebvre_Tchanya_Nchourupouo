using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //SerializeField
    [SerializeField] private GameObject[] _listeEnemyPrefabs = default;
    [SerializeField] private GameObject _enemyCountainer =default;

    private bool _stopSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(14f, randomEtage(), 0f);
            
        }
    }

    private float randomEtage()
    {
        float random = Random.Range(1, 4);
        switch(random)
        {
            case 1: return -4.99f;
            case 2: return -1.99f;
            case 3: return 1.026f;
            case 4: return 4.01f;
        }
        return -10f;
    }
}
