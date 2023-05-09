using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.Shopping.Core.DTOs;
using UESAN.Shopping.Core.Entities;
using UESAN.Shopping.Core.Interfaces;

namespace UESAN.Shopping.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var products = await _productRepository.GetAll();
            var productsDTO = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Description = p.Description,
                Discount = p.Discount,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Stock   = p.Stock,
                CategoryId = p.CategoryId,
            });
            return productsDTO;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return null;

            var productDTO = new ProductDTO()
            {
                Id = product.Id,
                Description = product.Description,
                Discount = product.Discount,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                IsActive = product.IsActive
            };
            return productDTO;
        }

        public async Task<bool> Insert(ProductInsertDTO productInsertDTO)
        {
            var product = new Product();
            product.Description = productInsertDTO.Description;
            product.IsActive = productInsertDTO.IsActive;

            var result = await _productRepository.Insert(product);
            return result;
        }

        public async Task<bool> Update(ProductUpdateDTO productUpdateDTO)
        {
            var product = await _productRepository.GetById(productUpdateDTO.Id);
            if (product == null)
                return false;
            product.Description = productUpdateDTO.Description;

            var result = await _productRepository.Update(product);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return false;

            var result = await _productRepository.Delete(id);
            return result;
        }

    }
}
