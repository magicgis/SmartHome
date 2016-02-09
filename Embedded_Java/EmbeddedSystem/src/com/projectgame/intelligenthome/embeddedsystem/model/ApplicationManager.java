package com.projectgame.intelligenthome.embeddedsystem.model;

import com.projectgame.intelligenthome.core.FileSystem;
import javafx.scene.image.Image;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.lang.reflect.Constructor;
import java.net.URL;
import java.net.URLClassLoader;
import java.util.ArrayList;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class ApplicationManager {
    private static ApplicationManager instance;
    public static ApplicationManager getInstance(){
        if(instance == null)
            instance = new ApplicationManager();

        return instance;
    }

    File appDir;
    File jarDir;
    File imgDir;
    File lisDir;

    ArrayList<Application> apps;

    private ApplicationManager(){
        checkDirs();

        apps = new ArrayList<>();

        for(String name : getAppList()){
            Application application = getApp(name);

            if(application == null){
                System.out.println("Could not load " + name);
                continue;
            }

            apps.add(application);
        }
    }

    public ArrayList<Application> getApps(){
        return (ArrayList<Application>)apps.clone();
    }

    public void closeAllApps(){
        for(Application app : apps)
            app.getApp().onApplicationClose();
    }

    private void checkDirs(){
        appDir = new File(FileSystem.getStartupPath() + "/Apps");
        jarDir = new File(appDir.getAbsolutePath() + "/Jar");
        imgDir = new File(appDir.getAbsoluteFile() + "/Img");
        lisDir = new File(appDir.getAbsoluteFile() + "/Lis");

        if(!appDir.exists())
            appDir.mkdirs();
        if(!jarDir.exists())
            jarDir.mkdirs();
        if(!imgDir.exists())
            imgDir.mkdirs();
        if(!lisDir.mkdirs())
            lisDir.mkdirs();
    }
    private ArrayList<String> getAppList(){
        File[] names = lisDir.listFiles();
        ArrayList<String> apps = new ArrayList<>();

        for(File name : names) {
            if(!new File(jarDir.getAbsolutePath() + "/" + name.getName() + ".jar").exists())
                continue;
            if(!new File(imgDir.getAbsolutePath() + "/" + name.getName() + ".png").exists())
                continue;

            apps.add(name.getName());
        }

        return apps;
    }
    private Application getApp(String name){
        File lis = new File(lisDir.getAbsolutePath() + "/" + name);
        File img = new File(imgDir.getAbsolutePath() + "/" + name + ".png");
        File jar = new File(jarDir.getAbsolutePath() + "/" + name + ".jar");

        Image image = null;
        String className = new String(FileSystem.readAllBytes(lis));
        try {
            image = new Image(new FileInputStream(img));
        } catch (FileNotFoundException e) {
            e.printStackTrace();
            return null;
        }

        com.projectgame.intelligenthome.core.Application app = null;

        try {
            ClassLoader loader = URLClassLoader.newInstance(new URL[]{jar.toURL()}, new Main().getClass().getClassLoader());
            Class<?> dest = Class.forName(className, true, loader);
            Class<? extends com.projectgame.intelligenthome.core.Application> runClass = dest.asSubclass(com.projectgame.intelligenthome.core.Application.class);
            Constructor<? extends com.projectgame.intelligenthome.core.Application> constructor = runClass.getConstructor();
            app = constructor.newInstance();

        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }

        if(app == null)
            return null;

        return new Application(name, image, app);
    }
}
