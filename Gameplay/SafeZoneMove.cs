using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneMove : MonoBehaviour
{
    public bool enableMove = false;
    public bool enableShrink = false;

    private bool isShrink = false;
    private float max_s, min_s;
    private bool up = true;
    private float speed = 8;
    public float add_speed = 0.5f;

    void Start()
    {
        max_s = this.GetComponent<RectTransform>().sizeDelta.x;
        min_s = this.GetComponent<RectTransform>().sizeDelta.x / 2;
    }    

    void Update()
    {
        if(enableMove)
        {
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

            if (this.transform.localPosition.y >= 300)
            {
                up = false;
                speed = 8f;
            }
            if (this.transform.localPosition.y <= -300)
            {
                up = true;
                speed = 8f;
            }
        }

        if (enableShrink)
        {
            float size_s = this.GetComponent<RectTransform>().sizeDelta.x;
           
            if(isShrink)
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector3(this.GetComponent<RectTransform>().sizeDelta.x - 0.5f, this.GetComponent<RectTransform>().sizeDelta.y - 0.5f, 1);
                this.GetComponent<CircleCollider2D>().radius = this.GetComponent<RectTransform>().sizeDelta.x / 2;
            }

            if(!isShrink)
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector3(this.GetComponent<RectTransform>().sizeDelta.x + 0.5f, this.GetComponent<RectTransform>().sizeDelta.y + 0.5f, 1);
                this.GetComponent<CircleCollider2D>().radius = this.GetComponent<RectTransform>().sizeDelta.x / 2;
            }

            if(size_s >= max_s)
            {
                isShrink = true;
            }

            if(size_s <= min_s)
            {
                isShrink = false;
            }    
        }
    }
}
