using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NJsonSchema;

namespace jsonschema_to_cs.code_name_generators
{
    public class jsonnamer:ITypeNameGenerator{
        public int annon=0;
//        protected override string Generate(JsonSchema4 schema, string typeNameHint){

        public string Generate(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames){

            if (!string.IsNullOrEmpty(typeNameHint)){
                var start = typeNameHint.LastIndexOf('.') + 1;

                return ConversionUtilities.ConvertToUpperCamelCase(typeNameHint.Substring(start, typeNameHint.Length - start), true);
            }
   
            try{
            if(!String.IsNullOrWhiteSpace(schema.Id)) {
                    string typename=schema.Id;
                    typename=typename.Replace(".json","");
                    typename=typename.Replace("http://","");
                    typename=typename.Replace("https://","");
                    typename=typename.Replace("/",".");
                /*string[] tokens=schema.DocumentPath.Split('/');
                if(null==tokens) {
                    Console.WriteLine("Token Err");
                }
                string last=tokens.Last();
                string[] tokens2=last.Split('.');
                 if(null==tokens2) {
                    Console.WriteLine("Token Err");
                }*/

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
        }
    }}
