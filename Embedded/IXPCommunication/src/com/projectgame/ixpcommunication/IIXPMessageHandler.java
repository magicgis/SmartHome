package com.projectgame.ixpcommunication;

import com.projectgame.ixp.IXPFile;

/**
 * Created by Beppo-Laptop on 10/9/2015.
 */
public interface IIXPMessageHandler {
    void onMessageReceived(IXPFile message);
}
