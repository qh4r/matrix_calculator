using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MatrixesDb.Migrations.Types;

namespace MatrixApi.Controllers
{
    public class MatrixStorageController : ApiController
    {
        public List<MatrixMeta> Get()
        {
            return new List<MatrixMeta>() ;
        }
    }
}