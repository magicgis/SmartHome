package com.projectgame.intelligenthome.clock.view;

import com.projectgame.intelligenthome.core.AppScreen;
import com.projectgame.intelligenthome.core.IAppScreenBarButtonHandler;
import com.projectgame.uisystem.Drawable;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

import java.util.Hashtable;

/**
 * Created by Beppo-Laptop on 11/12/2015.
 */
public class TimerScreen extends AppScreen {
    private com.projectgame.intelligenthome.clock.controller.TimerScreen controller;

    public TimerScreen(com.projectgame.intelligenthome.clock.controller.TimerScreen controller) {
        super("Alarms", Color.PINK);
        this.controller = controller;
    }

    @Override
    public void onActivate() {
        controller.onActivate();
    }
    @Override
    public void onDeactivate() {
        controller.onDeactivate();
    }

    @Override
    public Hashtable<String, IAppScreenBarButtonHandler> getBarButtons() {
        return null;
    }
    @Override
    protected void draw(GraphicsContext context, Drawable parent) {

    }
}
