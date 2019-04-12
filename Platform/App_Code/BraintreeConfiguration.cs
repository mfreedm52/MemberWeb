using Braintree;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Platform.App_Code
{
    public class BraintreeConfiguration 
    {
        public string Environment { get; set; }
        public string MerchantId { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        private IBraintreeGateway BraintreeGateway { get; set; }

        public IBraintreeGateway CreateGateway()
        {
            Environment =  Braintree.Environment.SANDBOX.EnvironmentName;
            MerchantId = "wvkz93y8s5pxpcmw"; //System.Environment.GetEnvironmentVariable("BraintreeMerchantId");
            PublicKey = "54mysvrd9xy87hbp";  //System.Environment.GetEnvironmentVariable("BraintreePublicKey");
            PrivateKey = "6e78c30d998447b1ccdca8a9a09f531a"; //System.Environment.GetEnvironmentVariable("BraintreePrivateKey");

            if (MerchantId == null || PublicKey == null || PrivateKey == null)
            {
                Environment = GetConfigurationSetting("BraintreeEnvironment");
                MerchantId = GetConfigurationSetting("BraintreeMerchantId");
                PublicKey = GetConfigurationSetting("BraintreePublicKey");
                PrivateKey = GetConfigurationSetting("BraintreePrivateKey");
            }

            return new BraintreeGateway(Environment, MerchantId, PublicKey, PrivateKey);
        }

        public string GetConfigurationSetting(string setting)
        {
            return ConfigurationManager.AppSettings[setting];
        }

        public IBraintreeGateway GetGateway()
        {
            if (BraintreeGateway == null)
            {
                BraintreeGateway = CreateGateway();
            }

            return BraintreeGateway;
        }
    }
}