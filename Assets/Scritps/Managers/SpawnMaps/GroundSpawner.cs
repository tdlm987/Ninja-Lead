
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : Spawner<Ground>
{
    public static GroundSpawner Instance;


    [SerializeField] private MapSpawnManager listMaps;
    [SerializeField] private Transform startSpawnOffset;
    [SerializeField] private Vector3 SpawnMapOffset;

    [SerializeField] private bool _canSpawnMap = true;

    private int currentLevel = -1;
    private Vector3 currentPositionMap;
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    protected override void Awake()
    {
        base.Awake();
        if(Instance == null)
        {
            Instance = this;
        }
    }
    protected override void Start()
    {
        base.Start();
        this.SpawnMapsFirst();
        StartCoroutine(this.SpawnFirstMapRoutine());
    }
    private void SpawnMapsFirst()
    {
        for(int i = 0; i < listMaps.listMaps.Count; i++)
        {
            Transform ground = this.Spawn(this.listMaps.listMaps[i].transform);
        }
    }

    //Sinh ra map đầu tiên một cách ngẫu nhiên
    private IEnumerator SpawnFirstMapRoutine()
    {
        yield return new WaitUntil(() => this.inPoolObjs.Count == this.listMaps.listMaps.Count);
        this.SpawnFirstLevel();
    }
    private void SpawnFirstLevel()
    {
        if (!_canSpawnMap) return;
        int randomLevel = Random.Range(0, this.inPoolObjs.Count);
        this.currentPositionMap = this.startSpawnOffset.position;
        Ground firstLevel = this.Spawn(this.inPoolObjs[randomLevel], currentPositionMap);
        firstLevel.gameObject.SetActive(true);
        currentLevel = randomLevel;
    }
    public void SpawnRandomLevel()
    {
        if (!_canSpawnMap) return;
        int randomNextLevel = -1;
        while (true)
        {
            randomNextLevel = Random.Range(0, this.inPoolObjs.Count);
            if(randomNextLevel != currentLevel) break;
        }
        this.currentPositionMap += this.SpawnMapOffset;
        Ground currentMapLevel = this.Spawn(this.inPoolObjs[randomNextLevel], currentPositionMap);
        currentMapLevel.gameObject.SetActive(true);
        currentLevel = randomNextLevel;
    }
}
