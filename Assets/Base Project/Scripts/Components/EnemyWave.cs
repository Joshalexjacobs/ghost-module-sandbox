using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWave : MonoBehaviour {

  public float waveDelay;
  
  public List<GameObject> groups;
  public List<float> groupSpawnDelay;

  public List<int> checkpointPositions;

  public GameObject bossGroup;

  public List<GameObject> thingsToInstantiate;
  public GameObject boundariesToInstantiate;

  public GameObject nextParallaxGroup;

  public CheckpointManager checkpointManager;

  private List<GameObject> groupsSpawned = new List<GameObject>();

  private FadeToBlack _fadeToBlack;
  private ParallaxWaveManager _parallaxWaveManager;
  
  private void Awake() {
    if (groups.Count != groupSpawnDelay.Count) {
      throw new Exception("Error encountered starting EnemyWave: Groups and GroupSpawnDelay lists must be the exact same length.");
    }

    _fadeToBlack = GameObject.FindObjectOfType<FadeToBlack>();
    _parallaxWaveManager = GameObject.FindObjectOfType<ParallaxWaveManager>();
  }

  public IEnumerator IterateAndSpawnGroups(float transitionDelay) {
    for (int i = 0; i < groups.Count; i++) {
      GameObject group = Instantiate(groups[i], transform.position, Quaternion.identity);
      
      if (group != null) groupsSpawned.Add(group);
      yield return new WaitForSeconds(groupSpawnDelay[i]);
    }

    if (bossGroup) {
      GameObject bossGroupObj = Instantiate(bossGroup, transform.position, Quaternion.identity);

      List<Enemy> list = bossGroupObj.GetComponentsInChildren<Enemy>().Where((enemy) => { return enemy.isABoss; }).ToList();

      Debug.Log("list.Count");
      Debug.Log(list.Count);
      
      if (list.Count > 0) {
        Enemy boss = list[0];
        yield return new WaitUntil(() => { return boss == null; });
      }
    }

    yield return new WaitForSeconds(transitionDelay / 2);
    
    yield return StartCoroutine(_fadeToBlack.FadeIn());
    
    _parallaxWaveManager.ChangeParallax(nextParallaxGroup);
    
    InstantiateThings();
    InstantiateBoundaries();
    DestroyRemainingGroups();
    
    yield return new WaitForSeconds(transitionDelay);
    
    yield return StartCoroutine(_fadeToBlack.FadeOut());
  }
  
  private void InstantiateThings() {
    thingsToInstantiate.ForEach(thing => { Instantiate(thing, Vector3.zero, Quaternion.identity); });
  }
  
  private void InstantiateBoundaries() {
    if (boundariesToInstantiate != null) {
      GameObject playerBoundaries = GameObject.FindGameObjectWithTag("PlayerBoundaries");

      List<Transform> oldBoundaries = playerBoundaries.GetComponentsInChildren<Transform>().ToList();
      oldBoundaries.RemoveAt(0);

      if (oldBoundaries.Count > 0) {
        oldBoundaries.ForEach(boundaries => {
          Destroy(boundaries.gameObject);
        });
      }
      
      GameObject newBoundaries = Instantiate(boundariesToInstantiate, Vector3.zero, Quaternion.identity);
      newBoundaries.transform.parent = playerBoundaries.transform;
    }
  }
  
  private void DestroyRemainingGroups() {
    groupsSpawned.ForEach(group => {
      Destroy(group);
    });
  }
  
}
