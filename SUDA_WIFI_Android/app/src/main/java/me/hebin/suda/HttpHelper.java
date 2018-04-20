package me.hebin.suda;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import okhttp3.Cookie;
import okhttp3.CookieJar;
import okhttp3.FormBody;
import okhttp3.HttpUrl;
import okhttp3.OkHttpClient;
import okhttp3.MediaType;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;
import okhttp3.ResponseBody;

public class HttpHelper {
    private static final MediaType JSON  = MediaType.parse("application/json; charset=utf-8");
    private static final MediaType Form  = MediaType.parse("application/x-www-form-urlencoded; charset=utf-8");
    private OkHttpClient okHttpClient;
    private List<Cookie> cookieList;

    public List<Cookie> getCookieList() {
        return cookieList;
    }

    public void setCookieList(List<Cookie> cookieList) {
        this.cookieList = cookieList;
    }

    public HttpHelper(){
        okHttpClient = new OkHttpClient.Builder()
        .cookieJar(new CookieJar() {
            @Override
            public void saveFromResponse(HttpUrl httpUrl, List<Cookie> list) {
                cookieList = list;
            }

            @Override
            public List<Cookie> loadForRequest(HttpUrl httpUrl) {
                List<Cookie> cookies = cookieList;
                return cookies != null ? cookies : new ArrayList<Cookie>();
            }
        })
        .build();
    }

    String DoGet(String url){
        Request request = new Request.Builder()
                .url(url)
                .build();

        try (Response response = okHttpClient.newCall(request).execute()) {
            return response.body().string();
        }
        catch(IOException e){
            return "";
        }
    }

    String DoPostJson(String url, String json){
        RequestBody body = RequestBody.create(JSON, json);
        Request request = new Request.Builder()
                .url(url)
                .post(body)
                .build();
        try (Response response = okHttpClient.newCall(request).execute()) {
            return response.body().string();
        }
        catch(IOException e){
            return "";
        }
    }

    String DoPostForm(String url, HashMap<String,String> form){
        FormBody.Builder body=new FormBody.Builder();
        for (String key : form.keySet()) {
            body.add(key, form.get(key));
        }
        Request request = new Request.Builder()
                .url(url)
                .post(body.build())
                .build();
        try (Response response = okHttpClient.newCall(request).execute()) {
            return response.body().string();
        }
        catch(IOException e){
            return "";
        }
    }

    Bitmap GetValid(String url){
        Bitmap bitmap = null;
        try {
            //获取请求对象
            Request request = new Request.Builder().url(url).build();

            //获取响应体
            ResponseBody body = okHttpClient.newCall(request).execute().body();

            //获取流
            InputStream in = body.byteStream();
            //转化为bitmap
            bitmap = BitmapFactory.decodeStream(in);
        }
        catch (IOException e){

        }
        return bitmap;
    }
}
