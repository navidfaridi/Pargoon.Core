using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pargoon.Utility;

public enum MessageViewType : byte
{
    None = 0,
    Success = 1,
    Failed = 2,
    Warning = 3,
    Notification = 4,
    Information = 5
};

[Serializable]
public class SimpleMessageModel
{
    public SimpleMessageModel()
    {

    }
    public SimpleMessageModel(string messageText, MessageViewType messageType)
    {
        this.Message = messageText;
        this.MessageType = messageType;
    }

    public string Message { get; set; }
    public MessageViewType MessageType { get; set; }
}

public class ProcessResult_Model
{

    public bool ShowLoading { get; set; }
    public string DefaultViewName { get { return "ProcessResult"; } }
    public int Success { get; set; }

    public int Failed { get; set; }
    public string ReturnUrl { get; set; }
    public bool IsValid { get { return Failed == 0; } }
    public List<SimpleMessageModel> Messages { get; set; }

    public string ClientScript { get; set; }

    public void Clear()
    {
        Messages = new List<SimpleMessageModel>();
        Failed = 0;
        Success = 0;
    }

    public void AppendMessage(string newMessage, MessageViewType messageType)
    {
        if (!string.IsNullOrEmpty(newMessage))
        {
            Messages.Add(new SimpleMessageModel(newMessage, messageType));
        }
    }
    public void AddError(string newMessage, params object?[] args)
    {
        Failed++;
        AppendMessage(string.Format(newMessage, args), MessageViewType.Failed);
    }
    public void AddError(string newMessage)
    {
        Failed++;
        AppendMessage(newMessage, MessageViewType.Failed);
    }
    public void AddError(List<ValidationResult> errors)
    {
        foreach (var e in errors)
            AddError(e.ErrorMessage);
    }
    public void AddError(string ErrorType, string ErrorCode, string newMessage)
    {
        Failed++;
        AppendMessage(ErrorType + ":" + ErrorCode + " : " + newMessage, MessageViewType.Failed);
    }
    public void AddError(string ErrorCode, string newMessage)
    {
        Failed++;
        AppendMessage(ErrorCode + " : " + newMessage, MessageViewType.Failed);
    }
    public void AddException(Exception ex)
    {
        if (ex != null)
        {
            Failed++;
            AppendMessage(ex.Message, MessageViewType.Failed);
            if (ex.InnerException != null)
            {
                AppendMessage(ex.InnerException.Message, MessageViewType.Failed);
                if (ex.InnerException.InnerException != null)
                    AppendMessage(ex.InnerException.InnerException.Message, MessageViewType.Failed);
            }
        }
    }

    public void AddSuccess(string newMessage)
    {
        Success++;
        AppendMessage(newMessage, MessageViewType.Success);
    }
    public void AddSuccess(string newMessage, MessageViewType mt)
    {
        Success++;
        AppendMessage(newMessage, mt);
    }
    public ProcessResult_Model()
    {
        Success = 0;
        Failed = 0;
        Messages = new List<SimpleMessageModel>();
    }

    public ProcessResult_Model(SimpleMessageModel mm)
    {
        this.Messages.Add(mm);
    }
    public ProcessResult_Model(string newMessage, MessageViewType messageType)
    {
        if (!string.IsNullOrEmpty(newMessage))
        {
            Messages.Add(new SimpleMessageModel(newMessage, messageType));
        }
    }

    public override string ToString()
    {
        string result = string.Empty;
        foreach (var msg in this.Messages)
        {
            if (!string.IsNullOrEmpty(result))
                result += "<br>";
            result += msg.Message;
        }
        return result;
    }
    public string ToStringStriped(string seperator = ";")
    {
        string result = string.Empty;
        foreach (var msg in this.Messages)
        {
            if (!string.IsNullOrEmpty(result))
                result += seperator;
            result += msg.Message;
        }
        return result;
    }

    public object Data { get; set; }
}
