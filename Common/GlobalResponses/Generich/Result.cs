namespace Common.GlobalResponses.Generich;

public class Result<T> : Result
{
    public T Data { get; set; }

    public Result(List<string> message) : base(message)
    {

    }
    public Result()
    {

    }
}
