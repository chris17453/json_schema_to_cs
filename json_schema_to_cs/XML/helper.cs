﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Xml.XMLGen;

namespace jsonschema_to_cs.XML{
 
    class helper    {
        static void HandleValidationEventHandler(object sender, ValidationEventArgs e)
        {



        }  



        public static bool GetSchema(string source_library,string dest_xsd,string dest_xml,string dest_java,string name_space)
        {
            if(!File.Exists(source_library)) {
                Console.WriteLine("DLL File not found. -> "+source_library);
                return false;
            }
            var DLL = Assembly.LoadFile(source_library);
            try{
            int index=0;
            string el_name="";
            XmlSchemas schemas = new XmlSchemas();
            foreach(Type type in DLL.GetExportedTypes()){
                    if(!type.BaseType.Name.Contains("event")) continue;
                    var c = Activator.CreateInstance(type.UnderlyingSystemType);
                    XmlAttributeOverrides   xao = new XmlAttributeOverrides();
                    AttachXmlAttributes(xao, type.UnderlyingSystemType);
                    XmlReflectionImporter   importer = new XmlReflectionImporter(xao);
                    XmlSchemaExporter       exporter = new XmlSchemaExporter(schemas);
                    XmlTypeMapping          map = importer.ImportTypeMapping(type.UnderlyingSystemType,type.Namespace);
                    el_name=type.Name;
                    exporter.ExportTypeMapping(map);



//                }
                index++;
                    break;

            }//end loop

            string o="";
            using (MemoryStream ms = new MemoryStream()){
                    XmlSchemaSet ss=new XmlSchemaSet();
                                    
                foreach(XmlSchema s in  schemas) {
                        ss.Add(s);
                }

                ss.Compile();
                var  x=ss.Schemas();
                foreach(XmlSchema s in  x) {
                    s.Write(ms);
                    ms.Position = 0;
                    o+=new StreamReader(ms).ReadToEnd();
                }
                assembly_generator.write_file(dest_xsd,o);
                build_xml(ss,el_name,dest_xml);
                ExecuteCommand(string.Format("/bin/xjc -d {0} -p {1} {2}",dest_java,name_space,dest_xsd));        
                //    Process.Start("ls ","-?");
            }//end memory stream

            }catch(Exception ex) {
                Console.Write("Eh");
            }
             
            return true;
        }

        public static void ExecuteCommand(string command)
        {
            Process proc = new System.Diagnostics.Process ();
            proc.StartInfo.FileName = "/bin/bash";
            proc.StartInfo.Arguments = "-c \" " + command + " \"";
            proc.StartInfo.UseShellExecute = false; 
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start ();

            while (!proc.StandardOutput.EndOfStream) {
                Console.WriteLine (proc.StandardOutput.ReadLine ());
            }
        }


        public static void build_xml(XmlSchemaSet schemas,string localName,string dest_xml){
            
            XmlQualifiedName qname = new XmlQualifiedName(localName);
            string ns = string.Empty;
            int max = 4;
            int listLength = 5;

            string filename =dest_xml;
            string path     =Path.GetDirectoryName(filename);
            Directory.CreateDirectory(path);


            XmlTextWriter textWriter = new XmlTextWriter(dest_xml, null);
            textWriter.Formatting = Formatting.Indented;
            XmlSampleGenerator genr = new XmlSampleGenerator(schemas, qname);
            if (max > 0) genr.MaxThreshold = max;
            if (listLength > 0) genr.ListLength = listLength;
            genr.WriteXml(textWriter);
        }

       public static void AttachXmlAttributes(XmlAttributeOverrides xao, Type t) {
            List<Type> types = new List<Type>();
            AttachXmlAttributes(xao, types, t);
        }
        private static void AttachXmlAttributes(XmlAttributeOverrides xao, List<Type> all, Type t)
        {
            if (all.Contains(t))
            {
                return;
            }
            else
            {
                all.Add(t);
            }

            var list1 = GetAttributeList(t.GetCustomAttributes(false));
            xao.Add(t, list1);

            foreach (var prop in t.GetProperties())
            {
                var propType = prop.PropertyType;
                if (propType.IsGenericType) // is list?
                {
                    var args = propType.GetGenericArguments();
                    if (args != null && args.Length == 1)
                    {                        
                        var genType = args[0];
                        if (genType.Name.ToLower() != "object")
                        {
                            var list2 = GetAttributeList(prop.GetCustomAttributes(false));
                            xao.Add(t, prop.Name, list2);
                            AttachXmlAttributes(xao, all, genType);
                        }                        
                    }
                }
                else
                {
                    var list2 = GetAttributeList(prop.GetCustomAttributes(false));
                    xao.Add(t, prop.Name, list2);
                    AttachXmlAttributes(xao, all, prop.PropertyType);
                }
            }
        }        

        private static XmlAttributes GetAttributeList(object[] attributes)
        {
            var list = new XmlAttributes();
            
            foreach (var attr in attributes)
            {
                Type type = attr.GetType();
                switch (type.Name)
                {
                    case "XmlAttributeAttribute":
                        list.XmlAttribute = (XmlAttributeAttribute)attr;
                        break;                    
                    case "XmlRootAttribute":
                        list.XmlRoot = (XmlRootAttribute)attr;
                        break;
                    case "XmlType":
                        list.XmlType = (XmlTypeAttribute)attr;
                        break;
                    case "XmlElementAttribute":
                        list.XmlElements.Add((XmlElementAttribute)attr);
                        break;
                    case "XmlArrayAttribute":
                        list.XmlArray = (XmlArrayAttribute)attr;
                        break;
                    case "XmlArrayItemAttribute":
                        list.XmlArrayItems.Add((XmlArrayItemAttribute)attr);
                        break;
                }
            }
            return list;
        }


        public static XmlSchema GetXSDFileAsXMLSchema(string path){
            try{
                FileStream fs = new FileStream(path, FileMode.Open);
            if(fs.Length==0) return null;
            XmlSchema schema = XmlSchema.Read(fs, new ValidationEventHandler(ValidationCallBack));
            return schema;
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            return null;
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs args){
            return; 
        }  

        public static void combine_xsd(string[] list,string path,string ns) {
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            int i=0;
            foreach (string xsd in list) {
                //if(i>) break;
                i++;
                jsonschema_to_cs.model.schema_map map=new jsonschema_to_cs.model.schema_map(xsd,path,ns);
                XmlSchema schema=GetXSDFileAsXMLSchema(map.xsd_file);
                if(null==schema) continue;
                schemaSet.Add(schema);
            }


            schemaSet.Compile();
            var  x=schemaSet.Schemas();
            string o="";
            using (MemoryStream ms = new MemoryStream()){
                foreach(XmlSchema s in  x) {
                    s.Write(ms);
                }
                ms.Position = 0;
                o=new StreamReader(ms).ReadToEnd();
                string  dest_xsd=path+"/combined.xsd";
                assembly_generator.write_file(dest_xsd,o);
            }
        }
    }//end class
}//end namespace
