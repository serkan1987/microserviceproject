﻿using Infrastructure.Security.Model;

namespace Infrastructure.Communication.Model.Basics
{
    /// <summary>
    /// Websocketten gelen veri
    /// </summary>
    public class WebSocketResultModel
    {
        /// <summary>
        /// Gelen verinin dağıtıcı sahibi kullanıcı
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        /// Gelen verinin içeriği
        /// </summary>
        public WebSocketContentModel Content { get; set; }
    }
}