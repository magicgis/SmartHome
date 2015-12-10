package com.projectgame.intelligenthome.core.ui;

import javafx.geometry.VPos;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.TextAlignment;

import java.util.ArrayList;

/**
 * Created by Beppo on 06.10.2015.
 */
public class ButtonDrawable extends Drawable {
    private Image normal;
    private Image clicked;

    private Color normalLabelColor;
    private Color clickedLabelColor;

    private LabelDrawable label;

    private boolean isClicked;
    private ArrayList<IButtonHandler> handlers;

    public ButtonDrawable(Image normal, Image clicked, Font labelFont, Color normalLabelColor, Color clickedLabelColor, String text){
        this.normal = normal;
        this.clicked = clicked;

        this.normalLabelColor = normalLabelColor;
        this.clickedLabelColor = clickedLabelColor;

        label = new LabelDrawable();
        label.setFont(labelFont);
        label.setText(text);
        label.setTextAlignment(TextAlignment.CENTER);
        label.setBaseline(VPos.CENTER);

        isClicked = false;

        handlers = new ArrayList<>();
    }

    public LabelDrawable getLabel(){
        return label;
    }

    public void addHandler(IButtonHandler handler){
        handlers.add(handler);
    }
    public void removeHandler(IButtonHandler handler){
        handlers.remove(handler);
    }

    private void onClicked(){
        isClicked = true;
        Display.getInstance().draw();
    }
    private void onReleased(boolean inside){
        if(!isClicked)
            return;

        isClicked = false;
        Display.getInstance().draw();

        if(!inside)
            return;

        for(IButtonHandler handler : handlers)
            handler.click();
    }

    @Override
    public void draw(GraphicsContext context, Drawable parent) {
        Image drawable = null;

        if(isClicked){
            drawable = clicked;
            label.setColor(clickedLabelColor);
        }else{
            drawable = normal;
            label.setColor(normalLabelColor);
        }

        context.save();
        context.rotate(getTransform().getRotation());
        context.drawImage(
                drawable,
                getTransform().getRect().getPosition().getX(),
                getTransform().getRect().getPosition().getY(),
                getTransform().getRect().getSize().getX(),
                getTransform().getRect().getSize().getY()
        );
        label.getLocalTransform().getRect().setPosition(
                new Vector2(
                        getLocalTransform().getRect().getSize().getX() / 2,
                        getLocalTransform().getRect().getSize().getY() / 2
                )
        );
        label.redraw(context, this);
        context.restore();

        InputHandler manager = new InputHandler(getTransform().getRect()) {
            @Override
            public void onMouseClicked() {
                onClicked();
            }

            @Override
            public void onMouseReleased(boolean inside){
                onReleased(inside);
            }
        };

        InputManager.getInstance().registerManager(manager);
    }
}
