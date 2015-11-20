package com.projectgame.ixp.Test;

import com.projectgame.ixp.IXPFile;

import java.util.Hashtable;

/**
 * Created by Beppo-Laptop on 10/8/2015.
 */
public class Main {
    public static void main(String[]args){
        try {
            String testString =
                    "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    "<IXP>" +
                        "<Target nfunction=\"test\"/>" +
                        "<Header/>" +
                        "<Body/>" +
                    "</IXP>";
            IXPFile ixp = IXPFile.parse(testString);
            ixp.addHeader("abc123", "123abc");
            ixp.addInfo("dah", "fose8j");
            System.out.println(ixp.getXML());
        }catch(Exception e){
            e.printStackTrace();
        }
    }
}
