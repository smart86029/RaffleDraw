using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RaffleDraw.Models;

namespace RaffleDraw.Data
{
    /// <summary>
    /// 獎項倉儲介面。
    /// </summary>
    public interface IPrizeRepository
    {
        /// <summary>
        /// 傳回倉儲中符合指定之條件的實體。
        /// </summary>
        /// <param name="predicate">用來測試每個實體是否符合條件的函式。</param>
        /// <returns><see cref="IEnumerable{Prize}"/>，其中包含倉儲中通過函式指定之測試的實體。</returns>
        IEnumerable<Prize> Many(Expression<Func<Prize, bool>> predicate);
    }
}