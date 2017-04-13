using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager
{
    private static EntityManager _instance;
    public List<Enemy> enemyList = new List<Enemy>();

    public static EntityManager Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new EntityManager();
            }
            return _instance;
        }
    }

    public T CreateEntity<T>(string entityName,Vector3 pos,Vector3 dir) where T:Entity
    {
        GameObject objPrefab = GameManager.Instance.LoadResources<GameObject>(entityName);
        GameObject obj = Object.Instantiate(objPrefab);
        Entity entity = obj.GetComponent<Entity>();
        if(entity==null)
        {
            obj.AddComponent<Entity>();
        }
        entity.InitEntity(pos, dir);
        return entity as T;
    }

    public Enemy CreateEnemy(string entityName,Vector3 pos,Vector3 dir)
    {
        Enemy enemy = CreateEntity<Enemy>(entityName, pos, dir);
        enemyList.Add(enemy);
        return enemy;
    }

    public bool RemoveEnemy(Enemy enemy)
    {
        if(!enemyList.Contains(enemy))
        {
            return false;
        }
        enemyList.Remove(enemy);
        return true;
    }
}
