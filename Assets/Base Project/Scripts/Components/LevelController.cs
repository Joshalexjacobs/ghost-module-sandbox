using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
  
  public List<EnemyWave> waves;
  public List<float> transitionDelay;

  public float delayBetweenWaves = 0f;
  
  public GameObject boss;
  public GameObject bossParallax;

  private CheckpointManager _checkpointManager;
  private ParallaxWaveManager _parallaxWaveManager;
  private FadeToBlack _fadeToBlack;
  private SceneManagerUtil _sceneManagerUtil;
  
  private GameObject _playerBoundaries;
  
  private void Awake() {
    if (waves.Count != transitionDelay.Count) {
      throw new Exception("Error encountered starting LevelController: Waves and TransitionDelay lists must be the exact same length.");
    }

    _checkpointManager = FindObjectOfType<CheckpointManager>();
    _parallaxWaveManager = GameObject.FindObjectOfType<ParallaxWaveManager>();
    _fadeToBlack = GameObject.FindObjectOfType<FadeToBlack>();
    _sceneManagerUtil = GameObject.FindObjectOfType<SceneManagerUtil>();
    
    StartCoroutine(Level());
  }

  IEnumerator Level() {
    for (int i = 0; i < waves.Count; i++) {
      yield return new WaitForSeconds(waves[i].waveDelay);

      EnemyWave waveObj = Instantiate(waves[i], Vector3.zero, Quaternion.identity).GetComponent<EnemyWave>();

      UpdateCheckpointManager(waveObj);

      yield return StartCoroutine(waveObj.IterateAndSpawnGroups(transitionDelay[i]));

      Debug.Log("Wave Completed.");

      yield return new WaitForSeconds(delayBetweenWaves);
    }

    if (boss) {
      yield return StartCoroutine(SpawnBoss());
    }
    
    Debug.Log("game is over");
    
    yield return new WaitForSeconds(5f);
    
    _sceneManagerUtil.ReloadScene();
  }

  private void UpdateCheckpointManager(EnemyWave wave) {
    _checkpointManager.UpdateCurrentWave(wave);
    wave.checkpointManager = _checkpointManager;
  }

  private IEnumerator SpawnBoss() {
    GameObject bossObj = Instantiate(boss, new Vector3(0f, 1f, 0f), Quaternion.identity);
    yield return new WaitUntil(() => { return bossObj == null; });
  }
  
}
