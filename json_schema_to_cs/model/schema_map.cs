using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonschema_to_cs.model{
    public class schema_map {
        public string url                 { get; set; }
        public string base_dir            { get; set; }
        public string base_name           { get; set; }
        public string event_name          { get; set; }
        public string code_file           { get; set; }
        public string code_dir            { get; set; }
        public string compiled_json_dir   { get; set; }
        public string compiled_json_path  { get; set; }
        public string web_api_dir         { get; set; }
        public string web_api_file        { get; set; }

        public string dll_dir             { get; set; }
        public string dll_file            { get; set; }

        public string xsd_dir             { get; set; }
        public string xsd_file            { get; set; }

        public string xml_dir             { get; set; }
        public string xml_file            { get; set; }

        public string java_dir            { get; set; }

        public string @namespace          { get; set; }
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
            string dll_base="";
            int index=0;
            foreach(string path_part in tokens3) {
                if(path_part.Length>3) {
                    if(!string.IsNullOrWhiteSpace(code_dir)) {
                        code_dir+=@"\"+path_part;
                        dll_base+="."+path_part;
                    } else {
                        //index++;
                        //if(index<2) continue;

                        code_dir+=path_part;
                        dll_base+=path_part;
                    }

                }
            }
            string base_code_dir=code_dir;


            string seperator="";
            if(IsLinux) { 
                seperator="/"; 
                base_code_dir=base_code_dir.Replace("\\","/");
            } else { 
                seperator="\\"; 
                base_code_dir=base_code_dir.Replace("/","\\");
            }
            //dirs
            this.code_dir               =String.Format(@"{0}{1}csharp{1}"        ,base_dir,seperator,base_code_dir);
            this.web_api_dir            =String.Format(@"{0}{1}controllers{1}{2}",base_dir,seperator,base_code_dir);
            this.compiled_json_dir      =String.Format(@"{0}{1}jsonschema{1}"    ,base_dir,seperator,base_code_dir);
            this.xsd_dir                =String.Format(@"{0}{1}xsd{1}"           ,base_dir,seperator,base_code_dir);
            this.dll_dir                =String.Format(@"{0}{1}dll{1}"           ,base_dir,seperator);
            this.xml_dir                =String.Format(@"{0}{1}xml{1}"           ,base_dir,seperator,base_code_dir);
            this.java_dir               =String.Format(@"{0}{1}java{1}"          ,base_dir,seperator,base_code_dir);
                                    //files
            this.code_file              =String.Format(@"{0}{1}{2}.{3}.cs"            ,code_dir          ,seperator ,dll_base, base_name);
            this.web_api_file           =String.Format(@"{0}{1}{2}.{3}Controller.cs"  ,web_api_dir       ,seperator ,dll_base, base_name);
            this.compiled_json_path     =String.Format(@"{0}{1}{2}.{3}.json"          ,compiled_json_dir ,seperator ,dll_base, base_name);
            this.xsd_file               =String.Format(@"{0}{1}{2}.{3}.xsd"           ,xsd_dir           ,seperator ,dll_base, base_name);
            this.xml_file               =String.Format(@"{0}{1}{2}.{3}.xml"           ,xml_dir           ,seperator ,dll_base, base_name);
            this.dll_file               =String.Format(@"{0}{1}{2}.{3}.dll"           ,dll_dir           ,seperator ,dll_base, base_name);
            if(event_name=="event") event_name="@event";
            
            string[] ns2=dll_base.Replace("-","_").Split('.');
            string ns3="";
            if(ns2.Length>=3) {
                for(int i=3;i<ns2.Length;i++) {
                    if(ns2[i]=="event") continue;
                    if(ns3!="") ns3+="." ;
                    ns3+=ns2[i];
                }
            } 
            this.@namespace=string.Format("{0}.{1}.{2}",code_namespace,ns3,event_name);
//            this.@namespace=code_namespace;
        }//end constructor
    }//end class
}//end namespace
