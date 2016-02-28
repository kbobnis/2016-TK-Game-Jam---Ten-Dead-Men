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
		if(Input.GetAxisRaw("Submit") != 0 || Input.GetAxisRaw("Restart") != 0 || Input.GetAxisRaw("Jump") != 0)
		{
			Application.LoadLevel("scenka");
		}
		else if(Input.GetAxisRaw("Cancel") != 0)
		{
			Application.Quit();
			Debug.Break();
		}
    }
}
