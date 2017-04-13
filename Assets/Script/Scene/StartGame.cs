using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartGame : MonoBehaviour
{
    private Button startButton;

	// Use this for initialization
	void Start () {
        startButton = GameObject.Find("Canvas/BG/Button").GetComponent<Button>();
        startButton.onClick.AddListener(StartClick);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void StartClick()
    {
        CharacterType type = GameManager.Instance.CharacterType;
        if (type== CharacterType.none)
        {
            GameManager.Instance.LoadScene(SceneNameDefine.CreateCharacter);
        }
        else
        {
            GameManager.Instance.LoadScene(SceneNameDefine.Scene_Forest);
        }
    }
}
