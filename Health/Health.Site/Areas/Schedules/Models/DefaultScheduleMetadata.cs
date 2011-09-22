using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Site.Models.Metadata;

namespace Health.Site.Areas.Schedules.Models
{
    public class DefaultScheduleAddMetadata : ParameterMetadata
    {
        [DisplayName("Идентификатор параметра.")]
        [Required(ErrorMessage = "Нужно указать номер параметра.")]
        public new int Id { get; set; }
    }
}