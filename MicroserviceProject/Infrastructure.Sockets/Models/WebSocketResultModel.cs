﻿using Infrastructure.Security.Model;

namespace Infrastructure.Sockets.Models
{
    /// <summary>
    /// Websocketten gelen veri
    /// </summary>
    public class WebSocketResultModel
    {
        /// <summary>
        /// Gelen verinin dağıtıcı sahibi kullanıcı
        /// </summary>
        public AuthenticatedUser Sender { get; set; }

        /// <summary>
        /// Gelen verinin içeriği
        /// </summary>
        public WebSocketContentModel Content { get; set; }
    }
}
