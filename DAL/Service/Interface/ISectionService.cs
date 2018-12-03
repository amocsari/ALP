using Common.Model.Dto;
using Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface ISectionService
    {
        Task<AlpApiResponse<List<SectionDto>>> GetAllSections();
        Task<AlpApiResponse<List<SectionDto>>> GetAvailableSections();
        Task<AlpApiResponse<SectionDto>> GetSectionById(int sectionId);
        Task<AlpApiResponse<SectionDto>> InsertNewSection(SectionDto section);
        Task<AlpApiResponse> DeleteSectionById(int sectionId);
        Task<AlpApiResponse> ToggleSectionLockStateById(int sectionId);
        Task<AlpApiResponse> UpdateSection(SectionDto dto);
    }
}
