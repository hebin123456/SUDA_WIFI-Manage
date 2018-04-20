package me.hebin.suda;

public class ManageLoginBean {
    /**
     * data : null
     * info : 验证码错误
     * status : 1
     */

    private Object data;
    private String url;
    private String info;
    private int status;

    public Object getData() {
        return data;
    }

    public void setData(Object data) {
        this.data = data;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public String getInfo() {
        return info;
    }

    public void setInfo(String info) {
        this.info = info;
    }

    public int getStatus() {
        return status;
    }

    public void setStatus(int status) {
        this.status = status;
    }
}
