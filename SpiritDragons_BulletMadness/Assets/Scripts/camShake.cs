using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camShake : MonoBehaviour
{
    CinemachineVirtualCamera cVM;
    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeTime;

    float timer;
    CinemachineBasicMultiChannelPerlin _cbmcp;

    // Start is called before the first frame update
    void Awake()
    {
        cVM = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        StopShake();
    }

    public void ShakeCamera()
    {
        _cbmcp = cVM.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = shakeIntensity;
        timer = shakeTime;
    }

    void StopShake()
    {
        _cbmcp = cVM.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0;
        timer = 0;
    }
}
