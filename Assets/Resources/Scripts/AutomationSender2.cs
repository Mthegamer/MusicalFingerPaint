using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class AutomationSender2 : MonoBehaviour {

  [DllImport("LeapSynthEngine2")]
  private static extern void setupSynth();

  [DllImport("LeapSynthEngine2")]
  private static extern void destroySynth();

  [DllImport("LeapSynthEngine2")]
  private static extern void setPitch(float frequency);

  [DllImport("LeapSynthEngine2")]
  private static extern void setWaveform(float amount);

  [DllImport("LeapSynthEngine2")]
  private static extern void setDelayFeedback(float amount);

  private const float SCALE_WIDTH = 1.5f;
  private const int SNAP_POWER = 8;
  private const int SEMITONES_PER_OCTAVE = 12;
  private int[] scale;

	void Start() {
    scale = new int[] {0, 3, 5, 7, 10};
	}
	
	void LateUpdate() {
    Vector3 last_point = GameObject.Find("Canvas").GetComponent<Automation>().GetLastPoint();
    int octave = (int)Mathf.Floor(last_point[1] / SCALE_WIDTH);
    float scale_pos = (last_point[1] / SCALE_WIDTH - octave) * scale.Length;
    int scale_index = (int)scale_pos;

    float pitch_detune = 2.0f * (scale_pos - scale_index) - 1.0f;
    int adjacent_scale_index = (scale.Length + scale_index - 1) % scale.Length;
    int scale_jump = (SEMITONES_PER_OCTAVE + scale[scale_index] - scale[adjacent_scale_index]) % SEMITONES_PER_OCTAVE;
    float scale_pitch = scale[scale_index] + scale_jump * Mathf.Pow(pitch_detune, SNAP_POWER) / 2.0f;

    setPitch(60.0f * Mathf.Pow(2.0f, octave + scale_pitch / SEMITONES_PER_OCTAVE));
    float waveform = Mathf.Clamp(last_point[0] + 4, 0.0f, 9.0f) / 9.0f;
    setWaveform(waveform);
    float feedback = Mathf.Clamp(last_point[2] + 4, 0.0f, 9.0f) / 20.0f + 0.3f;
    setDelayFeedback(feedback);
    Debug.Log(waveform + "     " + feedback);
	}

  void Awake() {
    setupSynth();
  }

  void OnDestroy() {
    destroySynth();
  }
}
