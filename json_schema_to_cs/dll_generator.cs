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
        public static int debug_level = 1;


        public static void write_file(string file_path,string text){ 
            string filename =file_path;
            string path     =Path.GetDirectoryName(filename);
            Directory.CreateDirectory(path);
            if (debug_level > 2)Console.WriteLine("Source file Written : "+filename);
            try{
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename)) {
                    file.Write(text);
                }
            }catch(Exception ex) {
                if(debug_level>2) Console.WriteLine("Error Writing source file :"+ex.Message);

            }
        }//end write_source

        public static bool compile_dll (string path,string filename,string code,bool save_source){
            if(debug_level>2)Console.WriteLine("Compiling");
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
            parameters.ReferencedAssemblies.Add("System.Xml.Serialization.dll");
            parameters.ReferencedAssemblies.Add("System.Xml.dll");
            parameters.ReferencedAssemblies.Add("Newtonsoft.Json.dll");
            parameters.ReferencedAssemblies.Add("NJsonSchema.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            //  parameters.ReferencedAssemblies.Add("Microsoft.SqlServer.Types.dll");


            if(String.IsNullOrWhiteSpace(code)) {
                if (debug_level > 2)Console.WriteLine("No code.");
                return false;
            }
            if(String.IsNullOrWhiteSpace(filename)) {
                if (debug_level > 2)Console.WriteLine("No DLL name.");
                return false;
            }

            if(save_source) {
                write_file(filename+".cs",code);
            }

            CompilerResults cr = codeProvider.CompileAssemblyFromSource(parameters, code);


            if( cr.Errors.Count > 0 ) {
                for( int i=0; i<cr.Output.Count; i++ )  Console.WriteLine( cr.Output[i] );
                for( int i=0; i<cr.Errors.Count; i++ )  Console.WriteLine( i.ToString() + ": " + cr.Errors[i].ToString() );
                return false;
            } else {
                // Display information about the compiler's exit code and the generated assembly.
                if (debug_level > 2)Console.WriteLine( "Compiler returned with result code: " + cr.NativeCompilerReturnValue.ToString() );
                if (debug_level > 2)Console.WriteLine( "Generated assembly name: " + cr.CompiledAssembly.FullName );
                if( cr.PathToAssembly == null )
                if (debug_level > 2)Console.WriteLine( "The assembly has been generated in memory." );
                else
                    if (debug_level > 2)Console.WriteLine( "Path to assembly: " + cr.PathToAssembly );

                // Display temporary files information.
                if( !cr.TempFiles.KeepFiles ) if (debug_level > 2)Console.WriteLine( "Temporary build files were deleted." );
                else {
                    if (debug_level > 2)Console.WriteLine( "Temporary build files were not deleted." );
                    // Display a list of the temporary build files
                    IEnumerator enu = cr.TempFiles.GetEnumerator();                                        
                    for( int i=0; enu.MoveNext(); i++ )                                          
                        if (debug_level > 2)Console.WriteLine( "TempFile " + i.ToString() + ": " + (string)enu.Current );                  
                }
            }        
            if (debug_level > 2)Console.WriteLine("Compiling Finished");
            return true;
        }        
    }
}