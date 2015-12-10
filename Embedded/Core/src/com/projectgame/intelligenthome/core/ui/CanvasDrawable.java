package com.projectgame.intelligenthome.core.ui;

import javafx.scene.canvas.GraphicsContext;

import java.util.ArrayList;

/**
 * Created by Beppo on 06.10.2015.
 */
public class CanvasDrawable extends Drawable{
    private ArrayList<Drawable> drawables;

    public CanvasDrawable(){
        drawables = new ArrayList<>();
    }

    public void addDrawable(Drawable drawable){
        drawables.add(drawable);
    }
    public void removeDrawable(Drawable drawable){drawables.remove(drawable);}
    public void clearDrawables(){drawables.clear();}

    @Override
    public void draw(GraphicsContext context, Drawable parent) {
        for(Drawable drawable : drawables)
            drawable.redraw(context, this);
    }
}
