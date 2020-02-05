using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField]int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start()
    {
        do{
            yield return StartCoroutine(SpawnAllWaves());
        }
        while(looping);  
    }

    private IEnumerator SpawnAllWaves(){
        for(int waveindex = startingWave; waveindex < waveConfigs.Count; waveindex++){
            var currentWave = waveConfigs[waveindex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    /*
    //* The function takes a waveconfig file and instanciates a new enemy as long as the max number of
    //* is not reached.
    //* then the function sets the right wave in the enemy pathing and makes the coroutine wait 
    //* before spawning a new enemy according to the wave configuration.
    */

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig){   
       for(int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++){
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
       }
    }
}
