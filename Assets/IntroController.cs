using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    void Update()
    {
	if(Input.anyKey)
	{
	    SceneManager.LoadScene("scenka");
	}
    }
}
