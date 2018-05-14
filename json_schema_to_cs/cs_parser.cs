using System;
using System.Collections.Generic;
using System.IO;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using jsonschema_to_cs.code_name_generators;
using System.Text;

namespace jsonschema_to_cs {
    public class cs_parser {
        public static void parse(JsonSchema4 schema,string root,string part) {
        
                    
            int i=0;
            foreach(KeyValuePair<string,JsonSchema4> schema_part in  schema.Definitions) {
                string name=String.Format("{0}.{1}.cs",part,i);
                cs_parser.parse(schema_part.Value,root,name);
                i++;
            }
            if(i!=0) return;
            string custom_ns=part;
            
            
            string cs_file=generate_class(schema);
            
            
            //create directory
            DirectoryInfo di = Directory.CreateDirectory(root);
            //write file
            string filename=string.Format("{0}/{1}.cs",root,custom_ns);
            filename=filename.Replace("//","/");
            File.WriteAllText(filename, cs_file);            
            
        }
        
        public class name_gen{
            private int annon=0;
            public string Generate(string id,string typeNameHint){
    
                if (!string.IsNullOrEmpty(typeNameHint)){
                    var start = typeNameHint.LastIndexOf('.') + 1;
    
                    return ConversionUtilities.ConvertToUpperCamelCase(typeNameHint.Substring(start, typeNameHint.Length - start), true);
                }
       
                try{
                if(!String.IsNullOrWhiteSpace(id)) {
                    string typename=id;
                    typename=typename.Replace(".json","");
                    typename=typename.Replace("http://","");
                    typename=typename.Replace("https://","");
                    typename=typename.Replace("/",".");
    
                   string name=typename;
                        if(name=="event") return "@event";
                        if(name=="class") return "@class";
    
                    if(string.IsNullOrWhiteSpace(name)) {
                        name=typeNameHint;
                    }
                    return name; 
                }
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                if(string.IsNullOrWhiteSpace(typeNameHint)) {
                    annon++;
                    return "annon_"+annon.ToString();
                 }
    
                return "Anonymous";
            }//end func
        }//end class
        
        
        public static string generate_class(JsonSchema4 schema){
            StringBuilder o=new StringBuilder();
            name_gen ng=new name_gen();
            string ns_hint=ng.Generate(schema.Id,"UNK");
            string ns=String.Format("namespace {0}",ns_hint);
            string properties="";


            string id           =makeschema.Id;
            string schema_name  =ns;
            string type         =schema.Type.ToString();;
            string title        =schema.Title;
            string description  =schema.Description;
            string pattern      =schema.Pattern;
            string constructor  =string.Format("public {0}(){{ }}",ns); //name


            string code_file=String.Format(




@"{0} {{ //namespace

//properties
{1} //id
{2} //schema
{3} //type
{4} //title
{5} //description
{6} //pattern
{7} //property    
  
//constructor    
{8}
         
}}",   
ns,          //0
id,          //1
schema,      //2
type,        //3
title,       //4
description, //5
pattern,     //6
"variable",     //6
constructor //7
);
            
            
            return code_file;
        }
        
    }
}
