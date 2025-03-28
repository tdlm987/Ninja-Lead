using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RacingBoySpawner : Spawner<RacingBoyobj>
{
    [SerializeField] private RacingBoysManager racingBoysManager;
    [SerializeField] private float time;
    [SerializeField] private float distanceEachRacing;
    private int currentRacingBoy = -1;
    private Vector3 currentPosition = Vector3.zero;
    protected override void Start()
    {
        base.Start();
        for (int i = 0;i<racingBoysManager.racingBoys.Count;i++)
        {
            Transform racingBoyPooling = Spawn(this.racingBoysManager.racingBoys[i]);
        }
    }
    public void SpawnRandomRacingBoy(Vector3 leftPosition,Vector3 rightPosition)
    {
        float x = Random.Range(leftPosition.x, rightPosition.x);
        while (Mathf.Abs(x -  currentPosition.x) < distanceEachRacing)
        {
            x = Random.Range(leftPosition.x, rightPosition.x);
        }
        float y = leftPosition.y;
        float z = leftPosition.z;
        Vector3 randomPosition = new Vector3(x, y, z);
        currentPosition = randomPosition;
        int randomRacingBoy = Random.Range(0, inPoolObjs.Count);
        while (randomRacingBoy == currentRacingBoy) randomRacingBoy = Random.Range(0, inPoolObjs.Count);
        currentRacingBoy = randomRacingBoy;
        RacingBoyobj racingBoyObj = Spawn(this.inPoolObjs[currentRacingBoy], randomPosition);
        racingBoyObj.gameObject.SetActive(true);
    }
    public IEnumerator SpawnRacingBoyRoutine(int amount, Vector3 leftPosition, Vector3 rightPosition)
    {
        for (int i = 0; i < amount; i++)
        {
            this.SpawnRandomRacingBoy(leftPosition, rightPosition);
            yield return new WaitForSeconds(this.time);
        }
    }
}
