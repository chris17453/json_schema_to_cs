//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v9.10.42.0 (Newtonsoft.Json v11.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------

namespace IG.kafka.entityupdated
{
    #pragma warning disable // Disable all warnings

    /// <summary>The headers and contents of an event message</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class @event 
    {
        [Newtonsoft.Json.JsonProperty("actor", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string actor { get; set; }
    
        /// <summary>A URN of the producer service</summary>
        [Newtonsoft.Json.JsonProperty("agent", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string agent { get; set; }
    
        [Newtonsoft.Json.JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string identifier { get; set; }
    
        [Newtonsoft.Json.JsonProperty("occurred", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public System.DateTime occurred { get; set; }
    
        /// <summary>The contents of an event message</summary>
        [Newtonsoft.Json.JsonProperty("payload", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public object payload { get; set; } = new object();
    
        /// <summary>An URI which can be used for reference by validators. TODO: Generally, JSON messages do not carry their schema around. The consumer and producer services are supposed to know against which schema to validate. This actually duplicates the envelope's type field below</summary>
        [Newtonsoft.Json.JsonProperty("schema", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string schema { get; set; }
    
        /// <summary>DEPRECATED: Duplicates schema</summary>
        [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string type { get; set; }
    
        /// <summary>DEPRECATED: Use additionalProperties instead.</summary>
        [Newtonsoft.Json.JsonProperty("headers", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
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
    
    /// <summary>Organizational ownership entity</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class entity 
    {
        [Newtonsoft.Json.JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string identifier { get; set; }
    
        [Newtonsoft.Json.JsonProperty("legacyIdentifier", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int legacyIdentifier { get; set; }
    
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
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
    
    /// <summary>Represents a bulk update of an existing entity.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class EntityUpdated : @event
    {
        [Newtonsoft.Json.JsonProperty("payload", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public payload payload { get; set; } = new payload();
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        
        public static EntityUpdated FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<EntityUpdated>(data);
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class payload 
    {
        [Newtonsoft.Json.JsonProperty("entity", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public entity entity { get; set; } = new entity();
    
        [Newtonsoft.Json.JsonProperty("email", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string email { get; set; }
    
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