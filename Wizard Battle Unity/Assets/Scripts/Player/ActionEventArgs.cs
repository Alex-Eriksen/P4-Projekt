using System;

public class ActionEventArgs : EventArgs
{
    public static new readonly ActionEventArgs Empty;

    public readonly ActionEventArgsFlag Flag;
    public readonly string Message;

    public ActionEventArgs(ActionEventArgsFlag flag = ActionEventArgsFlag.None, string message = "Empty")
    {
        Flag = flag;
        Message = message;
    }
}