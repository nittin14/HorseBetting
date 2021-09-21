﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using context = System.Web.HttpContext;
using System.Configuration;


/// <summary>
/// Summary description for ErrorHandling
/// </summary>
public static class ErrorHandling
{
    private static String ErrorlineNo, Errormsg, extype, exurl, ErrorLocation, errDescription;

    public static void SendErrorToText(Exception ex)
    {
        var line = Environment.NewLine + Environment.NewLine;

        ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);


        Errormsg = ex.GetType().Name.ToString();
        extype = ex.GetType().ToString();
        exurl = context.Current.Request.Url.ToString();
        ErrorLocation = ex.Message.ToString();
        errDescription = ex.StackTrace;

        try
        {
            string filepath = ConfigurationManager.AppSettings["ErrorLog"].ToString();   //Text File Path

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);

            }
            filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
            if (!File.Exists(filepath))
            {


                File.Create(filepath).Dispose();

            }
            using (StreamWriter sw = File.AppendText(filepath))
            {
                string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "Description:" + errDescription;
                sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                sw.WriteLine("-------------------------------------------------------------------------------------");
                sw.WriteLine(line);
                sw.WriteLine(error);
                sw.WriteLine("--------------------------------*End*------------------------------------------");
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();

            }

        }
        catch (Exception e)
        {
            e.ToString();

        }
    }

    public static void CheckEachSteps(String ex)
    {
        var line = Environment.NewLine + Environment.NewLine;
       try
        {
            string filepath = ConfigurationManager.AppSettings["ErrorLog"].ToString(); 
             if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);

            }
            filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
            if (!File.Exists(filepath))
            {


                File.Create(filepath).Dispose();

            }
            using (StreamWriter sw = File.AppendText(filepath))
            {
                //string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
                //sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                string errormsg = DateTime.Today + ":" + ex;
                sw.WriteLine(errormsg);
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();

            }

        }
        catch (Exception e)
        {
            e.ToString();

        }
    }
}
