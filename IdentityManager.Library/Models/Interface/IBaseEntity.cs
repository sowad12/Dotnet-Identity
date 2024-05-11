using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Library.Models.Interface
{
    public interface IBaseEntity : IEntity
    {
        void OnCreate();

        void OnUpdate();

        void OnDelete();
    }
}
