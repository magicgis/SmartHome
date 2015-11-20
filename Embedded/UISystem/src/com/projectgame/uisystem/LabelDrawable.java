package com.projectgame.uisystem;

import javafx.geometry.VPos;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.*;
import javafx.scene.paint.Paint;
import javafx.scene.text.Font;
import javafx.scene.text.TextAlignment;

/**
 * Created by Beppo-Laptop on 10/7/2015.
 */
public class LabelDrawable extends Drawable{
    private String text;
    private Font font;
    private Color color;
    private TextAlignment textAlignment;
    private VPos baseline;

    public LabelDrawable(){
        text = "";
        font = new Font("Arial", 14);
        color = javafx.scene.paint.Color.BLACK;
        textAlignment = TextAlignment.LEFT;
        baseline = VPos.BASELINE;
    }

    public void setTextAlignment(TextAlignment textAlignment){
        this.textAlignment = textAlignment;
    }
    public void setBaseline(VPos baseline){
        this.baseline = baseline;
    }

    public String getText(){
        return text;
    }
    public void setText(String text){
        this.text = text;
    }

    public Font getFont(){
        return font;
    }
    public void setFont(Font f){
        font = f;
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
        context.setFont(font);
        context.setFill(color);
        context.setTextAlign(textAlignment);
        context.setTextBaseline(baseline);
        context.fillText(
                text,
                getTransform().getRect().getPosition().getX(),
                getTransform().getRect().getPosition().getY());
        context.restore();
    }
}
