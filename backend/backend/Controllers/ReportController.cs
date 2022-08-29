using backend.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private IReportRepository _reportRepository;

        public ReportController(ILogger<ReportController> logger, IReportRepository reportRepository)
        {
            _logger = logger;
            _reportRepository = reportRepository;
        }

        /// <summary>
        /// Get a complete report in .xlsx 
        /// </summary>
        [HttpGet("generate-report")]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        public IActionResult GetReport()
        {
            var stream = _reportRepository.GetXLSXReport();

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }
    }
}
