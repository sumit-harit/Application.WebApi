using Application.WebApi.Application.Exceptions;
using Application.WebApi.Application.Interfaces.Repositories;
using Application.WebApi.Application.Wrappers;
using Application.WebApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.WebApi.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Response<Product>>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<Product>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            public GetProductByIdQueryHandler(IProductRepositoryAsync productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<Response<Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _productRepository.Get(query.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                return new Response<Product>(product);
            }
        }
    }
}
