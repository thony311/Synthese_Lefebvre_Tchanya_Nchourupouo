using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //SerializedField
    [SerializeField] float _vitesse = 15;

    //type
    private float _horizontal = 1;
    private Player _player = default;
    private bool _sense =false;
    
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Mouvement();
        DestroyArrow();
    }

    private void DestroyArrow()
    {
        if (transform.position.x > 14 || transform.position.x < -14)
        {
            Destroy(gameObject);
        }
    }

    private void Mouvement()
    {
        if (!_sense)
        {
            if(_player.GetCotePlayer())
            {
                _horizontal = 1;
            }
            else
            {
                _horizontal = -1;
            }
            _sense = true;
        }
        Vector3 direction = default;
        direction = new Vector3(_horizontal, 0f, 0f);
        this.transform.Translate(direction * Time.deltaTime * _vitesse);
    }
}
