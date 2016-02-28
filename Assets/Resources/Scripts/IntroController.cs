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
		if(Input.anyKey)
		{
			Application.LoadLevel("scenka");
		}
    }
}
