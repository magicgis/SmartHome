package com.projectgame.intelligenthome.core;

import com.projectgame.networking.IXPClient;

import java.io.IOException;

/**
 * Created by Beppo-Laptop on 11/11/2015.
 */
public class Networking {
    private static Networking instance;
    public static Networking getInstance(){
        if(instance == null)
            instance = new Networking();

        return instance;
    }

    private IXPClient client;

    public Networking(){
        try {
            if(Settings.getInstance().isNetworkingAutoConnect())
                client = IXPClient.creatNew(Settings.getInstance().getNetworkingUrl(), Settings.getInstance().getNetworkingPort());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void close(){
        try {
            client.disconnect();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public IXPClient getClient(){
        return client;
    }
}
