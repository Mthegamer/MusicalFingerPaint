using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class AutomationSender1a : MonoBehaviour {

  [DllImport("LeapSynthEngine1a")]
  private static extern void setupSynth();

  [DllImport("LeapSynthEngine1a")]
  private static extern void destroySynth();

  [DllImport("LeapSynthEngine1a")]
  private static extern void setPitch(float frequency);

  [DllImport("LeapSynthEngine1a")]
  private static extern void setCutoff(float frequency);

  [DllImport("LeapSynthEngine1a")]
  private static extern void setFm(float amount);

	void Start() {
	}
	
	void LateUpdate() {
    Vector3 last_point = GameObject.Find("Canvas").GetComponent<Automation>().GetLastPoint();
    setPitch(25.0f * Mathf.Pow(2.0f, last_point[1]));
    setCutoff(200 * Math.Abs(last_point[0] + 4));
    setFm((last_point[2] + 4) / 3.0f);
	}

  void Awake() {
    setupSynth();
  }

  void OnDestroy() {
    destroySynth();
  }
}
