//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v9.10.42.0 (Newtonsoft.Json v11.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------

namespace IG.kafka.candidatesentjobappcode
{
    #pragma warning disable // Disable all warnings

    /// <summary>The headers and contents of a candidate event message</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class @event 
    {
        [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string type { get; set; }
    
        [Newtonsoft.Json.JsonProperty("performedBy", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string performedBy { get; set; }
    
        [Newtonsoft.Json.JsonProperty("eventId", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string eventId { get; set; }
    
        [Newtonsoft.Json.JsonProperty("created", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public System.DateTime created { get; set; }
    
        [Newtonsoft.Json.JsonProperty("version", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
        public int version { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        
        public static @event FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<@event>(data);
        }
    
    }
    
    /// <summary>A candidate has been activated.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.42.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class CandidateSentJobAppCode : @event
    {
        [Newtonsoft.Json.JsonProperty("candidateId", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string candidateId { get; set; }
    
        /// <summary>TODO: Validation rules?</summary>
        [Newtonsoft.Json.JsonProperty("jobAppCode", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string jobAppCode { get; set; }
    
        [Newtonsoft.Json.JsonProperty("submissionDate", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public System.DateTime submissionDate { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        
        public static CandidateSentJobAppCode FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<CandidateSentJobAppCode>(data);
        }
    
    }
}