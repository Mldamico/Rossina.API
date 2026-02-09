using Rossina.Common.Application.Messaging;

namespace Rossina.Modules.Catalog.Application.Catalog.Brands.CreateBrand;

public sealed record CreateBrandCommand(string Name, string Logo) : ICommand<CreateBrandResponse>;
