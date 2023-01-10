using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float velocity;
    [SerializeField] private float _incrementVelocity = 1f;
    private Vector2 _velocityPrev;


    private void Awake()
    {
        GameManager.Instance.Ball = this;
    }
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void FixedUpdate()
    {
        _velocityPrev = _rigidbody2D.velocity;       //La velocidadPrev se iguala a la normal ya que una vez que colisione se va a parar la normal por la colision, pero la prev sigue siendo la misma
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()    //Es el saque
    {
        float v = Random.Range(04f, 1f);

        float x = v * (Random.Range(0, 2) == 0 ? -1 : 1);

        float y = v * (Random.Range(0, 2) == 0 ? -1 : 1);

        transform.position = Vector2.zero;
        _rigidbody2D.velocity = velocity * (new Vector2(x, y)).normalized;
        _velocityPrev = _rigidbody2D.velocity;      //La velocidadPrev se iguala a la normal ya que una vez que colisione se va a parar la normal por la colision, pero la prev sigue siendo la misma
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _rigidbody2D.velocity = _velocityPrev + Accelerate(_velocityPrev);      //Igualamos la veocidad a la previa, ya que la normal ha reducido con el impacto pero la previa no, y queremos que siga con la misma velocidad
            _rigidbody2D.velocity = new Vector2 (-_rigidbody2D.velocity.x, _rigidbody2D.velocity.y);
            
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            _rigidbody2D.velocity = _velocityPrev;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_rigidbody2D.velocity.y);

        }
    }

    private Vector2 Accelerate(Vector2 velocity)
    {
        return _incrementVelocity * velocity.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (_rigidbody2D != null)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + _rigidbody2D.velocity);
        }
    }
}
