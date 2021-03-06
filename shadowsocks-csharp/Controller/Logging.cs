﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Shadowsocks.Controller
{
    public class Logging
    {
        public static string LogFile;

        public static bool OpenLogFile()
        {
            try
            {
                string temppath = Path.GetTempPath();
                LogFile = Path.Combine(temppath, "shadowsocks.log");
                FileStream fs = new FileStream(LogFile, FileMode.Append);
                TextWriter tmp = Console.Out;
                StreamWriter sw = new StreamWriter(fs);
                sw.AutoFlush = true;
                Console.SetOut(sw);
                Console.SetError(sw);
                
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public static void LogUsefulException(Exception e)
        {
            // just log useful exceptions, not all of them
            if (e is SocketException)
            {
                SocketException se = (SocketException)e;
                if (se.SocketErrorCode == SocketError.ConnectionAborted)
                {
                    // closed by browser when sending
                    // normally happens when download is canceled or a tab is closed before page is loaded
                }
                else if (se.SocketErrorCode == SocketError.ConnectionReset)
                {
                    // received rst
                }
                else
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                Console.WriteLine(e);
            }
        }
    }
}
