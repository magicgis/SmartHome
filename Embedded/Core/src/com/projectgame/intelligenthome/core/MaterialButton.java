package com.projectgame.intelligenthome.core;

import com.projectgame.uisystem.*;
import javafx.scene.image.Image;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;

/**
 * Created by Beppo-Laptop on 10/8/2015.
 */
public class MaterialButton extends ButtonDrawable {

    public MaterialButton(String text) {
        super(
                ImageCache.getImage(ImageType.RAISED_BUTTON),
                ImageCache.getImage(ImageType.RAIDED_BUTTON_CLICK),
                FontCache.getFont(FontType.ROBOTO_REGULAR, Display.getRect().getY() / 30),
                Color.BLACK,
                Color.WHITE,
                text
        );
    }
}
