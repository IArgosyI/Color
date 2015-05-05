using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {
    public GameObject colorOrb;
    public float timeInterval;
    public SpriteRenderer[] backgrounds;
    public Sprite[] borderBG;
    public Sprite[] originalBG;
    public Sprite[] whiteBG;
    public int selectedBG;
    public Color[] tmpColor;

    public int[,] answers;

    public GameObject button;
    public SpriteRenderer winScreen;
    public SpriteRenderer retryScreen;

    public bool playing = true;

    public int maxSlot;

	// Use this for initialization
	void Start () {
        selectedBG = 0;
        ColorOrb.colors = tmpColor;
        //spawnInitialColorOrbs();
        
        StartCoroutine(flashingSelected());
        StartCoroutine(rainingColorOrbs());

        answers = new int[2,5];
        answers[0, 0] = 0;
        answers[0, 1] = 1;
        answers[0, 2] = 2;
        answers[0, 3] = 3;
        answers[0, 4] = 4;
        answers[1, 0] = 5;
        answers[1, 1] = 1;
        answers[1, 2] = 6;
        answers[1, 3] = 7;
        answers[1, 4] = 8;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (retryScreen.enabled)
            {
                Time.timeScale = 1f;
                //retryScreen.enabled = false;
                Application.LoadLevel("levelSelection");
            }
            if (winScreen.enabled)
            {
                Time.timeScale = 1f;
                PlayerPrefs.SetInt("level", 2);
                PlayerPrefs.Save();
                Application.LoadLevel("levelSelection");
            }
        }
	}

    void spawnInitialColorOrbs()
    {
        for (int i = 0; i < ColorOrb.colors.Length; i++)
        {
            int r = Random.Range(0, maxSlot);
            spawnOneColorOrb(i);

        }
    }

    void spawnOneColorOrb(int n)
    {
        GameObject co = Instantiate(colorOrb);
        co.GetComponent<ColorOrb>().mainGame = this;
        co.GetComponent<ColorOrb>().setColor(n);
        int r = Random.Range(1, maxSlot-1);
        co.transform.position = new Vector3(r * GameScreen.worldWidth / maxSlot,
            GameScreen.worldHeight * 0.9f);
        co.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * co.GetComponent<ColorOrb>().speed;
    }

    IEnumerator flashingSelected()
    {
        while (playing)
        {
            SpriteRenderer s = (SpriteRenderer)Instantiate(backgrounds[selectedBG]);
            s.sortingOrder = -101;
            backgrounds[selectedBG].sprite = borderBG[selectedBG];
            yield return new WaitForSeconds(0.1f);
            backgrounds[selectedBG].sprite = whiteBG[selectedBG];
            yield return new WaitForSeconds(0.5f);

            Destroy(s.gameObject);
        }
    }

    IEnumerator rainingColorOrbs()
    {
        while (playing)
        {
            int r = Random.Range(0, ColorOrb.colors.Length);
            spawnOneColorOrb(r);
            yield return new WaitForSeconds(timeInterval);
        }
    }

    public void eatColorOrb(ColorOrb co)
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (i == selectedBG)
            {
                backgrounds[i].sortingOrder = -99;
            }
            else
            {
                backgrounds[i].sortingOrder = -50;
            }
        }
        co.StartCoroutine(co.colorAnimation());
        spawnOneColorOrb(co.nColor);
    }

    public void updateColors(int nColor)
    {
        backgrounds[selectedBG].sprite = whiteBG[selectedBG];
        backgrounds[selectedBG].color = ColorOrb.colors[nColor];
        selectedBG++;
        selectedBG %= backgrounds.Length;
    }

    public void submitColors()
    {
        if (chkColors())
        {
            Debug.Log("yeah");
            winScreen.enabled = true;
            Time.timeScale = 0.0f;
            
        }
        else
        {
            StartCoroutine(returnToWhite());
            retryScreen.enabled = true;
            Time.timeScale = 0.0f;
        }
    }

    public bool chkColors()
    {
        for (int i = 0; i < 2; i++)
        {
            int j = 0;
            for (j = 0; j < backgrounds.Length; j++)
            {
                if (!backgrounds[j].color.Equals(ColorOrb.colors[answers[i, j]]))
                {
                    break;
                }
            }
            if (j == backgrounds.Length)
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerator returnToWhite()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].sprite = originalBG[i];
            backgrounds[i].color = Color.grey;
        }
        selectedBG = 0;
        int count = 0;
        while (count++ < 20)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                Color c = backgrounds[i].color;
                c = new Color(c.r * 1.1f,c.g*1.1f,c.b*1.1f,c.a);
                backgrounds[i].color = c;
            }
            yield return new WaitForSeconds(0.005f);
        }
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
