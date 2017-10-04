using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class Datastream
    {
        public string Id
        {
            get;
            set;
        }

        public string SourceId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Tenant
        {
            get;
            set;
        }

        public long CreateTime
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public long UpdateTime
        {
            get;
            set;
        }

        public string UpdatedBy
        {
            get;
            set;
        }
        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
        public Stats Stats
        {
            get;
            set;
        }

        public Field Field
        {
            get;
            set;
        }

        public Datasource DataSource
        {
            get;
            set;
        }

        public List<Input> InputList
        {
            get;
            set;
        }


        public string Live
        {
            get;
            set;
        }

        public bool Streaming
        {
            get;
            set;
        }
        public string BaseTimeProperty
        {
            get;
            set;
        }

    }
}
