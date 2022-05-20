﻿namespace SmartFamily.WinUI.Serialization;

internal interface ISettingsSerializer
{
    bool CreateFile(string path);

    string? ReadFromFile();

    bool WriteToFile(string? text);
}