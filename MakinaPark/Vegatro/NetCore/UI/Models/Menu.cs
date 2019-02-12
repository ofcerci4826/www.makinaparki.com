using System.Collections.Generic;

namespace Vegatro.NetCore.UI.Models
{
    /// <summary>
    /// Menu class
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// MVC controller name
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Menu name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Menu icon, generally material icon name
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Sub Menu list
        /// </summary>
        public List<MenuSub> SubMenu { get; set; } = new List<MenuSub>();
    }
}
