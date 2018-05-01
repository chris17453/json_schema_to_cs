using NJsonSchema;
using NJsonSchema.CodeGeneration;

namespace jsonschema_to_cs.code_name_generators{
    public class custom_property : IPropertyNameGenerator{
        public string Generate(JsonProperty property){
            if(property.Name=="event") return "@event";
            if(string.IsNullOrWhiteSpace(property.Name)) {
                return "UNK!?";
            }
            return property.Name;
        }//end function
    }//end class
}//end namespace
