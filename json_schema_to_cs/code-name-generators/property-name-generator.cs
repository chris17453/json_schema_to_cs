using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NJsonSchema;
using NJsonSchema.CodeGeneration;

namespace kafka_json_parser.code_name_generators{
    public class custom_property : IPropertyNameGenerator{
        public string Generate(JsonProperty property){
            if(property.Name=="event") return "@event";
            return property.Name;
        }//end function
    }//end class
}//end namespace
