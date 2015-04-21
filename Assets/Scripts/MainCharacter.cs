using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour {
    public float speed;
    public float size;
    public MainGame mainGame;

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
       // Camera.setPositionInWorld(this.transform);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        ColorOrb co = (ColorOrb)col.gameObject.GetComponent<ColorOrb>();
        if (co)
        {
            mainGame.eatColorOrb(co);

        }
    }

}
