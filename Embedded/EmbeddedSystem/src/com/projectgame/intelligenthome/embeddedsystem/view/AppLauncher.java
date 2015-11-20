package com.projectgame.intelligenthome.embeddedsystem.view;

import com.projectgame.intelligenthome.core.FontCache;
import com.projectgame.intelligenthome.core.FontType;
import com.projectgame.intelligenthome.embeddedsystem.model.Application;
import com.projectgame.intelligenthome.embeddedsystem.model.ApplicationManager;
import com.projectgame.intelligenthome.embeddedsystem.model.Main;
import com.projectgame.uisystem.*;
import javafx.geometry.VPos;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.text.TextAlignment;

import java.awt.*;
import java.util.ArrayList;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class AppLauncher extends Drawable {
    private static final int GRID_WIDTH = 4;
    private static final int GRID_HEIGHT = 3;
    private static final int BORDER_TOP = Display.getRect().getY() / 12;
    private static final int BORDER_BOTTOM = Display.getRect().getY() / 12;
    private static final int BORDER_LEFT = Display.getRect().getX() / 16;
    private static final int BORDER_RIGHT = Display.getRect().getX() / 16;
    private static final int SPACING_X = (int)(Display.getRect().getX() / 12.5);
    private static final int SPACING_Y = (int)(Display.getRect().getX() / 26.67);
    private static final int TEXT_HEIGHT = Display.getRect().getY() / 30;
    private static final float CLICK_SCALE_FACTOR = 0.85f;

    private ImageDrawable background;

    private Vector2 clickedPos;

    public AppLauncher(){
        background = new ImageDrawable("/com/projectgame/intelligenthome/embeddedsystem/res/Png/AppLauncherBackground.png");
        background.getLocalTransform().setRect(new Rect(new Vector2(0, 0), Display.getRect()));
        clickedPos = new Vector2(-1, -1);
    }

    @Override
    protected void draw(GraphicsContext context, Drawable parent) {
        background.redraw(context, this);

        ArrayList<Application> apps = ApplicationManager.getInstance().getApps();
        int appWidth = (Display.getRect().getX() - BORDER_LEFT - BORDER_RIGHT - ((GRID_WIDTH - 1) * SPACING_X)) / GRID_WIDTH;
        int appHeight = (Display.getRect().getY() - BORDER_TOP - BORDER_BOTTOM - ((GRID_HEIGHT - 1) * SPACING_Y)) / GRID_HEIGHT;
        int iconWidth = appWidth;
        final int iconHeight = appHeight - TEXT_HEIGHT;

        if(iconWidth != iconHeight)
            iconWidth = iconHeight;

        ArrayList<ImageDrawable> imageDrawables = new ArrayList<>();
        ArrayList<LabelDrawable> labelDrawables = new ArrayList<>();

        int counter = 0;
        for(final Application app : apps){
            final int xGridPos = counter % GRID_WIDTH;
            final int yGridPos = counter / GRID_WIDTH;

            final int xPos = (xGridPos * appWidth) + (xGridPos * SPACING_X) + BORDER_LEFT;
            final int yPos = (yGridPos * appHeight) + (yGridPos * SPACING_Y) + BORDER_TOP;

            final ImageDrawable imageDrawable = new ImageDrawable(app.getImage());

            if(clickedPos.getX() == xGridPos && clickedPos.getY() == yGridPos){
                int newWidth = (int)(iconWidth * CLICK_SCALE_FACTOR);
                int newHeight = (int)(iconHeight * CLICK_SCALE_FACTOR);
                imageDrawable.getLocalTransform().getRect().setPosition(new Vector2(xPos + ((iconWidth - newWidth) / 2), yPos + ((iconHeight - newHeight) / 2)));
                imageDrawable.getLocalTransform().getRect().setSize(new Vector2(newWidth, newHeight));
            }else {
                imageDrawable.getLocalTransform().getRect().setPosition(new Vector2(xPos, yPos));
                imageDrawable.getLocalTransform().getRect().setSize(new Vector2(iconWidth, iconHeight));
            }
            imageDrawables.add(imageDrawable);

            LabelDrawable labelDrawable = new LabelDrawable();
            labelDrawable.setFont(FontCache.getFont(FontType.ROBOTO_REGULAR, TEXT_HEIGHT));
            labelDrawable.setText(app.getName());
            labelDrawable.setTextAlignment(TextAlignment.CENTER);
            labelDrawable.setBaseline(VPos.CENTER);
            labelDrawable.getLocalTransform().getRect().setSize(new Vector2(1, 1));
            labelDrawable.getLocalTransform().getRect().setPosition(new Vector2(xPos + (iconWidth / 2), yPos + iconHeight + TEXT_HEIGHT));
            labelDrawables.add(labelDrawable);

            InputManager.getInstance().registerManager(new InputHandler(imageDrawable.getLocalTransform().getRect()) {
                @Override
                public void onMouseClicked() {
                    clickedPos = new Vector2(xGridPos, yGridPos);
                    Display.getInstance().draw();
                }

                @Override
                public void onMouseReleased(boolean inside) {
                    if(!inside)
                        return;

                    clickedPos = new Vector2(-1, -1);
                    Display.getInstance().draw();

                    Main.canvas.clearDrawables();
                    Main.canvas.addDrawable(Main.singleApplicationDrawable);
                    Main.singleApplicationDrawable.setAppScreen(app.getApp().getMainScreen());
                }
            });

            counter++;
        }

        for(ImageDrawable id : imageDrawables)
            id.redraw(context, this);
        for(LabelDrawable ld : labelDrawables)
            ld.redraw(context, this);
    }
}
