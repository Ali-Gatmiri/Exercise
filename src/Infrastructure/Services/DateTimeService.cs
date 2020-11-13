using Tecsys.Exercise.Application.Common.Interfaces;
using System;

namespace Tecsys.Exercise.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
