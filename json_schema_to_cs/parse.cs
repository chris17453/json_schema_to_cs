using System;
using System.IO;
using System.Linq;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using NJsonSchema.CodeGeneration;
using NJsonSchema.Generation;
using jsonschema_to_cs.code_name_generators;
using System.Collections.Generic;

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
                
                
                
                
                //build c# files...
                cs_parser.parse(schema_object,map.code_dir,"_");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }//end function     
    }
}


