namespace RecruitmentManager.Domain.Results;

public static class ResultsExtensions
{
    public static TOutput Match<TOutput>(
        this Result result,
        Func<TOutput> onSuccess,
        Func<Result, TOutput> onFailure) =>
        result.IsValid ? onSuccess() : onFailure(result);
    
    public static async Task<TOutput> Match<TOutput>(
        this Task<Result> resultTask,
        Func<TOutput> onSuccess,
        Func<Result, TOutput> onFail)
    {
        var result = await resultTask;
        return result.IsValid ? onSuccess() : onFail(result);
    }

    public static TOutput Match<TSource, TOutput>(
        this Result<TSource> result,
        Func<TSource, TOutput> onSuccess,
        Func<Result<TSource>, TOutput> onFail) =>
        result.IsValid ? onSuccess(result.Value) : onFail(result);

    public static async Task<TOutput> Match<TSource, TOutput>(
        this Task<Result<TSource>> resultTask,
        Func<TSource, TOutput> onSuccess,
        Func<Result<TSource>, TOutput> onFail)
    {
        Result<TSource> result = await resultTask;
        return result.IsValid ? onSuccess(result.Value) : onFail(result);
    }
}
