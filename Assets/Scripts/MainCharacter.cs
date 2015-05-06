using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour {
    public float speed;
    public float size;
    public MainGame mainGame;
    public Sprite[] pictures;// L R U D

	// Use this for initialization
	void Start () {
        this.transform.localScale = new Vector3(size, size, 1);
	}
	
	// Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().velocity = new Vector2(h, v)*speed;
        setFace(h, v);

        //Camera.setPositionInWorld(this.transform);
        //Debug.Log(this.transform.position);
        
    }

    void setFace(float h, float v)
    {
        if (Mathf.Abs(h) + Mathf.Abs(v) < 0.1f) return;
        if (Mathf.Abs(h) > Mathf.Abs(v)) {
            if (h < 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = pictures[0];
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = pictures[1];
            }
        }
        else
        {
            if (v < 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = pictures[3];
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = pictures[2];
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        ColorOrb co = (ColorOrb)col.gameObject.GetComponent<ColorOrb>();
        if (co)
        {
            mainGame.EatColorOrb(co);

        }
    }

}
