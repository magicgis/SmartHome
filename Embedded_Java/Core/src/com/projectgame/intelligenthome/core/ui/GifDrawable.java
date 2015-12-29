package com.projectgame.intelligenthome.core.ui;

import javafx.application.Platform;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class GifDrawable extends Drawable {
    private Image[] images;
    private float fps;
    private int currentIndex;
    private boolean running;

    public GifDrawable(Image[] images, float fps){
        this.images = images;
        this.fps = fps;
        currentIndex = 0;
        running = false;
    }

    public void start(){
        if(running)
            return;

        running = true;
        Thread t = new Thread(){
           public void run(){
               runGif();
           }
        };
        t.start();
    }

    public void stop(){
        running = false;
    }

    private void runGif(){
        while(running){
            Platform.runLater(new Runnable() {
                @Override
                public void run() {
                    Display.getInstance().draw();
                    currentIndex = (currentIndex + 1) % images.length;
                }
            });
            try {
                Thread.sleep((long) (1000 / fps));
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    @Override
    protected void draw(GraphicsContext context, Drawable parent) {
        context.drawImage(images[currentIndex],
                getTransform().getRect().getPosition().getX(),
                getTransform().getRect().getPosition().getY(),
                getTransform().getRect().getSize().getX(),
                getTransform().getRect().getSize().getY());
    }
}
