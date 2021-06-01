using System;
using TesteDextra.Models.Enums;

namespace TesteDextra.Models
{
    /// <summary>
    /// Determines a character;
    /// TODO: Divide and specify the attributes from person and wizard;
    /// </summary>
    public class Character
    {
        /// <summary>
        /// The character's identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Character's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Character's role
        /// </summary>
        public Occupation Role { get; set; }

        /// <summary>
        /// Character's school name
        /// TODO: Make this data a Guid and create a new class School;
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// Character's house identifier
        /// </summary>
        public Guid House { get; set; }

        /// <summary>
        /// Character's patronus
        /// </summary>
        public string Patronus { get; set; }
    }
}
