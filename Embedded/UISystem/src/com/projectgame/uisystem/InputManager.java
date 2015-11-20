package com.projectgame.uisystem;

import java.util.ArrayList;

/**
 * Created by Beppo on 06.10.2015.
 */
public class InputManager {
    private static InputManager instance;
    public static InputManager getInstance(){
        return instance;
    }

    private ArrayList<InputHandler> managers;

    public InputManager(){
        instance = this;
        managers = new ArrayList<>();
    }

    public void registerManager(InputHandler manager){
        managers.add(manager);
    }

    public void clear(){
        managers.clear();
    }

    public void onMouseClicked(Vector2 position){
        ArrayList<InputHandler> tmp = (ArrayList<InputHandler>)managers.clone();
        for(InputHandler manager : tmp){
            if(position.getX() < manager.getRect().getPosition().getX())
                continue;
            if(position.getX() > manager.getRect().getPosition().getX() + manager.getRect().getSize().getX())
                continue;

            if(position.getY() < manager.getRect().getPosition().getY())
                continue;
            if(position.getY() > manager.getRect().getPosition().getY() + manager.getRect().getSize().getY())
                continue;

            manager.onMouseClicked();
        }
    }
    public void onMouseReleased(Vector2 position){
        ArrayList<InputHandler> tmp = (ArrayList<InputHandler>)managers.clone();
        for(InputHandler manager : tmp) {
            boolean inside = true;

            if(position.getX() < manager.getRect().getPosition().getX())
                inside = false;
            if(position.getX() > manager.getRect().getPosition().getX() + manager.getRect().getSize().getX())
                inside = false;

            if(position.getY() < manager.getRect().getPosition().getY())
                inside = false;
            if(position.getY() > manager.getRect().getPosition().getY() + manager.getRect().getSize().getY())
                inside = false;

            manager.onMouseReleased(inside);
        }
    }
}
