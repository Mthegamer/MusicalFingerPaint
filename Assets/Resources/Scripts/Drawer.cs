using UnityEngine;
using System.Collections;
using Leap;

public class Drawer : MonoBehaviour {

  public Automation canvas;

  private const float THUMB_TRIGGER_DISTANCE = 0.04f;
  private bool drawing_ = false;

	void Update () {
    Hand leap_hand = GetComponent<HandModel>().GetLeapHand();

    bool draw_trigger = false;
    Vector3 thumb_tip = leap_hand.Fingers[0].JointPosition(Finger.FingerJoint.JOINT_TIP).ToUnityScaled();

    for (int i = 1; i < 5 && !draw_trigger; ++i) {
      for (int j = 0; j < 4 && !draw_trigger; ++j) {
        Vector3 difference = leap_hand.Fingers[i].JointPosition((Finger.FingerJoint)(j)).ToUnityScaled() -
          thumb_tip;
        if (difference.magnitude < THUMB_TRIGGER_DISTANCE && leap_hand.Confidence > 0.5f)
          draw_trigger = true;
      }
    }
    if (drawing_ && draw_trigger)
      canvas.NextPoint(transform.TransformPoint(leap_hand.Fingers[1].TipPosition.ToUnityScaled()));
    drawing_ = draw_trigger;
  }
}
