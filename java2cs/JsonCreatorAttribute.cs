using System;

namespace JavaDeps
{
    public class JsonCreator: Attribute
    {
        public string type;
        public JsonCreator(string json_types) {
            this.type=json_types;
        }
    }
    public class JsonInclude: Attribute
    {
        public string type;
        public JsonInclude(string type) {
            this.type=type;
        }
    }
    public class JsonAttribute: Attribute
    {
        public string max;
        public string min;
        public string type;
           public JsonAttribute(string type) {
            this.type=type;
        }
    }
    public class DomainEventSchema: Attribute {

        public string name;

        public DomainEventSchema(string name) {
            this.name=name;
        }
    }
}