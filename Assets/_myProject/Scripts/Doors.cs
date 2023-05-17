using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    //SerializeField =====================================================================================================================================================================
    [SerializeField] int _id = 0;
    [SerializeField] float _TimGoInDoors = 2f;
    //Variables =====================================================================================================================================================================
    private Player _player;
    private float _canGoInDoors = 0f;
    // Start =====================================================================================================================================================================
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    // Update =====================================================================================================================================================================
    void Update()
    {
        
    }
    //Trigger =====================================================================================================================================================================
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Permet de voyager d'étages en étages avec les portes
        if(collision.gameObject.tag == "Player") 
        {
            if(Time.time >= _canGoInDoors)
            {
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    switch(_id)
                    {
                        case 1: _player.SetPositionJoueur(4.393077f, -4.230212f); _canGoInDoors = Time.time + _TimGoInDoors; break;
                        case 2: _player.SetPositionJoueur(-3.864129f, 4.738822f); _canGoInDoors = Time.time + _TimGoInDoors; break;
                        case 3: _player.SetPositionJoueur(9.660606f, 1.692198f); _canGoInDoors = Time.time + _TimGoInDoors; break;
                        case 4: _player.SetPositionJoueur(-6.450286f, -1.272921f); _canGoInDoors = Time.time + _TimGoInDoors; break;
                    }
                }
                else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    switch (_id)
                    {
                        case 1: _player.SetPositionJoueur(9.660606f, 1.692198f); _canGoInDoors = Time.time + _TimGoInDoors; break;
                        case 2: _player.SetPositionJoueur(-6.450286f, -1.272921f); _canGoInDoors = Time.time + _TimGoInDoors; break;
                        case 3: _player.SetPositionJoueur(4.393077f, -4.230212f); _canGoInDoors = Time.time + _TimGoInDoors; break;
                        case 4: _player.SetPositionJoueur(-3.864129f, 4.738822f); _canGoInDoors = Time.time + _TimGoInDoors; break;
                    }
                }
            }
        }
    }

}
