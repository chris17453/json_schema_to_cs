﻿using System;
using Microsoft.Owin.Hosting;

namespace model_to_webapi{
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
