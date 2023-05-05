using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ennemy : MonoBehaviour
{
    //SerializedField
    [SerializeField] float _vitesse = 5;
    [SerializeField] int _id = 1;

    //autre
    private Player _player;
    private int _nbVie = 1;
    private bool _enMouvement = true;
    private Animator _animator;
    private Ennemy[] _enemy;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
        _enemy = FindObjectsOfType<Ennemy>();
        if (_id == 1)
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(),_player.GetComponent<Collider2D>());
            for(int c= 0; c < _enemy.Length; c++)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), _enemy[c].GetComponent<Collider2D>());
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_enMouvement)
        {
            Mouvement();
        }
        
    }

    private void Mouvement()
    {
        Vector3 direction = default;
        direction = new Vector3(-1f, 0f, 0f);
        this.transform.Translate(direction * Time.deltaTime * _vitesse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Player" && _id != 1)
        {
            _enMouvement = false;
            _animator.SetBool("GoIdleSkeleton", true);
        }
    }
}
