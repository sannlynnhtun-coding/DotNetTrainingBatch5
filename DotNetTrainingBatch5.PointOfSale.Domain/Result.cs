using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Domain
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public bool IsError { get { return !IsSuccess; } }

        public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }

        public bool IsSystemError { get { return Type == EnumRespType.SystemError; } }

        public bool IsDataError { get { return Type == EnumRespType.Error; } }

        public bool IsNotFound { get { return Type == EnumRespType.NotFound; } }

        public bool IsDuplicateRecord { get { return Type == EnumRespType.DuplicateRecord; } }

        public bool IsInvalidData { get { return Type == EnumRespType.InvalidData; } }

        private EnumRespType Type { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public static Result<T> Success(T data, string message = "Success")
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Type = EnumRespType.Success,
                Data = data,
                Message = message
            };
        }
        public static Result<T> ValidationError(string message, T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
                Type = EnumRespType.ValidationError,
            };
        }
        public static Result<T> SystemError(string message, T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
                Type = EnumRespType.SystemError,
            };
        }
        public static Result<T> Error(string message = "Some ErrorOccured", T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
                Type = EnumRespType.Error
            };
        }
        public static Result<T> DuplicateRecordError(string message = "DuplicateRecord Error Occured", T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
                Type = EnumRespType.DuplicateRecord
            };
        }
        public static Result<T> NotFoundError(string message = "NotFound Error Occured", T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
                Type = EnumRespType.NotFound
            };
        }
        public static Result<T> InvalidDataError(string message = "InvalidData Error Occured", T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
                Type = EnumRespType.InvalidData
            };
        }
        public static Result<T> BadRequestError(string message = "User Input Error", T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
                Type = EnumRespType.BadRequest
            };
        }
        public enum EnumRespType
        {
            None,
            Success,
            Error,
            ValidationError,
            SystemError,
            NotFound,
            DuplicateRecord,
            InvalidData,
            BadRequest
        }
    }
}
