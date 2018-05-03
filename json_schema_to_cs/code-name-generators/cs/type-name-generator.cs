using System;
using System.Collections.Generic;
using System.Linq;
using NJsonSchema;

namespace jsonschema_to_cs.code_name_generators{
    public class custom_type : ITypeNameGenerator{
        public int annon=0;
        public string Generate(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames) {
            
            try{
            //string bob="http://schema.dev.box/account-management/account.v1.json";
            if(!String.IsNullOrWhiteSpace(schema.DocumentPath)) {
                string[] tokens=schema.DocumentPath.Split('/');
                if(null==tokens) {
                    Console.WriteLine("Token Err");
                }
                string last=tokens.Last();
                string[] tokens2=last.Split('.');
                 if(null==tokens2) {
                    Console.WriteLine("Token Err");
                }
                string name=tokens2.First();
                if(name=="class") return "@class";
                if(name=="event") return "@event";

                if(string.IsNullOrWhiteSpace(name)) {
                    name=typeNameHint;
                }
                return name; 
            }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return "ff";
            }
            if(string.IsNullOrWhiteSpace(typeNameHint)) {
                annon++;
                return "annon_"+annon.ToString();
             }
            return typeNameHint;
        }//end function
    }//end class
}//end namespace
