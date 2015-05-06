using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public interface IAreaTimeService
    {
        bool AddAreaTime(AreaTime areaTime);
        bool UpdateAreaTime(AreaTime areaTime);
        bool DeleteAreaTime(int areaTimeId);
        List<AreaTime> GetAllAreaTimes();
        AreaTime GetSingleAreaTime(int areaTimeId);
    }
}
