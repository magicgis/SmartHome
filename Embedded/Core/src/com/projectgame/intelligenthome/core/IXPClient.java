package com.projectgame.intelligenthome.core;

import javax.xml.transform.TransformerException;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.Hashtable;

/**
 * Created by Beppo-Laptop on 10/9/2015.
 */
public class IXPClient {
    public static IXPClient creatNew(String host, int port) throws IOException {
        Client client = Client.createNew(host, port);

        return new IXPClient(client);
    }

    private Client client;
    private ArrayList<IIXPMessageHandler> handlers;
    private Hashtable<String, IIXPMessageHandler> internalHandlers;

    private IXPClient(Client client){
        this.client = client;
        client.registerListener(new IMessageHandler() {
            @Override
            public void onMessageReceived(byte[] data) {
                client_MessageReceived(data);
            }
        });
        handlers = new ArrayList<>();
        internalHandlers = new Hashtable<>();
    }

    public void disconnect() throws IOException {
        client.disconnect();
    }

    public void registerHandler(IIXPMessageHandler handler){
        if(!handlers.contains(handler))
            handlers.add(handler);
    }
    public void unregisterHandler(IIXPMessageHandler handler){
        handlers.remove(handler);
    }

    public IXPFile send(IXPFile file) throws TransformerException, IOException {
        client.write(file.getXML().getBytes("UTF-8"));
        return file;
    }

    public void noResponseRequest(IXPFile file) throws TransformerException, IOException {
        client.write(file.getXML().getBytes("UTF-8"));
    }
    public String simpleRequest(IXPFile file) throws InterruptedException, TransformerException, IOException {
        IXPFile response = ixpRequest(file);

        return response.getInfos().get("Response");
    }
    public IXPFile ixpRequest(IXPFile file) throws TransformerException, IOException, InterruptedException {
        final IXPFile[] response = {null};
        internalHandlers.put(file.getNetworkFunction(), new IIXPMessageHandler() {
            @Override
            public void onMessageReceived(IXPFile message) {
                response[0] = message;
            }
        });

        client.write(file.getXML().getBytes("UTF-8"));

        while(response[0] == null){
            Thread.sleep(200);
        }

        return response[0];
    }

    private void client_MessageReceived(byte[] data){
        try {
            String message = new String(data, "UTF-8");
            ArrayList<Integer> indices = findTexts(message, "<?xml version=");

            if(indices.size() == 1){
                handleMessage(message);
                return;
            }

            for(int i = 0; i < indices.size(); i++){
                String sub = null;

                if(i == indices.size() - 1){
                    sub = message.substring(indices.get(i));
                }else{
                    sub = message.substring(indices.get(i), indices.get(i + 1) - indices.get(i));
                }

                handleMessage(sub);
            }
        } catch (UnsupportedEncodingException e) {
            System.out.println("Invalid server data! No string format");
        }
    }
    private void handleMessage(String message){
        try{
            IXPFile file = IXPFile.parse(message);

            if(internalHandlers.containsKey(file.getNetworkFunction())){
                IIXPMessageHandler handler = internalHandlers.get(file.getNetworkFunction());
                handlers.remove(file.getNetworkFunction());
                handler.onMessageReceived(file);
                return;
            }

            for(IIXPMessageHandler handler : handlers)
                handler.onMessageReceived(file);
        }catch(Exception e){
            System.out.println("Invalid server data! No IXP format");
        }
    }
    private ArrayList<Integer> findTexts(String source, String regex){
        int sourceCounter = 0;
        int regexCounter = 0;
        ArrayList<Integer> indices = new ArrayList<>();

        while(sourceCounter < source.length() - regex.length()){
            if(regexCounter == regex.length() - 1){
                indices.add(sourceCounter);
                regexCounter = 0;
                sourceCounter++;
            }

            if(source.charAt(sourceCounter + regexCounter) == regex.charAt(regexCounter))
                regexCounter++;
            else{
                regexCounter = 0;
                sourceCounter++;
            }
        }

        return indices;
    }
}
