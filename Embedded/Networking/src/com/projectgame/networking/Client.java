package com.projectgame.networking;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.util.ArrayList;

/**
 * Created by Beppo-Laptop on 10/8/2015.
 */
public class Client {
    public static Client createNew(String host, int port) throws IOException {
        Socket socket = new Socket(host, port);
        InputStream inputStream = socket.getInputStream();
        OutputStream outputStream = socket.getOutputStream();

        return new Client(socket, inputStream, outputStream);
    }

    private Socket socket;
    private InputStream inputStream;
    private OutputStream outputStream;

    private ArrayList<IMessageHandler> handlers;

    private Client(Socket socket, InputStream inputStream, OutputStream outputStream){
        this.socket = socket;
        this.inputStream = inputStream;
        this.outputStream = outputStream;
        handlers = new ArrayList<>();

        new Thread(){
            @Override
            public void run() {
                work();
            }
        }.start();
    }

    public void disconnect() throws IOException {
        socket.close();
    }

    public void registerListener(IMessageHandler handler){
        if(!handlers.contains(handler))
            handlers.add(handler);
    }
    public void unregisterListener(IMessageHandler handler){
        handlers.remove(handler);
    }

    public void write(byte[] data) throws IOException {
        outputStream.write(data, 0, data.length);
    }

    private void work(){
        ByteArrayOutputStream stream = new ByteArrayOutputStream();
        byte[] buffer = new byte[1024];

        while(socket.isConnected()){
            stream.reset();

            try {
                while (!socket.isClosed() && socket.isConnected() && inputStream.available() > 0) {
                    int read = inputStream.read(buffer);

                    if(read <= 0)
                        continue;

                    stream.write(buffer, 0, read);
                }
            }catch(Exception e){
                e.printStackTrace();
                return;
            }

            if(stream.size() > 0){
                byte[] data = stream.toByteArray();
                for(IMessageHandler handler : handlers)
                    handler.onMessageReceived(data);
            }else{
                try {
                    Thread.sleep(200);
                }catch(Exception e){
                    e.printStackTrace();
                    return;
                }
            }
        }
    }
}
