package com.projectgame.intelligenthome.clock.model;

/**
 * Created by Beppo-Laptop on 10/11/2015.
 */
public class Time {
    private int hours;
    private int minutes;
    private int seconds;

    public Time() {
        hours = 0;
        minutes = 0;
        seconds = 0;
    }
    public Time(int hours, int minutes, int seconds){
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }

    public int getHours(){
        return hours;
    }
    public void setHours(int hours){
        this.hours = hours;
    }

    public int getMinutes(){
        return minutes;
    }
    public void setMinutes(int minutes){
        this.minutes = minutes;
    }

    public int getSeconds(){
        return seconds;
    }
    public void setSeconds(int seconds){
        this.seconds = seconds;
    }

    public Time clone(){
        return new Time(hours, minutes, seconds);
    }
}
