package com.projectgame.intelligenthome.core.ui;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.event.EventHandler;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;

/**
 * Created by Beppo on 06.10.2015.
 */
public class Display extends Application{
    private static int displayWidth;
    private static int displayHeight;
    private static Drawable rootDrawable;
    private static Group group;
    private static Canvas canvas;
    private static GraphicsContext context;
    private static IDrawableProvider rootProvider;

    private static Display instance;

    public static Vector2 getRect(){
        return new Vector2(displayWidth, displayHeight);
    }

    public static void initDisplay(int width, int height, IDrawableProvider provider){
        displayWidth = width;
        displayHeight = height;
        rootProvider = provider;

        group = new Group();
        canvas = new Canvas(displayWidth, displayHeight);
        group.getChildren().add(canvas);

        new InputManager();
    }

    public static void run(){
        Display.launch(new String[0]);
    }

    public static Display getInstance(){
        return instance;
    }


    @Override
    public void start(Stage primaryStage) throws Exception {
        //primaryStage.initStyle(StageStyle.UNDECORATED);
        instance = this;

        primaryStage.setScene(new Scene(group));
        canvas.setOnMousePressed(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                InputManager.getInstance().onMouseClicked(new Vector2((int) event.getSceneX(), (int) event.getSceneY()));
            }
        });
        canvas.setOnMouseReleased(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                InputManager.getInstance().onMouseReleased(new Vector2((int)event.getSceneX(), (int)event.getSceneY()));
            }
        });
        context = canvas.getGraphicsContext2D();
        rootDrawable = rootProvider.getDrawable();

        draw();

        primaryStage.show();
    }

    public void draw(){
        if(rootDrawable == null)
            return;

        Platform.runLater(new Runnable() {
            @Override
            public void run() {
                Drawable dummy = new Drawable() {
                    @Override
                    protected void draw(GraphicsContext context, Drawable parent) {

                    }
                };

                InputManager.getInstance().clear();
                rootDrawable.redraw(context, dummy);
            }
        });
    }
}
