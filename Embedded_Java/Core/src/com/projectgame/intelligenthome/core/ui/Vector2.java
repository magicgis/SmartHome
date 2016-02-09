package com.projectgame.intelligenthome.core.ui;

/**
 * Created by Beppo on 06.10.2015.
 */
public class Vector2 {
    private int x;
    private int y;

    public Vector2(){
        x = 0;
        y = 0;
    }
    public Vector2(int x, int y){
        this.x = x;
        this.y = y;
    }
    public Vector2(Vector2 source){
        this.x = source.getX();
        this.y = source.getY();
    }

    public void set(Vector2 source){
        x = source.getX();
        y = source.getY();
    }

    public int getX(){
        return x;
    }
    public int getY(){
        return y;
    }

    public void setX(int x){
        this.x = x;
    }
    public void setY(int y){
        this.y = y;
    }
}
