﻿
using Microsoft.Extensions.Configuration;

namespace Services.Communication.Mq.Rabbit.Queue.Buying.Configuration.Mock
{
    public class CreateInventoryRequestRabbitConfigurationProvider
    {
        private static CreateInventoryRequestRabbitConfiguration publisher;

        public static CreateInventoryRequestRabbitConfiguration GetCreateInventoryRequestPublisher(IConfiguration configuration)
        {
            if (publisher == null)
            {
                publisher = new CreateInventoryRequestRabbitConfiguration(configuration);
            }

            return publisher;
        }
    }
}