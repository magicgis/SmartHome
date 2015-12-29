package com.projectgame.intelligenthome.core;

import javafx.scene.image.Image;
import javafx.stage.Screen;

import java.util.ArrayList;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public abstract class Application {
    private static IAppScreenListener appScreenListener;

    public static void setAppScreenListener(IAppScreenListener appScreenListener){
        Application.appScreenListener = appScreenListener;
    }

    public static void switchScreen(AppScreen screen){
        if(appScreenListener == null)
            return;

        appScreenListener.onScreenChange(screen);
    }

    public abstract AppScreen getMainScreen();
    public abstract void onApplicationClose();
    public abstract ArrayList<BackgroundProcess> getBackgroundProcesses();
}
