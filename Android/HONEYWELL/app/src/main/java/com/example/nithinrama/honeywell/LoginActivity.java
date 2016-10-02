package com.example.nithinrama.honeywell;

import android.app.LoaderManager.LoaderCallbacks;
import android.content.Context;
import android.content.Intent;
import android.content.Loader;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.util.Patterns;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
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
 * A login screen that offers login via email/password.
 */
public class LoginActivity extends AppCompatActivity implements LoaderCallbacks<Cursor> {


    static String mURL = "http://"+constants.URL+"/api/location/mobile";

    // UI references.
    TelephonyManager tel;
    String Phone_id;
    private String mLog ="fail";
    private boolean mSuccess = true;
    private String mEmployeeid;
    private String mEmployeename;
    private View mProgressView;
    private View mLoginFormView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        SharedPreferences sp = getSharedPreferences("reg", Context.MODE_PRIVATE);
        boolean b = sp.getBoolean("reg", false);
        if (b) {
            startActivity(new Intent(this, WifiChecker.class));
            finish();
        } else {
            setContentView(R.layout.activity_main);




            FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);


            fab.setOnClickListener(new OnClickListener() {

                @Override
                public void onClick(View view) {
                    boolean connection = isConnected();
                    if (!connection) {
                        Toast.makeText(getApplicationContext(), "Please check Internet connection and try again", Toast.LENGTH_LONG)
                                .show();
                    } else {
                        EditText e = (EditText) findViewById(R.id.employee_id);
                        mEmployeeid = e.getText().toString();
                        e = (EditText) findViewById(R.id.employee_name);
                        mEmployeename = e.getText().toString();
                        tel = (TelephonyManager) getSystemService(Context.TELEPHONY_SERVICE);
                        Phone_id = tel.getDeviceId().toString();

                        boolean match = Patterns.EMAIL_ADDRESS.matcher(mEmployeeid).matches();
                        if(!match||mEmployeename.equals("")||mEmployeeid.equals(""))
                        {
                            Toast.makeText(getApplicationContext(),"PLEASE FILL IN ALL THE ENTRIES",Toast.LENGTH_LONG).show();
                        }
                        else
                        {
                            new SendDetails().execute();

                        }


                    }

                }
            });

        }
    }



    @Override
    protected void onResume() {
        super.onResume();
    }




    private boolean isConnected()
    {
        ConnectivityManager cm =
                (ConnectivityManager)this.getSystemService(Context.CONNECTIVITY_SERVICE);

        NetworkInfo activeNetwork = cm.getActiveNetworkInfo();
        return (activeNetwork != null && activeNetwork.isConnectedOrConnecting());
    }

    @Override
    public Loader<Cursor> onCreateLoader(int i, Bundle bundle) {
        return null;
    }

    @Override
    public void onLoadFinished(Loader<Cursor> loader, Cursor cursor) {

    }

    @Override
    public void onLoaderReset(Loader<Cursor> loader) {

    }


    private class SendDetails extends AsyncTask<Void,Void,Void>
    {
        @Override
        protected Void doInBackground(Void... params) {
            String json = "";

            // 3. build jsonObject
            JSONObject jsonObject = new JSONObject();
            try{
            jsonObject.accumulate("email", mEmployeeid);
            jsonObject.accumulate("name", mEmployeename);

                jsonObject.accumulate("imei", Phone_id);
            } catch (JSONException e) {
                e.printStackTrace();
            }

            // 4. convert JSONObject to JSON to String
            json = jsonObject.toString();
            Log.v("id",Phone_id);
            Log.v("id",mEmployeeid);
            Log.v("id",mEmployeename);

            String pass = "email=";
            String pass1 = "&name=";
            String id ="&imei=";

            try {
                URL myURL = new URL(mURL);
                HttpURLConnection conn = (HttpURLConnection)myURL.openConnection();
                conn.setDoOutput(true);
                conn.setConnectTimeout(50000);
                conn.setRequestMethod("POST");
                conn.setRequestProperty("Content-Type", "application/json");
                conn.setRequestProperty("charset", "utf-8");
                conn.setRequestProperty("Content-Language", "en-US");
                DataOutputStream out = new DataOutputStream(conn.getOutputStream());
                out.write(json.getBytes());

                out.close();
                Log.v(mLog, Integer.toString(conn.getResponseCode()));
            }
            catch (MalformedURLException e) {
                // new URL() failed
                // ...
                Log.v(mLog,"In malformed URL");
            }
            catch (ConnectException e)
            {
                Log.v(mLog, "connectException");
                mSuccess = false;
            }
            catch (SocketTimeoutException e)
            {
                mSuccess = false;
            }
            catch (IOException e) {
                Log.v(mLog, "In IOException" + e.toString());
                mSuccess = false;
            }
            catch (Exception e)
            {
                mSuccess = false;
                Log.v(mLog,e.toString());
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            super.onPostExecute(aVoid);
            //Toast.makeText(getApplicationContext(),"async done",Toast.LENGTH_SHORT).show();

            if(mSuccess)
            {
                getSharedPreferences("reg",Context.MODE_PRIVATE).edit().putBoolean("reg", true).commit();
                getSharedPreferences("reg",Context.MODE_PRIVATE).edit().putString("emailid",mEmployeeid).commit();

                Intent intent = new Intent(getApplicationContext(),WifiChecker.class);
                startActivity(intent);

            }
            else
            {
                Toast.makeText(getApplicationContext(),"could not authenticate,try again",Toast.LENGTH_LONG)
                        .show();
            }
        }
    }
}
