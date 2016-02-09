package com.projectgame.intelligenthome.clock.model;

import com.projectgame.intelligenthome.clock.controller.LoadingScreen;
import com.projectgame.intelligenthome.core.AppScreen;
import com.projectgame.intelligenthome.core.Application;
import com.projectgame.intelligenthome.core.BackgroundProcess;

import java.io.IOException;
import java.util.ArrayList;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class App extends Application {
    private LoadingScreen loadingScreen;

    @Override
    public AppScreen getMainScreen() {
        if(loadingScreen == null)
            loadingScreen = new LoadingScreen();

        return loadingScreen.getScreen();
    }

    @Override
    public void onApplicationClose() {}

    @Override
    public ArrayList<BackgroundProcess> getBackgroundProcesses() {
        ArrayList<BackgroundProcess> processes = new ArrayList<>();
        processes.add(new AlarmManager());
        processes.add(TimeManager.getInstance());
        return processes;
    }
}
