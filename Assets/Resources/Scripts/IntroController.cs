using UnityEngine;

public class IntroController : MonoBehaviour
{
    void Update()
    {
		if(Input.anyKey)
		{
			Application.LoadLevel("scenka");
		}
    }
}
