using NJsonSchema;
using NJsonSchema.CodeGeneration;

namespace jsonschema_to_cs.code_name_generators{
    public class custom_enum : IEnumNameGenerator {
        public string Generate(int index, string name, object value, JsonSchema4 schema){
            if(name=="event") return "@event";
            if(name=="class") return "@class";
            name=name.Replace("(","_");
            name=name.Replace(")","_");
            name=name.Replace(" ","_");
            name=name.Replace("-","_");
            name=name.Replace(",","_");
            name=name.Replace("/","_");
            name=name.Replace("&","_");
            name=name.Replace(";","_");
            switch(name[0]){
                case '0' : name="_"+name; break;
                case '1' : name="_"+name; break;
                case '2' : name="_"+name; break;
                case '3' : name="_"+name; break;
                case '4' : name="_"+name; break;
                case '5' : name="_"+name; break;
                case '6' : name="_"+name; break;
                case '7' : name="_"+name; break;
                case '8' : name="_"+name; break;
                case '9' : name="_"+name; break;
            }
             
            if(string.IsNullOrWhiteSpace(name)) {
                name="UNK!?";
            }
            return name;
        }//end function
    }//end class
}//end namespace



