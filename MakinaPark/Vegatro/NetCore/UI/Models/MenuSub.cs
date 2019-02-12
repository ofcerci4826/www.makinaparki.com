namespace Vegatro.NetCore.UI.Models
{
    /// <summary>
    /// Sub menu class
    /// </summary>
    public class MenuSub
    {
        /// <summary>
        /// MVC action name
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Menu name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Menu icon, generally material icon name
        /// </summary>
        public string Icon { get; set; }
    }
}
