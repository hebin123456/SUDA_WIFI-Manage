package me.hebin.suda;

import java.io.Serializable;
import java.util.ArrayList;

public class SerializableOkHttpCookiesArray implements Serializable{
    ArrayList<SerializableOkHttpCookies> arrayList = new ArrayList<SerializableOkHttpCookies>();

    public ArrayList<SerializableOkHttpCookies> getArrayList() {
        return arrayList;
    }

    public void setArrayList(ArrayList<SerializableOkHttpCookies> arrayList) {
        this.arrayList = arrayList;
    }
}
