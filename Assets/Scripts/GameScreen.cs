using UnityEngine;
using System.Collections;

public class GameScreen : MonoBehaviour {
    
    public static float worldHeight;
    public static float worldWidth;

	// Use this for initialization
    void Start()
    {
        Camera cam = Camera.main;
        worldHeight = 2f * cam.orthographicSize;
        worldWidth = worldHeight * cam.aspect;
        Debug.Log(worldWidth + " " + worldHeight);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static int SetPositionInWorld(Transform t)
    {
        if (t.position.x < -worldWidth)
        {
            t.position = new Vector3(-worldWidth,t.position.y, 0);
            return -1;
        }
        if (t.position.x > worldWidth)
        {
            t.position = new Vector3(worldWidth, t.position.y, 0);
            return 1;
        }
        if (t.position.y < -worldHeight)
        {
            t.position = new Vector3(t.position.x, -worldHeight, 0);
            return -2;
        }
        if (t.position.y > worldHeight)
        {
            t.position = new Vector3(t.position.x, worldHeight, 0);
            return 2;
        }
        return 0;
    }
}
