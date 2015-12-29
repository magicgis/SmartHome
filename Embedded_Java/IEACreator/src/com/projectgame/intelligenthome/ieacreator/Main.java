package com.projectgame.intelligenthome.ieacreator;

import com.projectgame.intelligenthome.core.IEAData;

import java.io.*;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLClassLoader;

/**
 * Created by Beppo-Laptop on 11/10/2015.
 */
public class Main {
    public static void main(String[] args){
        if(args.length != 5) {
            System.out.println("Wrong param count. You need the following params:");
            System.out.println("1: Your App Name");
            System.out.println("2: Path to apps png icon");
            System.out.println("3: Path to the apps jar file");
            System.out.println("4: Class that extends the application class");
            System.out.println("5: Destination path with .iea extension");
            return;
        }

        File imageSource = new File(args[1]);
        File jarSource = new File(args[2]);
        File destination = new File(args[4]);

        if(!imageSource.exists() || imageSource.isDirectory()){
            System.out.println("Image not found");
            return;
        }

        if(!jarSource.exists() || jarSource.isDirectory()){
            System.out.println("Jar not found");
            return;
        }

        if(!destination.getParentFile().exists()){
            System.out.println("Destination directory does not exist");
            return;
        }

        if(!args[4].endsWith(".iea")){
            System.out.println("Wrong destination extension");
            return;
        }

        if(!classExists(jarSource, args[3])){
            System.out.println("Jar does not contain the specified class");
            return;
        }

        byte[] image = null;
        byte[] jar = null;

        try {
            FileInputStream fin = new FileInputStream(imageSource);
            ByteArrayOutputStream bout = new ByteArrayOutputStream();
            byte[] buffer = new byte[1024];
            int read = 0;

            while((read = fin.read(buffer)) != -1){
                bout.write(buffer, 0, read);
            }

            image = bout.toByteArray();

            fin.close();
            fin = new FileInputStream(jarSource);
            bout = new ByteArrayOutputStream();
            buffer = new byte[1024];
            read = 0;

            while((read = fin.read(buffer)) != -1){
                bout.write(buffer, 0,  read);
            }

            jar = bout.toByteArray();
            fin.close();
        } catch (Exception e) {
            e.printStackTrace();
            return;
        }

        IEAData ieaData = new IEAData(args[0], args[3], image, jar);

        try {
            FileOutputStream fout = new FileOutputStream(destination);
            ObjectOutputStream oout = new ObjectOutputStream(fout);
            oout.writeObject(ieaData);
            oout.close();
            fout.close();
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    private static boolean classExists(File jar, String className){
        try {
            ClassLoader loader = URLClassLoader.newInstance(new URL[]{jar.toURL()}, new Main().getClass().getClassLoader());
            Class<?> dest = Class.forName(className, true, loader);

            return dest != null;

        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }
}
