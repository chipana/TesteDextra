using System;
using System.Collections.Generic;

namespace TesteDextra.Models
{
    public class House
    {
        public Guid Id { get; set; }
        

        public string Name { get; set; }
        
        /// <summary>
        /// Name of the house head
        /// TODO: Make this data a Guid referencing a character
        /// </summary>
        public string HeadOfHouse { get; set; }

        /// <summary>
        /// The values of the house
        /// </summary>
        public List<string> Values { get; set; }

        /// <summary>
        /// Colors of the house
        /// TODO: Make this string a <see cref="System.Drawing.Color"/>
        /// </summary>
        public List<string> Colors { get; set; }

        /// <summary>
        /// Name of the School that house belongs
        /// TODO: Make this data a Guid and create a new class School
        /// </summary
        public string School { get; set; }

        /// <summary>
        /// Name of the house mascot
        /// TODO: Make this data a Guid referencing a character
        /// </summary>
        public string Mascot { get; set; }

        /// <summary>
        /// Name of the house ghost
        /// TODO: Make this data a Guid referencing a character
        /// </summary>
        public string HouseGhost { get; set; }

        /// <summary>
        /// Name of the house founder
        /// TODO: Make this data a Guid referencing a character
        /// </summary>
        public string Founder { get; set; }

    }
}
