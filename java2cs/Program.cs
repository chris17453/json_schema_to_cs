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
            if(file.Contains("test")) continue;
                string lines = File.ReadAllText(file, System.Text.Encoding.UTF8);
                string dest_path=file.Replace("java","cs");
                dest_path=dest_path.Replace("/",".");
                dest_path=dest_path.Replace("src.main.cs.com.igi.domain.","");
                dest_path=dest_path.Replace(".home.nd.repos2.","/home/nd/repos/igkafka/");
                gen_cs(lines, dest_path);
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
            char[] whitespace={' ','\r','\n','\t'};
            while(start_position<data.Length){
                start_position=data.IndexOf("@",start_position);
                if(start_position<0)  {
                    return data;
                }
                bool modifier=true;
                for(int a=start_position-1;a>=0;a--) {
                    if(!whitespace.Contains(data[a])) {
                        modifier=false;
                        break;
                    }
                    if(data[a]=='\r' || data[a]=='\n') {
                        break;
                    }
                }

                if(!modifier) {
                    start_position++;
                    continue;
                }

                end_position=data.IndexOf("\n",start_position);
                data=data.Remove(start_position,1);
                data=data.Insert(start_position,"[");
                data=data.Insert(end_position,"]");
                start_position=end_position+1;
            }
            return data;
        }


        public static string nottnull(string data){
            int start_position=0;
            char[] whitespace={' ','\r','\n','\t'};
            while(start_position<data.Length){
                start_position=data.IndexOf("@NotNull",start_position);
                if(start_position<0)  {
                    return data;
                }
                for(int a=start_position-1;a>=0;a--) {
                    if(data[a]=='\r' || data[a]=='\n') {
                        data=data.Remove(start_position,9);    
                        data=data.Insert(a,"\n\t[XmlElementAttribute(IsNullable=false)]");
                        break;
                    }
                }
                start_position++;
            }
            return data;
        }

        public static string size(string data) {
            int start_position=0;
            char[] whitespace={' ','\r','\n','\t'};
            while(start_position<data.Length){
                int[] block=get_block(data,"@Size",")",start_position);
                if(block==null) return data;

                for(int a=block[0]-1;a>=0;a--) {
                    if(data[a]=='\r' || data[a]=='\n') {
                        string attrib=data.Substring(block[0],block[1]);
                        attrib=attrib.Replace("@Size","").Replace("(","").Replace(")","");
                        data=data.Remove(block[0],block[1]+1);
                        //data=data.Insert(a,"\n\t[JsonAttribute("+attrib+")]");
                        break;
                                    
                    }
                }

                start_position++;
            }
            return data;
        }

        public static string _override(string data) {
            int start_position=0;
            char[] whitespace={' ','\r','\n','\t'};
            while(start_position<data.Length){
                int[] block=get_block(data,"[Override","}",start_position);
                if(block==null) return data;

                for(int a=block[0]-1;a>=0;a--) {
                    if(data[a]=='\r' || data[a]=='\n') {
                        //string attrib=data.Substring(block[0],block[1]);
                        //attrib=attrib.Replace("@Size","").Replace("(","").Replace(")","");
                        data=data.Remove(block[0],block[1]+1);
                        //data=data.Insert(a,"\n\t[JsonAttribute("+attrib+")]");
                        break;

                    }
                }

                start_position++;
            }
            return data;
        }

        public static void gen_cs(string data, string output_file){
            data=data.Replace("\r","");
            int[] block;
            data=delete_all(data,"import",";");
            block=get_block(data,"package",";");
            //if(null!=block) {
            data=data.Remove(block[1]-1,1);
            data=data.Insert(block[1]-1,"{\n"+
                             "using System;\n"+
                             "using System.Xml;\n"+
                             "using System.Xml.Serialization;\n"+
                             "using System.Runtime.Serialization;\n"+
                             "using System.Collections.Generic;\n"+
                             "using JavaDeps;\n"
                             );
            data=data+"}//end namespace";
            data=data.Replace("package","namespace");
            data=complete_modifiers(data);
            data=nottnull(data);
            data=size(data);
            data=_override(data);
            data=data.Replace("JsonInclude.Include.NON_EMPTY","\"NON_EMPTY\"");
            data=data.Replace("final class"  ,"sealed class");        //correct class
            data=data.Replace("final"        ,"readonly");        //correct properties/fields
            data=data.Replace("String"       ,"string");              //
            data=data.Replace("Boolean"      ,"bool");              //
            data=data.Replace("boolean"     ,"bool");              //
            data=data.Replace("tostring"     ,"ToString");              //
            data=data.Replace("readonly"     ,"");              //
            data=data.Replace("extends"      ,":");              //
            data=data.Replace("Set"          ,"List");              //
            data=data.Replace("implements"   ,"");              //
            data=data.Replace("Serializable" ,"");              //
            data=data.Replace("LocalDate"    ,"DateTime");              //
            data=data.Replace("Integer"      ,"int");              //
            data=data.Replace("{]" ,"{");              //
            data=data.Replace("  "," ");
            data=data.Replace("\n\n","\n");
            write_file(output_file,data);
        }
    }
}

//XmlRoot("PurchaseOrder", Namespace="http://www.cpandl.com",            IsNullable = false)]  
