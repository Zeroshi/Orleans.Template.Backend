﻿using System;
using SimpleLoggingClient.LoggingInterfaces;
using static SimpleLoggingClient.LoggingInterfaces.Enums.Enums;

namespace SimpleLoggingClient.LoggingEntities
{
    public class ApplicationEntity : IApplicationEntity
    {
        public string ApplicationMessage { get; set; }
        public string CurrentMethod { get; set; }
        public bool WrittenToPlatform { get; set; }
        public bool OnlyInnerException { get; set; }
        public string Note { get; set; }
        public LogLevel LogLevel { get; set; }
        public Exception Error { get; set; }
    }
}