package com.projectgame.intelligenthome.clock.view;

import com.projectgame.intelligenthome.core.AppScreen;
import com.projectgame.intelligenthome.core.FontCache;
import com.projectgame.intelligenthome.core.FontType;
import com.projectgame.intelligenthome.core.IAppScreenBarButtonHandler;
import com.projectgame.uisystem.*;
import javafx.geometry.VPos;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.TextAlignment;

import java.util.Hashtable;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class AlarmScreen extends AppScreen {
    private com.projectgame.intelligenthome.clock.controller.AlarmScreen controller;

    private LabelDrawable alarmName;
    private LabelDrawable time;

    public AlarmScreen(com.projectgame.intelligenthome.clock.controller.AlarmScreen controller) {
        super("Alarm", Color.WHITE);
        this.controller = controller;

        alarmName = new LabelDrawable();
        alarmName.setTextAlignment(TextAlignment.CENTER);
        alarmName.setBaseline(VPos.CENTER);
        alarmName.setText("ALARM!");
        Font f = FontCache.getFont(FontType.ROBOTO_REGULAR, 75);
        alarmName.setFont(f);

        time = new LabelDrawable();
        time.setTextAlignment(TextAlignment.CENTER);
        time.setBaseline(VPos.CENTER);
        time.setText("00:00");
        f = FontCache.getFont(FontType.ROBOTO_CONDENSED_LIGHT, 275);
        time.setFont(f);
    }

    @Override
    public void onActivate() {
        controller.onActivate();
    }
    @Override
    public void onDeactivate() {
        controller.onDeactivate();
    }

    @Override
    public Hashtable<String, IAppScreenBarButtonHandler> getBarButtons() {
        return null;
    }

    public void setTime(int hours, int minutes){
        String text = "";

        if(hours < 10)
            text += "0" + hours;
        else
            text += hours;

        text += ":";

        if(minutes < 10)
            text += "0" + minutes;
        else
            text += minutes;

        time.setText(text);
        Display.getInstance().draw();
    }

    public void setAlarmName(String name){
        alarmName.setText(name);
        Display.getInstance().draw();
    }

    @Override
    protected void draw(GraphicsContext context, Drawable parent) {
        time.getLocalTransform().setRect(
                new Rect(
                        new Vector2(getLocalTransform().getRect().getSize().getX() / 2, getLocalTransform().getRect().getSize().getY() / 2 + 60),
                        new Vector2(1, 1)
                )
        );
        alarmName.getLocalTransform().setRect(
                new Rect(
                        new Vector2(getLocalTransform().getRect().getSize().getX() / 2, getTransform().getRect().getSize().getY() / 2 - 150),
                        new Vector2(1, 2)
                )
        );

        time.redraw(context, this);
        alarmName.redraw(context, this);
    }
}
