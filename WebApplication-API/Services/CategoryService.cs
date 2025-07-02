using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication_API.Data;
using WebApplication_API.DTOs;
using WebApplication_API.Interfaces;

namespace WebApplication_API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category == null ? null : _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> UpdateAsync(CategoryDTO categoryDto)
        {
            var existing = await _context.Categories.FindAsync(categoryDto.Id);
            if (existing == null) return false;

            // تحديث الحقول المطلوبة
            existing.Name = categoryDto.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return false;

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
