using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RaffleDraw.Models;

namespace RaffleDraw.Data
{
    /// <summary>
    /// 員工倉儲介面。
    /// </summary>
    public interface IEmployeeRepository : IExcelAvailable
    {
        /// <summary>
        /// 傳回倉儲中符合指定之條件的實體。
        /// </summary>
        /// <param name="predicate">用來測試每個實體是否符合條件的函式。</param>
        /// <returns><see cref="IEnumerable{Employee}"/>，其中包含倉儲中通過函式指定之測試的實體。</returns>
        IEnumerable<Employee> Many(Expression<Func<Employee, bool>> predicate);
    }
}