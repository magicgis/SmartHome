package com.projectgame.intelligenthome.clock.model;

import com.projectgame.intelligenthome.clock.controller.AlarmScreen;
import com.projectgame.intelligenthome.core.Application;
import com.projectgame.intelligenthome.core.BackgroundProcess;
import com.projectgame.intelligenthome.core.Networking;
import com.projectgame.ixp.IXPFile;
import com.projectgame.ixpcommunication.IIXPMessageHandler;

/**
 * Created by Beppo-Laptop on 11/11/2015.
 */
public class TimeManager extends BackgroundProcess{
    private static TimeManager instance;
    public static TimeManager getInstance(){
        if(instance == null)
            instance = new TimeManager();

        return instance;
    }

    private Time currentTime = new Time();

    public Time getCurrentTime(){
        return currentTime;
    }

    @Override
    public void onProcessStart() {
        try {
            IXPFile file = IXPFile.createNew();
            file.setNetworkFunction("com.projectgame.clock.clock.registertotimeservice");
            file.addInfo("functionName", Main.TIME_SERVICE_FUNCTION);
            Networking.getInstance().getClient().noResponseRequest(file);
        } catch (Exception e) {
            e.printStackTrace();
        }

        Networking.getInstance().getClient().registerHandler(new IIXPMessageHandler() {
            @Override
            public void onMessageReceived(IXPFile message) {
                if(message.getNetworkFunction().equals(Main.TIME_SERVICE_FUNCTION)){
                    int hours = Integer.parseInt(message.getInfos().get("hours"));
                    int minutes = Integer.parseInt(message.getInfos().get("minutes"));
                    int seconds = Integer.parseInt(message.getInfos().get("seconds"));

                    currentTime = new Time(hours, minutes, seconds);
                }
            }
        });
    }

    @Override
    public void onProcessEnd() {

    }
}
