using UnityEngine;
using System.Collections;

public class ColorOrb : MonoBehaviour {
    public float speed;
    public Vector3 velocity;
    public static Color[] colors;
    public int nColor;
    public MainGame mainGame;
    public GameObject circleRing;

	// Use this for initialization
	void Start () {
        //InitializePosition();
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

        if (transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }
	}

    void InitializePosition()
    {
        float r = Random.Range(0.0f, GameScreen.worldHeight * 2 + GameScreen.worldWidth * 2);
        float buffer = 20.0f;
        if (r < GameScreen.worldHeight)
        {
            this.transform.position = new Vector3(buffer, r, 0);
        }
        else if (r < GameScreen.worldHeight + GameScreen.worldWidth)
        {
            this.transform.position = new Vector3(r - GameScreen.worldHeight, GameScreen.worldHeight - buffer, 0);
        }
        else if (r < GameScreen.worldHeight * 2 + GameScreen.worldWidth)
        {
            this.transform.position = new Vector3(GameScreen.worldWidth - buffer, r - GameScreen.worldHeight - GameScreen.worldWidth, 0);
        }
        else
        {
            this.transform.position = new Vector3(r - GameScreen.worldHeight * 2 - GameScreen.worldWidth, buffer, 0);
        }
        float p = Random.Range(0.0f, 1.0f);
        velocity = new Vector3(speed * p, speed * (1 - p), 0);
        //Debug.Log(r+" "+this.transform.position);

        //nColor = Random.Range(0, colors.Length);
        ////Simple 0 - R, 1 - G, 2 - B
        //SpriteRenderer sr = (SpriteRenderer)this.GetComponent<SpriteRenderer>();
        //sr.color = colors[nColor];

        speed += Random.Range(-speed * 0.2f, speed * 0.2f);
        GetComponent<Rigidbody2D>().velocity = velocity * speed;
    }

    void OnMouseDown()
    {
        //Debug.Log("?");
        if(mainGame.animationDone)mainGame.EatColorOrb(this);
    }

    public IEnumerator ColorAnimation()
    {
        mainGame.animationDone = false;
        GameObject cR = Instantiate(circleRing);
        cR.transform.position = transform.position;
        cR.GetComponent<SpriteRenderer>().sortingOrder = 0;
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<SpriteRenderer>().sortingOrder = -60;
        //Time.timeScale = 0.3f;
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
        int i = 0;
        while (i++<40)
        {
            cR.transform.localScale += new Vector3(5, 5, 0);
            transform.localScale += new Vector3(250, 250, 0);
            yield return new WaitForSeconds(0.01f);
        }
        mainGame.UpdateColors(nColor);
        //Time.timeScale = 1f;
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
        Destroy(cR);
        mainGame.animationDone = true;
        Destroy(this.gameObject);

    }

    public void SetColor(int n)
    {
        nColor = n;
        SpriteRenderer sr = (SpriteRenderer)this.GetComponent<SpriteRenderer>();
        sr.color = colors[n];
    }
}
