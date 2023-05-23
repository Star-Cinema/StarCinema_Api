using StarCinema_Api.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.DTOs
{
    public class RequestDTO
    {
        [DefaultValue(0)]
        public int PageIndex { get; set; } = 0;

        [DefaultValue(10)] 
        [Range(1, 100)] 
        public int PageSize { get; set; } = 10; 

        [DefaultValue("Name")]  
        [SortColumnValidator(typeof(StarCinema_Api.Data.Entities.Services))] 
        public string? SortColumn { get; set; } = "Name";

        [DefaultValue("ASC")]
        [RegularExpression("ASC|DESC")]
        public string? SortOrder { get; set; } = "ASC";

        [DefaultValue(null)] 
        public string? FilterQuery { get; set; } = null; 
    }
}
