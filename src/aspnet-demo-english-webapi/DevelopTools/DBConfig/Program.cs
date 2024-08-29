using CommonInitializer;
using FileService.Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;
using DBConfig.Options;

namespace DBConfig;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // 配置 T_Configs
        var contextFactory = new DbConfigContextFactory();
        await using var ctx = contextFactory.CreateDbContext(args);

        ctx.DbConfigEntities.Add(CreateCorsOptionsEntity());
        ctx.DbConfigEntities.Add(CreateFileServiceSMBEntity());
        ctx.DbConfigEntities.Add(CreateFileServiceEndpointEntity());
        ctx.DbConfigEntities.Add(CreateRedisOptions());
        ctx.DbConfigEntities.Add(CreateRabbitMQ());
        ctx.DbConfigEntities.Add(CreateElasticSearchOptions());
        ctx.DbConfigEntities.Add(CreateJWTOption());

        await ctx.SaveChangesAsync();

        Console.WriteLine("Hello, World!");
    }

    private static DBConfigEntity CreateCorsOptionsEntity()
    {
        // 准备数据
        var entity = new DBConfigEntity();
        entity.Name = "Cors";
        entity.Value = JsonSerializer.Serialize(DBConfig.Options.CorsOptions.InitCorsOptions());
        return entity;
    }

    private static DBConfigEntity CreateFileServiceSMBEntity()
    {
        var entity = new DBConfigEntity();
        entity.Name = "FileService:SMB";
        entity.Value = JsonSerializer.Serialize(SMBStorageOptions.Init());
        return entity;
    }

    private static DBConfigEntity CreateFileServiceEndpointEntity()
    {
        var entity = new DBConfigEntity();
        entity.Name = "FileService:Endpoint";
        entity.Value = JsonSerializer.Serialize(FsEndPointOptions.Init());
        return entity;
    }

    private static DBConfigEntity CreateRedisOptions()
    {
        var entity = new DBConfigEntity
        {
            Name = "Redis",
            Value = JsonSerializer.Serialize(RedisOptions.Init())
        };
        return entity;
    }

    private static DBConfigEntity CreateRabbitMQ()
    {
        var entity = new DBConfigEntity
        {
            Name = "RabbitMQ",
            Value = JsonSerializer.Serialize(RabbitMQOptions.Init())
        };
        return entity;
    }

    private static DBConfigEntity CreateElasticSearchOptions()
    {
        var entity = new DBConfigEntity
        {
            Name = "ElasticSearch",
            Value = JsonSerializer.Serialize(ElasticSearchOptions.Init())
        };
        return entity;
    }

    private static DBConfigEntity CreateJWTOption()
    {
        var entity = new DBConfigEntity
        {
            Name = "JWT",
            Value = JsonSerializer.Serialize(JWTOptions.Init())
        };
        return entity;
    }
}
