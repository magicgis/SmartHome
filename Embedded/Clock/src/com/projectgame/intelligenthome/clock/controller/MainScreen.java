package com.projectgame.intelligenthome.clock.controller;

import com.projectgame.intelligenthome.clock.model.Main;
import com.projectgame.intelligenthome.core.AppScreen;
import com.projectgame.intelligenthome.core.Networking;
import com.projectgame.networking.IXPFile;
import com.projectgame.networking.IIXPMessageHandler;
import org.xml.sax.SAXException;

import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import java.io.IOException;

/**
 * Created by Beppo-Laptop on 10/8/2015.
 */
public class MainScreen {
    private static MainScreen instance;
    public static MainScreen getInstance(){
        if(instance == null)
            instance = new MainScreen();

        return instance;
    }

    private com.projectgame.intelligenthome.clock.view.MainScreen screen;
    private IIXPMessageHandler handler;

    private int hourCache = -1;

    public MainScreen(){
        screen = new com.projectgame.intelligenthome.clock.view.MainScreen(this);
        handler = new IIXPMessageHandler() {
            @Override
            public void onMessageReceived(IXPFile message) {
                client_MessageReceived(message);
            }
        };
    }


    private void client_MessageReceived(IXPFile message){
        if(message.getNetworkFunction().equals(Main.TIME_SERVICE_FUNCTION)){
            updateTime(message);
            return;
        }

        if(message.getNetworkFunction().equals("com.projectgame.clock.clock.getdate")){
            updateDate(message);
            return;
        }
    }

    private void requestDate() throws ParserConfigurationException, SAXException, IOException, TransformerException {
        IXPFile request = IXPFile.createNew();
        request.setNetworkFunction("com.projectgame.clock.clock.getdate");
        IXPFile response = Networking.getInstance().getClient().send(request);
    }

    private void updateTime(IXPFile message){
        int hours = Integer.parseInt(message.getInfos().get("hours"));
        int minutes = Integer.parseInt(message.getInfos().get("minutes"));
        int seconds = Integer.parseInt(message.getInfos().get("seconds"));

        screen.setTime(hours, minutes);

        if(hours != hourCache){
            try {
                requestDate();
            } catch (ParserConfigurationException e) {
                e.printStackTrace();
            } catch (SAXException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } catch (TransformerException e) {
                e.printStackTrace();
            }
            hourCache = hours;
        }
    }
    private void updateDate(IXPFile response){
        try {
            int day = Integer.parseInt(response.getInfos().get("day"));
            int month = Integer.parseInt(response.getInfos().get("month"));
            int year = Integer.parseInt(response.getInfos().get("year"));
            int weekday = Integer.parseInt(response.getInfos().get("weekday"));
            String sWd = null;

            switch(weekday){
                case 0:
                    sWd = "Monday";
                    break;
                case 1:
                    sWd = "Tuesday";
                    break;
                case 2:
                    sWd = "Wednesday";
                    break;
                case 3:
                    sWd = "Thursday";
                    break;
                case 4:
                    sWd = "Friday";
                    break;
                case 5:
                    sWd = "Saturday";
                    break;
                case -1:
                case 6:
                    sWd = "Sunday";
                    break;
            }

            screen.setDate(sWd, day, month, year);
        }catch(Exception e){
            e.printStackTrace();
        }
    }

    public AppScreen getScreen() {
        return screen;
    }

    public void onActivate() {
        Networking.getInstance().getClient().registerHandler(handler);
    }

    public void onDeactivate() {
        Networking.getInstance().getClient().unregisterHandler(handler);
    }
}
