using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{

	public InputDeviceCharacteristics controllerCharacteristics;
	public GameObject controllerModel;
	public ClickManager clickm;
	private InputDevice targetDevice;
	private GameObject spawnedController;
	private Animator animatorController;

	public bool debugPrintON;

    // Start is called before the first frame update
    void Start()
    {
		List<InputDevice> devices = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

		if (debugPrintON) {
			foreach (var item in devices) {
				Debug.Log(item.name+item.characteristics);
			}
		}

		if (devices.Count>0) {
			targetDevice=devices[0];
			spawnedController=Instantiate(controllerModel, transform);
			animatorController=spawnedController.GetComponent<Animator>();
		}
    }

	void UpdateHandAnimation() {
		if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) animatorController.SetFloat("Trigger", triggerValue);
		else animatorController.SetFloat("Trigger", 0);
		if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) animatorController.SetFloat("Grip", gripValue);
		else animatorController.SetFloat("Grip", 0);
	}

	// Update is called once per frame
	void Update()
    {
		if (debugPrintON) {
			targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
			if (primaryButtonValue) Debug.Log("Pressing Primary Button");
			//targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
			//if (triggerValue>0.1f) Debug.Log("Trigger pressed "+triggerValue);
			targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
			if (primary2DAxisValue!=Vector2.zero) Debug.Log("Primary Touchpad "+primary2DAxisValue);
		}
		targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
		if (triggerValue>0.1f && clickm) clickm.LeftClick();
		UpdateHandAnimation();
    }
}
