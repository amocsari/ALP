using DAL.Entity;
using DAL.Context;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using Model.Model;
using Microsoft.Extensions.Logging;

namespace DAL.Service
{
    public class SectionService : ISectionService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<SectionService> _logger;

        public SectionService(IAlpContext context, ILogger<SectionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AlpApiResponse> DeleteSectionById(int sectionId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(DeleteSectionById),
                    sectionId
                }.ToString());

                var entity = await _context.Section.FirstOrDefaultAsync(section => section.SectionId == sectionId);
                _context.Section.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<SectionDto>>> GetAllSections()
        {
            var response = new AlpApiResponse<List<SectionDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAllSections)
                }.ToString());

                var sections = await _context.Section.AsNoTracking()
                    .Include(section => section.Floor)
                    .Include(section => section.Department)
                    .ToListAsync();
                response.Value = sections.Select(Section => Section.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<SectionDto>>> GetAvailableSections()
        {
            var response = new AlpApiResponse<List<SectionDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAvailableSections)
                }.ToString());

                var sections = await _context.Section.AsNoTracking()
                    .Include(section => section.Floor)
                    .Include(section => section.Department)
                    .Where(section => !section.Locked).ToListAsync();
                response.Value = sections.Select(section => section.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<SectionDto>> GetSectionById(int sectionId)
        {
            var response = new AlpApiResponse<SectionDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetSectionById),
                    sectionId
                }.ToString());

                var entity = await _context.Section.FirstOrDefaultAsync(section => section.SectionId == sectionId);
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<SectionDto>> InsertNewSection(SectionDto dto)
        {
            var response = new AlpApiResponse<SectionDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(InsertNewSection),
                    dto = dto.ToString()
                }.ToString());

                dto.Validate();

                var entity = dto.DtoToEntity();
                entity.Floor = null;
                entity.Department = null;
                await _context.Section.AddAsync(entity);
                await _context.SaveChangesAsync();
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse> UpdateSection(SectionDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(UpdateSection),
                    dto = dto.ToString()
                }.ToString());

                dto.Validate();

                var updatedEntity = await _context.Section
                    .Include(section => section.Floor)
                    .Include(section => section.Department)
                    .FirstOrDefaultAsync(section => section.SectionId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse> ToggleSectionLockStateById(int sectionId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ToggleSectionLockStateById),
                    sectionId
                }.ToString());

                var getByIdReply = await GetSectionById(sectionId);
                if (!getByIdReply.Success)
                {
                    response.Success = getByIdReply.Success;
                    response.Message = getByIdReply.Message;
                    return response;
                }

                var section = getByIdReply.Value;

                section.Locked = !section.Locked;
                var updateReply = await UpdateSection(section);
                if (!updateReply.Success)
                {
                    response.Success = updateReply.Success;
                    response.Message = updateReply.Message;
                    return response;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
