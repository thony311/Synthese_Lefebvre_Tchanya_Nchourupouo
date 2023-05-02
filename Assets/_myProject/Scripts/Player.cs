using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //SerializedField
    [SerializeField] float _vitesse = 10;
    [SerializeField] GameObject _arrow = default;
    [SerializeField] float _fireRate = 0.5f;
    //
    private float _canFire = -1f;
    private Animator _animator;
    private bool _cotePlayer = true;

    private Arrow _scriptArrow;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _scriptArrow = _arrow.GetComponent<Arrow>();
    }

    // Update is called once per frame
    void Update()
    {
        ActionJoueur();
        Tir();
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
            _scriptArrow.SetCoteArrow(false);
        }
        else if (horizontal > 0)
        {
            _animator.SetBool("Run", true);
            this.GetComponent<SpriteRenderer>().flipX = false;
            _cotePlayer = true;
            _scriptArrow.SetCoteArrow(true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }
    }

    private void Tir()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            if(_cotePlayer == true)
            {
                Instantiate(_arrow, transform.position + new Vector3(0.7f,-0.4f,0f), Quaternion.identity);
            }
            else
            {
                Instantiate(_arrow, transform.position + new Vector3(-0.7f, -0.4f, 0f), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
        
    }
    
}
