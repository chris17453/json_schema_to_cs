﻿using System;
using Microsoft.Owin.Hosting;

namespace webAPI_self_host{
    public class main{
        private static void Main(string[] args){
            int port = 8888;
            string url = "http://localhost:"+port;

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Hosted: "+url);
                Console.ReadLine();
            }        
        }
    }
}
/*
 using System.Runtime.Serialization;
using System.ComponentModel;
using Newtonsoft.Json;
 
namespace {{ Namespace }}
{
    #pragma warning disable // Disable all warnings

    {{ TypesCode | tab }}
}
     */