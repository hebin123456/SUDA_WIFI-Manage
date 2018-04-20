package me.hebin.suda;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AbsListView.OnScrollListener;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import okhttp3.Cookie;

public class ManageListActivity extends Activity {
    private List<String> list = new ArrayList<String>(15);
    private Set<SwipeListLayout> sets = new HashSet();
    private static final String ASUDA = "http://10.9.0.14/index.php/service/";
    private HttpHelper httpHelper;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_list);

        getWindow().setStatusBarColor(getResources().getColor(R.color.lightblue));

        //严苛模式，使主线程可以处理网络请求
        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }

        httpHelper = new HttpHelper();

        /*Bundle bundle = getIntent().getExtras();
        SerializableList serializableList = (SerializableList) bundle.get("cookie");

        httpHelper.setCookieList(toCookielist(serializableList.getList()));*/

        Intent intent = getIntent();
        Bundle bundle = intent.getExtras();
        httpHelper.setCookieList(toList((ArrayList<SerializableOkHttpCookies>) bundle.getSerializable("cookie")));

        //Log.e("error", httpHelper.DoGet(ASUDA + "AccountInfo/index/"));

        ListView lv_main = (ListView) findViewById(R.id.lv_main);

        initList(httpHelper.DoGet(ASUDA + "AccountInfo/index/"));
        lv_main.setAdapter(new ListAdapter());
        lv_main.setOnScrollListener(new OnScrollListener() {

            @Override
            public void onScrollStateChanged(AbsListView view, int scrollState) {
                switch (scrollState) {
                    //当listview开始滑动时，若有item的状态为Open，则Close，然后移除
                    case SCROLL_STATE_TOUCH_SCROLL:
                        if (sets.size() > 0) {
                            for (SwipeListLayout s : sets) {
                                s.setStatus(SwipeListLayout.Status.Close, true);
                                sets.remove(s);
                            }
                        }
                        break;
                }
            }

            @Override
            public void onScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount) {

            }
        });
    }

    private List<Cookie> toList(ArrayList<SerializableOkHttpCookies> list){
        List<Cookie> objlist = new ArrayList<Cookie>();
        for(SerializableOkHttpCookies e : list){
            objlist.add(e.getCookies());
        }
        return objlist;
    }

    private void initList(String html) {
        try { //加载一个Document对象
            Document doc = Jsoup.parse(html);
            Element table = doc.getElementById("onlineTable");
            Elements tr = table.getElementsByTag("tr");
            int i = 0;
            for(Element element : tr){
                if(i > 0){
                    int j = 0;
                    String item = "";
                    Elements td = element.getElementsByTag("td");
                    for(Element subelement : td){
                        if(j == 1) {
                            item += "位置：" + subelement.text();
                        }
                        if(j == 2) {
                            item += " IP：" + subelement.text();
                        }
                        j++;
                    }
                    list.add(item);
                }
                i++;
            }
        }
        catch (Exception e){
        }
    }

    class MyOnSlipStatusListener implements SwipeListLayout.OnSwipeStatusListener {

        private SwipeListLayout slipListLayout;

        public MyOnSlipStatusListener(SwipeListLayout slipListLayout) {
            this.slipListLayout = slipListLayout;
        }

        @Override
        public void onStatusChanged(SwipeListLayout.Status status) {
            if (status == SwipeListLayout.Status.Open) {
                //若有其他的item的状态为Open，则Close，然后移除
                if (sets.size() > 0) {
                    for (SwipeListLayout s : sets) {
                        s.setStatus(SwipeListLayout.Status.Close, true);
                        sets.remove(s);
                    }
                }
                sets.add(slipListLayout);
            } else {
                if (sets.contains(slipListLayout))
                    sets.remove(slipListLayout);
            }
        }

        @Override
        public void onStartCloseAnimation() {

        }

        @Override
        public void onStartOpenAnimation() {

        }

    }

    class ListAdapter extends BaseAdapter {

        @Override
        public int getCount() {
            return list.size();
        }

        @Override
        public Object getItem(int arg0) {
            return list.get(arg0);
        }

        @Override
        public long getItemId(int arg0) {
            return arg0;
        }

        @Override
        public View getView(final int arg0, View view, ViewGroup arg2) {
            if (view == null) {
                view = LayoutInflater.from(ManageListActivity.this).inflate(
                        R.layout.slip_item_layout, null);
            }
            TextView tv_name = (TextView) view.findViewById(R.id.tv_name);
            tv_name.setText(list.get(arg0));
            final SwipeListLayout slip_main = (SwipeListLayout) view
                    .findViewById(R.id.slip_item_layout);
            //TextView tv_top = (TextView) view.findViewById(R.id.tv_top);
            TextView tv_delete = (TextView) view.findViewById(R.id.tv_delete);
            slip_main.setOnSwipeStatusListener(new MyOnSlipStatusListener(
                    slip_main));
            /*tv_top.setOnClickListener(new OnClickListener() {

                @Override
                public void onClick(View view) {
                    sll_main.setStatus(SwipeListLayout.Status.Close, true);
                    String str = list.get(arg0);
                    list.remove(arg0);
                    list.add(0, str);
                    notifyDataSetChanged();
                }
            });*/
            tv_delete.setOnClickListener(new OnClickListener() {
                @Override
                public void onClick(View view) {
                    slip_main.setStatus(SwipeListLayout.Status.Close, false);

                    String content = list.get(arg0).toString();
                    String reg = "(\\d+\\.){3}[\\d]+";
                    Pattern pattern= Pattern.compile(reg);
                    //让正则对象和要作用的字符串相关联，获取匹配器对象。把多个方式都封装到了匹配器当中。
                    //引擎或匹配器
                    Matcher matcher = pattern.matcher(content);

                    if(matcher.find()){
                        String ip = matcher.group();

                        //Toast.makeText(ManageListActivity.this, group, (int)2000).show();
                        if(Logout(ASUDA + "AccountInfo/logout", ip)) {
                            list.remove(arg0);
                            notifyDataSetChanged();
                            Toast.makeText(ManageListActivity.this, "下线成功", (int)2000).show();
                        }
                        else {
                            Toast.makeText(ManageListActivity.this, "下线失败", (int)2000).show();
                        }
                    }
                }
            });
            return view;
        }

        private boolean Logout(String url, String ip){
            HashMap<String, String> hashMap = new HashMap<String, String>();
            hashMap.put("framedipaddress", ip);
            hashMap.put("onlinetype", "0");

            String result = httpHelper.DoPostForm(url, hashMap);

            ManageLogoutBean manageLogoutBean = new ManageLogoutBean();
            manageLogoutBean = new Gson().fromJson(result, ManageLogoutBean.class);

            if(manageLogoutBean == null){
                return false;
            }
            else{
                if(manageLogoutBean.getStatus() == 1){
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }
}
