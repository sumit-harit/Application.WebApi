using Application.WebApi.Application.Exceptions;
using Application.WebApi.Application.Interfaces.Repositories;
using Application.WebApi.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.WebApi.Application.Features.Products.Commands.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Response<int>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            public DeleteProductByIdCommandHandler(IProductRepositoryAsync productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<Response<int>> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.Get(command.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                await _productRepository.DeleteAsync(product);
                return new Response<int>(product.Id);
            }
        }
    }
}
