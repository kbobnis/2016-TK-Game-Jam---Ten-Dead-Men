using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{

	void OnEnable() {
	}

    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel("backup");
		}
		else if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
			Debug.Break();
		}
    }
}
