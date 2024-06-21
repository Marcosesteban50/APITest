using APIPruebas.DTOs;
using APIPruebas.Models;
using APIPruebas.Utilidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPruebas.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CategoriaController : Controller
    {
        private readonly DBAPIContext dbcontext;
        private readonly IMapper mapper;

        public CategoriaController(DBAPIContext dbcontext,IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
        }




        [HttpGet]
        [Route("Lista")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductoDTO>>> Lista()
        {
            //List<Producto> lista = new List<Producto>();


            try
            {

                var categorias = await dbcontext.Categoria.Include(x => x.Productos).ToListAsync();
                //lista = await dbcontext.Productos.Include(o => o.oCategoria).ToListAsync();

                return Ok(categorias);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpGet]
        [Route("Obtener/{idCategoria:int}")]
        public async Task<IActionResult> Obtener(int idCategoria)
        {
            var oCategoria = await dbcontext.Categoria.FindAsync(idCategoria);

            if (oCategoria == null)
            {
                return BadRequest("categoria no encontrado");

            }



            try
            {
               

                return Ok(oCategoria);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost]
        [Route("NuevaCategoria")]
        public async Task<IActionResult> Nuevo([FromBody]CategoriaCreacionDTO CategoriaCreacionDTO)
        {


            try
            {



                var categoria = mapper.Map<Categoria>(CategoriaCreacionDTO);




                await dbcontext.Categoria.AddAsync(categoria);
                await dbcontext.SaveChangesAsync();


                return Ok();


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


        [HttpPut]
        [Route("Editar/{idCategoria:int}")]
        public async Task<IActionResult> Editar([FromBody]CategoriaCreacionDTO CategoriaDTO, int idCategoria)
        {

            var oCategoria = await dbcontext.Categoria.AnyAsync(x => x.IdCategoria == idCategoria);

            if (!oCategoria)
            {
                return NotFound("Categoria no encontrada");

            }


            

                var categoria = mapper.Map<Categoria>(CategoriaDTO);

            categoria.IdCategoria = idCategoria;


            try
            {



                dbcontext.Categoria.Update(categoria);
                await dbcontext.SaveChangesAsync();

                return Ok();



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


        [HttpDelete]
        [Route("Eliminar/{idCategoria:int}")]
        public async Task<IActionResult> Eliminar(int idCategoria)
        {

            var oCategoria = await dbcontext.Categoria.AnyAsync(x => x.IdCategoria == idCategoria);

            if (!oCategoria)
            {
                return NotFound("Categoria no encontrada");

            }

            var categoriaConvertida = await dbcontext.Categoria.FindAsync(idCategoria);

            if (categoriaConvertida == null)
            {
                return BadRequest("Categoria no encontrada");
            }

            try
            {



                dbcontext.Categoria.Remove(categoriaConvertida);
                await dbcontext.SaveChangesAsync();

                return Ok();



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

    }
}
