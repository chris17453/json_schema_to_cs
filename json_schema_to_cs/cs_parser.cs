using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using NJsonSchema;

using NJsonSchema.CodeGeneration.CSharp;
using jsonschema_to_cs.code_name_generators;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace jsonschema_to_cs {
    public class cs_parser {
       // [Required]
       // [Range(0,3)]
        public string bob;
        private static CSharpGeneratorSettings settings;

        public static void parse(JsonSchema4 schema,string root,string part,string base_class) {
                    
            /*int i=0;
            foreach(KeyValuePair<string,JsonSchema4> schema_part in  schema.Definitions) {
                string name=String.Format("{0}.{1}.cs",part,i);
                cs_parser.parse(schema_part.Value,root,name,base_class);
                i++;
            }*/
            //if(i!=0) return;
            name_gen ng=new name_gen();
            names names=ng.Generate(schema.Id,base_class);
            string custom_ns=string.Format("{0}.{1}",names.namespace_name,names.class_name);


            string cs_file=generate_class(schema,base_class);
            
            
            //create directory
            DirectoryInfo di = Directory.CreateDirectory(root);
            //write file

            string filename=string.Format("{0}/{1}.cs",root,custom_ns);
            filename=filename.Replace("//","/");
            File.WriteAllText(filename, cs_file);            
            
        }
        
        public class names {
            public string class_name;
            public string namespace_name;
            public names(string class_name,string name_space) {
                this.class_name=class_name;
                this.namespace_name=name_space;
            }
        }

        public class name_gen{
            private int annon=0;
            public string safe_type_name(string name) {
                name=name.Replace(";","_");
                name=name.Replace("=","_");
                name=name.Replace("-","_");
                if(name=="class") return "@class";
                if(name=="event") return "@event";
                name=name.Replace(".class","@class");
                name=name.Replace(".event","@event");
                return name;
            }
            public names Generate(string id,string base_class){
               try{
                if(!String.IsNullOrWhiteSpace(id)) {
                    string typename=id;

                        typename=typename.Replace("-","_");
                        typename=typename.Replace("=","_");
                        typename=typename.Replace(";",".");
                        typename=typename.Replace(".json","");
                        typename=typename.Replace("http://","");
                        typename=typename.Replace("https://","");
                        typename=typename.Replace("/",".");
                        //first 3 parts are the domainlast 2 are the "class/version";
                    string[] parts=typename.Split('.');
                        string version="v1";
                        string name_space=base_class;
                        string class_name="UNK";
                        if(parts.Length<5) {
                            return new names("UNK_CLASS","UNK_NS");
                        }
                        if(parts.Length>=5) {
                            version=parts[parts.Length-1];
                            class_name=safe_type_name(parts[parts.Length-2]+"_"+version);
                            name_space=base_class;
                            for(int i=3;i<parts.Length-2;i++) {
                                if(!string.IsNullOrWhiteSpace(name_space)) name_space+=".";

                                name_space+=safe_type_name(parts[i]);
                            }

                        }

                        return new names(class_name,name_space);

                }
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                annon++;
                return new names("","");
    
            }//end func
        }//end class
        
        public static string make_attribute(string attribute_name,string attribute_value){
            if(string.IsNullOrWhiteSpace(attribute_value)) return String.Empty;
            return String.Format("[{0}(\"{1}\")]",attribute_name,attribute_value.Replace("\"","\\\"" ));

        }
        
        public static string  build_property(KeyValuePair<string,JsonProperty> x,string base_class){
            string id=x.Value.Id;
            if(string.IsNullOrWhiteSpace(id)) {
                if(x.Value.HasReference) id=x.Value.Reference.Id;
            }

            string schema_type  =x.Value.Type.ToString();
            switch(schema_type.ToLower()){
                case "string"  : schema_type="string"; break;
                case "object"  : schema_type="object"; break;
                case "boolean" : schema_type="bool"; break;
                case "integer" : schema_type="int"; break;
                case "number"  : schema_type="float"; break;
                case "array"   : schema_type="array"; break;
                case "none"    : schema_type=""; break;
            }
            name_gen ng=new name_gen();
            names prop_names=ng.Generate(id,base_class);
            string schema_type_interpreted=schema_type;
            if(schema_type=="") {
                schema_type_interpreted=string.Format("{0}.{1}",prop_names.namespace_name,prop_names.class_name);
            }
            if(schema_type=="array") {
                if(x.Value.Item!=null && x.Value.Item.HasReference) id=x.Value.Item.Reference.Id;
                if(x.Value.Item!=null && !x.Value.Item.HasReference) id=x.Value.Item.Id;
                prop_names=ng.Generate(id,base_class);
                schema_type_interpreted=string.Format("List<{0}.{1}> ",prop_names.namespace_name,prop_names.class_name);
            }

            string variable_name=ng.safe_type_name(x.Value.Name);

            if(string.IsNullOrWhiteSpace(variable_name)) variable_name=x.Key;
            string variable=String.Format("public {0}  {1} {{ get; set; }}",schema_type_interpreted,variable_name); 

            //string id           =make_attribute("JSON_SCHEMA_ID"     ,x.Value.Id);
            string schema_name  =make_attribute("JsonProperty"         ,prop_names.class_name);
            string type         =make_attribute("JSON_SCHEMA_TYPE"     ,x.Value.Type.ToString());
            string title        =make_attribute("JSON_SCHEMA_TITLE"    ,x.Value.Title);
            string description  =make_attribute("JSON_SCHEMA_DESC"     ,x.Value.Description);
            string pattern      =make_attribute("JSON_SCHEMA_PATTERN"  ,x.Value.Pattern);
            string format       =make_attribute("JSON_SCHEMA_FORMAT"   ,x.Value.Format);


            string maximum      =make_attribute("JSON_SCHEMA_MAXIMUM"  ,x.Value.Maximum.ToString());
            string minimum      =make_attribute("JSON_SCHEMA_MINIMUM"  ,x.Value.Minimum.ToString());
            string multipleOf   =make_attribute("JSON_SCHEMA_MULTIPLE" ,x.Value.MultipleOf.ToString());
            if(x.Value.Type.ToString()=="None") type="";

            string property=String.Format(
                "\t\t//{0}\n" +
                "\t\t//{1}\n" +
                "\t\t//{2}\n" +
                "\t\t//{3}\n" +
                "\t\t//{4}\n" +
                "\t\t//{5}\n" +
                "\t\t//{6}\n" +
                "\t\t//{6}\n" +
                "\t\t//{7}\n" +
                "\t\t//{8}\n" +
                "\t\t//{9}\n" +
                "\t\t{10}\n\n" ,
                id,
                schema_name,
                type,       
                title,      
                description,
                pattern,
                format,
                minimum,
                maximum,
                multipleOf,
                variable
            );
            property=property.Replace("\t\t//\n","");
            return property;
        }


        public static string generate_class(JsonSchema4 schema,string base_class){
            StringBuilder o=new StringBuilder();
            name_gen ng=new name_gen();
            names names=ng.Generate(schema.Id,base_class);
            string ns_fragment=String.Format("namespace {0}",names.namespace_name);
            string extends=get_extends(schema,base_class);
            if(!string.IsNullOrWhiteSpace(extends)) extends=" : " + extends;
            string class_fragment=String.Format("\tpublic class {0} {1} {{",names.class_name,extends);


            string schema_type  =schema.Type.ToString();
            switch(schema_type.ToLower()){
                case "string"  : schema_type="string"; break;
                case "object"  : schema_type="object"; break;
                case "boolean" : schema_type="bool"; break;
                case "integer" : schema_type="int"; break;
                case "number"  : schema_type="float"; break;
                case "none"    : schema_type=""; break;
            }


            string constructorn_fragment1  =string.Format("public {0}(){{ }}",names.class_name);
            string constructorn_fragment2=string.Format("public {0}({1} obj){{ this.value=obj; }}",names.class_name,schema_type); //name


            string implicit_fragment=string.Format("public static implicit override {0}({1} obj){{\n\t\t\treturn obj.value;\n\t\t}}",schema_type,names.class_name);


            string properties=get_properties(schema,base_class);

                /*foreach(KeyValuePair<string,JsonProperty> x in schema.ActualSchema) {
                    properties+=build_property(x,base_class);
                }*/


                    
            string code_file=String.Format(
                "{0} {{" +
                "\n\tusing System;\n" +
                "\n\tusing System.Collections.Generic;\n" +
                "{1}\n" +
                "\n" +
                "{2}\n"+
                "\n" +
                "\t\t{3}\n" +
                "\t\t{4}\n" +
                "\n" +
                "\t\t{5}\n" +
                "\n" +
                "\t}}//end class\n" +
                "}}//end namespace",   
                ns_fragment,            
                class_fragment,         
			    properties,             
                constructorn_fragment1, 
                "", 
                ""  
                );
            
            code_file=code_file.Replace("\t\t\n","");   
            code_file=code_file.Replace("\n\n","\n");   
            return code_file;
        }

        public static string get_extends(JsonSchema4 schema,string base_class) {
            string extends="";
            if(schema.HasReference) {
                extends+=schema.Reference.Id;
            }

            foreach(JsonSchema4 schema_part in schema.AllOf) {    
                
                if(schema_part.HasReference) {
                    extends+=schema_part.Reference.Id;
                }
            }
            return extends;
        }

        public static string get_properties(JsonSchema4 schema,string base_class) {
            string properties ="";    
            bool has_any_ref=false;
            if(schema.HasReference) has_any_ref=true;


            if(schema.Properties.Count!=0) {
                foreach(KeyValuePair<string,JsonProperty> x in schema.Properties) {
                    properties+=build_property(x,base_class);
                }
            }

            if(schema.HasReference) {
                has_any_ref=true;
               // string property=get_properties(schema.Reference,base_class);
               // if(property=="") properties="//NO PROPERTIES";
               // properties+=property;
            }

            foreach(JsonSchema4 schema_part in schema.AllOf) {    
                if(schema_part.HasReference) {
                 //   has_any_ref=true;
                  //  string property=get_properties(schema_part.Reference,base_class);
                 //   if(property=="") properties="//NO PROPERTIES";
                //    properties+=property;
                }
                if(schema_part.Properties.Count!=0) {
                    foreach(KeyValuePair<string,JsonProperty> x in schema_part.Properties) {
                        properties+=build_property(x,base_class);
                    }

                }
            }
            //if(properties=="") properties="//SIMPLE_OBJECT";
            return properties;
        }//end func
        
    }
}
