﻿using MicroserviceProject.Test.Services.Providers.Configuration;

using Microsoft.Extensions.Configuration;

namespace MicroserviceProject.Services.Business.Departments.HR.Test.Prepreations.Infrastructure
{
    public class ConfigurationFactory
    {
        private static AppConfigurationProvider appConfigurationProvider = null;

        public static IConfiguration GetConfiguration()
        {
            if (appConfigurationProvider == null)
            {
                appConfigurationProvider = new AppConfigurationProvider();

                appConfigurationProvider.AuthorizationSection.CredentialSection["email"] = "MicroserviceProject.Services.Business.Departments.HR@service.service";
                appConfigurationProvider.AuthorizationSection.CredentialSection["password"] = "1234";

                appConfigurationProvider.AuthorizationSection.EndpointsSection["GetToken"] = "authorization.auth.gettoken";
                appConfigurationProvider.AuthorizationSection.EndpointsSection["GetUser"] = "authorization.auth.getuser";

                appConfigurationProvider.LocalizationSection["TranslationDbConnnectionString"] = "server=localhost;DataBase=MicroserviceDB;user=sa;password=Srkn_CMR*1987;MultipleActiveResultSets=true";
                appConfigurationProvider.LocalizationSection["DefaultRegion"] = "tr-TR";
                appConfigurationProvider.LocalizationSection["CacheKey"] = "localization.translations";

                appConfigurationProvider.LoggingSection.RequestResponseLoggingSection.DataBaseConfigurationSection["DataSource"] = "server=localhost;DataBase=MicroserviceDB;user=sa;password=Srkn_CMR*1987;MultipleActiveResultSets=true";
                appConfigurationProvider.LoggingSection.RequestResponseLoggingSection.FileConfigurationSection["Path"] = "D:\\Logs\\MicroserviceProject.Services.Business.Departments.AA\\";
                appConfigurationProvider.LoggingSection.RequestResponseLoggingSection.FileConfigurationSection["FileName"] = "RequestResponseLog";
                appConfigurationProvider.LoggingSection.RequestResponseLoggingSection.FileConfigurationSection["Encoding"] = "UTF-8";
                appConfigurationProvider.LoggingSection.RequestResponseLoggingSection.MongoConfigurationSection["ConnectionString"] = "mongodb://localhost:27017";
                appConfigurationProvider.LoggingSection.RequestResponseLoggingSection.MongoConfigurationSection["DataBase"] = "Logs";
                appConfigurationProvider.LoggingSection.RequestResponseLoggingSection.MongoConfigurationSection["CollectionName"] = "RequestResponseLog";

                appConfigurationProvider.RoutingSection["DataSource"] = "server=localhost;DataBase=MicroserviceDB;user=sa;password=Srkn_CMR*1987;MultipleActiveResultSets=true";

                appConfigurationProvider.RabbitQueuesSection["DefaultHost"] = "localhost";
                appConfigurationProvider.RabbitQueuesSection["DefaultUserName"] = "guest";
                appConfigurationProvider.RabbitQueuesSection["DefaultPassword"] = "guest";
                appConfigurationProvider.RabbitQueuesSection.ServicesSection.AASection.QueueNamesSection["AssignInventoryToWorker"] = "aa.queue.inventory.assignInventorytoworker";
                appConfigurationProvider.RabbitQueuesSection.ServicesSection.AASection.QueueNamesSection["InformInventoryRequest"] = "aa.queue.request.informinventoryrequest";
                appConfigurationProvider.RabbitQueuesSection.ServicesSection.ITSection.QueueNamesSection["AssignInventoryToWorker"] = "it.queue.inventory.assignInventorytoworker";
                appConfigurationProvider.RabbitQueuesSection.ServicesSection.ITSection.QueueNamesSection["InformInventoryRequest"] = "it.queue.request.informinventoryrequest";
                appConfigurationProvider.RabbitQueuesSection.ServicesSection.AccountingSection.QueueNamesSection["CreateBankAccount"] = "accounting.queue.bankaccounts.createbankaccount";
                appConfigurationProvider.RabbitQueuesSection.ServicesSection.AccountingSection.QueueNamesSection["CreateInventoryRequest"] = "buying.queue.request.createinventoryrequest";
                appConfigurationProvider.RabbitQueuesSection.ServicesSection.AccountingSection.QueueNamesSection["NotifyCostApprovement"] = "buying.queue.cost.notifycostapprovement";
                appConfigurationProvider.RabbitQueuesSection.ServicesSection.FinanceSection.QueueNamesSection["InventoryRequest"] = "finance.queue.request.inventoryrequest";

                appConfigurationProvider.ServicesSection.EndpointsSection.AASection["GetInventories"] = "aa.inventory.getinventories";
                appConfigurationProvider.ServicesSection.EndpointsSection.AASection["CreateInventory"] = "aa.inventory.createinventory";
                appConfigurationProvider.ServicesSection.EndpointsSection.AASection["AssignInventoryToWorker"] = "aa.inventory.assigninventorytoworker";
                appConfigurationProvider.ServicesSection.EndpointsSection.AASection["GetInventoriesForNewWorker"] = "aa.inventory.getinventoriesfornewworker";
                appConfigurationProvider.ServicesSection.EndpointsSection.AASection["CreateDefaultInventoryForNewWorker"] = "aa.inventory.createdefaultinventoryfornewworker";
                appConfigurationProvider.ServicesSection.EndpointsSection.AASection["RollbackTransaction"] = "aa.transaction.rollbacktransaction";
                appConfigurationProvider.ServicesSection.EndpointsSection.AASection["InformInventoryRequest"] = "aa.inventory.informinventoryrequest";

                appConfigurationProvider.ServicesSection.EndpointsSection.AccountingSection["CreateBankAccount"] = "accounting.bankaccounts.createbankaccount";
                appConfigurationProvider.ServicesSection.EndpointsSection.AccountingSection["GetBankAccountsOfWorker"] = "accounting.bankaccounts.getbankaccountsofworker";
                appConfigurationProvider.ServicesSection.EndpointsSection.AccountingSection["GetCurrencies"] = "accounting.bankaccounts.getcurrencies";
                appConfigurationProvider.ServicesSection.EndpointsSection.AccountingSection["CreateCurrency"] = "accounting.bankaccounts.createcurrency";
                appConfigurationProvider.ServicesSection.EndpointsSection.AccountingSection["RollbackTransaction"] = "accounting.transaction.rollbacktransaction";

                appConfigurationProvider.ServicesSection.EndpointsSection.BuyingSection["GetInventoryRequests"] = "buying.request.getinventoryrequests";
                appConfigurationProvider.ServicesSection.EndpointsSection.BuyingSection["CreateInventoryRequest"] = "buying.request.createinventoryrequest";
                appConfigurationProvider.ServicesSection.EndpointsSection.BuyingSection["RollbackTransaction"] = "buying.transaction.rollbacktransaction";
                appConfigurationProvider.ServicesSection.EndpointsSection.BuyingSection["ValidateCostInventory"] = "buying.request.validatecostinventory";

                appConfigurationProvider.ServicesSection.EndpointsSection.FinanceSection["GetDecidedCosts"] = "finance.cost.getdecidedcosts";
                appConfigurationProvider.ServicesSection.EndpointsSection.FinanceSection["CreateCost"] = "finance.cost.createcost";
                appConfigurationProvider.ServicesSection.EndpointsSection.FinanceSection["DecideCost"] = "finance.cost.decidecost";

                appConfigurationProvider.ServicesSection.EndpointsSection.HRSection["GetDepartments"] = "hr.department.getdepartments";
                appConfigurationProvider.ServicesSection.EndpointsSection.HRSection["CreateDepartment"] = "hr.department.createdepartment";
                appConfigurationProvider.ServicesSection.EndpointsSection.HRSection["GetPeople"] = "hr.person.getpeople";
                appConfigurationProvider.ServicesSection.EndpointsSection.HRSection["CreatePerson"] = "hr.person.createperson";
                appConfigurationProvider.ServicesSection.EndpointsSection.HRSection["GetTitles"] = "hr.person.gettitles";
                appConfigurationProvider.ServicesSection.EndpointsSection.HRSection["CreateTitle"] = "hr.person.createtitle";
                appConfigurationProvider.ServicesSection.EndpointsSection.HRSection["GetWorkers"] = "hr.person.getworkers";
                appConfigurationProvider.ServicesSection.EndpointsSection.HRSection["CreateWorker"] = "hr.person.createworker";
                appConfigurationProvider.ServicesSection.EndpointsSection.HRSection["RollbackTransaction"] = "hr.transaction.rollbacktransaction";

                appConfigurationProvider.ServicesSection.EndpointsSection.ITSection["GetInventories"] = "it.inventory.getinventories";
                appConfigurationProvider.ServicesSection.EndpointsSection.ITSection["CreateInventory"] = "it.inventory.createinventory";
                appConfigurationProvider.ServicesSection.EndpointsSection.ITSection["AssignInventoryToWorker"] = "it.inventory.assigninventorytoworker";
                appConfigurationProvider.ServicesSection.EndpointsSection.ITSection["GetInventoriesForNewWorker"] = "it.inventory.getinventoriesfornewworker";
                appConfigurationProvider.ServicesSection.EndpointsSection.ITSection["CreateDefaultInventoryForNewWorker"] = "it.inventory.createdefaultinventoryfornewworker";
                appConfigurationProvider.ServicesSection.EndpointsSection.ITSection["RollbackTransaction"] = "it.transaction.rollbacktransaction";
                appConfigurationProvider.ServicesSection.EndpointsSection.ITSection["InformInventoryRequest"] = "it.inventory.informinventoryrequest";
            }

            return appConfigurationProvider;
        }
    }
}