using System;
using System.IO;
using System.Linq;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using NJsonSchema.CodeGeneration;
using NJsonSchema.Generation;
using jsonschema_to_cs.code_name_generators;

namespace jsonschema_to_cs
{



    public class parse    {
        public static bool parseapi(model.schema_map map){
            try{

                //This is a pain.. but must be done.
                JsonSchemaGeneratorSettings js=new JsonSchemaGeneratorSettings();
                js.TypeNameGenerator=new jsonnamer();
                Func<JsonSchema4, JsonReferenceResolver> referenceResolverFactory =
                schema => new JsonReferenceResolver(new JsonSchemaResolver(schema, js));
            

                System.Runtime.CompilerServices.ConfiguredTaskAwaitable<JsonSchema4> bob= JsonSchema4.FromUrlAsync(map.url,referenceResolverFactory).ConfigureAwait(true);
                System.Runtime.CompilerServices.ConfiguredTaskAwaitable<JsonSchema4>.ConfiguredTaskAwaiter awaiter=bob.GetAwaiter();
                JsonSchema4 schema_object=awaiter.GetResult();
                
                //Console.Write(schema_object.Xml.ToString());
            
                
                //Custom name generation
                var settings = new CSharpGeneratorSettings();
                settings.ClassStyle             = CSharpClassStyle.Poco;
                settings.PropertyNameGenerator  = new code_name_generators.custom_property();
                settings.TypeNameGenerator      = new code_name_generators.custom_type();
                settings.EnumNameGenerator      = new code_name_generators.custom_enum();
                settings.Namespace              = map.@namespace;
                settings.TemplateDirectory      = @"template/";
                
                DirectoryInfo di = Directory.CreateDirectory(map.compiled_json_dir);
                File.WriteAllText(map.compiled_json_path,schema_object.ToJson());
                //generate c# class
                var generator = new CSharpGenerator(schema_object,settings);             
            
                var cs_file = generator.GenerateFile();

                //create directory
                di = Directory.CreateDirectory(map.code_dir);
                //write file
                File.WriteAllText(map.code_file, cs_file);

                assembly_generator.compile_dll(map.dll_dir,map.dll_file,cs_file,false);
              
                
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }//end function     
    }
}
