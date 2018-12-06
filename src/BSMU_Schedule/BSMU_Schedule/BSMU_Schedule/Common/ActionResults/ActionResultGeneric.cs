namespace BSMU_Schedule.Common.ActionResults
{
    public class ActionResult<T>: ActionResult
    {
        public T Data { get; set; }
    }
}
