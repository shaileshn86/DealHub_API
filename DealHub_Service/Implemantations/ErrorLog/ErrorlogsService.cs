﻿using DealHub_Dal.ErrorLog;
using DealHub_Domain.Entity.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Service.Implemantations.ErrorLog
{
    public class ErrorService
    {
        public static string Add(Errorlogs errorlogs)
        {

            return DALErrorlogs.Add(errorlogs);

        }

        public static PaginationDto<Errorlogs> GeterrorlogsList(int PageNo, int PageSize, string Searchkey = "", string orderby = "")

        {
            return DALErrorLogs.GeterrorlogsList(PageNo, PageSize, Searchkey, orderby);
        }
    }
}
