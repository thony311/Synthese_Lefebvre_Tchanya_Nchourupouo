using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //SerializedField
    [SerializeField] float _vitesse = 10;
    [SerializeField] GameObject _arrow = default;
    [SerializeField] float _fireRate = 1.51f;
    //
    private float _canFire = -1f;
    private Animator _animator;
    private bool _cotePlayer = true;
    private bool _enTir = false;
    [SerializeField]  private int _nbVie = 5;
    private bool _death = false;
    

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_death)
        {
            if(!_enTir)
            {
                ActionJoueur();
            }
            Tir();
            if(transform.position.y < -6)
            {
                transform.position = new Vector3(transform.position.x, 5f, 0f);
            }
        }
        
    }

    private void ActionJoueur()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, 0f);
        this.transform.Translate(direction * Time.deltaTime * _vitesse);

        if(horizontal < 0)
        {
            _animator.SetBool("Run", true);
            this.GetComponent<SpriteRenderer>().flipX = true;
            _cotePlayer = false;
            Debug.Log(_cotePlayer);
        }
        else if (horizontal > 0)
        {
            _animator.SetBool("Run", true);
            this.GetComponent<SpriteRenderer>().flipX = false;
            _cotePlayer = true;
            Debug.Log(_cotePlayer);
        }
        else
        {
            _animator.SetBool("Run", false);
        }

        if(vertical < 0)
        {
            GetComponent<BoxCollider2D>().enabled= false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void Tir()
    {
        
        if (Input.GetKeyUp(KeyCode.Space) && Time.time > _canFire)
        {
            _enTir = true;
            float positionX;
            if(_cotePlayer == true)
            {
                positionX = 0.7f;
            }
            else
            {
                positionX = -0.85f;
            }
            _animator.SetBool("Attack", true);
            StartCoroutine(SpawnArrow(positionX));
            //Instantiate(_arrow, transform.position + new Vector3(positionX, -0.4f, 0f), Quaternion.identity); 
        }
    }

    IEnumerator SpawnArrow(float positionX)
    {
        _canFire = Time.time + _fireRate;
        yield return new WaitForSeconds(0.3f);
        Instantiate(_arrow, transform.position + new Vector3(positionX, -0.4f, 0f), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("Attack", false);
        _enTir = false;

    }

    public bool GetCotePlayer()
    {
        return _cotePlayer;
    }

    public void ReduireVie()
    {
        _nbVie -= 1;
        if(_nbVie <= 0)
        {
            _death = true;
            _animator.SetBool("Death",true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public int GetVie()
    {
        return _nbVie;
    }
}
