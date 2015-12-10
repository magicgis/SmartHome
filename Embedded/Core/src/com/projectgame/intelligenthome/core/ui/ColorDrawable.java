package com.projectgame.intelligenthome.core.ui;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

/**
 * Created by Beppo-Laptop on 10/8/2015.
 */
public class ColorDrawable extends Drawable {
    private Color color;

    public ColorDrawable(Color color){
        this.color = color;
    }

    public Color getColor(){
        return color;
    }
    public void setColor(Color color){
        this.color = color;
    }

    @Override
    public void draw(GraphicsContext context, Drawable parent) {
        context.save();
        context.rotate(getTransform().getRotation());
        context.setFill(color);
        context.fillRect(
                getTransform().getRect().getPosition().getX(),
                getTransform().getRect().getPosition().getY(),
                getTransform().getRect().getSize().getX(),
                getTransform().getRect().getSize().getY()
        );
        context.restore();
    }
}
