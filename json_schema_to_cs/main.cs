using System;
using System.Collections.Generic;
using Mono.Options;

namespace jsonschema_to_cs{
    class Program{
        static void Main(string[] args) {
            string url=string.Empty;
            string path=string.Empty;
            string @namespace=String.Empty;

            bool show_help = false;
            List<string> names = new List<string> ();

            // -n IG.kafka -u http://schema.dev.box/list.txt -p C:\repos\kafka-project\
            var p = new OptionSet () {
                { "p|path="     , "the {PATH} of c# project"  , v => { path=v; }},
                { "u|url="      , "the {URL} of text file containing all schema's to parse"     , v => { url=v; }},
                { "n|namespace=", "the {NAMESPACE} of codegenerated"                            , v => { @namespace=v; }},
                { "?|help"     , "show this message and exit"                                   , v => show_help = v != null },
            };

            List<string> extra;
            try {
                extra = p.Parse (args);
            }
            catch (OptionException e) {
                Console.Write ("jsonschema4_map: ");
                Console.WriteLine (e.Message);
                Console.WriteLine ("Try `jsonschema4_map --help' for more information.");
                return;
            }


            string contents;
            try{
            using (var wc = new System.Net.WebClient()){
                contents = wc.DownloadString(url);
                //    contents="http://schema.dev.box/candidate/event/CandidateResumeReplaced.v1.json";
             //  contents="http://schema.dev.box/engagement/event/InternalEngagementInitiated.v1.json";
                //    contents="http://schema.dev.box/engagement/esf.v1.json";

            }
            } catch(Exception ex) {
                Console.WriteLine("Downloading the url list failed. -> "+url);
                Console.WriteLine("Message: "+ex.Message);
                return ;
            }

            string [] url_list=contents.Split('\n')  ;
            
            foreach(string target_url in url_list)   {
                if(string.IsNullOrWhiteSpace(target_url)) continue;                    //skip empty lines
                model.schema_map map=new model.schema_map(target_url,path,@namespace);
                try{
                    if(!parse.parseapi(map)) Console.WriteLine("FAILED: "+target_url);
                } catch(Exception ex) {
                    Console.WriteLine("Processing failed: "+target_url);
                    Console.WriteLine("Message: "+ex.Message);
                }
            }
            
            
                Console.Write("Done.");
        }//end main

      
    }//end class
}//end namespace 
