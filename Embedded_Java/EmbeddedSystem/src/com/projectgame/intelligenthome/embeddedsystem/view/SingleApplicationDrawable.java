package com.projectgame.intelligenthome.embeddedsystem.view;

import com.projectgame.intelligenthome.core.*;
import com.projectgame.intelligenthome.core.ui.*;
import com.projectgame.intelligenthome.embeddedsystem.model.Main;
import javafx.geometry.VPos;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.TextAlignment;

import java.util.ArrayList;
import java.util.Hashtable;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class SingleApplicationDrawable extends Drawable {
    //Title Bar (Image Drawable)
    private static final int TITLE_BAR_X = 0;
    private static final int TITLE_BAR_Y = 0;
    private static final int TITLE_BAR_WIDTH = Display.getRect().getX();
    private static final int TITLE_BAR_HEIGHT = Display.getRect().getY() / 8;

    private static final int BOTTOM_BAR_HEIGHT = Display.getRect().getY() / 10;
    private static final int HOME_BUTTON_SIZE = Display.getRect().getY() / 12;

    private ColorDrawable cdBackground;
    private ImageDrawable imgTitleBar;
    private LabelDrawable lblTitle;
    private ColorDrawable bottomBar;
    private ButtonDrawable homeButton;

    private AppScreen currentScreen;
    private ArrayList<MaterialButton> barButtonCache;

    public SingleApplicationDrawable(){
        barButtonCache = new ArrayList<>();
        Application.setAppScreenListener(new IAppScreenListener() {
            @Override
            public void onScreenChange(AppScreen screen) {
                setAppScreen(screen);
            }
        });

        cdBackground = new ColorDrawable(Color.WHITE);
        cdBackground.getLocalTransform().setRect(
                new Rect(
                        new Vector2(0, 0),
                        new Vector2(Display.getRect())
                )
        );

        imgTitleBar = new ImageDrawable(ImageCache.getImage(ImageType.APP_BAR));
        imgTitleBar.getLocalTransform().setRect(
                new Rect(
                        new Vector2(TITLE_BAR_X, TITLE_BAR_Y),
                        new Vector2(TITLE_BAR_WIDTH, TITLE_BAR_HEIGHT)
                )
        );

        lblTitle = new LabelDrawable();
        lblTitle.getLocalTransform().setRect(
                new Rect(
                        new Vector2(Display.getRect().getX() / 50, TITLE_BAR_HEIGHT / 2),
                        new Vector2(1, 1)
                )
        );

        bottomBar = new ColorDrawable(Color.BLACK);
        bottomBar.getLocalTransform().setRect(
                new Rect(
                        new Vector2(0, Display.getRect().getY() - BOTTOM_BAR_HEIGHT),
                        new Vector2(Display.getRect().getX(), BOTTOM_BAR_HEIGHT)
                )
        );

        homeButton = new ButtonDrawable(
                new Image("/com/projectgame/intelligenthome/embeddedsystem/res/Png/BottomBarHome_Normal.png"),
                new Image("/com/projectgame/intelligenthome/embeddedsystem/res/Png/BottomBarHome_Clicked.png"),
                FontCache.getFont(FontType.ROBOTO_CONDENSED_LIGHT, 0),
                Color.BLACK,
                Color.BLACK,
                "");
        homeButton.getLocalTransform().getRect().setSize(new Vector2(HOME_BUTTON_SIZE, HOME_BUTTON_SIZE));
        homeButton.getLocalTransform().getRect().setPosition(new Vector2(Display.getRect().getX() / 2 - (HOME_BUTTON_SIZE / 2), Display.getRect().getY() - HOME_BUTTON_SIZE - ((BOTTOM_BAR_HEIGHT - HOME_BUTTON_SIZE) / 2)));
        homeButton.addHandler(new IButtonHandler() {
            @Override
            public void click() {
                currentScreen.onDeactivate();
                currentScreen = null;
                Main.canvas.clearDrawables();
                Main.canvas.addDrawable(Main.appLauncher);
                Display.getInstance().draw();
            }
        });

        lblTitle.setText("");
        lblTitle.setTextAlignment(TextAlignment.LEFT);
        lblTitle.setBaseline(VPos.CENTER);
        Font f = FontCache.getFont(FontType.ROBOTO_REGULAR, Display.getRect().getY() / 20);
        lblTitle.setFont(f);
    }

    public void setAppScreen(AppScreen screen){
        if(currentScreen != null)
            currentScreen.onDeactivate();

        if(currentScreen == null) {
            Main.canvas.clearDrawables();
            Main.canvas.addDrawable(Main.singleApplicationDrawable);
        }

        currentScreen = screen;
        barButtonCache.clear();

        Hashtable<String, IAppScreenBarButtonHandler> buttons = currentScreen.getBarButtons();

        if(buttons == null)
            buttons = new Hashtable<>();

        ArrayList<MaterialButton> materialButtons = new ArrayList<>();
        int counter = 0;

        for (final String label : buttons.keySet()) {
            MaterialButton mb = new MaterialButton(label);
            mb.getLocalTransform().getRect().setSize(new Vector2(Display.getRect().getX() / 7, (int) (TITLE_BAR_HEIGHT * 0.7f)));
            mb.getLocalTransform().getRect().setPosition(new Vector2(Display.getRect().getX() - 5 - ((Display.getRect().getX() / 7 + 10) * (counter + 1)), (int) (TITLE_BAR_HEIGHT * 0.15f)));

            final Hashtable<String, IAppScreenBarButtonHandler> finalButtons = buttons;
            mb.addHandler(new IButtonHandler() {
                @Override
                public void click() {
                    finalButtons.get(label).onClick();
                }
            });

            barButtonCache.add(mb);
            counter++;
        }

        lblTitle.setText(currentScreen.getName());
        currentScreen.onActivate();
        Display.getInstance().draw();
    }

    @Override
    protected void draw(GraphicsContext context, Drawable parent) {
        if(currentScreen == null)
            return;

        cdBackground.setColor(currentScreen.getBackground());
        cdBackground.redraw(context, this);

        currentScreen.getLocalTransform().setRect(new Rect(
                        new Vector2(
                                0,
                                TITLE_BAR_HEIGHT
                        ),
                        new Vector2(
                                Display.getRect().getX(),
                                Display.getRect().getY() - TITLE_BAR_HEIGHT - BOTTOM_BAR_HEIGHT
                        ))
        );

        currentScreen.redraw(context, this);
        imgTitleBar.redraw(context, this);
        lblTitle.redraw(context, this);
        bottomBar.redraw(context, this);
        homeButton.redraw(context, this);

        for(MaterialButton btn : barButtonCache)
            btn.redraw(context, this);
    }
}
