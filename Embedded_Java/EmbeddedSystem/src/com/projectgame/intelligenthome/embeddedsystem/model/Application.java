package com.projectgame.intelligenthome.embeddedsystem.model;

import javafx.scene.image.Image;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class Application {
    private String name;
    private Image image;
    private com.projectgame.intelligenthome.core.Application app;

    public Application(String name, Image image, com.projectgame.intelligenthome.core.Application app){
        this.name = name;
        this.image = image;
        this.app = app;
    }

    public String getName(){
        return name;
    }
    public Image getImage(){
        return image;
    }
    public com.projectgame.intelligenthome.core.Application getApp(){
        return app;
    }
}
