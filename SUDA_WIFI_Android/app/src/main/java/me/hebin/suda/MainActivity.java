package me.hebin.suda;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.os.StrictMode;
import android.util.Base64;
import android.util.Log;
import android.view.*;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.google.gson.Gson;

import java.util.HashMap;

public class MainActivity extends Activity {
    private static final String ASUDA = "http://a.suda.edu.cn/index.php/index/";

    // 定义一个变量，来标识是否退出
    private static boolean isExit = false;

    private Button btn_Forget;
    private Button btn_Regist;
    private Button btn_Login;
    private Button btn_Manage;
    private EditText et_Username;
    private EditText et_Password;
    private boolean isLogin = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        initController();

        //严苛模式，使主线程可以处理网络请求
        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }

        btn_Forget.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast toast = Toast.makeText(MainActivity.this, "请到电脑上操作或联系网管中心！", (int)2000);
                toast.setGravity(Gravity.CENTER, 0, 0);
                toast.show();
            }
        });

        btn_Regist.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast toast = Toast.makeText(MainActivity.this, "暂时不支持", (int)2000);
                toast.setGravity(Gravity.CENTER, 0, 0);
                toast.show();
            }
        });

        btn_Login.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                String btn_Text = ((Button) v).getText().toString();
                switch (btn_Text){
                    case "登录":
                        String Username = et_Username.getText().toString();
                        String Password = et_Password.getText().toString();
                        Toast.makeText(MainActivity.this, Login(Username, Password), (int)2000).show();
                        if(isLogin){
                            SharedPreferences settings = getSharedPreferences("hebin", 0);
                            SharedPreferences.Editor editor = settings.edit();
                            editor.clear();
                            editor.putString("Username", Username);
                            editor.putString("Password", Password);

                            // 提交本次编辑
                            editor.commit();

                            et_Username.setFocusable(false);
                            et_Username.setFocusableInTouchMode(false);
                            et_Password.setFocusable(false);
                            et_Password.setFocusableInTouchMode(false);

                            ((Button) v).setText("退出");
                        }
                        break;
                    case "退出":
                        Toast.makeText(MainActivity.this, Logout(), (int)2000).show();
                        if(!isLogin){
                            et_Username.setFocusable(true);
                            et_Username.setFocusableInTouchMode(true);
                            et_Password.setFocusable(true);
                            et_Password.setFocusableInTouchMode(true);

                            SharedPreferences settings = getSharedPreferences("hebin", 0);
                            Username = settings.getString("Username","");
                            Password = settings.getString("Password","");
                            et_Username.setText(Username);
                            et_Password.setText(Password);

                            ((Button) v).setText("登录");
                        }
                        break;
                    default:
                        break;
                }
                //toTestActivity();
            }
        });

        btn_Manage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                toManageActivity();
            }
        });

        if(hasLogin()) {
            isLogin = true;
            btn_Login.setText("退出");
        }
        else {
            isLogin = false;
            btn_Login.setText("登录");
        }
    }

    private void initController(){
        et_Username = (EditText)findViewById(R.id.editTextUsername);
        et_Password = (EditText)findViewById(R.id.editTextPassword);
        btn_Forget = (Button)findViewById(R.id.buttonForget);
        btn_Regist = (Button)findViewById(R.id.buttonRegist);
        btn_Login = (Button)findViewById(R.id.buttonLogin);
        btn_Manage = (Button)findViewById(R.id.buttonManage);

        SharedPreferences settings = getSharedPreferences("hebin", 0);
        String Username = settings.getString("Username","");
        String Password = settings.getString("Password","");
        et_Username.setText(Username);
        et_Password.setText(Password);
    }

    //检测是否登录
    private Boolean hasLogin(){
        HttpHelper httpHelper = new HttpHelper();
        String result = httpHelper.DoGet(ASUDA + "init");

        CheckLoginBean checkLoginBean = new CheckLoginBean();
        checkLoginBean = new Gson().fromJson(result, CheckLoginBean.class);
        if(checkLoginBean != null) {
            if(checkLoginBean.getStatus() == 1){
                et_Username.setText(checkLoginBean.getLogout_username());
                et_Password.setText("********");
                et_Username.setFocusable(false);
                et_Username.setFocusableInTouchMode(false);
                et_Password.setFocusable(false);
                et_Password.setFocusableInTouchMode(false);
                return true;
            }
            else{
                return false;
            }
        }
        else{
            return false;
        }
    }

    //登录
    private String Login(String Username, String Password){
        HttpHelper httpHelper = new HttpHelper();
        HashMap<String, String> hashMap = new HashMap<String, String>();
        hashMap.put("username", Username);
        hashMap.put("password", new String(Base64.encode(Password.getBytes(), Base64.DEFAULT)));
        hashMap.put("enablemacauth", "0");
        String result = httpHelper.DoPostForm(ASUDA + "login", hashMap);

        LoginBean loginBean = new LoginBean();
        loginBean = new Gson().fromJson(result, LoginBean.class);

        if(loginBean == null){
            return "登录失败";
        }
        else{
            if(loginBean.getStatus() == 1){
                isLogin = true;
                return "登录成功";
            }
            else {
                return loginBean.getInfo();
            }
        }
    }

    //注销
    private String Logout(){
        HttpHelper httpHelper = new HttpHelper();
        String result = httpHelper.DoPostJson(ASUDA + "logout", "");

        LogoutBean logoutBean = new LogoutBean();
        logoutBean = new Gson().fromJson(result, LogoutBean.class);
        if(logoutBean == null) {
            return "退出失败";
        }
        else{
            isLogin = false;
            if(logoutBean.getStatus() == 1){
                return "退出成功";
            }
            else {
                return logoutBean.getInfo();
            }
        }
    }

    private void toManageActivity() {
        Intent intent = new Intent(MainActivity.this, ManageActivity.class);
        startActivity(intent);
    }

    private void toTestActivity() {
        Intent intent = new Intent(MainActivity.this, TestActivity.class);
        startActivity(intent);
    }

    Handler mHandler = new Handler() {

        @Override
        public void handleMessage(Message msg) {
            super.handleMessage(msg);
            isExit = false;
        }
    };

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
            exit();
            return false;
        }
        return super.onKeyDown(keyCode, event);
    }

    private void exit() {
        if (!isExit) {
            isExit = true;
            Toast.makeText(getApplicationContext(), "再按一次退出程序",
                    Toast.LENGTH_SHORT).show();
            // 利用handler延迟发送更改状态信息
            mHandler.sendEmptyMessageDelayed(0, 2000);
        } else {
            finish();
            System.exit(0);
        }
    }
}