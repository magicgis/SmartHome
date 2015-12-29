package com.projectgame.intelligenthome.embeddedsystem.view;

import com.projectgame.intelligenthome.core.ui.*;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class BootScreen extends Drawable {
    private GifDrawable gifDrawable;

    public BootScreen(){
        Image[] images = new Image[10];
        for(int i = 0; i < 10; i++){
            String name = "/com/projectgame/intelligenthome/embeddedsystem/res/LoadingScreen/LoadingScreen_00";

            if(i < 10)
                name += "00" + i;
            else if(i < 100)
                name += "0" + i;
            else
                name += i;

            name += ".png";

            Image img = new Image(name);
            images[i] = img;
        }

        getLocalTransform().setRect(new Rect(new Vector2(0, 0), Display.getRect()));
        gifDrawable = new GifDrawable(images, 25);
        gifDrawable.getLocalTransform().setRect(getLocalTransform().getRect());
        gifDrawable.start();
    }

    public void finishUp(){
        gifDrawable.stop();
    }

    @Override
    protected void draw(GraphicsContext context, Drawable parent) {
        if(gifDrawable != null)
            gifDrawable.redraw(context, this);
    }
}
