package me.hebin.suda;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Base64;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Toast;

import com.google.gson.Gson;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import okhttp3.Cookie;
import okhttp3.CookieJar;
import okhttp3.HttpUrl;
import okhttp3.OkHttpClient;

public class ManageActivity extends Activity {
    private static final String ASUDA = "http://10.9.0.14/index.php/service/";
    private ImageView imageView_ShowValid;
    private Button btn_Forget;
    private Button btn_Login;
    private EditText et_Username;
    private EditText et_Password;
    private EditText et_Valid;
    private HttpHelper httpHelper;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage);

        initController();

        //严苛模式，使主线程可以处理网络请求
        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }

        httpHelper = new HttpHelper();

        imageView_ShowValid.setImageBitmap(getValid());

        imageView_ShowValid.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ((ImageView) v).setImageBitmap(getValid());
            }
        });

        btn_Forget.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast toast = Toast.makeText(ManageActivity.this, "请到电脑上操作或联系网管中心！", (int)2000);
                toast.setGravity(Gravity.CENTER, 0, 0);
                toast.show();
            }
        });

        btn_Login.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                String Username = et_Username.getText().toString();
                String Password = et_Password.getText().toString();
                String Valid = et_Valid.getText().toString();
                String result = Login(Username, Password, Valid);
                if(result != "登录成功"){
                    Toast toast = Toast.makeText(ManageActivity.this, result, (int)2000);
                    toast.setGravity(Gravity.CENTER, 0, 0);
                    toast.show();
                }
                else{
                    toManageActivity(httpHelper);
                }
            }
        });
    }

    private void initController(){
        imageView_ShowValid = (ImageView)findViewById(R.id.imageViewShowValid);
        btn_Forget = (Button)findViewById(R.id.buttonForget);
        et_Username = (EditText)findViewById(R.id.editTextUsername);
        et_Password = (EditText)findViewById(R.id.editTextPassword);
        et_Valid = (EditText)findViewById(R.id.editTextValid);
        btn_Login = (Button)findViewById(R.id.buttonLogin);

        SharedPreferences settings = getSharedPreferences("hebin", 0);
        String Username = settings.getString("Username","");
        String Password = settings.getString("Password","");
        et_Username.setText(Username);
        et_Password.setText(Password);
    }

    private void toManageActivity(HttpHelper httpHelper) {
        Intent intent = new Intent(ManageActivity.this, ManageListActivity.class);

        /*Bundle bundle = new Bundle();
        bundle.putString("cookie", persistentCookieStore.encodeCookie(new SerializableOkHttpCookies(httpHelper.getCookieList())));

        intent.setAction("action");
        intent.putExtra("cookielist", toArrayList(httpHelper.getCookieList()));

        intent.putExtras(bundle);*/

        Bundle bundle = new Bundle();
        bundle.putSerializable("cookie", toArrayList(httpHelper.getCookieList()));
        intent.putExtras(bundle);

        startActivity(intent);
    }

    private ArrayList<SerializableOkHttpCookies> toArrayList(List<Cookie> list){
        ArrayList<SerializableOkHttpCookies> objlist = new ArrayList<SerializableOkHttpCookies>();
        for(Cookie e : list){
            objlist.add(new SerializableOkHttpCookies(e));
        }
        return objlist;
    }

    private Bitmap getValid(){
        return httpHelper.GetValid(ASUDA + "public/verify/");
    }

    private String Login(String Username, String Password, String Valid){
        MD5Util md5Util = new MD5Util();
        HashMap<String, String> hashMap = new HashMap<String, String>();
        hashMap.put("username", Username);
        hashMap.put("password", new String(Base64.encode(Password.getBytes(), Base64.DEFAULT)));
        hashMap.put("verify", MD5Util.encrypt(Valid));

        String result = httpHelper.DoPostForm(ASUDA + "public/checkLogin", hashMap);
        //Log.e("error", "123\n");
        //Log.e("error", result);
        ManageLoginBean manageLoginBean = new ManageLoginBean();
        manageLoginBean = new Gson().fromJson(result, ManageLoginBean.class);

        if(manageLoginBean == null){
            return "登录失败";
        }
        else{
            if(manageLoginBean.getStatus() == 1){
                return "登录成功";
            }
            else{
                return manageLoginBean.getInfo();
            }
        }
    }
}
