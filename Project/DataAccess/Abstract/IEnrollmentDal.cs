﻿using Project.Core.DataAccess;
using Project.Models;

namespace Project.DataAccess.Abstract
{
    public interface IEnrollmentDal : IEntityRepository<Enrollment>
    {
        List<EnrollmentWithAllDetails> GetAllWithDetails();
    }
}