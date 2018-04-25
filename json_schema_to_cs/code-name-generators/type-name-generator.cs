using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NJsonSchema;
using NJsonSchema.CodeGeneration;

namespace kafka_json_parser.code_name_generators{
    public class custom_type : ITypeNameGenerator{
        public string Generate(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames) {
            //http://schema.dev.box/account-management/account.v1.json
            if(!String.IsNullOrWhiteSpace(schema.DocumentPath)) {
                string[] tokens=schema.DocumentPath.Split('/');
                string last=tokens.Last();
                string[] tokens2=last.Split('.');
                string name=tokens2.First();
                if(name=="event") return "@event";
                return name; 
            }
            return typeNameHint;
        }//end function
    }//end class
}//end namespace
