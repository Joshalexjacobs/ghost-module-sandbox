using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForAll : CustomYieldInstruction {
	
  private bool _keepWaiting;
	
  private HashSet<int> completedRoutines = new HashSet<int>();
  private List<IEnumerator> routines = new List<IEnumerator>();

  public WaitForAll(params IEnumerator[] routines) {
    this.routines = new List<IEnumerator>(routines);
    for (int i = 0; i < routines.Length; i++) {
      CoroutineService.StartCoroutine(WaitForRoutine(i));
    }
  }

  private IEnumerator WaitForRoutine(int index) {
    yield return CoroutineService.StartCoroutine(routines[index]);
    completedRoutines.Add(index);
  }
	
  public override bool keepWaiting {
    get { return completedRoutines.Count < routines.Count; }
  }
}