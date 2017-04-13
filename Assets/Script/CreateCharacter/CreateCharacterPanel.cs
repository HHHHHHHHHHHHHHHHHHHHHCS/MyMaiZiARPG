using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateCharacterPanel : MonoBehaviour
{
    private Button warriorButton;
    private Button archerButton;
    private Button createButton;
    private InputField nameInput;
    private Transform mRoot;
    private GameObject warriorObj;
    private GameObject archerObj;
    private CharacterType selectRole = CharacterType.none;

    void Start()
    {
        mRoot = transform;
        warriorButton = mRoot.Find("WarriorButton").GetComponent<Button>();
        archerButton = mRoot.Find("ArcherButton").GetComponent<Button>();
        createButton = mRoot.Find("CreateButton").GetComponent<Button>();
        nameInput = mRoot.Find("Name/NameInput").GetComponent<InputField>();
        warriorButton.onClick.AddListener(WarriorClick);
        archerButton.onClick.AddListener(ArcherClick);
        createButton.onClick.AddListener(CreateClick);
    }


    void WarriorClick()
    {
        selectRole = CharacterType.warrior;
        HideObj(archerObj);
        ShowObj(ref warriorObj, GameDefine.warriorPath);
    }

    void ArcherClick()
    {
        selectRole = CharacterType.archer;
        HideObj(warriorObj);
        ShowObj(ref archerObj, GameDefine.archerPath);
    }

    void HideObj(GameObject obj)
    {
        if (obj != null && obj.activeSelf)
        {
            obj.SetActive(false);
        }
    }

    void ShowObj(ref GameObject obj,string path)
    {
        if (obj == null)
        {
            GameObject loadObj = GameManager.Instance.LoadResources<GameObject>(path);
            obj = Instantiate(loadObj);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
        }
        else if (!obj.activeSelf)
        {
            obj.SetActive(true);
        }
    }

    void CreateClick()
    {
        if(selectRole== CharacterType.none)
        {
            Debug.Log("请选择职业");
            return;
        }
        if (nameInput.name == string.Empty)
        {
            Debug.Log("请输入名字");
            return;
        }
        MyJsonManager.UpdateData(PlayerSaveDefine.playerSaveName, PlayerSaveDefine.key_playerName, nameInput.name);
        MyJsonManager.UpdateData(PlayerSaveDefine.playerSaveName, PlayerSaveDefine.key_playerRole, ((int)selectRole).ToString());
        SceneManager.LoadScene("Scene_Forest");
    }


}
