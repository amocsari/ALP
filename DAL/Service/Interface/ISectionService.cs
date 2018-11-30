using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface ISectionService
    {
        Task<List<SectionDto>> GetAllSections();
        Task<List<SectionDto>> GetAvailableSections();
        Task<SectionDto> GetSectionById(int sectionId);
        Task<SectionDto> InsertNewSection(SectionDto section);
        Task DeleteSectionById(int sectionId);
        Task ToggleSectionLockStateById(int sectionId);
        Task<SectionDto> UpdateSection(SectionDto dto);
    }
}
