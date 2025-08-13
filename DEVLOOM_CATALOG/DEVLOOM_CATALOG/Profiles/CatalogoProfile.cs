using AutoMapper;
using DEVLOOM_CATALOG.Domain;
using DEVLOOM_CATALOG.Application.DTOs;

namespace DEVLOOM_CATALOG.Application.Mappings
{
    public class CatalogoProfile : Profile
    {
        public CatalogoProfile()
        {
            // Categoria
            CreateMap<CategoriaRequestDto, Categoria>();
            CreateMap<Categoria, CategoriaResponseDto>();

            // Produto
            CreateMap<ProdutoRequestDto, Produto>();
            CreateMap<Produto, ProdutoResponseDto>();
        }
    }
}
