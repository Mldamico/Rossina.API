using Rossina.Modules.Catalog.Application.Messaging;

namespace Rossina.Modules.Catalog.Application.Catalog.Brands.DeleteBrand;

public sealed record DeleteBrandCommand(Guid Id) : ICommand<Guid>;