using System.Web.Mvc;
using IBA_Common;

namespace IBAMembersApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private IbaCmsDbContext _db;

        public IbaCmsDbContext Db
        {
            get
            {
                _db = _db ?? new IbaCmsDbContext();
                return _db;
            }
        }

        private IBAEntities1 _ibadb;

        public IBAEntities1 IbaDb
        {
            get
            {
                _ibadb = _ibadb ?? new IBAEntities1();
                return _ibadb;
            }
        }

    }
}