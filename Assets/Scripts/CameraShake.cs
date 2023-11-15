using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
	private CinemachineVirtualCamera CinemachineVirtualCamera;
	[SerializeField] private float ShakeIntensity = 10f;
	[SerializeField] private float ShakeTime = 0.5f;
	[SerializeField] private float timer;
	private CinemachineBasicMultiChannelPerlin _cbmcp;

	private void Awake()
	{
		CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();	
	}

	private void Start()
	{
		StopShake();
	}

	public void ShakeCamera()
	{
		CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		_cbmcp.m_AmplitudeGain = ShakeIntensity;

		timer = ShakeTime;
	}

	void StopShake()
	{
		CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		_cbmcp.m_AmplitudeGain = 0f;
		timer = 0;
	}

	private void Update()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			ShakeCamera();
		}

		if(timer >0)
		{
			timer -= Time.deltaTime;
			if(timer < 0)
			{
				StopShake(); 
			}
		}
	}
}
