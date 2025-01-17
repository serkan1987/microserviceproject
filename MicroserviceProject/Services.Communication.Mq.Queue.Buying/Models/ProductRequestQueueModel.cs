﻿using Services.Communication.Mq.Models;

namespace Services.Communication.Mq.Queue.Buying.Models
{
    public class ProductRequestQueueModel : BaseQueueModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int ReferenceNumber { get; set; }
    }
}
