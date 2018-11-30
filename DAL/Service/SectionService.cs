using DAL.Entity;
using DAL.Context;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Service
{
    public class SectionService : ISectionService
    {
        private IAlpContext _context;

        public SectionService(IAlpContext context)
        {
            _context = context;
        }

        public async Task DeleteSectionById(int sectionId)
        {
            try
            {
                var entity = await _context.Section.FirstOrDefaultAsync(section => section.SectionId == sectionId);
                _context.Section.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }

        public async Task<List<SectionDto>> GetAllSections()
        {
            try
            {
                var sections = await _context.Section.AsNoTracking()
                    .Include(section => section.Floor)
                    .Include(section => section.Department)
                    .ToListAsync();
                return sections.Select(Section => Section.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<List<SectionDto>> GetAvailableSections()
        {
            try
            {
                var sections = await _context.Section.AsNoTracking()
                    .Include(section => section.Floor)
                    .Include(section => section.Department)
                    .Where(section => !section.Locked).ToListAsync();
                return sections.Select(section => section.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<SectionDto> GetSectionById(int sectionId)
        {
            try
            {
                var entity = await _context.Section.FirstOrDefaultAsync(section => section.SectionId == sectionId);
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<SectionDto> InsertNewSection(SectionDto section)
        {
            try
            {
                var entity = section.DtoToEntity();
                entity.Floor = null;
                entity.Department = null;
                await _context.Section.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<SectionDto> UpdateSection(SectionDto dto)
        {
            try
            {
                var updatedEntity = await _context.Section
                    .Include(section => section.Floor)
                    .Include(section => section.Department)
                    .FirstOrDefaultAsync(section => section.SectionId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
                return updatedEntity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task ToggleSectionLockStateById(int sectionId)
        {
            try
            {
                var section = await GetSectionById(sectionId);
                section.Locked = !section.Locked;
                await UpdateSection(section);
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }
    }
}
