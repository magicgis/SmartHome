package com.projectgame.intelligenthome.core;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class FileSystem {
    public static String getStartupPath(){
        String path = FileSystem.class.getProtectionDomain().getCodeSource().getLocation().getPath();
        File file = new File(path);

        if(!file.isDirectory()){
            path = file.getParentFile().getAbsolutePath();
        }

        String decodedPath = "";

        try {
            decodedPath = URLDecoder.decode(path, "UTF-8");
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }

        return decodedPath;
    }

    public static byte[] readAllBytes(File file){
        try {
            FileInputStream fin = new FileInputStream(file);
            ByteArrayOutputStream bout = new ByteArrayOutputStream();
            byte[] buffer = new byte[1024];
            int read = 0;

            while((read = fin.read(buffer)) != -1){
                bout.write(buffer, 0, read);
            }

            fin.close();
            return bout.toByteArray();
        }catch (Exception e){
            e.printStackTrace();
            return null;
        }
    }
}
