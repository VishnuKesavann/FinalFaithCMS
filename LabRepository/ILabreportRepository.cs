﻿using FinalCMS.LabViewModel;
using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.LabRepository
{
    public interface ILabreportRepository
    {
        Task<List<LabReportVM>> GetViewModelReport();
        Task<int> AddReport(LabReportGeneration report);
        Task<GetIDVM> GetIDViewModel();
    }
}