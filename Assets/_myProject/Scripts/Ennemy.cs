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
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        if (_id == 1)
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(),_player.GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Mouvement();
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
    }
}
