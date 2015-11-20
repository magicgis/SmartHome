package com.projectgame.intelligenthome.clock.controller;

import com.projectgame.intelligenthome.clock.model.*;
import com.projectgame.intelligenthome.clock.view.*;
import com.projectgame.intelligenthome.clock.view.MainScreen;
import com.projectgame.intelligenthome.core.*;
import com.projectgame.ixp.IXPFile;
import com.projectgame.ixpcommunication.IIXPMessageHandler;
import com.projectgame.ixpcommunication.IXPClient;
import com.projectgame.uisystem.Display;
import com.projectgame.uisystem.Drawable;

import java.io.IOException;

/**
 * Created by Beppo-Laptop on 10/9/2015.
 */
public class LoadingScreen {
    private com.projectgame.intelligenthome.clock.view.LoadingScreen screen;

    private boolean loaded = false;

    public LoadingScreen(){
        screen = new com.projectgame.intelligenthome.clock.view.LoadingScreen(this);
    }

    private void setup(){

    }

    private void load(){
        if(!loaded){
            setup();
            loaded = true;
        }

        Application.switchScreen(com.projectgame.intelligenthome.clock.controller.MainScreen.getInstance().getScreen());
        //Application.switchScreen(AlarmScreen.getInstance().getScreen());
    }

    public AppScreen getScreen() {
        return screen;
    }

    public void onActivate() {
       /* new Thread(){
            @Override
            public void run() {
                load();
            }
        }.start();*/

        Display.getInstance().draw();
        load();
    }

    public void onDeactivate() {

    }
}
