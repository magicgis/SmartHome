package com.projectgame.networking;

/**
 * Created by Beppo-Laptop on 10/9/2015.
 */
public interface IMessageHandler {
    void onMessageReceived(byte[] data);
}
