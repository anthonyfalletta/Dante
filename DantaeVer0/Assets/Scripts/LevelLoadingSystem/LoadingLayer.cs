using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class LoadingLayer : MonoBehaviour
{
    public event EventHandler OnReloadGrid;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasReleasedThisFrame){
            Debug.Log("Load Room 1");
            var spawnRoom = Resources.Load("LayerPrefabs/Layer1/RoomConfig1");
            Instantiate(spawnRoom);

            OnReloadGrid?.Invoke(this, EventArgs.Empty);

            var spawnWave = Resources.Load("LayerPrefabs/Layer1/WaveConfig1");
            Instantiate(spawnWave);
        }
    }
}
