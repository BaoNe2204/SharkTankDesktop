using System;

namespace SharkTank.Core.Models
{
    public class SystemConfig
    {
        public int Id { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string ConfigGroup { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; } = "string";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string GetStringValue() => ConfigValue ?? string.Empty;
        public int GetIntValue() => int.TryParse(ConfigValue, out int result) ? result : 0;
        public bool GetBoolValue() => ConfigValue == "1" || ConfigValue?.ToLower() == "true";
        public DateTime GetDateTimeValue() => DateTime.TryParse(ConfigValue, out DateTime result) ? result : DateTime.MinValue;

        public void SetValue(string value) => ConfigValue = value;
        public void SetValue(int value) => ConfigValue = value.ToString();
        public void SetValue(bool value) => ConfigValue = value ? "1" : "0";
    }
}
