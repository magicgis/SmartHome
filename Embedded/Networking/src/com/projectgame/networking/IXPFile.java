package com.projectgame.networking;

import org.w3c.dom.Document;
import org.w3c.dom.NamedNodeMap;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.*;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import java.io.*;
import java.util.Hashtable;

/**
 * Created by Beppo-Laptop on 10/8/2015.
 */
public class IXPFile {
    private static final String XML_HEADER = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
    private static final String XML_IXP = "IXP";
    private static final String XML_IXP_TARGET = "Target";
    private static final String XML_IXP_TARGET_NFUNCTION = "nfunction";
    private static final String XML_IXP_HEADER = "Header";
    private static final String XML_IXP_HEADER_INFO = "Info";
    private static final String XML_IXP_HEADER_INFO_NAME = "name";
    private static final String XML_IXP_HEADER_INFO_VALUE = "value";
    private static final String XML_IXP_BODY = "Body";
    private static final String XML_IXP_BODY_INFO = "Info";
    private static final String XML_IXP_BODY_INFO_NAME = "name";

    private static final String XML_BASE =
            XML_HEADER +
            "<" + XML_IXP + ">" +
                "<"  + XML_IXP_TARGET + " " + XML_IXP_TARGET_NFUNCTION + "=\"\"" + "/>" +
                "<"  + XML_IXP_HEADER + ">" +
                "</" + XML_IXP_HEADER + ">" +
                "<"  + XML_IXP_BODY + ">" +
                "</" + XML_IXP_BODY + ">" +
            "</" + XML_IXP + ">";

    public static IXPFile createNew() throws IOException, SAXException, ParserConfigurationException {
        return parse(XML_BASE);
    }
    public static IXPFile parse(String source) throws ParserConfigurationException, IOException, SAXException {
        DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
        DocumentBuilder db = dbf.newDocumentBuilder();

        byte[] data = source.getBytes("UTF-8");
        ByteArrayInputStream bin = new ByteArrayInputStream(data);

        Document doc = db.parse(bin);

        return new IXPFile(doc);
    }

    private Document doc;

    private IXPFile(Document doc) {
        this.doc = doc;
    }

    public String getXML() throws TransformerException {
        TransformerFactory tf = TransformerFactory.newInstance();
        Transformer transformer = tf.newTransformer();
        transformer.setOutputProperty(OutputKeys.OMIT_XML_DECLARATION, "yes");
        StringWriter writer = new StringWriter();
        transformer.transform(new DOMSource(doc), new StreamResult(writer));
        String output = writer.getBuffer().toString().replace("\n|\r", "");

        return XML_HEADER + output;
    }

    public String getNetworkFunction(){
        return getNFunctionAttr().getNodeValue();
    }
    public void setNetworkFunction(String networkFunction){
        getNFunctionAttr().setNodeValue(networkFunction);
    }

    public Hashtable<String, String> getHeaders(){
        Node header = getHeaderNode();
        NodeList childs = header.getChildNodes();
        Hashtable<String, String> headers = new Hashtable<>();

        for(int i = 0; i < childs.getLength(); i++){
            Node info = childs.item(i);
            Node name = info.getAttributes().getNamedItem(XML_IXP_HEADER_INFO_NAME);
            Node value = info.getAttributes().getNamedItem(XML_IXP_HEADER_INFO_VALUE);

            headers.put(name.getNodeValue(), value.getNodeValue());
        }
        return headers;
    }
    public Hashtable<String, String> getInfos(){
        Node body = getBodyNode();
        NodeList list = body.getChildNodes();
        Hashtable<String, String> infos = new Hashtable<>();

        for(int i = 0; i < list.getLength(); i++){
            Node info = list.item(i);
            Node name = info.getAttributes().getNamedItem(XML_IXP_BODY_INFO_NAME);
            Node value = info.getFirstChild();

            infos.put(name.getNodeValue(), value.getNodeValue());
        }

        return infos;
    }

    public void addHeader(String key, String value){
        Node header = getHeaderNode();

        Node nameAttr = doc.createAttribute(XML_IXP_HEADER_INFO_NAME);
        Node valueAttr = doc.createAttribute(XML_IXP_HEADER_INFO_VALUE);

        nameAttr.setNodeValue(key);
        valueAttr.setNodeValue(value);

        Node newInfo = doc.createElement(XML_IXP_HEADER_INFO);

        newInfo.getAttributes().setNamedItem(nameAttr);
        newInfo.getAttributes().setNamedItem(valueAttr);

        header.appendChild(newInfo);
    }
    public void addInfo(String key, String value){
        Node header = getBodyNode();

        Node nameAttr = doc.createAttribute(XML_IXP_HEADER_INFO_NAME);
        nameAttr.setNodeValue(key);

        Node newInfo = doc.createElement(XML_IXP_HEADER_INFO);
        newInfo.getAttributes().setNamedItem(nameAttr);;

        Node val = doc.createTextNode(value);
        newInfo.appendChild(val);

        header.appendChild(newInfo);
    }

    private Node getRootNode(){
        return doc.getDocumentElement();
    }
    private Node getTargetNode(){
        NodeList list = getRootNode().getChildNodes();

        for(int i = 0; i < list.getLength(); i++){
            Node node = list.item(i);

            if(node.getNodeName().equals(XML_IXP_TARGET))
                return node;
        }

        return null;
    }
    private Node getNFunctionAttr(){
        Node node = getTargetNode();
        NamedNodeMap attributes = node.getAttributes();
        Node attribute = attributes.getNamedItem(XML_IXP_TARGET_NFUNCTION);

        return attribute;
    }
    private Node getHeaderNode(){
        NodeList list = getRootNode().getChildNodes();

        for(int i = 0; i < list.getLength(); i++){
            Node node = list.item(i);

            if(node.getNodeName().equals(XML_IXP_HEADER))
                return node;
        }

        return null;
    }
    private Node getBodyNode(){
        NodeList list = getRootNode().getChildNodes();

        for(int i = 0; i < list.getLength(); i++){
            Node node = list.item(i);

            if(node.getNodeName().equals(XML_IXP_BODY))
                return node;
        }

        return null;
    }
}
