package com.projectgame.intelligenthome.embeddedsystem.model;

import com.projectgame.intelligenthome.core.BackgroundProcess;

import java.util.ArrayList;

/**
 * Created by Beppo-Laptop on 11/11/2015.
 */
public class BackgroundProcessManager {
    private static BackgroundProcessManager instance;
    public static BackgroundProcessManager getInstance(){
        if(instance == null)
            instance = new BackgroundProcessManager();

        return instance;
    }

    private ArrayList<BackgroundProcess> processes;

    public BackgroundProcessManager(){
        processes = new ArrayList<>();

        for(Application app : ApplicationManager.getInstance().getApps()){
            ArrayList<BackgroundProcess> appProcesses = app.getApp().getBackgroundProcesses();

            if(appProcesses == null)
                continue;

            for(BackgroundProcess appProcess : appProcesses){
                appProcess.onProcessStart();
                processes.add(appProcess);
            }
        }
    }

    public void stopAllProcesses(){
        for(BackgroundProcess process : processes)
            process.onProcessEnd();
    }
}
