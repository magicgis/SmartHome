package com.projectgame.intelligenthome.clock.view;

import com.projectgame.intelligenthome.core.AppScreen;
import com.projectgame.intelligenthome.core.IAppScreenBarButtonHandler;
import com.projectgame.intelligenthome.core.ui.Drawable;
import com.projectgame.intelligenthome.core.ui.LabelDrawable;
import com.projectgame.intelligenthome.core.ui.Vector2;
import javafx.geometry.VPos;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.TextAlignment;

import java.util.Hashtable;

/**
 * Created by Beppo-Laptop on 10/9/2015.
 */
public class LoadingScreen extends AppScreen {
    private LabelDrawable label;
    private com.projectgame.intelligenthome.clock.controller.LoadingScreen controller;

    public LoadingScreen(com.projectgame.intelligenthome.clock.controller.LoadingScreen controller){
        super("Loading...", Color.WHITE);
        this.controller = controller;
        label = new LabelDrawable();
        Font f = Font.loadFont(MainScreen.class.getResourceAsStream("/com/projectgame/intelligenthome/core/res/Fonts/Roboto-Regular.ttf"), 50);
        label.setFont(f);
        label.setText("Loading...");
        label.setTextAlignment(TextAlignment.CENTER);
        label.setBaseline(VPos.CENTER);
    }

    @Override
    protected void draw(GraphicsContext context, Drawable parent) {
        label.getLocalTransform().getRect().setPosition(new Vector2(getTransform().getRect().getSize().getX() / 2, getTransform().getRect().getSize().getY() / 2));
        label.redraw(context, this);
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
}
