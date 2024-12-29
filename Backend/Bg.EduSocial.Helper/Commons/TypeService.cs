using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Helper.Commons
{
    public static class TypeService
    {
        public static object GetKeyValue<T>(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Lấy kiểu dữ liệu của đối tượng
            Type type = typeof(T);

            // Duyệt qua tất cả các thuộc tính của đối tượng
            foreach (PropertyInfo prop in type.GetProperties())
            {
                // Kiểm tra xem thuộc tính có attribute [Key] không
                if (Attribute.IsDefined(prop, typeof(KeyAttribute)))
                {
                    // Trả về giá trị của thuộc tính này
                    return prop.GetValue(entity);
                }
            }

            throw new InvalidOperationException("Key attribute not found!");
        }

    }
}
