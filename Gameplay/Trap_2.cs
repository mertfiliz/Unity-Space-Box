using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_2 : MonoBehaviour
{
    private bool up = true;
    private float speed = 5;
    public float add_speed = 0.7f;

    private int rotate_degree = 10;
    
    void Update()
    {
        this.transform.Rotate(0, 0, rotate_degree * Time.deltaTime * speed);

        if (up)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + speed * Time.deltaTime, 0);
            speed += add_speed;
        }

        if (!up)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - speed * Time.deltaTime, 0);
            speed += add_speed;
        }

        if(this.transform.localPosition.y >= 400)
        {
            rotate_degree *= -1;
            up = false;
            speed = 10f;
        }
        if(this.transform.localPosition.y <= -400) {
            rotate_degree *= -1;
            up = true;
            speed = 10f;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bounce")
        {
            rotate_degree *= -1;
            if(up)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 20, 0);
                up = false;
                speed = 10f;
            } else
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 20, 0);
                up = true;
                speed = 10f;
            }
        }
    }
}
