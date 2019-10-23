using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public BoxCollider2D boxColl;
    private float thrust = 2.0f;
    public static Vector2 mousePosition;
    public ParticleSystem particles;
    public Shake shake;
    public Vector3 offset = new Vector3(0, 0, 9);
    public Vector3 direction;
    public float hSliderValue = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        boxColl = gameObject.GetComponent<BoxCollider2D>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        particles = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb2D.AddForce(transform.up * 100f, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            rb2D.AddForce(-transform.right * thrust, ForceMode2D.Impulse);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            rb2D.AddForce(-transform.up * thrust, ForceMode2D.Impulse);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            rb2D.AddForce(transform.right * thrust, ForceMode2D.Impulse);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        StartCoroutine(shake.ShakeEffect(0.1f, .05f));
        var emission = particles.emission;
        particles.Play();
        GetComponent<AudioSource>().Play();
    }
    private void FixedUpdate()
    {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        direction.z = 0;
        Debug.DrawRay(transform.position, direction, Color.green);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(direction * thrust, ForceMode2D.Impulse);
        }
    }
}
