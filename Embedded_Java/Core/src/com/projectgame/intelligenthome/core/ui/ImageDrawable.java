package com.projectgame.intelligenthome.core.ui;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;

/**
 * Created by Beppo on 06.10.2015.
 */
public class ImageDrawable extends Drawable {
    private Image image;

    public ImageDrawable(String url){
        image = new Image(url);
    }
    public ImageDrawable(Image image){ this.image = image; }

    public Image getImage(){
        return image;
    }

    @Override
    public void draw(GraphicsContext context, Drawable parent) {
        context.save();
        context.rotate(getTransform().getRotation());
        context.drawImage(image,
                getTransform().getRect().getPosition().getX(),
                getTransform().getRect().getPosition().getY(),
                getTransform().getRect().getSize().getX(),
                getTransform().getRect().getSize().getY());
        context.restore();
    }
}
