﻿namespace SmartFamily.Backend.Services.Settings;

public interface IApplicationSettingsService : IBaseSettingsService
{
    DateTime UpdateLastChecked { get; set; }
}