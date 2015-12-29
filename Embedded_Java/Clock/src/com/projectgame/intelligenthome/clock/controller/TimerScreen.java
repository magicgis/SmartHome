package com.projectgame.intelligenthome.clock.controller;

import java.util.Timer;

/**
 * Created by Beppo-Laptop on 11/12/2015.
 */
public class TimerScreen {
    private static TimerScreen instance;

    public static TimerScreen getInstance(){
        if(instance == null)
            instance = new TimerScreen();

        return instance;
    }

    private com.projectgame.intelligenthome.clock.view.TimerScreen screen;

    public TimerScreen(){
        screen = new com.projectgame.intelligenthome.clock.view.TimerScreen(this);
    }

    public com.projectgame.intelligenthome.clock.view.TimerScreen getScreen(){
        return screen;
    }

    public void onActivate(){}
    public void onDeactivate(){}
}
