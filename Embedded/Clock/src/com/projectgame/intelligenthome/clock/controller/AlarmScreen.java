package com.projectgame.intelligenthome.clock.controller;


import com.projectgame.intelligenthome.clock.model.Main;
import com.projectgame.intelligenthome.clock.model.TimeManager;
import com.projectgame.intelligenthome.core.Networking;
import com.projectgame.ixp.IXPFile;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class AlarmScreen {
    public static String alarmName = "!ALARM!";

    private static AlarmScreen instance;
    public static AlarmScreen getInstance(){
        if(instance == null)
            instance = new AlarmScreen();

        return instance;
    }

    private com.projectgame.intelligenthome.clock.view.AlarmScreen screen;

    public AlarmScreen(){
        screen = new com.projectgame.intelligenthome.clock.view.AlarmScreen(this);
    }

    public com.projectgame.intelligenthome.clock.view.AlarmScreen getScreen(){
        return screen;
    }

    public void onActivate(){
        screen.setTime(TimeManager.getInstance().getCurrentTime().getHours(), TimeManager.getInstance().getCurrentTime().getMinutes());
        screen.setAlarmName(alarmName);
    }
    public void onDeactivate(){}
}
