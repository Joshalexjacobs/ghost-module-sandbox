using System.Collections;
using UnityEngine;

public class CoroutineService : MonoBehaviour {

  private static MonoBehaviour instance;

  public static MonoBehaviour Instance {
    get {
      if (instance == null) {
        GameObject g = new GameObject("CoroutineService");
        instance = g.AddComponent<CoroutineService>();
      }
			
      return instance;
    }
  }

  new public static Coroutine StartCoroutine(IEnumerator routine) {
    return Instance.StartCoroutine(Wrapper(routine));
  }

  new public static void StopCoroutine(IEnumerator coroutine) {
    if (coroutine != null) {
      Instance.StopCoroutine(coroutine);
    }
  }

  private static IEnumerator Wrapper(IEnumerator routine) {
    while (true) {
      if (routine.MoveNext() == false) {
        break;
      }

      var current = routine.Current;
      yield return current;
    }
		
    yield break;
  }
	
}