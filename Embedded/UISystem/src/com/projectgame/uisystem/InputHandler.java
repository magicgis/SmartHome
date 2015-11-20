package com.projectgame.uisystem;

/**
 * Created by Beppo on 06.10.2015.
 */
public abstract class InputHandler {
    private Rect rect;

    public InputHandler(Rect rect){
        this.rect = rect;
    }

    public Rect getRect(){
        return rect;
    }

    public abstract void onMouseClicked();
    public abstract void onMouseReleased(boolean inRect);
}
