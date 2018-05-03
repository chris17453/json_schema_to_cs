using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonschema_to_cs.model{
    public class schema_map {
        public string url                 { get; set; }
        public string base_dir            { get; set; }
        public string base_name           { get; set; }
        public string event_name          { get; set; }
        public string code_file           { get; set; }
        public string code_dir            { get; set; }
        public string compiled_json_dir   { get; set; }
        public string compiled_json_path  { get; set; }
        public string web_api_dir         { get; set; }
        public string web_api_file        { get; set; }

        public string dll_dir             { get; set; }
        public string dll_file            { get; set; }

        public string xsd_dir             { get; set; }
        public string xsd_file            { get; set; }

        public string @namespace          { get; set; }
        private static bool IsLinux {
            get {
                int p = (int) Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public schema_map(string url,string path,string code_namespace) {
            this.url=url;
            this.base_dir=path;
            if(string.IsNullOrWhiteSpace(url)) {
                Console.WriteLine("Invalid uri");
                return;
            }
            string[] tokens =url.Split('/');
            string   file=tokens.Last();
            string[] tokens2=file.Split('.');
            this.base_name=tokens2.First();
            event_name=base_name.ToLower();
            string prefix="http://";
            int indexof=url.LastIndexOf('/');
            string[] tokens3=url.Substring(prefix.Length,indexof-prefix.Length).Split('/');

            code_dir="";
            string dll_base="";
            foreach(string path_part in tokens3) {
                if(path_part.Length>3) {
                    if(!string.IsNullOrWhiteSpace(code_dir)) {
                        code_dir+=@"\"+path_part;
                        dll_base+="."+path_part;
                    } else {
                        code_dir+=path_part;
                        dll_base+=path_part;
                    }

                }
            }
            string base_code_dir=code_dir;


            string seperator="";
            if(IsLinux) { 
                seperator="/"; 
                base_code_dir=base_code_dir.Replace("\\","/");
            } else { 
                seperator="\\"; 
                base_code_dir=base_code_dir.Replace("/","\\");
            }
            //dirs
            this.code_dir               =String.Format(@"{0}{1}csharp{1}{2}"     ,base_dir,seperator,base_code_dir);
            this.web_api_dir            =String.Format(@"{0}{1}controllers{1}{2}",base_dir,seperator,base_code_dir);
            this.compiled_json_dir      =String.Format(@"{0}{1}jsonschema{1}{2}"   ,base_dir,seperator,base_code_dir);
            this.xsd_dir                =String.Format(@"{0}{1}xsd{1}{2}"        ,base_dir,seperator,base_code_dir);
            this.dll_dir                =String.Format(@"{0}{1}dll{1}"        ,base_dir,seperator);
            //files
            this.code_file              =String.Format(@"{0}{1}{2}.cs"            ,code_dir          ,seperator ,base_name);
            this.web_api_file           =String.Format(@"{0}{1}{2}Controller.cs"  ,web_api_dir       ,seperator ,base_name);
            this.compiled_json_path     =String.Format(@"{0}{1}{2}.json"          ,compiled_json_dir ,seperator ,base_name);
            this.xsd_file               =String.Format(@"{0}{1}.{2}.xsd"           ,xsd_dir           ,seperator ,base_name);
            this.dll_file               =String.Format(@"{0}{1}{2}.{3}.dll"           ,dll_dir           ,seperator ,dll_base, base_name);
            if(event_name=="event") event_name="@event";
            

            this.@namespace=string.Format("{0}.{1}",code_namespace,event_name);

        }//end constructor
    }//end class
}//end namespace
namespace IG.kafka.externalengagementinitiated
{
    //This prevents failure due to multiple inclusion.
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #pragma warning disable // Disable all warnings

    /// <summary>The headers and contents of an event message</summary>
    [DataContract]
    public partial class @event 
    {




        [JsonProperty("actor", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "actor")]
        [Required(AllowEmptyStrings = true)]


        public string actor { get; set; }

        /// <summary>A URN of the producer service</summary>




        [JsonProperty("agent", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "agent")]
        [Required(AllowEmptyStrings = true)]


        public string agent { get; set; }





        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "identifier")]
        [Required(AllowEmptyStrings = true)]


        public string identifier { get; set; }





        [JsonProperty("occurred", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "occurred")]
        [Required(AllowEmptyStrings = true)]


        public System.DateTime occurred { get; set; }

        /// <summary>The contents of an event message</summary>




        [JsonProperty("payload", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "payload")]
        [Required]


        public object payload { get; set; } = new object();

        /// <summary>An URI which can be used for reference by validators. TODO: Generally, JSON messages do not carry their schema around. The consumer and producer services are supposed to know against which schema to validate. This actually duplicates the envelope's type field below</summary>




        [JsonProperty("schema", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "schema")]
        [Required(AllowEmptyStrings = true)]


        public string schema { get; set; }

        /// <summary>DEPRECATED: Duplicates schema</summary>




        [JsonProperty("type", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "type")]


        public string type { get; set; }

        /// <summary>DEPRECATED: Use additionalProperties instead.</summary>




        [JsonProperty("headers", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "headers")]


        public object headers { get; set; } = new object();

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static @event FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<@event>(data);
        }

    }

    /// <summary>A Person.</summary>
    [DataContract]
    public partial class person 
    {




        [JsonProperty("givenName", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "givenName")]
        [Required(AllowEmptyStrings = true)]


        public string givenName { get; set; }





        [JsonProperty("middleName", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "middleName")]


        public string middleName { get; set; }





        [JsonProperty("surname", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "surname")]
        [Required(AllowEmptyStrings = true)]


        public string surname { get; set; }





        [JsonProperty("suffix", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "suffix")]


        public string suffix { get; set; }





        [JsonProperty("email", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "email")]
        [Required(AllowEmptyStrings = true)]


        public string email { get; set; }





        [JsonProperty("cellPhone", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "cellPhone")]


        public string cellPhone { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static person FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<person>(data);
        }

    }

    /// <summary>An Employee.</summary>
    [DataContract]
    public partial class employee : person
    {




        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("active", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "active")]


        public bool active { get; set; } = true;





        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "identifier")]
        [Required(AllowEmptyStrings = true)]


        public string identifier { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static employee FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<employee>(data);
        }

    }

    /// <summary>An employee or person's description</summary>
    [DataContract]
    public partial class user : employee
    {
        /// <summary>Deprecating: Please use middleName, instead</summary>




        [JsonProperty("middleInitial", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "middleInitial")]


        public string middleInitial { get; set; }





        [JsonProperty("salesDate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "salesDate")]


        public System.DateTime salesDate { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static user FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<user>(data);
        }

    }

    /// <summary>TODO: Should use existing standards for interoperability (e.g. http://json-schema.org/address)</summary>
    [DataContract]
    public partial class address 
    {




        [JsonProperty("organization", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "organization")]


        public string organization { get; set; }





        [JsonProperty("address1", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "address1")]
        [Required(AllowEmptyStrings = true)]


        public string address1 { get; set; }





        [JsonProperty("address2", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "address2")]


        public string address2 { get; set; }





        [JsonProperty("address3", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "address3")]


        public string address3 { get; set; }





        [JsonProperty("address4", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "address4")]


        public string address4 { get; set; }

        /// <summary>ANSI alphabetic state code (ANSI standard INCITS 38:2009)</summary>




        [JsonProperty("administrativeArea", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "administrativeArea")]
        [Required(AllowEmptyStrings = true)]


        [JsonConverter(typeof(StringEnumConverter))]
        public addressAdministrativeArea administrativeArea { get; set; }

        /// <summary>Country code, ISO-3166-1 alpha-2</summary>




        [JsonProperty("country", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "country")]
        [Required(AllowEmptyStrings = true)]


        [JsonConverter(typeof(StringEnumConverter))]
        public addressCountry country { get; set; }





        [JsonProperty("locality", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "locality")]
        [Required(AllowEmptyStrings = true)]


        public string locality { get; set; }





        [JsonProperty("postalCode", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "postalCode")]
        [Required(AllowEmptyStrings = true)]


        public string postalCode { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static address FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<address>(data);
        }

    }

    /// <summary>Candidate information</summary>
    [DataContract]
    public partial class candidate : user
    {




        [JsonProperty("preferredName", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "preferredName")]


        public string preferredName { get; set; }





        [JsonProperty("middleInitial", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "middleInitial")]


        public string middleInitial { get; set; }





        [JsonProperty("homePhone", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "homePhone")]


        public string homePhone { get; set; }





        [JsonProperty("enteredBy", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "enteredBy")]


        public employee enteredBy { get; set; } = new employee();





        [JsonProperty("address", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "address")]


        public address address { get; set; } = new address();

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static candidate FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<candidate>(data);
        }

    }

    /// <summary>A billing code relating to week frequency depending upon structure of employment.  Refers to billing field from the comtrak.customer table in comtrak.</summary>
    [DataContract]
    public partial class billingCode 
    {




        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "name")]
        [Required(AllowEmptyStrings = true)]


        [JsonConverter(typeof(StringEnumConverter))]
        public billingCodeName name { get; set; }





        [JsonProperty("freq", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "freq")]


        public billingCodeFreq freq { get; set; }





        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "identifier")]
        [Required(AllowEmptyStrings = true)]


        public string identifier { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static billingCode FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<billingCode>(data);
        }

    }

    /// <summary>Bill Client information</summary>
    [DataContract]
    public partial class billToClient 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "identifier")]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }





        [JsonProperty("billingCode", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "billingCode")]


        public billingCode billingCode { get; set; } = new billingCode();





        [JsonProperty("active", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "active")]


        public bool active { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static billToClient FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<billToClient>(data);
        }

    }

    /// <summary>Work week range</summary>
    [DataContract(Name = "weekPlan")]
    public enum weekPlan
    {
        [EnumMember(Value = "Sun-Sat")]
        Sun_Sat = 0,

        [EnumMember(Value = "Mon-Sun")]
        Mon_Sun = 1,

        [EnumMember(Value = "Tue-Mon")]
        Tue_Mon = 2,

        [EnumMember(Value = "Wed-Tue")]
        Wed_Tue = 3,

        [EnumMember(Value = "Thu-Wed")]
        Thu_Wed = 4,

        [EnumMember(Value = "Fri-Thu")]
        Fri_Thu = 5,

        [EnumMember(Value = "Sat-Fri")]
        Sat_Fri = 6,

    }

    /// <summary>Organizational ownership entity</summary>
    [DataContract]
    public partial class entity 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "identifier")]
        [Required(AllowEmptyStrings = true)]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static entity FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<entity>(data);
        }

    }

    /// <summary>End Client information</summary>
    [DataContract]
    public partial class endClient 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "identifier")]
        [Required(AllowEmptyStrings = true)]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "name")]
        [Required(AllowEmptyStrings = true)]


        public string name { get; set; }





        [JsonProperty("resourceManager", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "resourceManager")]


        public employee resourceManager { get; set; } = new employee();





        [JsonProperty("complianceManager", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "complianceManager")]


        public employee complianceManager { get; set; } = new employee();





        [JsonProperty("accountCoordinator", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "accountCoordinator")]


        public employee accountCoordinator { get; set; } = new employee();





        [JsonProperty("arSpecialist", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "arSpecialist")]


        public employee arSpecialist { get; set; } = new employee();





        [JsonProperty("discountRate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "discountRate")]


        public double discountRate { get; set; }





        [JsonProperty("term", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "term")]


        public term term { get; set; } = new term();





        [JsonProperty("weekPlan", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "weekPlan")]


        [JsonConverter(typeof(StringEnumConverter))]
        public weekPlan weekPlan { get; set; }





        [JsonProperty("industry", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "industry")]


        public industry industry { get; set; } = new industry();





        [JsonProperty("isVip", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "isVip")]


        public bool isVip { get; set; }





        [JsonProperty("active", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "active")]


        public bool active { get; set; }





        [JsonProperty("entity", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "entity")]
        [Required]


        public entity entity { get; set; } = new entity();





        [JsonProperty("facilityFee", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "facilityFee")]


        public double facilityFee { get; set; }





        [JsonProperty("mspFee", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "mspFee")]
        [Range(0, 1)]


        public double mspFee { get; set; }





        [JsonProperty("vmsFee", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "vmsFee")]
        [Range(0, 1)]


        public double vmsFee { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static endClient FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<endClient>(data);
        }

    }

    /// <summary>Account, End Client / Bill Client information</summary>
    [DataContract]
    public partial class account 
    {




        [JsonProperty("billToClient", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "billToClient")]


        public billToClient billToClient { get; set; } = new billToClient();





        [JsonProperty("endClient", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "endClient")]


        public endClient endClient { get; set; } = new endClient();

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static account FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<account>(data);
        }

    }

    /// <summary>Requisition Type</summary>
    [DataContract(Name = "requisitionType")]
    public enum requisitionType
    {
        [EnumMember(Value = "Contract")]
        Contract = 0,

        [EnumMember(Value = "Contract-to-Perm")]
        Contract_to_Perm = 1,

        [EnumMember(Value = "Contract,perm possible")]
        Contract_perm_possible = 2,

        [EnumMember(Value = "Perm")]
        Perm = 3,

    }

    /// <summary>Requisition Status</summary>
    [DataContract(Name = "requisitionStatus")]
    public enum requisitionStatus
    {
        [EnumMember(Value = "Open (Approved)")]
        Open__Approved_ = 0,

        [EnumMember(Value = "Closed")]
        Closed = 1,

        [EnumMember(Value = "Heads-up")]
        Heads_up = 2,

        [EnumMember(Value = "Closed (Inactivity)")]
        Closed__Inactivity_ = 3,

    }

    /// <summary>An Office facility where employees meet to work.</summary>
    [DataContract]
    public partial class office 
    {




        [JsonProperty("code", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "code")]
        [Required(AllowEmptyStrings = true)]


        [RegularExpression(@"^[A-Z]{3}$")]
        public string code { get; set; }





        [JsonProperty("address", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "address")]


        public address address { get; set; } = new address();





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "name")]
        [Required(AllowEmptyStrings = true)]


        public string name { get; set; }





        [JsonProperty("phone", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "phone")]


        public string phone { get; set; }





        [JsonProperty("phoneExt", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "phoneExt")]


        public string phoneExt { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static office FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<office>(data);
        }

    }

    [DataContract(Name = "payType")]
    public enum payType
    {
        [EnumMember(Value = "Hourly")]
        Hourly = 0,

        [EnumMember(Value = "Salary")]
        Salary = 1,

        [EnumMember(Value = "Stipend")]
        Stipend = 2,

        [EnumMember(Value = "Daily")]
        Daily = 3,

        [EnumMember(Value = "OneTimeAmount")]
        OneTimeAmount = 4,

        [EnumMember(Value = "UnitPay")]
        UnitPay = 5,

        [EnumMember(Value = "Milestone")]
        Milestone = 6,

    }

    /// <summary>ISO 4217 Currency Code</summary>
    [DataContract(Name = "currency")]
    public enum currency
    {
        [EnumMember(Value = "USD")]
        USD = 0,

        [EnumMember(Value = "CAD")]
        CAD = 1,

        [EnumMember(Value = "EUR")]
        EUR = 2,

        [EnumMember(Value = "GBP")]
        GBP = 3,

        [EnumMember(Value = "INR")]
        INR = 4,

        [EnumMember(Value = "SGD")]
        SGD = 5,

    }

    [DataContract]
    public partial class payRate 
    {




        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "type")]
        [Required(AllowEmptyStrings = true)]


        [JsonConverter(typeof(StringEnumConverter))]
        public payType type { get; set; }





        [JsonProperty("straight", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "straight")]
        [Range(0, int.MaxValue)]


        public double straight { get; set; }





        [JsonProperty("overtime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "overtime")]
        [Range(0, int.MaxValue)]


        public double overtime { get; set; }





        [JsonProperty("overtimeFactor", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "overtimeFactor")]
        [Range(1, int.MaxValue)]


        public double overtimeFactor { get; set; } = 1D;





        [JsonProperty("salary", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "salary")]
        [Range(0, int.MaxValue)]


        public double salary { get; set; }





        [JsonProperty("perDiem", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "perDiem")]
        [Range(0, int.MaxValue)]


        public double perDiem { get; set; }





        [JsonProperty("fringe", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "fringe")]
        [Range(0, int.MaxValue)]


        public double fringe { get; set; }





        [JsonProperty("currency", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "currency")]


        [JsonConverter(typeof(StringEnumConverter))]
        public currency currency { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static payRate FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<payRate>(data);
        }

    }

    /// <summary>Requisition Contact Person</summary>
    [DataContract]
    public partial class contact : person
    {




        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("active", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "active")]


        public bool active { get; set; }





        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "identifier")]
        [Required(AllowEmptyStrings = true)]


        public string identifier { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static contact FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<contact>(data);
        }

    }

    /// <summary>Requisition information</summary>
    [DataContract]
    public partial class requisition 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "identifier")]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("jobTitle", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "jobTitle")]


        public string jobTitle { get; set; }





        [JsonProperty("category", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "category")]


        public category category { get; set; } = new category();





        [JsonProperty("type", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "type")]


        [JsonConverter(typeof(StringEnumConverter))]
        public requisitionType type { get; set; }





        [JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "status")]


        [JsonConverter(typeof(StringEnumConverter))]
        public requisitionStatus status { get; set; }





        [JsonProperty("office", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "office")]


        public office office { get; set; } = new office();





        [JsonProperty("division", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "division")]


        public division division { get; set; } = new division();





        [JsonProperty("accountManager", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "accountManager")]


        public user accountManager { get; set; }





        [JsonProperty("recruiter", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "recruiter")]


        public user recruiter { get; set; }





        [JsonProperty("ratePerm", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "ratePerm")]


        public ratePerm ratePerm { get; set; } = new ratePerm();





        [JsonProperty("billRate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "billRate")]


        public payRate billRate { get; set; } = new payRate();





        [JsonProperty("endRate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "endRate")]


        public endRate endRate { get; set; } = new endRate();





        [JsonProperty("payRate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "payRate")]


        public payRate payRate { get; set; } = new payRate();





        [JsonProperty("startDate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "startDate")]


        public System.DateTime startDate { get; set; }





        [JsonProperty("duration", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "duration")]


        public string duration { get; set; }





        [JsonProperty("notes", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "notes")]


        public string notes { get; set; }





        [JsonProperty("workAddress", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "workAddress")]


        public address workAddress { get; set; } = new address();





        [JsonProperty("contact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "contact")]


        public contact contact { get; set; } = new contact();





        [JsonProperty("billContact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "billContact")]


        public contact billContact { get; set; } = new contact();





        [JsonProperty("billAddress", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "billAddress")]


        public address billAddress { get; set; } = new address();





        [JsonProperty("securityClearanceRequired", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "securityClearanceRequired")]


        public bool securityClearanceRequired { get; set; }





        [JsonProperty("workAnywhere", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "workAnywhere")]


        public bool workAnywhere { get; set; }





        [JsonProperty("isCorp", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "isCorp")]


        public bool isCorp { get; set; }





        [JsonProperty("isNrt", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "isNrt")]


        public bool isNrt { get; set; }





        [JsonProperty("isFederal", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "isFederal")]


        public bool isFederal { get; set; }





        [JsonProperty("federalClearance", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "federalClearance")]


        public federalClearance federalClearance { get; set; } = new federalClearance();





        [JsonProperty("federalDepartment", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "federalDepartment")]


        public federalDepartment federalDepartment { get; set; } = new federalDepartment();





        [JsonProperty("federalAgency", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "federalAgency")]


        public federalAgency federalAgency { get; set; } = new federalAgency();





        [JsonProperty("region", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "region")]


        public region region { get; set; } = new region();





        [JsonProperty("entity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "entity")]


        public entity entity { get; set; } = new entity();

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static requisition FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<requisition>(data);
        }

    }

    [DataContract(Name = "gender")]
    public enum gender
    {
        [EnumMember(Value = "Male")]
        Male = 0,

        [EnumMember(Value = "Female")]
        Female = 1,

        [EnumMember(Value = "Do not wish to disclose")]
        Do_not_wish_to_disclose = 2,

    }

    [DataContract(Name = "ethnicity")]
    public enum ethnicity
    {
        [EnumMember(Value = "White (Non-Hispanic and Non-Latino)")]
        White__Non_Hispanic_and_Non_Latino_ = 0,

        [EnumMember(Value = "American Indian/Alaskan Native")]
        American_Indian_Alaskan_Native = 1,

        [EnumMember(Value = "Native Hawaiian or Pacific Islander")]
        Native_Hawaiian_or_Pacific_Islander = 2,

        [EnumMember(Value = "Hispanic or Latino")]
        Hispanic_or_Latino = 3,

        [EnumMember(Value = "Black or African American")]
        Black_or_African_American = 4,

        [EnumMember(Value = "Asian")]
        Asian = 5,

        [EnumMember(Value = "Do not wish to disclose")]
        Do_not_wish_to_disclose = 6,

        [EnumMember(Value = "Two or More Races")]
        Two_or_More_Races = 7,

    }

    /// <summary>Job application information</summary>
    [DataContract]
    public partial class jobApplication 
    {




        [JsonProperty("uniqid", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "uniqid")]


        public string uniqid { get; set; }





        [JsonProperty("applicant", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "applicant")]


        public applicant applicant { get; set; } = new applicant();





        [JsonProperty("location", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "location")]


        public string location { get; set; }





        [JsonProperty("jobcategories", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "jobcategories")]


        public string jobcategories { get; set; }





        [JsonProperty("referral", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "referral")]


        public string referral { get; set; }





        [JsonProperty("referral_other", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "referral_other")]


        public string referral_other { get; set; }





        [JsonProperty("posted", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "posted")]


        public string posted { get; set; }





        [JsonProperty("posted_other", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "posted_other")]


        public string posted_other { get; set; }





        [JsonProperty("workPrior", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "workPrior")]


        public bool workPrior { get; set; }





        [JsonProperty("workLegal", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "workLegal")]


        public bool workLegal { get; set; }





        [JsonProperty("transport", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "transport")]


        public bool transport { get; set; }





        [JsonProperty("felony", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "felony")]


        public bool felony { get; set; }





        [JsonProperty("misdemeanor", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "misdemeanor")]


        public bool misdemeanor { get; set; }





        [JsonProperty("trueStmt", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "trueStmt")]


        public bool trueStmt { get; set; }





        [JsonProperty("ssn", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "ssn")]


        public string ssn { get; set; }





        [JsonProperty("gender", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "gender")]


        [JsonConverter(typeof(StringEnumConverter))]
        public gender gender { get; set; }





        [JsonProperty("ethnic", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "ethnic")]


        [JsonConverter(typeof(StringEnumConverter))]
        public ethnicity ethnic { get; set; }





        [JsonProperty("disability", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "disability")]


        public bool disability { get; set; }





        [JsonProperty("complete", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "complete")]


        public bool complete { get; set; }





        [JsonProperty("feedback", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "feedback")]


        public string feedback { get; set; }





        [JsonProperty("officeId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "officeId")]


        public int officeId { get; set; }





        [JsonProperty("ip", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "ip")]


        public string ip { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static jobApplication FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<jobApplication>(data);
        }

    }

    /// <summary>External Engagement Initiated</summary>
    [DataContract]
    public partial class ExternalEngagementInitiated : @event
    {




        [JsonProperty("payload", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "payload")]


        public payload payload { get; set; } = new payload();

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static ExternalEngagementInitiated FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ExternalEngagementInitiated>(data);
        }

    }

    [DataContract(Name = "addressAdministrativeArea")]
    public enum addressAdministrativeArea
    {
        [EnumMember(Value = "AL")]
        AL = 0,

        [EnumMember(Value = "AK")]
        AK = 1,

        [EnumMember(Value = "AZ")]
        AZ = 2,

        [EnumMember(Value = "AR")]
        AR = 3,

        [EnumMember(Value = "CA")]
        CA = 4,

        [EnumMember(Value = "CO")]
        CO = 5,

        [EnumMember(Value = "CT")]
        CT = 6,

        [EnumMember(Value = "DE")]
        DE = 7,

        [EnumMember(Value = "FL")]
        FL = 8,

        [EnumMember(Value = "GA")]
        GA = 9,

        [EnumMember(Value = "HI")]
        HI = 10,

        [EnumMember(Value = "ID")]
        ID = 11,

        [EnumMember(Value = "IL")]
        IL = 12,

        [EnumMember(Value = "IN")]
        IN = 13,

        [EnumMember(Value = "IA")]
        IA = 14,

        [EnumMember(Value = "KS")]
        KS = 15,

        [EnumMember(Value = "KY")]
        KY = 16,

        [EnumMember(Value = "LA")]
        LA = 17,

        [EnumMember(Value = "ME")]
        ME = 18,

        [EnumMember(Value = "MD")]
        MD = 19,

        [EnumMember(Value = "MA")]
        MA = 20,

        [EnumMember(Value = "MI")]
        MI = 21,

        [EnumMember(Value = "MN")]
        MN = 22,

        [EnumMember(Value = "MS")]
        MS = 23,

        [EnumMember(Value = "MO")]
        MO = 24,

        [EnumMember(Value = "MT")]
        MT = 25,

        [EnumMember(Value = "NE")]
        NE = 26,

        [EnumMember(Value = "NV")]
        NV = 27,

        [EnumMember(Value = "NH")]
        NH = 28,

        [EnumMember(Value = "NJ")]
        NJ = 29,

        [EnumMember(Value = "NM")]
        NM = 30,

        [EnumMember(Value = "NY")]
        NY = 31,

        [EnumMember(Value = "NC")]
        NC = 32,

        [EnumMember(Value = "ND")]
        ND = 33,

        [EnumMember(Value = "OH")]
        OH = 34,

        [EnumMember(Value = "OK")]
        OK = 35,

        [EnumMember(Value = "OR")]
        OR = 36,

        [EnumMember(Value = "PA")]
        PA = 37,

        [EnumMember(Value = "RI")]
        RI = 38,

        [EnumMember(Value = "SC")]
        SC = 39,

        [EnumMember(Value = "SD")]
        SD = 40,

        [EnumMember(Value = "TN")]
        TN = 41,

        [EnumMember(Value = "TX")]
        TX = 42,

        [EnumMember(Value = "UT")]
        UT = 43,

        [EnumMember(Value = "VT")]
        VT = 44,

        [EnumMember(Value = "VA")]
        VA = 45,

        [EnumMember(Value = "WA")]
        WA = 46,

        [EnumMember(Value = "WV")]
        WV = 47,

        [EnumMember(Value = "WI")]
        WI = 48,

        [EnumMember(Value = "WY")]
        WY = 49,

        [EnumMember(Value = "DC")]
        DC = 50,

        [EnumMember(Value = "AS")]
        AS = 51,

        [EnumMember(Value = "GU")]
        GU = 52,

        [EnumMember(Value = "MP")]
        MP = 53,

        [EnumMember(Value = "PR")]
        PR = 54,

        [EnumMember(Value = "UM")]
        UM = 55,

        [EnumMember(Value = "VI")]
        VI = 56,

        [EnumMember(Value = "AB")]
        AB = 57,

        [EnumMember(Value = "BC")]
        BC = 58,

        [EnumMember(Value = "MB")]
        MB = 59,

        [EnumMember(Value = "NB")]
        NB = 60,

        [EnumMember(Value = "NL")]
        NL = 61,

        [EnumMember(Value = "NS")]
        NS = 62,

        [EnumMember(Value = "ON")]
        ON = 63,

        [EnumMember(Value = "PE")]
        PE = 64,

        [EnumMember(Value = "QC")]
        QC = 65,

        [EnumMember(Value = "SK")]
        SK = 66,

        [EnumMember(Value = "NT")]
        NT = 67,

        [EnumMember(Value = "NU")]
        NU = 68,

        [EnumMember(Value = "YT")]
        YT = 69,

    }

    [DataContract(Name = "addressCountry")]
    public enum addressCountry
    {
        [EnumMember(Value = "AD")]
        AD = 0,

        [EnumMember(Value = "AE")]
        AE = 1,

        [EnumMember(Value = "AF")]
        AF = 2,

        [EnumMember(Value = "AG")]
        AG = 3,

        [EnumMember(Value = "AI")]
        AI = 4,

        [EnumMember(Value = "AL")]
        AL = 5,

        [EnumMember(Value = "AM")]
        AM = 6,

        [EnumMember(Value = "AO")]
        AO = 7,

        [EnumMember(Value = "AQ")]
        AQ = 8,

        [EnumMember(Value = "AR")]
        AR = 9,

        [EnumMember(Value = "AS")]
        AS = 10,

        [EnumMember(Value = "AT")]
        AT = 11,

        [EnumMember(Value = "AU")]
        AU = 12,

        [EnumMember(Value = "AW")]
        AW = 13,

        [EnumMember(Value = "AX")]
        AX = 14,

        [EnumMember(Value = "AZ")]
        AZ = 15,

        [EnumMember(Value = "BA")]
        BA = 16,

        [EnumMember(Value = "BB")]
        BB = 17,

        [EnumMember(Value = "BD")]
        BD = 18,

        [EnumMember(Value = "BE")]
        BE = 19,

        [EnumMember(Value = "BF")]
        BF = 20,

        [EnumMember(Value = "BG")]
        BG = 21,

        [EnumMember(Value = "BH")]
        BH = 22,

        [EnumMember(Value = "BI")]
        BI = 23,

        [EnumMember(Value = "BJ")]
        BJ = 24,

        [EnumMember(Value = "BL")]
        BL = 25,

        [EnumMember(Value = "BM")]
        BM = 26,

        [EnumMember(Value = "BN")]
        BN = 27,

        [EnumMember(Value = "BO")]
        BO = 28,

        [EnumMember(Value = "BQ")]
        BQ = 29,

        [EnumMember(Value = "BR")]
        BR = 30,

        [EnumMember(Value = "BS")]
        BS = 31,

        [EnumMember(Value = "BT")]
        BT = 32,

        [EnumMember(Value = "BV")]
        BV = 33,

        [EnumMember(Value = "BW")]
        BW = 34,

        [EnumMember(Value = "BY")]
        BY = 35,

        [EnumMember(Value = "BZ")]
        BZ = 36,

        [EnumMember(Value = "CA")]
        CA = 37,

        [EnumMember(Value = "CC")]
        CC = 38,

        [EnumMember(Value = "CD")]
        CD = 39,

        [EnumMember(Value = "CF")]
        CF = 40,

        [EnumMember(Value = "CG")]
        CG = 41,

        [EnumMember(Value = "CH")]
        CH = 42,

        [EnumMember(Value = "CI")]
        CI = 43,

        [EnumMember(Value = "CK")]
        CK = 44,

        [EnumMember(Value = "CL")]
        CL = 45,

        [EnumMember(Value = "CM")]
        CM = 46,

        [EnumMember(Value = "CN")]
        CN = 47,

        [EnumMember(Value = "CO")]
        CO = 48,

        [EnumMember(Value = "CR")]
        CR = 49,

        [EnumMember(Value = "CU")]
        CU = 50,

        [EnumMember(Value = "CV")]
        CV = 51,

        [EnumMember(Value = "CW")]
        CW = 52,

        [EnumMember(Value = "CX")]
        CX = 53,

        [EnumMember(Value = "CY")]
        CY = 54,

        [EnumMember(Value = "CZ")]
        CZ = 55,

        [EnumMember(Value = "DE")]
        DE = 56,

        [EnumMember(Value = "DJ")]
        DJ = 57,

        [EnumMember(Value = "DK")]
        DK = 58,

        [EnumMember(Value = "DM")]
        DM = 59,

        [EnumMember(Value = "DO")]
        DO = 60,

        [EnumMember(Value = "DZ")]
        DZ = 61,

        [EnumMember(Value = "EC")]
        EC = 62,

        [EnumMember(Value = "EE")]
        EE = 63,

        [EnumMember(Value = "EG")]
        EG = 64,

        [EnumMember(Value = "EH")]
        EH = 65,

        [EnumMember(Value = "ER")]
        ER = 66,

        [EnumMember(Value = "ES")]
        ES = 67,

        [EnumMember(Value = "ET")]
        ET = 68,

        [EnumMember(Value = "FI")]
        FI = 69,

        [EnumMember(Value = "FJ")]
        FJ = 70,

        [EnumMember(Value = "FK")]
        FK = 71,

        [EnumMember(Value = "FM")]
        FM = 72,

        [EnumMember(Value = "FO")]
        FO = 73,

        [EnumMember(Value = "FR")]
        FR = 74,

        [EnumMember(Value = "GA")]
        GA = 75,

        [EnumMember(Value = "GB")]
        GB = 76,

        [EnumMember(Value = "GD")]
        GD = 77,

        [EnumMember(Value = "GE")]
        GE = 78,

        [EnumMember(Value = "GF")]
        GF = 79,

        [EnumMember(Value = "GG")]
        GG = 80,

        [EnumMember(Value = "GH")]
        GH = 81,

        [EnumMember(Value = "GI")]
        GI = 82,

        [EnumMember(Value = "GL")]
        GL = 83,

        [EnumMember(Value = "GM")]
        GM = 84,

        [EnumMember(Value = "GN")]
        GN = 85,

        [EnumMember(Value = "GP")]
        GP = 86,

        [EnumMember(Value = "GQ")]
        GQ = 87,

        [EnumMember(Value = "GR")]
        GR = 88,

        [EnumMember(Value = "GS")]
        GS = 89,

        [EnumMember(Value = "GT")]
        GT = 90,

        [EnumMember(Value = "GU")]
        GU = 91,

        [EnumMember(Value = "GW")]
        GW = 92,

        [EnumMember(Value = "GY")]
        GY = 93,

        [EnumMember(Value = "HK")]
        HK = 94,

        [EnumMember(Value = "HM")]
        HM = 95,

        [EnumMember(Value = "HN")]
        HN = 96,

        [EnumMember(Value = "HR")]
        HR = 97,

        [EnumMember(Value = "HT")]
        HT = 98,

        [EnumMember(Value = "HU")]
        HU = 99,

        [EnumMember(Value = "ID")]
        ID = 100,

        [EnumMember(Value = "IE")]
        IE = 101,

        [EnumMember(Value = "IL")]
        IL = 102,

        [EnumMember(Value = "IM")]
        IM = 103,

        [EnumMember(Value = "IN")]
        IN = 104,

        [EnumMember(Value = "IO")]
        IO = 105,

        [EnumMember(Value = "IQ")]
        IQ = 106,

        [EnumMember(Value = "IR")]
        IR = 107,

        [EnumMember(Value = "IS")]
        IS = 108,

        [EnumMember(Value = "IT")]
        IT = 109,

        [EnumMember(Value = "JE")]
        JE = 110,

        [EnumMember(Value = "JM")]
        JM = 111,

        [EnumMember(Value = "JO")]
        JO = 112,

        [EnumMember(Value = "JP")]
        JP = 113,

        [EnumMember(Value = "KE")]
        KE = 114,

        [EnumMember(Value = "KG")]
        KG = 115,

        [EnumMember(Value = "KH")]
        KH = 116,

        [EnumMember(Value = "KI")]
        KI = 117,

        [EnumMember(Value = "KM")]
        KM = 118,

        [EnumMember(Value = "KN")]
        KN = 119,

        [EnumMember(Value = "KP")]
        KP = 120,

        [EnumMember(Value = "KR")]
        KR = 121,

        [EnumMember(Value = "KW")]
        KW = 122,

        [EnumMember(Value = "KY")]
        KY = 123,

        [EnumMember(Value = "KZ")]
        KZ = 124,

        [EnumMember(Value = "LA")]
        LA = 125,

        [EnumMember(Value = "LB")]
        LB = 126,

        [EnumMember(Value = "LC")]
        LC = 127,

        [EnumMember(Value = "LI")]
        LI = 128,

        [EnumMember(Value = "LK")]
        LK = 129,

        [EnumMember(Value = "LR")]
        LR = 130,

        [EnumMember(Value = "LS")]
        LS = 131,

        [EnumMember(Value = "LT")]
        LT = 132,

        [EnumMember(Value = "LU")]
        LU = 133,

        [EnumMember(Value = "LV")]
        LV = 134,

        [EnumMember(Value = "LY")]
        LY = 135,

        [EnumMember(Value = "MA")]
        MA = 136,

        [EnumMember(Value = "MC")]
        MC = 137,

        [EnumMember(Value = "MD")]
        MD = 138,

        [EnumMember(Value = "ME")]
        ME = 139,

        [EnumMember(Value = "MF")]
        MF = 140,

        [EnumMember(Value = "MG")]
        MG = 141,

        [EnumMember(Value = "MH")]
        MH = 142,

        [EnumMember(Value = "MK")]
        MK = 143,

        [EnumMember(Value = "ML")]
        ML = 144,

        [EnumMember(Value = "MM")]
        MM = 145,

        [EnumMember(Value = "MN")]
        MN = 146,

        [EnumMember(Value = "MO")]
        MO = 147,

        [EnumMember(Value = "MP")]
        MP = 148,

        [EnumMember(Value = "MQ")]
        MQ = 149,

        [EnumMember(Value = "MR")]
        MR = 150,

        [EnumMember(Value = "MS")]
        MS = 151,

        [EnumMember(Value = "MT")]
        MT = 152,

        [EnumMember(Value = "MU")]
        MU = 153,

        [EnumMember(Value = "MV")]
        MV = 154,

        [EnumMember(Value = "MW")]
        MW = 155,

        [EnumMember(Value = "MX")]
        MX = 156,

        [EnumMember(Value = "MY")]
        MY = 157,

        [EnumMember(Value = "MZ")]
        MZ = 158,

        [EnumMember(Value = "NA")]
        NA = 159,

        [EnumMember(Value = "NC")]
        NC = 160,

        [EnumMember(Value = "NE")]
        NE = 161,

        [EnumMember(Value = "NF")]
        NF = 162,

        [EnumMember(Value = "NG")]
        NG = 163,

        [EnumMember(Value = "NI")]
        NI = 164,

        [EnumMember(Value = "NL")]
        NL = 165,

        [EnumMember(Value = "NO")]
        NO = 166,

        [EnumMember(Value = "NP")]
        NP = 167,

        [EnumMember(Value = "NR")]
        NR = 168,

        [EnumMember(Value = "NU")]
        NU = 169,

        [EnumMember(Value = "NZ")]
        NZ = 170,

        [EnumMember(Value = "OM")]
        OM = 171,

        [EnumMember(Value = "PA")]
        PA = 172,

        [EnumMember(Value = "PE")]
        PE = 173,

        [EnumMember(Value = "PF")]
        PF = 174,

        [EnumMember(Value = "PG")]
        PG = 175,

        [EnumMember(Value = "PH")]
        PH = 176,

        [EnumMember(Value = "PK")]
        PK = 177,

        [EnumMember(Value = "PL")]
        PL = 178,

        [EnumMember(Value = "PM")]
        PM = 179,

        [EnumMember(Value = "PN")]
        PN = 180,

        [EnumMember(Value = "PR")]
        PR = 181,

        [EnumMember(Value = "PS")]
        PS = 182,

        [EnumMember(Value = "PT")]
        PT = 183,

        [EnumMember(Value = "PW")]
        PW = 184,

        [EnumMember(Value = "PY")]
        PY = 185,

        [EnumMember(Value = "QA")]
        QA = 186,

        [EnumMember(Value = "RE")]
        RE = 187,

        [EnumMember(Value = "RO")]
        RO = 188,

        [EnumMember(Value = "RS")]
        RS = 189,

        [EnumMember(Value = "RU")]
        RU = 190,

        [EnumMember(Value = "RW")]
        RW = 191,

        [EnumMember(Value = "SA")]
        SA = 192,

        [EnumMember(Value = "SB")]
        SB = 193,

        [EnumMember(Value = "SC")]
        SC = 194,

        [EnumMember(Value = "SD")]
        SD = 195,

        [EnumMember(Value = "SE")]
        SE = 196,

        [EnumMember(Value = "SG")]
        SG = 197,

        [EnumMember(Value = "SH")]
        SH = 198,

        [EnumMember(Value = "SI")]
        SI = 199,

        [EnumMember(Value = "SJ")]
        SJ = 200,

        [EnumMember(Value = "SK")]
        SK = 201,

        [EnumMember(Value = "SL")]
        SL = 202,

        [EnumMember(Value = "SM")]
        SM = 203,

        [EnumMember(Value = "SN")]
        SN = 204,

        [EnumMember(Value = "SO")]
        SO = 205,

        [EnumMember(Value = "SR")]
        SR = 206,

        [EnumMember(Value = "SS")]
        SS = 207,

        [EnumMember(Value = "ST")]
        ST = 208,

        [EnumMember(Value = "SV")]
        SV = 209,

        [EnumMember(Value = "SX")]
        SX = 210,

        [EnumMember(Value = "SY")]
        SY = 211,

        [EnumMember(Value = "SZ")]
        SZ = 212,

        [EnumMember(Value = "TC")]
        TC = 213,

        [EnumMember(Value = "TD")]
        TD = 214,

        [EnumMember(Value = "TF")]
        TF = 215,

        [EnumMember(Value = "TG")]
        TG = 216,

        [EnumMember(Value = "TH")]
        TH = 217,

        [EnumMember(Value = "TJ")]
        TJ = 218,

        [EnumMember(Value = "TK")]
        TK = 219,

        [EnumMember(Value = "TL")]
        TL = 220,

        [EnumMember(Value = "TM")]
        TM = 221,

        [EnumMember(Value = "TN")]
        TN = 222,

        [EnumMember(Value = "TO")]
        TO = 223,

        [EnumMember(Value = "TR")]
        TR = 224,

        [EnumMember(Value = "TT")]
        TT = 225,

        [EnumMember(Value = "TV")]
        TV = 226,

        [EnumMember(Value = "TW")]
        TW = 227,

        [EnumMember(Value = "TZ")]
        TZ = 228,

        [EnumMember(Value = "UA")]
        UA = 229,

        [EnumMember(Value = "UG")]
        UG = 230,

        [EnumMember(Value = "UM")]
        UM = 231,

        [EnumMember(Value = "US")]
        US = 232,

        [EnumMember(Value = "UY")]
        UY = 233,

        [EnumMember(Value = "UZ")]
        UZ = 234,

        [EnumMember(Value = "VA")]
        VA = 235,

        [EnumMember(Value = "VC")]
        VC = 236,

        [EnumMember(Value = "VE")]
        VE = 237,

        [EnumMember(Value = "VG")]
        VG = 238,

        [EnumMember(Value = "VI")]
        VI = 239,

        [EnumMember(Value = "VN")]
        VN = 240,

        [EnumMember(Value = "VU")]
        VU = 241,

        [EnumMember(Value = "WF")]
        WF = 242,

        [EnumMember(Value = "WS")]
        WS = 243,

        [EnumMember(Value = "YE")]
        YE = 244,

        [EnumMember(Value = "YT")]
        YT = 245,

        [EnumMember(Value = "ZA")]
        ZA = 246,

        [EnumMember(Value = "ZM")]
        ZM = 247,

        [EnumMember(Value = "ZW")]
        ZW = 248,

    }

    [DataContract(Name = "billingCodeName")]
    public enum billingCodeName
    {
        [EnumMember(Value = "Bi-Weekly (Even)")]
        Bi_Weekly__Even_ = 0,

        [EnumMember(Value = "Monthly")]
        Monthly = 1,

        [EnumMember(Value = "Weekly")]
        Weekly = 2,

        [EnumMember(Value = "Monthly (by Day)")]
        Monthly__by_Day_ = 3,

        [EnumMember(Value = "On-Demand")]
        On_Demand = 4,

        [EnumMember(Value = "Semi Monthly")]
        Semi_Monthly = 5,

        [EnumMember(Value = "Statement-of-Work")]
        Statement_of_Work = 6,

        [EnumMember(Value = "Quad Weekly")]
        Quad_Weekly = 7,

    }

    [DataContract(Name = "billingCodeFreq")]
    public enum billingCodeFreq
    {
        _12 = 12,

        _13 = 13,

        _24 = 24,

        _26 = 26,

        _52 = 52,

    }

    [DataContract]
    public partial class term 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "identifier")]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static term FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<term>(data);
        }

    }

    [DataContract]
    public partial class industry 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "identifier")]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static industry FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<industry>(data);
        }

    }

    [DataContract]
    public partial class category 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "identifier")]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static category FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<category>(data);
        }

    }

    [DataContract]
    public partial class division 
    {




        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static division FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<division>(data);
        }

    }

    [DataContract]
    public partial class ratePerm 
    {




        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static ratePerm FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ratePerm>(data);
        }

    }

    [DataContract]
    public partial class endRate 
    {




        [JsonProperty("straight", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "straight")]


        public double straight { get; set; }





        [JsonProperty("overtime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "overtime")]


        public double overtime { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static endRate FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<endRate>(data);
        }

    }

    [DataContract]
    public partial class federalClearance 
    {




        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static federalClearance FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<federalClearance>(data);
        }

    }

    [DataContract]
    public partial class federalDepartment 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "identifier")]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static federalDepartment FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<federalDepartment>(data);
        }

    }

    [DataContract]
    public partial class federalAgency 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "identifier")]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static federalAgency FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<federalAgency>(data);
        }

    }

    [DataContract]
    public partial class region 
    {




        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "identifier")]


        public string identifier { get; set; }





        [JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "legacyIdentifier")]


        public int legacyIdentifier { get; set; }





        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "name")]


        public string name { get; set; }

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static region FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<region>(data);
        }

    }

    [DataContract]
    public partial class applicant : user
    {




        [JsonProperty("homePhone", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "homePhone")]


        public string homePhone { get; set; }





        [JsonProperty("middleInitial", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "middleInitial")]


        public string middleInitial { get; set; }





        [JsonProperty("address", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "address")]


        public address address { get; set; } = new address();

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static applicant FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<applicant>(data);
        }

    }

    [DataContract]
    public partial class payload 
    {




        [JsonProperty("candidate", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "candidate")]
        [Required]


        public candidate candidate { get; set; } = new candidate();





        [JsonProperty("account", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "account")]
        [Required]


        public account account { get; set; } = new account();





        [JsonProperty("requisition", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "requisition")]
        [Required]


        public requisition requisition { get; set; } = new requisition();





        [JsonProperty("jobApplication", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "jobApplication")]


        public jobApplication jobApplication { get; set; } = new jobApplication();

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static payload FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<payload>(data);
        }

    }
}