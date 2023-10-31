package com.streambase;

import java.io.InputStream;

import org.slf4j.Logger;

import quickfix.ConfigError;
import quickfix.FieldConvertError;
import quickfix.FieldNotFound;
import quickfix.Message;
import quickfix.SessionID;
import quickfix.SessionSettings;
import quickfix.field.MsgType;

import com.streambase.sb.StreamBaseException;
import com.streambase.sb.adapter.fix.engine.quickfixj.QFJAdminMessageInterceptor;

public class CustomInterceptor implements QFJAdminMessageInterceptor{
	
	public static final String PASSWORD_FIELD_NAME = "Password";
	private String configuredPassword;
	
	@Override
	public void setConfigFile(Logger logger, InputStream isConfig) throws StreamBaseException{
        SessionSettings ss;
        try{
            ss = new SessionSettings(isConfig);
            if (ss.isSetting(PASSWORD_FIELD_NAME)) {
            	
                configuredPassword = ss.getString(PASSWORD_FIELD_NAME);
            
            }else{
            	
            	System.out.println(this.getClass().getName() + ": Cannot find \"" + PASSWORD_FIELD_NAME + "\" directive in QuickFIX/J configuration file");
            	
            }
        }catch (ConfigError ce){
        	
            throw new StreamBaseException(this.getClass().getName() + ": Error loading QuickFIX/J configuration file.", ce);
            
        }catch (FieldConvertError fce){
        	
            throw new StreamBaseException(this.getClass().getName() + ": Error reading \"" + PASSWORD_FIELD_NAME + "\" directive from QuickFIX/J configuration file", fce);
            
        }
    }
	
	@Override
	public void onAdminMessage(SessionID sessionID, Message msg) {
		
		// Are we trying to log on (i.e. MsgType="A"?)
		try {
			if (msg.getHeader().getString(MsgType.FIELD).compareTo("A") == 0){
				// Add the fields to the message
				msg.setInt(95, configuredPassword.length());
				msg.setString(96, configuredPassword);
			}
		} catch (FieldNotFound e) {
			System.out.println(this.getClass().getName() + ": MsgType field is missing.");
			e.printStackTrace();
		}

	}
	
}
