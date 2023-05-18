using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite : MonoBehaviour
{
    //Variables =============================================================================================================================================================
    private Ennemy[] _ennemy;
    // Start =============================================================================================================================================================
    void Start()
    {
        
    }
    // Update =============================================================================================================================================================
    void Update()
    {
        _ennemy = FindObjectsOfType<Ennemy>();
        foreach(Ennemy ennemy in _ennemy)
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), ennemy.gameObject.GetComponent<Collider2D>());
        }
    }
}
