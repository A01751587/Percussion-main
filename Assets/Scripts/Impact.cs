using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    private bool Activator = false;
    private bool _NiceHit = false;
    private bool _NormalHit = false;
    private bool _WasHit = false;

    public KeyCode Key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            print("Presionado");
            if (Activator)
            {
                if (_NiceHit)
                {
                    PerfectHit();
                }
                if (_NormalHit)
                {
                    NormalHit();
                }
            }
        }
    }

    public void PerfectHit()
    {
        print("Perfect metodo");
        LevelManager.instance.NiceHit();
        _WasHit = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

    }
    public void NormalHit()
    {
        print("Normal metodo");
        LevelManager.instance.Hit();
        _WasHit = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

    }

    public void WasHit()
    {
        print("WasHitTrue");
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.SetActive(false);
        _WasHit = false;
        _NiceHit = false;
        _NormalHit = false;
    }

    public void WasNotHit()
    {
        print("WasNotHitTrue");
        LevelManager.instance.Fail();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.SetActive(false);
        _WasHit = false;
        _NiceHit = false;
        _NormalHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            Activator = true;
            _NiceHit = true;
            _NormalHit = false;
            if (_NiceHit)
            {
                print("NiceHit true");
            }
        }

        if (collision.tag == "NormalHit")
        {
            Activator = true;
            _NormalHit = true;
            _NiceHit = false;
            if (_NormalHit)
            {
                print("NormalHit true");
            }
        }
        if (collision.tag == "Destroyer")
        {
            if (_WasHit)
            {
                WasHit();
            }
            else
            {
                WasNotHit();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NormalHit")
        {
            Activator = false;
        }
        
    }
}
