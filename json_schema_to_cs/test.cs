namespace IG.kafka.roleupdated
{
    //This prevents failure due to multiple inclusion.
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #pragma warning disable // Disable all warnings

    /// <summary>The headers and contents of an event message</summary>
    [DataContract]

    //Id=
    //Id=
    [XmlType(Namespace = "http://IG.kafka.roleupdated.xsd")] 
    public class @event 
    {

        [JsonProperty("actor", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "actor")]
        [XmlElement]
        [Required(AllowEmptyStrings = true)]
        public     string 
        actor
        { get; set; } 

        /// <summary>A URN of the producer service</summary>

        [JsonProperty("agent", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "agent")]
        [XmlElement]
        [Required(AllowEmptyStrings = true)]
        public     string 
        agent
        { get; set; } 


        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "identifier")]
        [XmlElement]
        [Required(AllowEmptyStrings = true)]
        public     string 
        identifier
        { get; set; } 


        [JsonProperty("occurred", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "occurred")]
        [XmlElement]
        [Required(AllowEmptyStrings = true)]
        public     System.DateTime 
                         occurred
        { get; set; } 

        /// <summary>The contents of an event message</summary>
        [XmlIgnoreAttribute]
        [DataMember(Name = "payload")]
        [XmlElement]
        [Required]
        public virtual     payload 
        payload2
        { get; set; } 

        /// <summary>An URI which can be used for reference by validators. TODO: Generally, JSON messages do not carry their schema around. The consumer and producer services are supposed to know against which schema to validate. This actually duplicates the envelope's type field below</summary>

        [JsonProperty("schema", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "schema")]
        [XmlElement]
        [Required(AllowEmptyStrings = true)]
        public     string 
        schema
        { get; set; } 

        /// <summary>DEPRECATED: Duplicates schema</summary>

        [JsonProperty("type", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "type")]
        [XmlElement]
        public     string 
        type
        { get; set; } 

        /// <summary>DEPRECATED: Use additionalProperties instead.</summary>

        [JsonProperty("headers", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "headers")]
        [XmlElement]
        public     object 
        headers
        { get; set; } 

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static @event FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<@event>(data);
        }

        public @event(){
        }
    }

    /// <summary>Represents a bulk update of an existing security role.</summary>
    [DataContract]

    [@XmlRootAttribute]
    [XmlType(Namespace = "http://IG.kafka.roleupdated.xsd")] 
    public class RoleUpdated : @event
    {

        [JsonProperty("payload", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "payload")]
        [XmlElement]
        public     payload 
        payload
        { get; set; } 

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static RoleUpdated FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RoleUpdated>(data);
        }

        public RoleUpdated(){
        }
    }

    [DataContract]

    //Id=
    //Id=
    [XmlType(Namespace = "http://IG.kafka.roleupdated.xsd")] 
    public class payload 
    {

        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "identifier")]
        [XmlElement]
        [Required(AllowEmptyStrings = true)]
        public     string 
        identifier
        { get; set; } 


        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [DataMember(Name = "name")]
        [XmlElement]
        [Required(AllowEmptyStrings = true)]
        public     string 
        name
        { get; set; } 


        [JsonProperty("description", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "description")]
        [XmlElement]
        public     string 
        description
        { get; set; } 


        [JsonProperty("authorities", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "authorities")]
        [XmlElement]
        public     System.Collections.ObjectModel.ObservableCollection<string> 
                         authorities
        { get; set; } 


        [JsonProperty("members", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [DataMember(Name = "members")]
        [XmlElement]
        public     System.Collections.ObjectModel.ObservableCollection<string> 
                         members
        { get; set; } 

        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static payload FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<payload>(data);
        }

        public payload(){
        }
    }
}