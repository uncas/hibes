using System.Collections.Generic;
using Uncas.EBS.Domain.ViewModel;

namespace Uncas.EBS.Domain.Repository
{
    /// <summary>
    /// Handles storage of person info.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Gets the person views.
        /// </summary>
        /// <returns></returns>
        IList<PersonView> GetPersonViews();
    }
}