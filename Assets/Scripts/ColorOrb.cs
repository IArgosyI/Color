using UnityEngine;
using System.Collections;

public class ColorOrb : MonoBehaviour {
    public float speed;
    public Vector3 velocity;
    public static Color[] colors;
    public Color[] tmpColors;
    public int nColor;
    public MainGame mainGame;

	// Use this for initialization
	void Start () {
        if(colors==null)colors = tmpColors;
        float r = Random.Range(0.0f, Camera.worldHeight * 2 + Camera.worldWidth * 2);
        if (r < Camera.worldHeight)
        {
            this.transform.position = new Vector3(-Camera.worldWidth + 0.5f, r, 0);
        }
        else if (r < Camera.worldHeight + Camera.worldWidth)
        {
            this.transform.position = new Vector3(r-Camera.worldHeight, Camera.worldHeight-0.5f, 0);
        }
        else if (r < Camera.worldHeight*2 + Camera.worldWidth)
        {
            this.transform.position = new Vector3(Camera.worldWidth - 0.5f, r - Camera.worldHeight- Camera.worldWidth, 0);
        }
        else
        {
            this.transform.position = new Vector3(r - Camera.worldHeight*2 - Camera.worldWidth, -Camera.worldHeight + 0.5f, 0);
        }
        float p = Random.Range(0.0f,1.0f);
	    velocity = new Vector3(speed*p, speed*(1-p),0);

        nColor = Random.Range(0, colors.Length);
        //Simple 0 - R, 1 - G, 2 - B
        SpriteRenderer sr = (SpriteRenderer)this.GetComponent<SpriteRenderer>();
        sr.color = colors[nColor];

        speed += Random.Range(-speed * 0.2f, speed * 0.2f);
        GetComponent<Rigidbody2D>().velocity = velocity * speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Rigidbody2D>() == null)
        {
            return;
        }
        velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.Normalize();
        GetComponent<Rigidbody2D>().velocity = velocity * speed;
        /*int bounce = Camera.setPositionInWorld(this.transform);
        if (bounce == -1 || bounce == 1)
        {
            velocity = new Vector3(-velocity.x, velocity.y, 0);
        }
        if (bounce == -2 || bounce == 2)
        {
            velocity = new Vector3(velocity.x, -velocity.y,0);
        }*/
	}

    public IEnumerator colorAnimation()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<SpriteRenderer>().sortingOrder = -60;
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        int i = 0;
        while (i++<30)
        {
            transform.localScale += new Vector3(10, 10, 0);
            yield return new WaitForSeconds(0.001f);
        }
        mainGame.updateColors(nColor);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        Destroy(this.gameObject);

    }
}
