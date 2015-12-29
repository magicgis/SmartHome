package com.projectgame.intelligenthome.core;

import org.w3c.dom.Document;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import java.io.*;

/**
 * Created by Beppo-Laptop on 11/11/2015.
 */
public class Settings {
    private static Settings instance;
    public static Settings getInstance(){
        if(instance == null)
            instance = new Settings();

        return instance;
    }

    private static final String XML_ROOT = "Settings";
    private static final String XML_NETWORKING = "Networking";
    private static final String XML_NETWORKING_AUTOCONNECT = "AutoConnect";
    private static final String XML_NETWORKING_URL = "Url";
    private static final String XML_NETWORKING_PORT = "Port";

    private static final String XML_BASE =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<"  + XML_ROOT + ">" +
                "<"  + XML_NETWORKING + ">" +
                    "<" + XML_NETWORKING_AUTOCONNECT + ">true</" + XML_NETWORKING_AUTOCONNECT + ">"+
                    "<" + XML_NETWORKING_URL + ">127.0.0.1</" + XML_NETWORKING_URL + ">" +
                    "<" + XML_NETWORKING_PORT + ">10250</" + XML_NETWORKING_PORT + ">" +
                "</" + XML_NETWORKING + ">" +
            "</" + XML_ROOT + ">";

    private boolean networkingAutoConnect;
    private String networkingUrl;
    private int networkingPort;

    public Settings(){
        File f = new File(FileSystem.getStartupPath() + "/Settings.cfg");

        if(!f.exists()){
            try {
                FileOutputStream fout = new FileOutputStream(f);
                fout.write(XML_BASE.getBytes());
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }

        try {
            DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
            DocumentBuilder db = dbf.newDocumentBuilder();

            byte[] data = FileSystem.readAllBytes(f);
            ByteArrayInputStream bin = new ByteArrayInputStream(data);

            Document doc = db.parse(bin);

            Node networkingAutoConnectNode = getNode(getNode(doc.getDocumentElement(), XML_NETWORKING), XML_NETWORKING_AUTOCONNECT);
            Node networkingUrlNode = getNode(getNode(doc.getDocumentElement(), XML_NETWORKING), XML_NETWORKING_URL);
            Node networkingPortNode = getNode(getNode(doc.getDocumentElement(), XML_NETWORKING), XML_NETWORKING_PORT);

            networkingAutoConnect = networkingAutoConnectNode.getFirstChild().getNodeValue().equals("true");
            networkingUrl = networkingUrlNode.getFirstChild().getNodeValue();
            networkingPort = Integer.parseInt(networkingPortNode.getFirstChild().getNodeValue());
        }catch(Exception e){
            e.printStackTrace();
        }
    }

    public boolean isNetworkingAutoConnect(){
        return networkingAutoConnect;
    }
    public String getNetworkingUrl(){
        return networkingUrl;
    }
    public int getNetworkingPort(){
        return networkingPort;
    }

    private Node getNode(Node parent, String child){
        NodeList list = parent.getChildNodes();

        for(int i = 0; i < list.getLength(); i++){
            Node node = list.item(i);

            if(node.getNodeName().equals(child))
                return node;
        }

        return null;
    }
}
