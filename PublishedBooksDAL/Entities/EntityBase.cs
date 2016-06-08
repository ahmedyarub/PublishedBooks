using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishedBooksDAL
{
    public class EntityBase
    {
        public ObjectId Id { get; set; }
    }
}
