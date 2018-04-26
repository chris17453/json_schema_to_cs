using System;
using System.IO;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace jsonschema_to_cs
{
    public class parse    {
        public static bool parseapi(model.schema_map map){
            try{

                //This is a pain.. but must be done.
                System.Runtime.CompilerServices.ConfiguredTaskAwaitable<JsonSchema4> bob= JsonSchema4.FromUrlAsync(map.url).ConfigureAwait(true);
                System.Runtime.CompilerServices.ConfiguredTaskAwaitable<JsonSchema4>.ConfiguredTaskAwaiter awaiter=bob.GetAwaiter();
                JsonSchema4 schema_object=awaiter.GetResult();

                //Custom name generation
                var settings = new CSharpGeneratorSettings();
                settings.ClassStyle             = CSharpClassStyle.Poco;
                settings.PropertyNameGenerator  = new code_name_generators.custom_property();
                settings.TypeNameGenerator      = new code_name_generators.custom_type();
                settings.EnumNameGenerator      = new code_name_generators.custom_enum();
                settings.Namespace              = map.@namespace;

                //generate c# class
                var generator = new CSharpGenerator(schema_object,settings);             
                var cs_file = generator.GenerateFile();

                //create directory









                DirectoryInfo di = Directory.CreateDirectory(map.code_dir);
                //write file
                File.WriteAllText(map.code_file, cs_file);

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }//end function     
    }
}
