using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace kafka_json_parser{
    class Program{
        static void Main(string[] args) {
            
            string contents;
            string url="http://schema.dev.box/list.txt";
            try{
            using (var wc = new System.Net.WebClient()){
                contents = wc.DownloadString(url);
            }
            } catch(Exception ex) {
                Console.WriteLine("Downloading the url list failed. -> "+url);
                Console.ReadKey();
                return ;
            }

            string [] uri_list=contents.Split('\n')  ;
            
            foreach(string uri in uri_list)   {
                if(string.IsNullOrWhiteSpace(uri)) continue;                    //skuip empty lines
                if(uri=="http://schema.dev.box/candidate/event/InHouseVisitAdded.v1.json") continue;
                model.schema_map map=new model.schema_map(uri);
                try{
                if(!parse.jsonAsync(map)) Console.WriteLine("FAILED: "+uri);
                } catch(Exception ex) {
                    Console.WriteLine("Processing failed: "+uri);

                }
            }
            Console.Write("Done.");
            Console.ReadKey();
        }//end main

      
    }//end class
}//end namespace 
