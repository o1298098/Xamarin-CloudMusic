using Aliyun.OTS;
using Aliyun.OTS.DataModel;
using Aliyun.OTS.Request;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Actions
{
    public class AliyunOTS
    {
        static string AccessKeyId = "LTAIfD6izQItuQ5A";

        static string AccessKeySecret = "l0GdwcDYdJwB1VJ5pv0ormyTV9nhvW ";

        static string Endpoint = "https://o1298098.cn-hangzhou.ots.aliyuncs.com";

        static string InstanceName = "o1298098";

        private static OTSClient OtsClient = null;
        public static OTSClient GetClient()
        {
            if (OtsClient != null)
            {
                return OtsClient;
            }

            OTSClientConfig config = new OTSClientConfig(Endpoint, AccessKeyId, AccessKeySecret, InstanceName)
            {
                OTSDebugLogHandler = null,
                OTSErrorLogHandler = null
            };
            OtsClient = new OTSClient(config);
            return OtsClient;
        }
        public static Dictionary<string, string> GetAppKey(string keyname)
        {
            GetClient();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            PrimaryKey primaryKey = new PrimaryKey();
            primaryKey.Add("KeyName", new ColumnValue(keyname));
            try
            {
                var request = new GetRowRequest("Appkey", primaryKey);
                var response = OtsClient.GetRow(request);
                var columns = response.Columns;
                foreach (var q in columns)
                {
                    keyValues[q.Name] = PrintColumnValue(q.Value);
                }
                return keyValues;                
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return null;
            }
        }
        public static string PrintColumnValue(ColumnValue value)
        {
            switch (value.Type)
            {
                case ColumnValueType.String: return value.StringValue;
                case ColumnValueType.Integer: return value.IntegerValue.ToString();
                case ColumnValueType.Boolean: return value.BooleanValue.ToString();
                case ColumnValueType.Double: return value.DoubleValue.ToString();
                case ColumnValueType.Binary: return value.BinaryValue.ToString();
            }

            throw new Exception("Unknow type.");
        }
    }
}
