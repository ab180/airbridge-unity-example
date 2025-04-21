using System.Collections.Generic;
using UnityEditor;

namespace AirbridgeUnityExample
{
    public class AirbridgeUserPrinter
    {
        private const string idKey = "userID";
        private const string emailKey = "userEmail";
        private const string phoneNumberKey = "userPhone";
        private const string aliasKey = "alias";
        private const string attributesKey = "attributes";

        private Dictionary<string, object> data = new Dictionary<string, object>();

        public AirbridgeUserPrinter(AirbridgeUser user)
        {
            if (user.GetId() != null)
            {
                AddData(idKey, user.GetId());
            }

            if (user.GetEmail() != null)
            {
                AddData(emailKey, user.GetEmail());
            }

            if (user.GetPhoneNumber() != null)
            {
                AddData(phoneNumberKey, user.GetPhoneNumber());
            }

            if (user.GetAlias().Count > 0)
            {
                AddData(aliasKey, user.GetAlias());
            }

            if (user.GetAttributes().Count > 0)
            {
                AddData(attributesKey, user.GetAttributes());
            }
        }

        public string ToJsonString()
        {
            return Json.Serialize(data);
        }

        private void AddData(string key, object value)
        {
            if (!data.ContainsKey(key))
            {
                data.Add(key, value);
            }
            else
            {
                data[key] = value;
            }
        }
    }
}