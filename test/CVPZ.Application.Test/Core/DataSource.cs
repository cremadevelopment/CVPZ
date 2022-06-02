using CVPZ.Infrastructure.Data;
using System;
using TestSupport.EfHelpers;

namespace CVPZ.Application.Test.Core;

public class DataSource : IDisposable
{
    public CVPZContext Context { get; init; }

    public DataSource()
    {
        var options = SqliteInMemory.CreateOptions<CVPZContext>();
        Context = new CVPZContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
    }
}
