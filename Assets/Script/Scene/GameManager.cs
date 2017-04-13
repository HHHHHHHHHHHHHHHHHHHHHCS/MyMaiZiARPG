using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager _instance;
    private static CharacterType playerRole = CharacterType.none;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    public CharacterType CharacterType
    {
        get
        {
            if (playerRole == CharacterType.none)
            {
                string str = MyJsonManager.GetData(PlayerSaveDefine.playerSaveName, PlayerSaveDefine.key_playerRole);
                if (str.Length != 0)
                {
                    playerRole = (CharacterType)int.Parse(str);
                }
                else
                {
                    playerRole = CharacterType.none;
                }
            }
            return playerRole;
        }
    }

    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public T LoadResources<T>(string path) where T : Object
    {
        Object obj = Resources.Load(path);
        return obj as T;
    }



}
