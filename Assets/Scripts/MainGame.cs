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

    public Answers[] ANS;

    public int[][] answers;

    public GameObject[] answerSet;

    public GameObject doneButton;
    public GameObject goButton;
    public GameObject homeButton;
    public GameObject replayButton;
    public GameObject nextLevelButton;

    public SpriteRenderer introScreen;
    public SpriteRenderer winScreen;
    public SpriteRenderer retryScreen;
    public SpriteRenderer blackScreen;

    public bool playing = true;
    public bool animationDone = true;

    public int maxSlot;

    public AudioSource audio;
    public AudioClip winSound;
    public AudioClip loseSound;

	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();
        selectedBG = 0;
        ColorOrb.colors = tmpColor;
        introScreen.enabled = true;
        blackScreen.enabled = true;
        playing = false;
        doneButton.SetActive(false);
        goButton.SetActive(true);
        homeButton.SetActive(true);
        replayButton.SetActive(false);
        nextLevelButton.SetActive(false);
        //spawnInitialColorOrbs();

        audio = GetComponent<AudioSource>();

        answers = new int[ANS.Length][];
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i] = (int[])ANS[i].answers.Clone();
        }
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (retryScreen.enabled)
        //    {
        //        Time.timeScale = 1f;
        //        //retryScreen.enabled = false;
        //        Application.LoadLevel("levelSelection");
        //    }
        //    if (winScreen.enabled)
        //    {
        //        Time.timeScale = 1f;
        //        PlayerPrefs.SetInt("level", 2);
        //        PlayerPrefs.Save();
        //        Application.LoadLevel("levelSelection");
        //    }
        //}
	}

    void SpawnInitialColorOrbs()
    {
        for (int i = 0; i < ColorOrb.colors.Length; i++)
        {
            SpawnOneColorOrb(i);
        }
    }

    void SpawnOneColorOrb(int n)
    {
        GameObject co = Instantiate(colorOrb);
        co.GetComponent<ColorOrb>().mainGame = this;
        co.GetComponent<ColorOrb>().SetColor(n);
        int r = Random.Range(1, maxSlot-1);
        co.transform.position = new Vector3(r * GameScreen.worldWidth / maxSlot,
            GameScreen.worldHeight * 0.9f);
        co.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * co.GetComponent<ColorOrb>().speed;
    }

    public void EatColorOrb(ColorOrb co)
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
        answerSet[selectedBG].GetComponent<SpriteRenderer>().color = ColorOrb.colors[co.nColor];
        co.StartCoroutine(co.ColorAnimation());
        //spawnOneColorOrb(co.nColor);
    }

    public void UpdateColors(int nColor)
    {
        backgrounds[selectedBG].sprite = whiteBG[selectedBG];
        backgrounds[selectedBG].color = ColorOrb.colors[nColor];
        selectedBG++;
        selectedBG %= backgrounds.Length;
    }

    public void SubmitColors()
    {
        Instantiate(Music.sfx);
        if (ChkColors())
        {
            winScreen.enabled = true;
            Music.Instance.GetComponent<AudioSource>().Pause();
            audio.clip = winSound;
            audio.Play();
            //winSound.Play();
            Time.timeScale = 0.0f;
            nextLevelButton.SetActive(true);

            string s = Application.loadedLevelName;
            s = s.Substring(s.Length - 1);
            int x = int.Parse(s)+1;
            PlayerPrefs.SetInt("level", x);
            PlayerPrefs.Save();
        }
        else
        {
            //StartCoroutine(ReturnToWhite());
            Music.Instance.GetComponent<AudioSource>().Pause();
            audio.clip = loseSound;
            audio.Play();
            retryScreen.enabled = true;
            Time.timeScale = 0.0f;
        }

        replayButton.SetActive(true);

        //Move the moving characters here
        MoveAnimateCharacters(1);

        //answers = (int[][])Answers.answers.Clone();
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i] = (int[])ANS[i].answers.Clone();
        }
    }

    public bool ChkColors()
    {
        for (int i = 0; i < answers.Length; i++)
        {
            int j = 0;
            for (j = 0; j < backgrounds.Length; j++)
            {
                int k;
                for (k = 0; k < answers[i].Length; k++)
                {
                    if (answers[i][k] == -1)
                    {
                        continue;
                    }
                    if (backgrounds[j].color.Equals(ColorOrb.colors[answers[i][k]]))
                    {
                        answers[i][k] = -1;
                        break;
                    }
                }
                if (k == answers[i].Length) break;
            }
            if (j == backgrounds.Length) return true;
        }
        return false;
    }

    public void GoButton()
    {
        Instantiate(Music.sfx);
        blackScreen.enabled = false;
        introScreen.enabled = false;
        doneButton.SetActive(true);
        goButton.SetActive(false);
        homeButton.SetActive(false);
        playing = true;

        Time.timeScale = 1.0f;
        StartCoroutine(FlashingSelected());
        StartCoroutine(RainingColorOrbs());
        StartCoroutine(AnimateAnswerSet());
    }

    public void ReplayButton()
    {
        Music.Instance.GetComponent<AudioSource>().Play();
        Instantiate(Music.sfx);
        blackScreen.enabled = false;
        introScreen.enabled = false;
        retryScreen.enabled = false;
        winScreen.enabled = false;
        doneButton.SetActive(true);
        goButton.SetActive(false);
        homeButton.SetActive(false);
        replayButton.SetActive(false);
        nextLevelButton.SetActive(false);
        playing = true;


        Time.timeScale = 1.0f;
        MoveAnimateCharacters(-1);
        //StartCoroutine(FlashingSelected());
        //StartCoroutine(RainingColorOrbs());
        //StartCoroutine(AnimateAnswerSet());
    }

    public void HomeButton()
    {
        Instantiate(Music.sfx);
        Application.LoadLevel("levelSelection");
    }

    public void MoveAnimateCharacters(int dir)
    {
        for (int i = 0; i < answerSet.Length; i++)
        {
            answerSet[i].transform.position += new Vector3(dir*240, dir*240, 0);
            answerSet[i].GetComponent<SpriteRenderer>().sortingOrder = dir*10;
        }
    }

    public void NextLevelButton()
    {
        Instantiate(Music.sfx);
        string s = Application.loadedLevelName;
        s = s.Substring(s.Length - 1);
        int x = int.Parse(s);
        Application.LoadLevel("level" + (x+1));
    }
    /*
    public IEnumerator ReturnToWhite()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].sprite = originalBG[i];
            backgrounds[i].color = Color.grey;
        }
        //selectedBG = 0;
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
    */
    IEnumerator AnimateAnswerSet()
    {
        while (playing)
        {
            GameObject g = answerSet[selectedBG];
            g.transform.position += new Vector3(0, 2f);
            yield return new WaitForSeconds(0.1f);
            g.transform.position += new Vector3(0, 1f);
            yield return new WaitForSeconds(0.1f);
            g.transform.position += new Vector3(0, 0.5f);
            yield return new WaitForSeconds(0.1f);
            g.transform.position += new Vector3(0, -0.5f);
            yield return new WaitForSeconds(0.1f);
            g.transform.position += new Vector3(0, -1f);
            yield return new WaitForSeconds(0.1f);
            g.transform.position += new Vector3(0, -2f);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator FlashingSelected()
    {
        while (playing)
        {
            SpriteRenderer s = (SpriteRenderer)Instantiate(backgrounds[selectedBG]);
            s.sprite = borderBG[selectedBG];
            s.color = Color.white;
            s.sortingOrder = 1;
            s.enabled = true;
            yield return new WaitForSeconds(0.2f);
            s.enabled = false;
            yield return new WaitForSeconds(0.4f);

            Destroy(s.gameObject);
        }
    }

    IEnumerator RainingColorOrbs()
    {
        while (playing)
        {
            int r = Random.Range(0, ColorOrb.colors.Length);
            SpawnOneColorOrb(r);
            yield return new WaitForSeconds(Random.Range(0.5f,2f));
        }
    }
}
