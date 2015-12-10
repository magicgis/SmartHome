package com.projectgame.intelligenthome.core.ui;

import javafx.scene.canvas.GraphicsContext;

/**
 * Created by Beppo on 06.10.2015.
 */
public abstract class Drawable {
    protected Transform localTransform;
    protected Transform transform;

    public Drawable(){
        transform = new Transform();
        localTransform = new Transform();
    }
    public Drawable(Transform transform){
        this.localTransform = transform;
    }

    public Transform getLocalTransform(){
        return localTransform;
    }
    public Transform getTransform(){
        return transform;
    }

    public void redraw(GraphicsContext context, Drawable parent){
        float rotation = parent.getTransform().getRotation() + localTransform.getRotation();
        int x = parent.getTransform().getRect().getPosition().getX() + localTransform.getRect().getPosition().getX();
        int y = parent.getTransform().getRect().getPosition().getY() + localTransform.getRect().getPosition().getY();
        int w = localTransform.getRect().getSize().getX(); //parent.getTransform().getRect().getSize().getX() + localTransform.getRect().getSize().getX();
        int h = localTransform.getRect().getSize().getY(); //parent.getTransform().getRect().getSize().getY() + localTransform.getRect().getSize().getY();

        transform.setRotation(rotation);
        transform.getRect().getPosition().setX(x);
        transform.getRect().getPosition().setY(y);
        transform.getRect().getSize().setX(w);
        transform.getRect().getSize().setY(h);

        draw(context, parent);
    }

    protected abstract void draw(GraphicsContext context, Drawable parent);
}
