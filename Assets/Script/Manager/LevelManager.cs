using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public Vector3[] spawnPoints;

    private Player player;

    void Start()
    {
        CharacterType type = GameManager.Instance.CharacterType;
        string playerResName = MyJsonManager.GetData(PlayerSaveDefine.playerSaveName, PlayerSaveDefine.key_playerName);
        if (type == CharacterType.warrior)
        {
            playerResName = GameDefine.warriorPath;
        }
        else
        {
            playerResName = GameDefine.archerPath;
        }

        player = EntityManager.Instance.CreateEntity<Player>(playerResName, new Vector3(-18f, 0.5f, -6f), Vector3.forward);
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 dir = new Vector3(0, Random.Range(0, 360), 0);
            Enemy enemy =EntityManager.Instance.CreateEnemy(GameDefine.enemyPath, spawnPoints[i], dir);
            enemy.InitEnemy(spawnPoints, player.mRoot);
        }
    }
}
