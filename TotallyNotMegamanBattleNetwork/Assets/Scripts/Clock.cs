using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    float CurrentTime = 0;
    int tickCount= 0;
    [SerializeField] float tickTime = 0.1f;
    public float DeltaTime { get { return tickTime; } }
    static private Clock instance = null;
    static public Clock Instance { get { return instance; } }

    public delegate void ClockDelegate();
    public ClockDelegate OnTick;
    public ClockDelegate Every2Ticks;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= tickTime)
        {
            tickCount++;
            CurrentTime = 0;
            if(OnTick!=null)
                OnTick();
            if (tickCount > 1)
            {
                tickCount = 0;
                if (Every2Ticks != null)
                    Every2Ticks();
            }
        }
    }
    public void Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
