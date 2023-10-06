using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    public class onTickEventArgs : EventArgs{
        public int tick;
    }

    public static event EventHandler<onTickEventArgs> OnTick;
    public static event EventHandler<onTickEventArgs> OnTick_5;

    private const float TICK_TIMER_MAX = .2f;

    private int tick;
    private float tickTimer;

    void Awake(){
        tick = 0;
    }

    void Update()
    {
        tickTimer += Time.deltaTime;
        if(tickTimer >= TICK_TIMER_MAX) {
            tickTimer -= TICK_TIMER_MAX;
            tick++;
        }
        if(OnTick != null) OnTick(this,new onTickEventArgs {tick = tick});

        if(tick % 5 == 0){
            if(OnTick_5 != null) OnTick_5(this,new onTickEventArgs {tick = tick});
        }

        
    }
}
