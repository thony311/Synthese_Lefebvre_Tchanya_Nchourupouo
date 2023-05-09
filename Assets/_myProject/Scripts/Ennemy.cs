using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ennemy : MonoBehaviour
{
    //SerializedField
    [SerializeField] float _vitesse = 5;
    [SerializeField] int _id = 1;

    //autre
    private Player _player;
    private int _nbVie;
    private bool _enMouvement = true;
    private Animator _animator;
    private Ennemy[] _enemy;
    private bool _enCollision;
    private bool _boucle = true;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
        _enemy = FindObjectsOfType<Ennemy>();
        _spawnManager = FindObjectOfType<SpawnManager>();

        for(int c= 0; c < _enemy.Length; c++)
        {
           Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), _enemy[c].GetComponent<Collider2D>());
        }
        if (_id == 1)
        {
            _nbVie = 1;
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(),_player.GetComponent<Collider2D>());
            
        }
        else if (_id == 2)
        {
            _nbVie = 3;
        }
        else if (_id == 3)
        {
            _nbVie = 5;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_enMouvement)
        {
            Mouvement();
        }
        MoveEnnemy();
        
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
            _nbVie -= 1;
            if (_nbVie <= 0)
            {
                if (_id == 1)
                {
                    _enMouvement = false;
                    _animator.SetBool("DeathGoblin", true);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                if (_id == 2)
                {
                    _animator.SetBool("DeathSkeleton", true);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                if (_id == 3)
                {
                    _animator.SetBool("DeathShroom", true);
                    GetComponent<BoxCollider2D>().enabled = false;
                }

            }
            
            
        }
        else if(collision.gameObject.tag == "Player" && _id == 2)
        {
            _boucle = true;
            _enMouvement = false;
            _animator.SetBool("GoIdleSkeleton", true);
            StartCoroutine(AttackSkeleton());
        }
    }

    IEnumerator AttackSkeleton()
    {
        while (_boucle == true) {
            //_boucle = false;
            yield return new WaitForSeconds(0.5f);
            _animator.SetBool("AttackSkeleton", true);
            yield return new WaitForSeconds(0.7f);
            if (_enCollision == true)
            {
                _player.ReduireVie();

            }
            yield return new WaitForSeconds(0.2f);
            _animator.SetBool("AttackSkeleton", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && _id == 2)
        {
            _boucle = false;
            _enMouvement = true;
            _animator.SetBool("GoIdleSkeleton", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && _id == 2)
        {
            _enCollision = true;
            _enMouvement = false;
            _animator.SetBool("GoIdleSkeleton", true);
            
        }
        else
        {
            _enCollision = false;
            _enMouvement = true;
            _animator.SetBool("GoIdleSkeleton", false);
            
        }
    }

    private void MoveEnnemy()
    {
        if(transform.position.x <= -12)
        {
            Vector3 newPosition = new Vector3(14f, _spawnManager.randomEtage(), 0f);
            transform.position = newPosition;
        }

        if(transform.position.y <= -7)
        {
            Destroy(gameObject);
        }
    }
}
