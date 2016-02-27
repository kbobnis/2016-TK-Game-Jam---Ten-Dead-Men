using UnityEngine;

class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}
    
    public float Lives = 10;

    public GameObject StartPoint;

    void Awake()
    {
	if(Instance != null && Instance != this)
	{
	    Destroy(gameObject);
	}

	else if(Instance == null)
	    Instance = this;

//	DontDestroyOnLoad(gameObject);
    }
    
    void Update()
    {
	
    }
    
    public void SpawnNew()
    {
	if(Lives > 0)
	{
	    
	}
	else
	{
	    Fail();
	}
    }

    public void Fail()
    {
	Restart();
    }

    public void Restart()
    {
	// ... Reload scene
    }

    public void Win()
    {
	// Load next
    }
}
