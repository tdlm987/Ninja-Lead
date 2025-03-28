using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : TrungMonoBehaviour where T : PoolObj
{
    [SerializeField] protected int spawnCount = 0;
    [SerializeField] protected List<T> inPoolObjs;
    protected Transform _newPosition;

    //Hàm sinh ra object ban đầu
    public virtual Transform Spawn(Transform prefab)
    {
        Transform newObject = Instantiate(prefab);
        newObject.transform.parent = transform;
        this.spawnCount++;
        this.UpdateName(prefab, newObject);
        return newObject;
    }

    //Hàm lấy object từ Pooling
    public virtual T Spawn(T groundPrefab)
    {
        T newObject = this.GetObjFromPool(groundPrefab);
        if(newObject == null)
        {
            newObject = Instantiate(groundPrefab);
            newObject.transform.parent = transform;
            this.spawnCount++;
            this.UpdateName(groundPrefab.transform, newObject.transform);
        }
        return newObject;
    }
    //Hàm set Position cho object vừa lấy từ Pooling
    public virtual T Spawn(T groundPrefab, Vector3 position)
    {
        T newGround = this.Spawn(groundPrefab);
        newGround.transform.position = position;
        return newGround;
    }
    public virtual void Despawn(T obj)
    {
        //Đối tượng chỉ ẩn được khi là một đối tượng MonoBehaviour
        if(obj is MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
            this.AddObjectToPool(obj);
        }
    }
    protected virtual void UpdateName(Transform prefab,Transform newObj)
    {
        newObj.name = prefab.name + "_" + this.spawnCount;
    }
    protected virtual void AddObjectToPool(T obj)
    {
        this.inPoolObjs.Add(obj);
    }
    protected virtual void RemoveObjectFromPool(T obj)
    {
        this.inPoolObjs.Remove(obj);
    }
    protected virtual T GetObjFromPool(T prefab)
    {
        foreach (T inPoolObj in this.inPoolObjs)
        {
            if(prefab.name == inPoolObj.name)
            {
                return inPoolObj;
            }
        }
        return null;
    }
}
