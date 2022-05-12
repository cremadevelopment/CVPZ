using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVPZ.Application.Job;

public class JobCountService
{
    private int _jobCount;

    public JobCountService()
    {
        _jobCount = 0;
    }

    public int JobCount { get { return _jobCount; } }

    public void AddJob()
    {
        _jobCount = _jobCount + 1;
    }

    public void RemoveJob()
    {
        _jobCount = _jobCount - 1;
    }
}
