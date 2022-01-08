using System;

namespace SmartFamily.Contracts.Services
{
    public interface IApplicationInfoService
    {
        Version GetVersion();
    }
}