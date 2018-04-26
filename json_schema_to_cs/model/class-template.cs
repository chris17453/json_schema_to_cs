using System;
using System.Reflection;
using System.Text;

namespace jsonschema_to_cs.templates
{
    class class_template {
        public string root {get; set; }
        public string get_properties(){
            Type T=this.GetType();
            StringBuilder o =new StringBuilder();

            o.AppendLine(String.Format("//declare json.schema variables"));

            foreach (PropertyInfo pi in T.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)){
                string field_name = pi.Name;
                object property_value = pi.GetValue(this, null);
                if (null == property_value) continue;
                else {
                    string type="";
                    o.AppendLine(String.Format("DECLARE @{0} {1};",pi.Name,type));
                }
            }

            foreach (PropertyInfo pi in T.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)){
                string field_name = pi.Name;
                object property_value = pi.GetValue(this, null);
                if (null == property_value) continue;
                else {
                //SET @ProdID =  @myDoc.value('(/Root/ProductDescription/@ProductID)[1]', 'int' )  
                    string path=String.Format("{0}/{1}",root,pi.Name);
                    o.AppendLine(String.Format("SET @{0}=@event.value('{2}');",pi.Name,path));
                }

            }


            return o.ToString();
        } 
/*        public string toSQL(){
            StringBuilder o=new StringBuilder();

            string [] properties=get_properties();
            foreach(string property in properties) {
                //SET @ProdID =  @myDoc.value('(/Root/ProductDescription/@ProductID)[1]', 'int' )  
                o.AppendLine(String.Format("",property));
            }
            return o.ToString();
        }*/
    }
}
