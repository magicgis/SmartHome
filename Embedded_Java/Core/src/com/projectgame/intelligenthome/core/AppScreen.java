package com.projectgame.intelligenthome.core;

import com.projectgame.intelligenthome.core.ui.Drawable;
import javafx.scene.paint.Color;

import java.util.Hashtable;


/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public abstract class AppScreen extends Drawable{
    private String screenName;
    private Color background;

    public AppScreen(String screenName, Color background){
        this.screenName = screenName;
        this.background = background;
    }

    public String getName(){
        return screenName;
    }
    public Color getBackground(){
        return background;
    }

    public abstract void onActivate();
    public abstract void onDeactivate();

    public abstract Hashtable<String, IAppScreenBarButtonHandler> getBarButtons();
}
