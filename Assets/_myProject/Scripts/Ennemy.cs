using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ennemy : MonoBehaviour
{
    //SerializedField ==============================================================================================================================================================
    [SerializeField] float _vitesse = 5;
    [SerializeField] int _id = 1;
    [SerializeField] AudioClip _attack = default;
    //Variable =====================================================================================================================================================================
    private Player _player;
    private int _nbVie;
    private bool _enMouvement = true;
    private Animator _animator;
    private Ennemy[] _enemy;
    private bool _enCollision;
    private bool _boucle = true;
    private SpawnManager _spawnManager;
    //private bool _destroyChest = false;
    //private Collision2D _collisionChest = default;
    private UI _ui;
    private bool _uneCollision = true;
    //start ========================================================================================================================================================================
    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
        _enemy = FindObjectsOfType<Ennemy>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _ui = FindObjectOfType<UI>();
        for(int c= 0; c < _enemy.Length; c++)
        {
           Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), _enemy[c].GetComponent<Collider2D>());
        }
        if (_id == 1)
        {
            _nbVie = 1;
            //Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(),_player.GetComponent<Collider2D>());
            
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
    // Update =======================================================================================================================================================================
    void Update()
    {
        if(_enMouvement)
        {
            Mouvement();
        }
        MoveEnnemy();
        //if (_destroyChest)
        //{
        //    _destroyChest = false;
        //    //_collisionChest.gameObject.GetComponent<Chest>().DestroyChest();
        //    //Debug.Log(_collisionChest.gameObject);
        //    Destroy(_collisionChest.gameObject);
        //    StopCoroutine("AttackShroom");
        //}

    }
    //On Collision ================================================================================================================================================================
    //Sortie de la collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && _id == 2)
        {
            _boucle = false;
            _enMouvement = true;
            _animator.SetBool("GoIdleSkeleton", false);
        }
        else if (collision.gameObject.tag == "Player" && _id == 1)
        {
            _boucle = false;
            _enMouvement = true;
        }
        else if (collision.gameObject.tag == "Player" && _id == 3)
        {
            _boucle = false;
            _enMouvement = true;
        }

    }
    //En restant dans la collision
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && _id == 2)
        {
            _enCollision = true;
            _enMouvement = false;
            _animator.SetBool("GoIdleSkeleton", true);
            
        }
        else if (collision.gameObject.tag== "Player" && (_id == 1 || _id == 3))
        {
            _enCollision = true;
            _enMouvement = false;
        }
        else
        {
            _enCollision = false;
            _enMouvement = true;
        }
    }
    //En rentrant dans la collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            Destroy(collision.gameObject);
            _nbVie -= 1;
            if (_nbVie <= 0)
            {
                switch (_id)
                {
                    case 1: MortGoblin(); break;
                    case 2: MortSkeleton(); break;
                    case 3: MortShroom(); break;
                }
            }
        }
        if (collision.gameObject.tag == "FireArrow")
        {
                switch (_id)
                {
                    case 1: MortGoblin(); break;
                    case 2: MortSkeleton(); break;
                    case 3: MortShroom(); break;
                }
        }
        else if(_id == 2)
        {
            if(collision.gameObject.tag == "Player")
            {
                CollisionPlayerSkeleton();
            }
        }
        else if(collision.gameObject.tag == "Player" && _id == 1)
        {
            //CollisionGoblinChest(collision);
            CollisionPlayerGoblin();
        }
        else if(collision.gameObject.tag == "Player" && _id == 3)
        {
            CollisionPlayerShroom();
        }
    }
    //Méthodes ====================================================================================================================================================================
    //permet de bouger l'ennemi vers la gauche
    private void Mouvement()
    {
        Vector3 direction = default;
        direction = new Vector3(-1f, 0f, 0f);
        this.transform.Translate(direction * Time.deltaTime * _vitesse);
    }
    //Déplace l'ennemi a droite de l'ecran
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
    //Le resultat de la collision avec le shroom et le chest qui a pour resultat de lancer lanimation du goblin et detruire le chest
    //private void CollisionShroomChest(Collision2D collision)
    //{
    //    _enMouvement = false;
    //    _animator.SetBool("IdleShroom", true);
    //    _animator.SetBool("RunShroom", false);
    //    _collisionChest = collision;
    //    Debug.Log(_collisionChest.gameObject);
    //    StartCoroutine(AttackShroom(collision));
    //}
    //Le resultat de la collision avec le goblin et le chest qui a pour resultat de lancer lanimation du goblin et detruire le chest
    //private void CollisionGoblinChest(Collision2D collision)
    //{
    //    _enMouvement = false;
    //    _animator.SetBool("IdleGoblin", true);
    //    _animator.SetBool("RunGoblin", false);
    //    _collisionChest = collision;
    //    StartCoroutine(AttackGoblin(collision));
    //}
    //Le resultat de la collision avec le goblin et le player qui a pour resultat de lancer une animation du goblin et faire perdre des points de vies au joueur
    private void CollisionPlayerGoblin()
    {
        _boucle = true;
        _enMouvement = false;
        _animator.SetBool("IdleGoblin", true);
        _animator.SetBool("RunGoblin", false);
        StartCoroutine(AttackGoblinPlayer());
    }
    //Le resultat de la collision avec le skeleton et le player qui a pour resultat de lancer les animations du skeleton et faire perdre des points de vies au joueur
    private void CollisionPlayerSkeleton()
    {
        _boucle = true;
        _enMouvement = false;
        _animator.SetBool("GoIdleSkeleton", true);
        StartCoroutine(AttackSkeleton());
    }
    //Le resultat de la collision avec le shroom et le player qui a pour resultat de lancer les animations du shroom et faire perdre des points de vies au joueur
    private void CollisionPlayerShroom()
    {
        _boucle = true;
        _enMouvement = false;
        _animator.SetBool("IdleShroom", true);
        _animator.SetBool("RunShroom", false);
        StartCoroutine(AttackShroomPlayer());
    }
    //Lance la mort du Shroom
    private void MortShroom()
    {
        _animator.SetBool("DeathShroom", true);
        GetComponent<BoxCollider2D>().enabled = false;
        _ui.AddPointage(150);
    }
    //Lance la mort du skeleton
    private void MortSkeleton()
    {
        _animator.SetBool("DeathSkeleton", true);
        GetComponent<BoxCollider2D>().enabled = false;
        _ui.AddPointage(100);
    }
    //Lance la mort du Goblin
    private void MortGoblin()
    {
        _enMouvement = false;
        _animator.SetBool("DeathGoblin", true);
        GetComponent<BoxCollider2D>().enabled = false;
        _ui.AddPointage(50);
    }
    // Coroutines ================================================================================================================================================================
    //Lance l'attack du Goblin contre le coffre et lance les animations
    //IEnumerator AttackGoblin(Collision2D collision)
    //{
    //    yield return new WaitForSeconds(1f);
    //    _animator.SetBool("AttackGoblin", true);
    //    _animator.SetBool("IdleGoblin", false);
    //    yield return new WaitForSeconds(0.8f);
    //    //if (_enCollision == true)
    //    //    Destroy(collision.gameObject);
    //    yield return new WaitForSeconds(0.1f);
    //    _animator.SetBool("AttackGoblin", false);
    //    _animator.SetBool("IdleGoblin", true);
    //    yield return new WaitForSeconds(0.1f);
    //    _animator.SetBool("RunGoblin", true);
    //    _animator.SetBool("IdleGoblin", false);
    //    _enMouvement = true;
    //}
    //Lance l'attack du skeleton contre le joueur avec les animation
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
                AudioSource.PlayClipAtPoint(_attack, Camera.main.transform.position, 0.3f);
            }
            yield return new WaitForSeconds(0.2f);
            _animator.SetBool("AttackSkeleton", false);
        }
    }
    IEnumerator AttackGoblinPlayer()
    {
        if (_uneCollision == true)
        {
            //Debug.Log("uneCollision");
            _uneCollision = false;
            while (_boucle == true)
            {
                yield return new WaitForSeconds(1f);
                _animator.SetBool("AttackGoblin", true);
                _animator.SetBool("IdleGoblin", false);
                yield return new WaitForSeconds(0.8f);
                if (_enCollision == true)
                {
                    _player.ReduireVie();
                    AudioSource.PlayClipAtPoint(_attack, Camera.main.transform.position, 0.3f);
                }
                    
                yield return new WaitForSeconds(0.1f);
                _animator.SetBool("AttackGoblin", false);
                _animator.SetBool("IdleGoblin", true);
                yield return new WaitForSeconds(0.1f);
                _animator.SetBool("IdleGoblin", true);
                _animator.SetBool("RunGoblin", false);
            }
        }
        _animator.SetBool("RunGoblin", true);
        _animator.SetBool("IdleGoblin", false);
        _enMouvement = true;
        _uneCollision = true;
    }

    IEnumerator AttackShroomPlayer()
    {
        if (_uneCollision == true)
        {
            while (_boucle == true)
            {
                _uneCollision = false;
                yield return new WaitForSeconds(1f);
                _animator.SetBool("AttackShroom", true);
                _animator.SetBool("IdleShroom", false);
                yield return new WaitForSeconds(0.8f);
                if (_enCollision == true) 
                {
                    _player.ReduireVie();
                    AudioSource.PlayClipAtPoint(_attack, Camera.main.transform.position, 0.3f);
                }
                    
                yield return new WaitForSeconds(0.1f);
                _animator.SetBool("AttackShroom", false);
                _animator.SetBool("IdleShroom", true);
                yield return new WaitForSeconds(0.1f);
                _animator.SetBool("RunShroom", false);
                _animator.SetBool("IdleShroom", true);
                yield return new WaitForSeconds(1f);
            }
        }
        _animator.SetBool("RunShroom", true);
        _animator.SetBool("IdleShroom", false);
        _uneCollision = true;
        _enMouvement = true;
    }
    //Lance l'attack du shroom contre des coffres avec les animation
    //IEnumerator AttackShroom(Collision2D toto)
    //{
    //    Debug.Log(toto.gameObject);
    //    yield return new WaitForSeconds(1f);
    //    _animator.SetBool("AttackShroom", true);
    //    _animator.SetBool("IdleShroom", false);
    //    Debug.Log("avant la boucle" + toto.gameObject);
    //    yield return new WaitForSeconds(0.8f);
    //    if (_enCollision == true )
    //    {   
    //        Debug.Log("tu rentre dans la boucle");
    //        Debug.Log(toto.gameObject);
    //        //Destroy(toto.gameObject);
    //        //_collisionChest.gameObject.GetComponent<Chest>().DestroyChest();
    //        _destroyChest = true;
    //    }
    //    yield return new WaitForSeconds(0.1f);
    //    _animator.SetBool("AttackShroom", false);
    //    _animator.SetBool("IdleShroom", true);
    //    yield return new WaitForSeconds(0.1f);
    //    _animator.SetBool("RunShroom", true);
    //    _animator.SetBool("IdleShroom", false);
    //    _enMouvement = true;
    //    //yield break;
    //}
}
