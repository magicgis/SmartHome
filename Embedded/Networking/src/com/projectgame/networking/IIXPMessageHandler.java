package com.projectgame.networking;

import com.projectgame.networking.IXPFile;

/**
 * Created by Beppo-Laptop on 10/9/2015.
 */
public interface IIXPMessageHandler {
    void onMessageReceived(IXPFile message);
}
