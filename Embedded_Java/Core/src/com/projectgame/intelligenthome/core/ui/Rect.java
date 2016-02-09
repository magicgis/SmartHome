package com.projectgame.intelligenthome.core.ui;

/**
 * Created by Beppo on 06.10.2015.
 */
public class Rect {
    private Vector2 position;
    private Vector2 size;

    public Rect(){
        position = new Vector2();
        size = new Vector2();
    }
    public Rect(Vector2 position, Vector2 size){
        this.position = position;
        this.size = size;
    }
    public Rect(Rect source){
        position = source.getPosition();
        size = source.getSize();
    }

    public void setPosition(Vector2 position){
        this.position = position;
    }
    public void setSize(Vector2 size){
        this.size = size;
    }

    public Vector2 getPosition(){
        return position;
    }
    public Vector2 getSize(){
        return size;
    }
}
