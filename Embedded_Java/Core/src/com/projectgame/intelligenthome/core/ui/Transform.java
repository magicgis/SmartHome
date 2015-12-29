package com.projectgame.intelligenthome.core.ui;

/**
 * Created by Beppo on 06.10.2015.
 */
public class Transform {
    private Rect rect;
    private float rotation;

    public Transform(){
        rect = new Rect();
        rotation = 0;
    }
    public Transform(Transform source){
        rect = source.getRect();
        rotation = source.getRotation();
    }

    public void setRect(Rect rect){
        this.rect = rect;
    }
    public void setRotation(float rotation){
        this.rotation = rotation;
    }

    public Rect getRect(){
        return rect;
    }
    public float getRotation(){
        return rotation;
    }
}
