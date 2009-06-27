namespace Uncas.EBS.Domain.Model
{
    /// <summary>
    /// Represents the status of an issue or a task.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Represents any status.
        /// </summary>
        Any = 0,
        
        /// <summary>
        /// Indicates that the item is open - ongoing work.
        /// </summary>
        Open = 1,
        
        /// <summary>
        /// Indicates that the item is closed - terminated work.
        /// </summary>
        Closed = 2
    }
}