﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using Glimpse.Protocol;

namespace Glimpse.Net.Plugin.Configuration
{
    [GlimpsePlugin]
    public class Config : IGlimpsePlugin
    {
        public string Name
        {
            get { return "Config"; }
        }

        public object GetData(HttpApplication application)
        {
            var ConnectionStrings = new Dictionary<string, string>();
            foreach (ConnectionStringSettings item in ConfigurationManager.ConnectionStrings)
            {
                ConnectionStrings.Add(item.Name, item.ConnectionString);
            }

            if (ConnectionStrings.Count == 0) return null;

            //TODO, add in other useful config sections like compilation, 
            var customErrorsSection = ConfigurationManager.GetSection("system.web/customErrors") as CustomErrorsSection;
            var authenticationSection = ConfigurationManager.GetSection("system.web/authentication") as AuthenticationSection;

            return new
                       {
                           AppSettings = ConfigurationManager.AppSettings.Flatten(),
                           ConnectionStrings,
                           CustomErrors = customErrorsSection,
                           Authentication = authenticationSection
                       };
        }

        public void SetupInit(HttpApplication application)
        {
            throw new NotImplementedException();
        }
    }
}