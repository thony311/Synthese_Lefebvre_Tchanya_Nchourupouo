using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float _vitesse = 15;
    private float _horizontal = 1;

    
    void Start()
    {
        
    }

    void Update()
    {
        Mouvement();
    }

    private void Mouvement()
    {
        Vector3 direction = default;
        direction = new Vector3(_horizontal, 0f, 0f);
        this.transform.Translate(direction * Time.deltaTime * _vitesse);
    }

    public void SetCoteArrow(bool cote)
    {
       
        if(cote == true)
        {
            _horizontal = 1;
        }
        else
        {
            _horizontal = -1;
        }
        
    }
}
