// Requires the following Azure NuGet packages and related dependencies:
// package id="Microsoft.Azure.Management.Authorization" version="2.0.0"
// package id="Microsoft.Azure.Management.ResourceManager" version="1.4.0-preview"
// package id="Microsoft.Rest.ClientRuntime.Azure.Authentication" version="2.2.8-preview"

using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest.Azure.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;

namespace CryptdriveCloud
{
    class Initialiser
    {
        //RG: https://www.netiq.com/communities/cool-solutions/creating-application-client-id-client-secret-microsoft-azure-new-portal/
        private static string subscriptionId = "8e46bf6d-7ff6-4770-9a2f-ac3bbdee018c";

        private static string clientId = "d343092c-86e0-4b13-93f9-527b1ce84edf";
        private static string clientSecret = @"A3n7L/hjqvZBP4k1aZPBI+uj7uUanVjXkKsc7s4mcKM=";
        private static string resourceGroupName = "CryptDriveResourceGroup";
        private static string resourceGroupLocation = "West Europe"; // must be specified for creating a new resource group
        private static string tenantId = "de3d5e4c-abb4-4005-9843-c7f77668489e";
        private static string authority = String.Format(CultureInfo.InvariantCulture, clientId, tenantId);
        private static AuthenticationContext authContext = new AuthenticationContext(authority, new DirectorySearcher.FileCache());

        public static void initAll()
        {
            createCloudRessourceGroup();
            createStorageAccount();
            createDatabase();
        }

        public static void createCloudRessourceGroup()
        {
            string deploymentNameCDC = "CryptDriveCore";
            string pathToTemplateFile = "json/CDCparameters.json";
            string pathToParameterFile = "json/CDCtemplate.json";
            Run(deploymentNameCDC, GetJsonFileContents(pathToTemplateFile), GetJsonFileContents(pathToParameterFile));
        }

        public static void createStorageAccount()
        {
            string deploymentNameSTOR = "CryptDriveStorage";
            string pathToTemplateFile = "json/STORparameters.json";
            string pathToParameterFile = "json/STORtemplate.json";
            Run(deploymentNameSTOR, GetJsonFileContents(pathToTemplateFile), GetJsonFileContents(pathToParameterFile));
        }

        public static void createDatabase()
        {
            string deploymentNameDB = "CryptDriveDatabase";
            string pathToTemplateFile = "json/DBparameters.json";
            string pathToParameterFile = "json/DBtemplate.json";
            Run(deploymentNameDB, GetJsonFileContents(pathToTemplateFile), GetJsonFileContents(pathToParameterFile));
            DbManager.CreateTableIfNotExists();
        }

        /// <summary>
        /// Reads a JSON file from the specified path
        /// </summary>
        /// <param name="pathToJson">The full path to the JSON file</param>
        /// <returns>The JSON file contents</returns>
        public static JObject GetJsonFileContents(string pathToJson)
        {
            JObject templatefileContent = new JObject();
            using (StreamReader file = File.OpenText(pathToJson))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    templatefileContent = (JObject)JToken.ReadFrom(reader);
                    return templatefileContent;
                }
            }
        }

        public static async void Run(String deploymentName, JObject templateFileContents, JObject parameterFileContents)
        {
            // Try to obtain the service credentials
            //     var serviceCreds = await ApplicationTokenProvider.LoginSilentAsync(tenantId, clientId, clientSecret);
            // var authResult = await authContext.AcquireTokenAsync( , clientId, redirectUri, PromptBehavior.Auto);

            //public Task<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, Uri redirectUri, IPlatformParameters parameters);

            // Read the template and parameter file contents
            // JObject templateFileContents = GetJsonFileContents(pathToTemplateFile);
            //JObject parameterFileContents = GetJsonFileContents(pathToParameterFile);

            // Create the resource manager client
            //  var resourceManagementClient = new ResourceManagementClient(serviceCreds);
            //  resourceManagementClient.SubscriptionId = subscriptionId;

            // Create or check that resource group exists
            //  EnsureResourceGroupExists(resourceManagementClient, resourceGroupName, resourceGroupLocation);

            // Start a deployment
            // DeployTemplate(resourceManagementClient, resourceGroupName, deploymentName, templateFileContents, parameterFileContents);
        }

        /// <summary>
        /// Ensures that a resource group with the specified name exists. If it does not, will attempt to create one.
        /// </summary>
        /// <param name="resourceManagementClient">The resource manager client.</param>
        /// <param name="resourceGroupName">The name of the resource group.</param>
        /// <param name="resourceGroupLocation">The resource group location. Required when creating a new resource group.</param>
        public static void EnsureResourceGroupExists(ResourceManagementClient resourceManagementClient, string resourceGroupName, string resourceGroupLocation)
        {
            if (resourceManagementClient.ResourceGroups.CheckExistence(resourceGroupName) != true)
            {
                Console.WriteLine(string.Format("Creating resource group '{0}' in location '{1}'", resourceGroupName, resourceGroupLocation));
                var resourceGroup = new ResourceGroup();
                resourceGroup.Location = resourceGroupLocation;
                resourceManagementClient.ResourceGroups.CreateOrUpdate(resourceGroupName, resourceGroup);
            }
            else
            {
                Console.WriteLine(string.Format("Using existing resource group '{0}'", resourceGroupName));
            }
        }

        /// <summary>
        /// Starts a template deployment.
        /// </summary>
        /// <param name="resourceManagementClient">The resource manager client.</param>
        /// <param name="resourceGroupName">The name of the resource group.</param>
        /// <param name="deploymentName">The name of the deployment.</param>
        /// <param name="templateFileContents">The template file contents.</param>
        /// <param name="parameterFileContents">The parameter file contents.</param>
        public static void DeployTemplate(ResourceManagementClient resourceManagementClient, string resourceGroupName, string deploymentName, JObject templateFileContents, JObject parameterFileContents)
        {
            Console.WriteLine(string.Format("Starting template deployment '{0}' in resource group '{1}'", deploymentName, resourceGroupName));
            var deployment = new Deployment();

            deployment.Properties = new DeploymentProperties
            {
                Mode = DeploymentMode.Incremental,
                Template = templateFileContents,
                Parameters = parameterFileContents["parameters"].ToObject<JObject>()
            };

            var deploymentResult = resourceManagementClient.Deployments.CreateOrUpdate(resourceGroupName, deploymentName, deployment);
            Console.WriteLine(string.Format("Deployment status: {0}", deploymentResult.Properties.ProvisioningState));
        }
    }
}
