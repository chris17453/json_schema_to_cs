using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kafka_json_parser.model{
    public class schema_map {
        public string uri           { get; set; }
        public string base_dir      { get; set; }
        public string base_name     { get; set; }
        public string event_name    { get; set; }
        public string code_file     { get; set; }
        public string code_dir      { get; set; }
        public string @namespace    { get; set; }
        public schema_map(string uri) {
            this.uri=uri;
            this.base_dir=@"C:\repos\atlis_cs_project\";
            if(string.IsNullOrWhiteSpace(uri)) {
                Console.WriteLine("Invalid uri");
                return;
            }
            string[] tokens =uri.Split('/');
            string   file=tokens.Last();
            string[] tokens2=file.Split('.');
            this.base_name=tokens2.First();
            this.event_name=base_name.ToLower();
            string prefix="http://";
            int indexof=uri.LastIndexOf('/');
            string[] tokens3=uri.Substring(prefix.Length,indexof-prefix.Length).Split('/');

            code_dir="";
            foreach(string path in tokens3) {
                if(path.Length>3) {
                    if(!string.IsNullOrWhiteSpace(code_dir)) {
                        code_dir+=@"\"+path;
                    } else {
                        code_dir+=path;
                    }
                }
            }

            code_dir=base_dir+"\\"+code_dir;

            this.code_file=String.Format(@"{0}\{1}.cs",code_dir,base_name);
            this.@namespace="IG.kafka."+this.event_name;
                
        }//end constructor
    }//end class
}//end namespace
