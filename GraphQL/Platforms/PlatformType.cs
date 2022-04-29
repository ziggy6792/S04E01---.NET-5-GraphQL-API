using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL.Platforms
{
  public class PlatformType : ObjectType<Platform>
  {
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
      descriptor.Description("Represents any software or service that has a command line interface.");

      descriptor
          .Field(p => p.Id)
          .Description("Represents the unique ID for the platform.");

      descriptor
          .Field(p => p.Name)
          .Description("Represents the name for the platform.");

      descriptor
          .Field(p => p.LicenseKey).Ignore();

      descriptor
          .Field(p => p.Commands)
          .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
          .UseDbContext<AppDbContext>()
          .Description("This is the list of available commands for this platform.");
    }

    private class Resolvers
    {
      public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext context)
      {
        // var ret = context.Commands.Where(p => p.PlatformId == platform.Id);

        var ret = platform.Commands.Where(p => p.PlatformId == 1).AsQueryable();
        return ret;
        // return Enumerable.Empty<Command>().AsQueryable();
        // return new List<Command>();
      }
    }
  }
}