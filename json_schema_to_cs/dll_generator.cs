using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using Microsoft.CSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace jsonschema_to_cs
{
    public static class assembly_generator
    {



        public static void write_source(string file_path,string source){ 
            string filename=file_path+".cs";
            Console.WriteLine("Source file Written : "+filename);
            try{
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename)) {
                    file.Write(source);
                }
            }catch(Exception ex) {
                Console.WriteLine("Error Writing source file :"+ex.Message);

            }
        }//end write_source

        public static bool compile_dll (string path,string filename,string code,bool save_source){
            Console.WriteLine("Compiling");
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = filename;
            //parameters.ReferencedAssemblies.Add("data.dll");


            DirectoryInfo di = Directory.CreateDirectory(path);

            parameters.ReferencedAssemblies.Add("System.Data.dll");
            parameters.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            parameters.ReferencedAssemblies.Add("System.ComponentModel.DataAnnotations.dll");
            parameters.ReferencedAssemblies.Add("System.Runtime.Serialization.dll");
            parameters.ReferencedAssemblies.Add("Newtonsoft.Json.dll");
            parameters.ReferencedAssemblies.Add("NJsonSchema.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            //  parameters.ReferencedAssemblies.Add("Microsoft.SqlServer.Types.dll");


            if(String.IsNullOrWhiteSpace(code)) {
                Console.WriteLine("No code.");
                return false;
            }
            if(String.IsNullOrWhiteSpace(filename)) {
                Console.WriteLine("No DLL name.");
                return false;
            }

            if(save_source) {
                write_source(filename,code);
            }

            CompilerResults cr = codeProvider.CompileAssemblyFromSource(parameters, code);


            if( cr.Errors.Count > 0 ) {
                for( int i=0; i<cr.Output.Count; i++ )  Console.WriteLine( cr.Output[i] );
                for( int i=0; i<cr.Errors.Count; i++ )  Console.WriteLine( i.ToString() + ": " + cr.Errors[i].ToString() );
                return false;
            } else {
                // Display information about the compiler's exit code and the generated assembly.
                Console.WriteLine( "Compiler returned with result code: " + cr.NativeCompilerReturnValue.ToString() );
                Console.WriteLine( "Generated assembly name: " + cr.CompiledAssembly.FullName );
                if( cr.PathToAssembly == null )
                    Console.WriteLine( "The assembly has been generated in memory." );
                else
                    Console.WriteLine( "Path to assembly: " + cr.PathToAssembly );

                // Display temporary files information.
                if( !cr.TempFiles.KeepFiles ) Console.WriteLine( "Temporary build files were deleted." );
                else {
                    Console.WriteLine( "Temporary build files were not deleted." );
                    // Display a list of the temporary build files
                    IEnumerator enu = cr.TempFiles.GetEnumerator();                                        
                    for( int i=0; enu.MoveNext(); i++ )                                          
                        Console.WriteLine( "TempFile " + i.ToString() + ": " + (string)enu.Current );                  
                }
            }        
            Console.WriteLine("Compiling Finished");
            return true;
        }        
    }
}
