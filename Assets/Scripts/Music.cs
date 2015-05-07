using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	// Use this for initialization
    private static Music instance = null;
    public AudioSource tmpSfx;
    public static AudioSource sfx;
    public static Music Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            sfx = tmpSfx;
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
