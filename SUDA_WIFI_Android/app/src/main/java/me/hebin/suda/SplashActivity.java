package me.hebin.suda;

import android.app.Activity;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.os.Handler;
import android.os.Message;
import android.widget.Button;
import android.view.*;
import android.content.Intent;

public class SplashActivity extends Activity {
    private static final int START_ACTIVITY = 0x1;
    private boolean InMainActivity = false;

    private Button mBtnJump;

    //计时器
    private CountDownTimer countDownTimer = new CountDownTimer(3200,1000) {
        @Override
        public void onTick(long l) {
            mBtnJump.setText("跳过 "+ l / 1000 + " s" );
        }

        @Override
        public void onFinish() {
            mBtnJump.setText("跳过 "+ 0 +" s");
        }
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);

        mBtnJump = (Button)findViewById(R.id.sp_jump_btn);
        startClock();

        mBtnJump.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                InMainActivity = true;
                toMainActivity();
                //销毁, 防止出现两个activity
                //SplashActivity.super.onDestroy();
            }
        });

        //延时自动进入MainActivity
        handler.sendEmptyMessageDelayed(START_ACTIVITY,3200);
    }

    private void startClock() {
        mBtnJump.setVisibility(View.VISIBLE);
        countDownTimer.start();
    }

    private void toMainActivity() {
        Intent intent = new Intent(SplashActivity.this, MainActivity.class);
        startActivity(intent);
        finish();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        if (countDownTimer != null) {
            countDownTimer.cancel();
        }
    }

    //延时自动进入MainActivity
    private Handler handler = new Handler(){
        @Override
        public void handleMessage(Message msg) {
            System.out.println("InMainActivity = " + InMainActivity);
            //如果InMainActivity == false，则进入MainActivity，为了避免重复进入MainActivity
            if (InMainActivity == false) {
                super.handleMessage(msg);
                switch (msg.what) {
                    case START_ACTIVITY:
                        startActivity(new Intent(SplashActivity.this, MainActivity.class));
                        finish();
                        break;
                }
            }
        }
    };
}
