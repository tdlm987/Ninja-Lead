using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRacingBoyTrigger : TrungMonoBehaviour
{
    [SerializeField] private RacingBoySpawner racingBoySpawner;
    [SerializeField] private List<Transform> listPositionForRacingBoys;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRacingBoyTrigger();
    }
    protected virtual void LoadRacingBoyTrigger()
    {
        if (racingBoySpawner != null) return;
        racingBoySpawner = FindAnyObjectByType<RacingBoySpawner>().GetComponent<RacingBoySpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if(player != null && !player.IsBoost)
        {
            this.SpawRcBoyTrigger();
        }
    }
    private void SpawRcBoyTrigger()
    {
        int randomRcBoy = Random.Range(1,3);
        Debug.Log(randomRcBoy);
        if (randomRcBoy == 0) return;
        Vector3 positionLeft = listPositionForRacingBoys[0].position;
        Vector3 positionRight = listPositionForRacingBoys[1].position;
        StartCoroutine(racingBoySpawner.SpawnRacingBoyRoutine(randomRcBoy, positionLeft, positionRight));
    }
}
