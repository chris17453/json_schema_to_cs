using NJsonSchema;
using NJsonSchema.CodeGeneration;

namespace jsonschema_to_cs.code_name_generators{
    public class custom_enum : IEnumNameGenerator {
        public string Generate(int index, string name, object value, JsonSchema4 schema){
            if(name=="event") return "@event";
            name=name.Replace("(","_");
            name=name.Replace(")","_");
            name=name.Replace(" ","_");
            name=name.Replace("-","_");
            return name;
        }//end function
    }//end class
}//end namespace