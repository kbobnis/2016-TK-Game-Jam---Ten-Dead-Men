using UnityEngine;
using System;

class PlayerLife : MonoBehaviour
{
    public float TimeLeft = 10.0f;

    public GameObject Game;
    private GameController GC;

    void Start()
    {
	GC = Game.GetComponent<GameController>();

	if(GC == null)
	{
	    throw new Exception("No spawner controller attached to Spawner object");
	}
    }
    
    void Update()
    {
	TimeLeft -= Time.deltaTime;
	if(TimeLeft <= 0)
	{
	    Die();
	}
    }

    void Die()
    {
	GC.SpawnNew();
	
	Destroy(gameObject);
    }
}
