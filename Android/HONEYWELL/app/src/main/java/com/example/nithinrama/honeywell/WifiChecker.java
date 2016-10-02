package com.example.nithinrama.honeywell;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ConnectException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.SocketTimeoutException;
import java.net.URL;

/**
 * Created by Nithin Rama on 30-09-2016.
 */
public class WifiChecker extends AppCompatActivity {

    private BroadcastReceiver bd;
    String mURL;

    private String mLog ="fail";
    private boolean mSuccess = true;
    private String mac = "00:00:00:00:00:00";

    @Override

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_wifi);
        SharedPreferences sp = getSharedPreferences("reg", Context.MODE_PRIVATE);
        String email = sp.getString("emailid", "");
        mURL = "http://"+constants.URL+"/api/location/mobile/"+email;
        //mURL = "http://122.167.240.91:3000/api/location/mobile/"+email;
        Button StartButton = (Button) findViewById(R.id.button);
        Button StopButton = (Button) findViewById(R.id.button2);
        StopButton.setEnabled(false);

        StartButton.setOnClickListener(new View.OnClickListener() {



            @Override
            public void onClick(View view) {

              findViewById(R.id.button2).setEnabled(true);;




                bd = new BroadcastReceiver() {
                    @Override
                    public void onReceive(Context context, Intent intent) {
                        ConnectivityManager connManager = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
                        NetworkInfo mWifi = connManager.getActiveNetworkInfo();
                        WifiManager wifiManager = (WifiManager) getSystemService(WIFI_SERVICE);

                        try {
                            if (mWifi.isConnected() == true && wifiManager.isWifiEnabled()) {

                                if (mWifi.getType() == ConnectivityManager.TYPE_WIFI) {

                                    WifiInfo wifiInfo = wifiManager.getConnectionInfo();


                                    String bssid = wifiInfo.getBSSID();
                                    if (!bssid.equals(mac)) {
                                        mac = bssid;
                                        new SendDetails().execute();

                                    }

                                    Log.v("mac", bssid);


                                    Toast.makeText(getApplicationContext(), bssid, Toast.LENGTH_LONG).show();


                                } else {
                                    Toast.makeText(getApplicationContext(), "please Switch on ur wifi or check if there is connection", Toast.LENGTH_LONG).show();
                                }
                            } else {
                                Toast.makeText(getApplicationContext(), "pls Switch on ur wifi ", Toast.LENGTH_LONG).show();
                                Log.v("sss", "wifi");

                            }
                        } catch (Exception e) {
                            Toast.makeText(getApplicationContext(), "wifi is changing or else please switch on ur wifi ", Toast.LENGTH_LONG).show();
                            Log.v("err", e.toString());
                        }

                    }

                };

                registerReceiver(bd, new IntentFilter(
                        new IntentFilter(
                                ConnectivityManager.CONNECTIVITY_ACTION)));
            }
        });
        StopButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                unregisterReceiver(bd);
                findViewById(R.id.button2).setEnabled(false);;

                mac = "00:00:00:00:00:00";



            }

        });



    }

    private class SendDetails extends AsyncTask<Void, Void, Void> {
        @Override
        protected Void doInBackground(Void... params) {
            String json = "";


            JSONObject jsonObject = new JSONObject();
            try {
                jsonObject.accumulate("mac", mac);
            } catch (JSONException e) {
                e.printStackTrace();
            }


            json = jsonObject.toString();


            try {
                URL myURL = new URL(mURL);
                HttpURLConnection conn = (HttpURLConnection) myURL.openConnection();
                conn.setDoOutput(true);
                conn.setConnectTimeout(50000);
                conn.setRequestMethod("PUT");
                conn.setRequestProperty("Content-Type", "application/json");
                conn.setRequestProperty("charset", "utf-8");
                conn.setRequestProperty("Content-Language", "en-US");
                DataOutputStream out = new DataOutputStream(conn.getOutputStream());
                out.write(json.getBytes());

                out.close();
                Log.v(mLog, Integer.toString(conn.getResponseCode()));
            } catch (MalformedURLException e) {
                // new URL() failed
                // ...
                Log.v(mLog, "In malformed URL");
            } catch (ConnectException e) {
                Log.v(mLog, "connectException");
                mSuccess = false;
            } catch (SocketTimeoutException e) {
                mSuccess = false;
            } catch (IOException e) {
                Log.v(mLog, "In IOException" + e.toString());
                mSuccess = false;
            } catch (Exception e) {
                mSuccess = false;
                Log.v(mLog, e.toString());
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            super.onPostExecute(aVoid);





        }

    }

}

