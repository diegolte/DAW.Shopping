using UESAN.Shopping.Core.DTOs;

namespace UESAN.Shopping.Core.Interfaces
{
    public interface IProductService
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(int id);
        Task<bool> Insert(ProductInsertDTO productInsertDTO);
        Task<bool> Update(ProductUpdateDTO productUpdateDTO);
    }
}