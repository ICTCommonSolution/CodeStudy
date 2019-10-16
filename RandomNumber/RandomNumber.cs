using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingStudy
{
    public class RandomNumber
    {
        public string GenerateIntNumber()
        {
            System.Random random = new Random();

            int value = random.Next();

            return string.Format("value={0}", value);
        }
    }


}
