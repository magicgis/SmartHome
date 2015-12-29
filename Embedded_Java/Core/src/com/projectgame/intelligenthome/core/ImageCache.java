package com.projectgame.intelligenthome.core;

import javafx.scene.image.Image;

import java.util.Hashtable;

/**
 * Created by Beppo-Laptop on 10/9/2015.
 */
public class ImageCache {
    private static Hashtable<ImageType, Image> imageCache;

    public static void init(){
        imageCache = new Hashtable<>();
        cacheImage(ImageType.APP_BAR, "/com/projectgame/intelligenthome/core/res/Png/AppBar.png");
        cacheImage(ImageType.RAISED_BUTTON, "/com/projectgame/intelligenthome/core/res/Png/RaisedButton.png");
        cacheImage(ImageType.RAIDED_BUTTON_CLICK, "/com/projectgame/intelligenthome/core/res/Png/RaisedButton_Click.png");
    }

    public static void cacheImage(ImageType name, String path){
        if(!imageCache.containsKey(name)){
            imageCache.put(name, new Image(path));
        }
    }

    public static Image getImage(ImageType name){
        return imageCache.get(name);
    }
}
