using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //SerializedField ======================================================================================================================================================
    [SerializeField] float _vitesse = 10;
    [SerializeField] GameObject _arrow = default;
    [SerializeField] GameObject _fireArrow = default;
    [SerializeField] float _fireRate = 1.51f;
    [SerializeField] float _fireRateFireArrow = 10f;
    [SerializeField]  private int _nbVie = 3;
    //Variables ======================================================================================================================================================
    private float _canFire = -1f;
    private float _canFireFireArrow = -1f;
    private Animator _animator;
    private bool _cotePlayer = true;
    private bool _enTir = false;
    private bool _death = false;
    private UI _ui;
    // Start ======================================================================================================================================================
    void Start()
    {
        _animator = GetComponent<Animator>();
        _ui = FindObjectOfType<UI>();
    }
    // Update ======================================================================================================================================================
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
    // Méthodes private ======================================================================================================================================================
    //Permet de bouger le joueur
    private void ActionJoueur()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0f, 0f);
        this.transform.Translate(direction * Time.deltaTime * _vitesse);
        if(horizontal < 0)
        {
            _animator.SetBool("Run", true);
            this.GetComponent<SpriteRenderer>().flipX = true;
            _cotePlayer = false;
        }
        else if (horizontal > 0)
        {
            _animator.SetBool("Run", true);
            this.GetComponent<SpriteRenderer>().flipX = false;
            _cotePlayer = true;
        }
        else
        {
            _animator.SetBool("Run", false);
        }
    }
    //permet de faire tirer des flèches
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
        }
        if (Input.GetKeyUp(KeyCode.F) && Time.time > _canFire && Time.time > _canFireFireArrow)
        {
            _enTir = true;
            float positionX;
            if (_cotePlayer == true)
            {
                positionX = 0.7f;
            }
            else
            {
                positionX = -0.85f;
            }
            _animator.SetBool("Attack", true);
            StartCoroutine(SpawnFireArrow(positionX));
        }
    }
    // Permet de sauvegarder le pointage du joueur et lance la coroutine qui va amener a la scene de fin
    private void Mort()
    {
        
        int valeur = _ui.GetPointage();
        PlayerPrefs.SetInt("pointage", valeur);
        PlayerPrefs.SetFloat("timeJeu",Time.time - PlayerPrefs.GetFloat("timePerdu"));
        PlayerPrefs.Save();
        StartCoroutine(ChangementScene());
    }
    // Coroutines ======================================================================================================================================================
    // Lance le changement de scene apres la mort du joueur
    IEnumerator ChangementScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }
    // Lance l'animation de tirrage de fleches et fait apparraitre une fleche
    IEnumerator SpawnArrow(float positionX)
    {
        _canFire = Time.time + _fireRate;
        yield return new WaitForSeconds(0.3f);
        Instantiate(_arrow, transform.position + new Vector3(positionX, -0.4f, 0f), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("Attack", false);
        _enTir = false;
    }
    // Lance l'animation de tirrage de fleches et fait apparraitre une fleche de feu
    IEnumerator SpawnFireArrow(float positionX)
    {
        _canFireFireArrow = Time.time + _fireRateFireArrow;
        yield return new WaitForSeconds(0.3f);
        Instantiate(_fireArrow, transform.position + new Vector3(positionX, -0.4f, 0f), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("Attack", false);
        _enTir = false;
    }
    //Méthodes public ==================================================================================================================================================
    //Retourne le côté du joueur
    public bool GetCotePlayer()
    {
        return _cotePlayer;
    }
    // Permet de réduire le nombre de vies du joueur
    public void ReduireVie()
    {
        _nbVie -= 1;
        if(_nbVie <= 0)
        {
            _death = true;
            _animator.SetBool("Death",true);
            GetComponent<BoxCollider2D>().enabled = false;
            Mort();
        }
    }
    //Retourne le nombre de point de vie restant
    public int GetVie()
    {
        return _nbVie;
    }
    //Retourne le temps que cela prend poru recharger une flèche de feu
    public float GetTimeFireArrow()
    {
        return _fireRateFireArrow;
    }
    //Retourn le temps avant de pouvoir tirer une flèche de feu
    public float GetCanFireFireArrow()
    {
        return _canFireFireArrow;
    }
    //Set la position du joueur
    public void SetPositionJoueur(float x, float y)
    {
        this.gameObject.transform.position = new Vector3(x, y, 0f);
    }
}
