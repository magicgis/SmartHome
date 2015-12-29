package com.projectgame.intelligenthome.core;

import javafx.scene.image.Image;

import java.io.Serializable;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class IEAData implements Serializable{
    private String applicationName;
    private String applicationClass;
    private byte[] applicationIcon;
    private byte[] jarFile;

    public IEAData(){

    }
    public IEAData(String applicationName, String applicationClass, byte[] applicationIcon, byte[] jarFile){
        this.applicationName = applicationName;
        this.applicationClass = applicationClass;
        this.applicationIcon = applicationIcon;
        this.jarFile = jarFile;
    }

    public String getApplicationName(){
        return applicationName;
    }
    public String getApplicationClass(){
        return applicationClass;
    }
    public byte[] getApplicationIcon(){
        return applicationIcon;
    }
    public byte[] getJarFile(){
        return jarFile;
    }
}
