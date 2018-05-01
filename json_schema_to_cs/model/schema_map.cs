using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonschema_to_cs.model{
    public class schema_map {
        public string url           { get; set; }
        public string base_dir      { get; set; }
        public string base_name     { get; set; }
        public string event_name    { get; set; }
        public string code_file     { get; set; }
        public string code_dir      { get; set; }
        public string web_api_dir   { get; set; }
        public string web_api_file  { get; set; }
        public string @namespace    { get; set; }
        private static bool IsLinux {
            get {
                int p = (int) Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public schema_map(string url,string path,string code_namespace) {
            this.url=url;
            this.base_dir=path;
            if(string.IsNullOrWhiteSpace(url)) {
                Console.WriteLine("Invalid uri");
                return;
            }
            string[] tokens =url.Split('/');
            string   file=tokens.Last();
            string[] tokens2=file.Split('.');
            this.base_name=tokens2.First();
            event_name=base_name.ToLower();
            string prefix="http://";
            int indexof=url.LastIndexOf('/');
            string[] tokens3=url.Substring(prefix.Length,indexof-prefix.Length).Split('/');

            code_dir="";
            foreach(string path_part in tokens3) {
                if(path_part.Length>3) {
                    if(!string.IsNullOrWhiteSpace(code_dir)) {
                        code_dir+=@"\"+path_part;
                    } else {
                        code_dir+=path_part;
                    }
                }
            }

            code_dir   =String.Format(@"{0}\models\{1}",base_dir,code_dir);
            web_api_dir=String.Format(@"{0}\controllers\{1}",base_dir,code_dir);

            if(IsLinux) {
                this.code_dir   =this.code_dir.Replace('\\','/');                        //because i dont live in windows
                this.web_api_dir=this.code_dir.Replace('\\','/');                        //because i dont live in windows
                this.code_file   =String.Format(@"{0}/{1}.cs",code_dir   ,base_name);
                this.web_api_file=String.Format(@"{0}/{1}Controller.cs",web_api_dir,base_name);
            } else {
                this.code_file=String.Format(@"{0}\{1}.cs",code_dir,base_name);
                this.web_api_file=String.Format(@"{0}\{1}Controller.cs",web_api_dir,base_name);
            }
            if(code_namespace=="event") code_namespace="@event";
            

            this.@namespace=string.Format("{0}.{1}",code_namespace,event_name);

        }//end constructor
    }//end class
}//end namespace
