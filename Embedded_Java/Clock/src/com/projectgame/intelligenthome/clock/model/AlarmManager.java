package com.projectgame.intelligenthome.clock.model;

import com.projectgame.intelligenthome.clock.controller.AlarmScreen;
import com.projectgame.intelligenthome.core.Application;
import com.projectgame.intelligenthome.core.BackgroundProcess;
import com.projectgame.intelligenthome.core.Networking;
import com.projectgame.intelligenthome.core.IXPFile;
import com.projectgame.intelligenthome.core.IIXPMessageHandler;

/**
 * Created by Beppo-Laptop on 11/11/2015.
 */
public class AlarmManager extends BackgroundProcess {
    @Override
    public void onProcessStart() {
        try {
            IXPFile file = IXPFile.createNew();
            file.setNetworkFunction("com.projectgame.clock.alarm.registertoalarmservice");
            file.addInfo("function", Main.ALARM_SERVICE_FUNCTION);
            Networking.getInstance().getClient().noResponseRequest(file);
        } catch (Exception e) {
            e.printStackTrace();
        }

        Networking.getInstance().getClient().registerHandler(new IIXPMessageHandler() {
            @Override
            public void onMessageReceived(IXPFile message) {
                if(message.getNetworkFunction().equals(Main.ALARM_SERVICE_FUNCTION)){
                    AlarmScreen.alarmName = message.getInfos().get("name");
                    Application.switchScreen(AlarmScreen.getInstance().getScreen());
                }
            }
        });
    }

    @Override
    public void onProcessEnd() {

    }
}
