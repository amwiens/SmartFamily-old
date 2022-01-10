using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.Contracts.Services
{
    public interface IApplicationSettingsService
    {
        void SetSetting(string key, object value);

        T GetSetting<T>(string key);
    }
}