package com.projectgame.intelligenthome.embeddedsystem.model;

import com.projectgame.intelligenthome.core.*;
import com.projectgame.intelligenthome.embeddedsystem.view.AppLauncher;
import com.projectgame.intelligenthome.embeddedsystem.view.BootScreen;
import com.projectgame.intelligenthome.embeddedsystem.view.SingleApplicationDrawable;
import com.projectgame.intelligenthome.core.ui.CanvasDrawable;
import com.projectgame.intelligenthome.core.ui.Display;
import com.projectgame.intelligenthome.core.ui.Drawable;
import com.projectgame.intelligenthome.core.ui.IDrawableProvider;
import com.sun.media.jfxmediaimpl.platform.Platform;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class Main {
    public static CanvasDrawable canvas;
    public static SingleApplicationDrawable singleApplicationDrawable;
    public static AppLauncher appLauncher;

    public static void main(String[] args){
        canvas = new CanvasDrawable();
        Display.initDisplay(800, 600, new IDrawableProvider() {
            @Override
            public Drawable getDrawable() {
                return canvas;
            }
        });

        Thread t = new Thread(){
            public void run(){
                boot();
            }
        };
        t.start();

        Display.run();

        ApplicationManager.getInstance().closeAllApps();
        BackgroundProcessManager.getInstance().stopAllProcesses();
        Networking.getInstance().close();
    }

    private static void boot(){
        try {
            Thread.sleep(1000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        BootScreen bootScreen = new BootScreen();
        canvas.addDrawable(bootScreen);

        try {
            Thread.sleep(1000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        Settings.getInstance();

        Networking.getInstance();

        ApplicationManager.getInstance();
        BackgroundProcessManager.getInstance();
        FontCache.init();
        ImageCache.init();

        singleApplicationDrawable = new SingleApplicationDrawable();

        bootScreen.finishUp();
        canvas.clearDrawables();
        appLauncher = new AppLauncher();
        canvas.addDrawable(appLauncher);
        Display.getInstance().draw();
    }
}
