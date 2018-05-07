using System;
using System.IO;
using System.Linq;

namespace java2cs
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string [] fname;
            string path = "/home/nd/repos2";
            fname = Directory.GetFiles(path, "*.java", SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();

            foreach(string file in fname){
                string lines = File.ReadAllText(file, System.Text.Encoding.UTF8);
                gen_cs(lines, file.Replace("java","cs"));
            }
        }
        
        

        public static void write_file(string file_path,string text){ 
            string filename =file_path;
            string path     =Path.GetDirectoryName(filename);
            Directory.CreateDirectory(path);
            Console.WriteLine("File Written : "+filename);
            try{
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename)) {
                    file.Write(text);
                }
            }catch(Exception ex) {
                Console.WriteLine("Error Writing source file :"+ex.Message);
            }
        }//end write_source

        public static int[] get_block(string data,string start,string stop,int index=0){
            int start_position=0;
            int end_position=0;
            start_position=data.IndexOf(start,index);
            if(start_position==-1) return null;                     //no start match
            end_position=data.IndexOf(stop,start_position+1);            
            if(end_position==-1) return null;                       //no end match
            bool in_quotes=false;
            int depth=0;
            for(int a=start_position;a<=end_position;a++) {           //make sure its not nested.
                switch(data[a]){
                    case '"': if(in_quotes==true) in_quotes=false; else in_quotes=true;break;
                    case '{': if(!in_quotes) depth++; break;
                    case '}': if(!in_quotes) depth--; break;
                }                
            }
            if(!in_quotes && depth==0) {
                return new int[] {start_position,end_position-start_position+stop.Length};
            }//end for i
            return null;
        }

        public static string delete(string data,int start,int stop){
            return data.Remove(start,stop);
        }
        
        public static string  delete_all(string data,string start,string stop){
            int position=0;
            int []block=null;
            bool first=true;
            while(block!=null || first==true){
                first=false;
                block=get_block(data,start,stop,position);
                if(block!=null) {
                    data=delete(data,block[0],block[1]);
                    position=block[0];
                } 
            }
            return data;
        }
        
        public static string complete_modifiers(string data) {
            int start_position=0;
            int end_position=0;
            int position=0;
            char[] whitespace={' ','\r','\n','\t'};
            while(position<data.Length){
                start_position=data.IndexOf("@",position);
                if(start_position<0) break;
                bool not_modifier=false;
                for(int a=start_position;a>=0;a--) {
                    if(!whitespace.Contains(data[a])) {
                        position=a;
                        not_modifier=true;
                        break;
                    }
                    if(data[a]=='\r' || data[a]=='\n') {
                        position=a;
                        break;
                    }
                }
                if(!not_modifier) continue;

                end_position=data.IndexOf("\n",start_position);
                data=data.Remove(start_position,1);
                data=data.Insert(start_position,"[");
                data=data.Insert(end_position,"]");
            }
            
            return data;
        }
        
        public static void gen_cs(string data, string output_file){
            int len=data.Length;
            int[] block;
            data=delete_all(data,"import",";");
            block=get_block(data,"package",";");
            //if(null!=block) {
            data=data.Remove(block[1]-1,1);
            data=data.Insert(block[1]-1,"{\n"+
                             "using System.Xml;\n"+
                             "using System.Xml.Serialization;\n"+
                             "using Java.Builders;\n"
                             );
            data=data+"}//end namespace";
            data=data.Replace("package","namespace");
            data=complete_modifiers(data);
            data=data.Replace("final class","sealed class");        //correct class
            data=data.Replace("final"      ,"readonly");        //correct properties/fields
            data=data.Replace("String"     ,"string");              //
            data=data.Replace("boolean"    ,"bool");              //
            data=data.Replace("tostring"   ,"ToString");              //
            data=data.Replace("extends"   ,":");              //
            write_file(output_file,data);
        }
    }
}
