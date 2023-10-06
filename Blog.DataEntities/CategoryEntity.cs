using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataEntities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Name {  get; set; }

        public virtual ICollection<ItemEntity> Items { get; set; }
        public CategoryEntity() 
        {
            Items = new HashSet<ItemEntity>();
        }
    }
}
