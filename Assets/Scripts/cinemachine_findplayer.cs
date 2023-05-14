using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cinemachine_findplayer : MonoBehaviour
{

    private CinemachineVirtualCamera cinemachineVirtCam;
    public GameObject Player;

    public void Awake()
    {
        cinemachineVirtCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        cinemachineVirtCam.Follow = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
