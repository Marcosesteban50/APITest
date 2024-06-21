using APIPruebas.DTOs;
using APIPruebas.Models;
using AutoMapper;

namespace APIPruebas.Utilidades
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<ProductoCreacionDTO, Producto>();


            CreateMap<CategoriaCreacionDTO, Categoria>();


        }
    }
}
