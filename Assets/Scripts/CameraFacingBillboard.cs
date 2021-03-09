using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;


public class CameraFacingBillboard : NetworkBehaviour
{
    static Camera m_Camera;

    private void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    /*public void OnStartLocalPlayer()
    {
        Debug.Log("called");
        if (GameObject.FindGameObjectWithTag("Player") == null) { Debug.Log("its not player find"); }
        m_Camera = GameObject.FindGameObjectWithTag("player").GetComponentInChildren<Camera>();

    }
    public void OnStartHost()
    {
        Debug.Log("Host has started");
    }
    public void OnStartClient(NetworkClient client)
    {
        Debug.Log("Client has started");
    }*/
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            m_Camera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
        }
               transform.LookAt(transform.position + 
            m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }
}