using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
	public Text IntroText;

	void OnEnable() {
		Debug.Log("on awake! text is " + Game.LoadSceneText);
		IntroText.text = Game.LoadSceneText;
	}

    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel("scenka");
		}
		else if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
			Debug.Break();
		}
    }
}
