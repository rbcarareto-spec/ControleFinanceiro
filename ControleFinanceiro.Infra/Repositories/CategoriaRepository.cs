using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Aplicacao.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Infra.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(CategoriaDto categoriaDto)
        {
            var categoria = new Categoria
            {
                Nome = categoriaDto.Nome,
                Tipo = categoriaDto.Tipo,
                Ativo = categoriaDto.Ativo
            };
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(CategoriaDto categoriaDto)
        {
            var categoria = new Categoria
            {
                Id = categoriaDto.Id,
                Nome = categoriaDto.Nome,
                Tipo = categoriaDto.Tipo,
                Ativo = categoriaDto.Ativo
            };
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(int id)
            => await _context.Categorias.AnyAsync(c => c.Id == id);

        public async Task<CategoriaDto?> ObterPorIdAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return null;

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Tipo = categoria.Tipo,
                Ativo = categoria.Ativo
            };
        }


        public async Task<IEnumerable<CategoriaDto>> ObterTodasAsync()
        {
            return await _context.Categorias
                .Select(c => new CategoriaDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Tipo = c.Tipo,
                    Ativo = c.Ativo
                })
                .ToListAsync();
        }
    }
}
