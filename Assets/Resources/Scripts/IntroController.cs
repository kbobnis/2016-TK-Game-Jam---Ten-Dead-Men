using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{

	void OnEnable() {
	}

    void Update()
    {
		//if(Input.GetKeyDown(KeyCode.Return))
		if(Input.GetAxis("go") != 0)
		{
			Application.LoadLevel("backup");
		}
		//else if(Input.GetKeyDown(KeyCode.Escape))
		else if(Input.GetAxis("exit") != 0)
		{
			Application.Quit();
			Debug.Break();
		}
    }
}
