using System.Text.Json.Serialization;

namespace Mep01Web.DTO
{
    public class ResponseBase
    {
        public bool Succeeded { get; init; }
        public IList<Error>? Errors { get; init; }

        [JsonIgnore]
        public bool HasErrors => (Errors?.Count ?? 0) != 0;
        [JsonIgnore]
        public string? ErrorCodes => !HasErrors ? null : string.Join(" ", Errors.Select(e => e.Code));

        public ResponseBase(bool succeeded, IList<Error>? errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
        public ResponseBase(bool succeeded)
        {
            Succeeded = succeeded;
            Errors = null;
        }
        public static implicit operator ResponseBase(bool r)
        {
            return new ResponseBase(r, null);
        }

        public static ResponseBase Failed(string errorCode, string? message = null)
        {
            return new ResponseBase(false, new List<Error> { new Error(errorCode, message) });
        }
        public static ResponseBase Failed(IList<Error> errors)
        {
            return new ResponseBase(false, errors);
        }
    }

    public sealed class ResponseBase<T> : ResponseBase
    {
        public ResponseBase(bool succeeded, T? value, IList<Error>? errors = null) : base(succeeded, errors)
        {
            Body = value;
        }
        public ResponseBase(bool succeeded) : base(succeeded)
        {
            Body = default;
        }

        public T? Body { get; init; }

        public static implicit operator ResponseBase<T>(bool r)
        {
            return new ResponseBase<T>(r, default, errors: null);
        }
        public static implicit operator ResponseBase<T>(T v)
        {
            return new ResponseBase<T>(true, v, errors: null);
        }

        public new static ResponseBase<T> Failed(string errorCode, string? message = null)
        {
            return new ResponseBase<T>(false, default, errors: new List<Error> { new Error(errorCode, message) });
        }
        public new static ResponseBase<T> Failed(IList<Error> errors)
        {
            return new ResponseBase<T>(false, default, errors: errors);
        }
        public new static ResponseBase<T> Success()
        {
            return new ResponseBase<T>(true, default, null);
        }
        public new static ResponseBase<T> Success(T v)
        {
            return new ResponseBase<T>(true, v, null);
        }

    }

    public sealed class Error
    {
        public Error(string code, string? message = null)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; init; }
        public string? Message { get; init; }
    }
}
