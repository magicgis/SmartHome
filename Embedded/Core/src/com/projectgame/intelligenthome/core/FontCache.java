package com.projectgame.intelligenthome.core;

import javafx.scene.text.Font;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.util.Hashtable;

/**
 * Created by Beppo-Laptop on 10/9/2015.
 */
public class FontCache {
    private static Hashtable<FontType, byte[]> fonts;

    public static void init(){
        fonts = new Hashtable<>();
        cacheFont(FontType.ROBOTO_REGULAR, "/com/projectgame/intelligenthome/core/res/Fonts/Roboto-Regular.ttf");
        cacheFont(FontType.ROBOTO_CONDENSED_LIGHT, "/com/projectgame/intelligenthome/core/res/Fonts/RobotoCondensed-Light.ttf");
    }

    public static void cacheFont(FontType name, String path){
        if(fonts.containsKey(name))
            return;

        try {
            InputStream raw = FontCache.class.getResourceAsStream(path);
            ByteArrayOutputStream bout = new ByteArrayOutputStream();
            byte[] buffer = new byte[1024];
            int read = 0;

            while ((read = raw.read(buffer)) > -1) {
                bout.write(buffer, 0, read);
            }

            fonts.put(name, bout.toByteArray());
        }catch (Exception e){
            e.printStackTrace();
        }
    }

    public static Font getFont(FontType name, int size){
        if(!fonts.containsKey(name))
            return null;

        return Font.loadFont(new ByteArrayInputStream(fonts.get(name)), size);
    }
}
