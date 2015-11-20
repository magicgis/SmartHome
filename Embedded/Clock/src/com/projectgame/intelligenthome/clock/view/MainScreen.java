package com.projectgame.intelligenthome.clock.view;

import com.projectgame.intelligenthome.clock.controller.*;
import com.projectgame.intelligenthome.clock.model.TimeManager;
import com.projectgame.intelligenthome.core.*;
import com.projectgame.uisystem.*;
import javafx.geometry.VPos;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.TextAlignment;

import java.util.Hashtable;


/**
 * Created by Beppo-Laptop on 10/8/2015.
 */
public class MainScreen extends AppScreen {
    private com.projectgame.intelligenthome.clock.controller.MainScreen controller;

    private LabelDrawable time;
    private LabelDrawable date;

    private int cacheHours = -1;
    private int cacheMinutes = -1;

    public MainScreen(com.projectgame.intelligenthome.clock.controller.MainScreen controller){
        super("Clock", Color.WHITE);
        this.controller = controller;

        time = new LabelDrawable();
        time.setTextAlignment(TextAlignment.CENTER);
        time.setBaseline(VPos.CENTER);
        time.setText("00:00");
        Font f = FontCache.getFont(FontType.ROBOTO_CONDENSED_LIGHT, (int)(Display.getRect().getY() / 2.1819));
        time.setFont(f);

        date = new LabelDrawable();
        date.setTextAlignment(TextAlignment.CENTER);
        date.setBaseline(VPos.CENTER);
        date.setText("Wednesday 08.10.2015");
        Font g = FontCache.getFont(FontType.ROBOTO_CONDENSED_LIGHT, Display.getRect().getY() / 12);
        date.setFont(g);

        setTime(TimeManager.getInstance().getCurrentTime().getHours(), TimeManager.getInstance().getCurrentTime().getMinutes());
    }

    /**
     * Updates the time
     * @param hours Current hours
     * @param minutes Current minutes
     */
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

        if(cacheHours != hours || cacheMinutes != minutes)
            Display.getInstance().draw();

        cacheHours = hours;
        cacheMinutes = minutes;
    }

    /**
     * Updates the day
     * @param weekday Current weekday
     * @param day Current day
     * @param month Current month
     * @param year Current year
     */
    public void setDate(String weekday, int day, int month, int year){
        String text = weekday + " ";

        if(day < 10)
            text += "0" + day;
        else
            text += day;

        text += ".";

        if(month < 10)
            text += "0" + month;
        else
            text += month;

        text += "." + year;

        date.setText(text);
        Display.getInstance().draw();
    }

    @Override
    public void draw(GraphicsContext context, Drawable parent) {
        time.getLocalTransform().setRect(
                new Rect(
                        new Vector2(getLocalTransform().getRect().getSize().getX() / 2, getLocalTransform().getRect().getSize().getY() / 2 - (int)(Display.getRect().getY() / 8.6)),
                        new Vector2(1, 1)
                )
        );
        date.getLocalTransform().setRect(
                new Rect(
                        new Vector2(getLocalTransform().getRect().getSize().getX() / 2, getTransform().getRect().getSize().getY() / 2 + Display.getRect().getY() / 5),
                        new Vector2(1, 1)
                )
        );

        time.redraw(context, this);
        date.redraw(context, this);
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
        Hashtable<String, IAppScreenBarButtonHandler> buttons = new Hashtable<>();
        buttons.put("Timer", new IAppScreenBarButtonHandler() {
            @Override
            public void onClick() {
                Application.switchScreen(com.projectgame.intelligenthome.clock.controller.TimerScreen.getInstance().getScreen());
            }
        });
        /*buttons.put("Alarms", new IAppScreenBarButtonHandler() {
            @Override
            public void onClick() {
                System.out.println("B");
            }
        });
        buttons.put("Stopwatch", new IAppScreenBarButtonHandler() {
            @Override
            public void onClick() {
                System.out.println("C");
            }
        });*/

        return buttons;
    }
}
