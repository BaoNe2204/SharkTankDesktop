using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SharkTank.Core.Models;

namespace SharkTank.DAL.Sql
{
    public class SqlCompanyRepository : ICompanyRepository
    {
        private readonly SqlConnection _conn;

        public SqlCompanyRepository(SqlConnection connection)
        {
            _conn = connection;
        }

        public Company GetCurrent()
        {
            var company = new Company();
            try
            {
                using (var cmd = new SqlCommand("SELECT TOP 1 * FROM Companies WHERE IsActive = 1", _conn))
                {
                    if (_conn.State != ConnectionState.Open) _conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            company = MapCompany(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetCurrentCompany Error: {ex.Message}");
            }
            return company;
        }

        public void Save(Company company)
        {
            try
            {
                var sql = @"
                    IF EXISTS (SELECT 1 FROM Companies WHERE Id = @Id)
                    BEGIN
                        UPDATE Companies SET
                            CompanyName = @CompanyName,
                            Address = @Address,
                            TaxCode = @TaxCode,
                            Phone = @Phone,
                            Email = @Email,
                            Website = @Website,
                            BusinessLicense = @BusinessLicense,
                            LogoPath = @LogoPath,
                            RepresentativeName = @RepresentativeName,
                            RepresentativePosition = @RepresentativePosition,
                            Hotline = @Hotline,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id
                    END
                    ELSE
                    BEGIN
                        INSERT INTO Companies (CompanyName, Address, TaxCode, Phone, Email, Website, 
                            BusinessLicense, LogoPath, RepresentativeName, RepresentativePosition, Hotline)
                        VALUES (@CompanyName, @Address, @TaxCode, @Phone, @Email, @Website,
                            @BusinessLicense, @LogoPath, @RepresentativeName, @RepresentativePosition, @Hotline)
                    END";

                using (var cmd = new SqlCommand(sql, _conn))
                {
                    if (_conn.State != ConnectionState.Open) _conn.Open();
                    cmd.Parameters.AddWithValue("@Id", company.Id > 0 ? company.Id : 0);
                    cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName ?? "");
                    cmd.Parameters.AddWithValue("@Address", company.Address ?? "");
                    cmd.Parameters.AddWithValue("@TaxCode", company.TaxCode ?? "");
                    cmd.Parameters.AddWithValue("@Phone", company.Phone ?? "");
                    cmd.Parameters.AddWithValue("@Email", company.Email ?? "");
                    cmd.Parameters.AddWithValue("@Website", company.Website ?? "");
                    cmd.Parameters.AddWithValue("@BusinessLicense", company.BusinessLicense ?? "");
                    cmd.Parameters.AddWithValue("@LogoPath", company.LogoPath ?? "");
                    cmd.Parameters.AddWithValue("@RepresentativeName", company.RepresentativeName ?? "");
                    cmd.Parameters.AddWithValue("@RepresentativePosition", company.RepresentativePosition ?? "");
                    cmd.Parameters.AddWithValue("@Hotline", company.Hotline ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SaveCompany Error: {ex.Message}");
                throw;
            }
        }

        private Company MapCompany(SqlDataReader reader)
        {
            return new Company
            {
                Id = Convert.ToInt32(reader["Id"]),
                CompanyName = reader["CompanyName"]?.ToString(),
                Address = reader["Address"]?.ToString(),
                TaxCode = reader["TaxCode"]?.ToString(),
                Phone = reader["Phone"]?.ToString(),
                Email = reader["Email"]?.ToString(),
                Website = reader["Website"]?.ToString(),
                BusinessLicense = reader["BusinessLicense"]?.ToString(),
                LogoPath = reader["LogoPath"]?.ToString(),
                RepresentativeName = reader["RepresentativeName"]?.ToString(),
                RepresentativePosition = reader["RepresentativePosition"]?.ToString(),
                Hotline = reader["Hotline"]?.ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
            };
        }
    }

    public class SqlSystemConfigRepository : ISystemConfigRepository
    {
        private readonly SqlConnection _conn;

        public SqlSystemConfigRepository(SqlConnection connection)
        {
            _conn = connection;
        }

        public SystemConfig GetByKey(string key)
        {
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM SystemConfigs WHERE ConfigKey = @Key AND IsActive = 1", _conn))
                {
                    if (_conn.State != ConnectionState.Open) _conn.Open();
                    cmd.Parameters.AddWithValue("@Key", key);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapConfig(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetByKey Error: {ex.Message}");
            }
            return new SystemConfig { ConfigKey = key };
        }

        public IEnumerable<SystemConfig> GetByGroup(string group)
        {
            var configs = new List<SystemConfig>();
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM SystemConfigs WHERE ConfigGroup = @Group AND IsActive = 1", _conn))
                {
                    if (_conn.State != ConnectionState.Open) _conn.Open();
                    cmd.Parameters.AddWithValue("@Group", group);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            configs.Add(MapConfig(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetByGroup Error: {ex.Message}");
            }
            return configs;
        }

        public IEnumerable<SystemConfig> GetAll()
        {
            var configs = new List<SystemConfig>();
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM SystemConfigs WHERE IsActive = 1", _conn))
                {
                    if (_conn.State != ConnectionState.Open) _conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            configs.Add(MapConfig(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAll Error: {ex.Message}");
            }
            return configs;
        }

        public void Save(SystemConfig config)
        {
            try
            {
                var sql = @"
                    IF EXISTS (SELECT 1 FROM SystemConfigs WHERE ConfigKey = @Key)
                    BEGIN
                        UPDATE SystemConfigs SET ConfigValue = @Value, UpdatedAt = GETDATE() WHERE ConfigKey = @Key
                    END
                    ELSE
                    BEGIN
                        INSERT INTO SystemConfigs (ConfigKey, ConfigValue, ConfigGroup, DataType) VALUES (@Key, @Value, @Group, @DataType)
                    END";

                using (var cmd = new SqlCommand(sql, _conn))
                {
                    if (_conn.State != ConnectionState.Open) _conn.Open();
                    cmd.Parameters.AddWithValue("@Key", config.ConfigKey);
                    cmd.Parameters.AddWithValue("@Value", config.ConfigValue ?? "");
                    cmd.Parameters.AddWithValue("@Group", config.ConfigGroup ?? "");
                    cmd.Parameters.AddWithValue("@DataType", config.DataType ?? "string");
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Save Config Error: {ex.Message}");
                throw;
            }
        }

        public void SaveBatch(IEnumerable<SystemConfig> configs)
        {
            foreach (var config in configs)
            {
                Save(config);
            }
        }

        private SystemConfig MapConfig(SqlDataReader reader)
        {
            return new SystemConfig
            {
                Id = Convert.ToInt32(reader["Id"]),
                ConfigKey = reader["ConfigKey"]?.ToString(),
                ConfigValue = reader["ConfigValue"]?.ToString(),
                ConfigGroup = reader["ConfigGroup"]?.ToString(),
                Description = reader["Description"]?.ToString(),
                DataType = reader["DataType"]?.ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
            };
        }
    }
}
